using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ConstValueInfo;
using System.Text;

public class Chatting : MonoBehaviour {

    CSender mSender;
    CListener mListener;

    GameObject mInputFieldObj;  // 채팅 입력 컴포넌트가 포함된 오브젝트
    InputField mInputComponent; // 채팅 입력 창 컴포넌트

    GameObject mViewPort;
    RectTransform mRectTransform;
    Text mText;
    List<String> DialogueRecord;

    int mLimitDialogueWindow;  // 몇줄까지 나타내는지 
    string my;
    void Awake ()
    {
        mSender = CSender.GetInstance();
        mListener = CListener.GetInstance();
        mInputFieldObj = GameObject.FindGameObjectWithTag("TextInput");
        mViewPort = GameObject.FindGameObjectWithTag(ConstValue.ProtocolMessageTag[(int)ProtocolMessageTag.Text]);
        mInputComponent = mInputFieldObj.GetComponent<InputField>();
        mInputComponent.text = "";
        mInputComponent.characterLimit = ConstValue.CharacterLimitChatting;
        mText = mViewPort.GetComponent<Text>(); 
        //Debug.Log("mRectTransform.sizeDelta.y = " + mRectTransform.sizeDelta.y);
        mRectTransform = mViewPort.GetComponent<RectTransform>();
        mLimitDialogueWindow = (int)(mRectTransform.sizeDelta.y / 28.5); // 분모가 커질 수록 글자는 위쪽에 출력됨
        //Debug.Log("mLimitDialogueWindow = " + mLimitDialogueWindow);
        mRectTransform.anchoredPosition = new Vector2(0, 0);
        mRectTransform.sizeDelta = new Vector2(960, 2200);
        DialogueRecordInit();
        my = "나 : ";
    }
	
    void DialogueRecordInit()
    {
        DialogueRecord = new List<string>();
        for (int i=0; i< mLimitDialogueWindow; ++i)
        {
            DialogueRecord.Add(" ");
        }
    }

	// Update is called once per frame
	void Update () {
        UpdateChatting();
        if (Input.GetKeyUp("return") || Input.GetKeyUp(KeyCode.KeypadEnter))
        {
            SendMessage();
        }
    }

    void UpdateChatting()
    {
        DataChatMessage recvMessage = mListener.GetRecvMessage();
        if (null != recvMessage)
        {
            //Debug.Log("신상 메세지 = " + recvMessage.DataValue);
            AddDialogue(recvMessage.DataValue);
        }
    }

    public void SendMessage()
    {
        string message = mInputComponent.text;
        if(message != null && message != "")
        {
            
            DataPacketInfo dataString = new DataPacketInfo((int)ProtocolInfo.ChattingMessage, (int)ProtocolDetail.Message, (int)ProtocolMessageTag.Text, message);
            //DrawText(message, new Vector2(0, 0));
            //Debug.Log("보낸 메세지 = " + message);
            mSender.Sendn(ref dataString);
            string addStr = (my + message);
            AddDialogue(addStr);
            mInputComponent.text = "";
            mInputComponent.ActivateInputField(); // 활성화 유지
        }
    }

    public void AddDialogue(string message)
    {
        mRectTransform.anchoredPosition = new Vector2(0, 0);
        if (DialogueRecord.Count >= mLimitDialogueWindow)
        {
            //ebug.Log("꽉참 지울 문장 = " + DialogueRecord[0]);
            DialogueRecord.RemoveAt(0);
        }
        //Debug.Log("추가될 문장 = " + message);
        DialogueRecord.Add(message);
        MessageUpdate();
    }

    void MessageUpdate()
    {
        StringBuilder setDialogue = new StringBuilder(); ; // 문자열 더하고 빼는데 이용함
        foreach (string m in DialogueRecord)
        {
            //string sentence = RemoveNullString(m);
            setDialogue.AppendLine(m);
            //char ch = m[m.Length - 1];
            //string m2 = "";
            //if(ch == '?')
            //{
            //   for(int i=0; i<m.Length; ++i)
            //    {
            //       // Debug.Log("m[" + i + "] = " + m[i]);
            //        if('\0' == m[i])
            //        {
            //            Debug.Log("null 문자 만남");
            //            Debug.Log("m2 = " + m2);
            //            break;
            //        }
            //        m2 += m[i];
            //    }
            //    setDialogue.AppendLine(m2);
            //    continue;
            //}
            //setDialogue.AppendLine(m);
        }
        //Debug.Log("결과 = " + setDialogue.ToString());
        mText.text = setDialogue.ToString(); // null 문자가 담긴 것이 들어가면 더 이상 문자열이 추가되지 않는다.
    }


}
