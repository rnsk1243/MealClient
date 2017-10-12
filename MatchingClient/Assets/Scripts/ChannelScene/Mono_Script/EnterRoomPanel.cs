using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ConstValueInfo;

public class EnterRoomPanel : MonoBehaviour {

    CSender mSender;
    GameObject mInputRoomPW;
    GameObject mInputRoomNumber;
    GameObject mSelectPrivateRoomToggle;
    GameObject mEnterButton;
    InputField mInputRoomPWComponent; // PW 입력 창 컴포넌트
    InputField mInputRoomNumberComponent; // PW 입력 창 컴포넌트
    Toggle mPrivateToggle;
    Button mEnterButtonComponent;

    void Awake()
    {
        mSender = CSender.GetInstance();
        mInputRoomPW = GameObject.FindGameObjectWithTag("TagEnterRoomPWInputField");
        mInputRoomNumber = GameObject.FindGameObjectWithTag("TagEnterRoomNumberInputField");
        mSelectPrivateRoomToggle = GameObject.FindGameObjectWithTag("TagSelectPrivateToggle");
        mEnterButton = GameObject.FindGameObjectWithTag("TagEnterRoomEnterButton");
        mInputRoomPWComponent = mInputRoomPW.GetComponent<InputField>();
        mInputRoomNumberComponent = mInputRoomNumber.GetComponent<InputField>();
        mPrivateToggle = mSelectPrivateRoomToggle.GetComponent<Toggle>();
        mEnterButtonComponent = mEnterButton.GetComponent<Button>();

        mInputRoomPWComponent.interactable = false;
        mInputRoomPWComponent.characterLimit = ConstValue.CharacterLimitPW;
        mInputRoomNumberComponent.characterLimit = ConstValue.CharacterLimitRoomNumber;
        mPrivateToggle.isOn = mInputRoomPWComponent.interactable;
        Debug.Log("AwakeEnterRoomPanel");
    }

    void Update()
    {
        if (CheckState.GetCurState() == State.ClientEnterSpecialRoom)
        {
            if ("" == mInputRoomNumberComponent.text)
            {
                mEnterButtonComponent.interactable = false;
            }
            else
            {
                mEnterButtonComponent.interactable = true;
            }

            if (true == mPrivateToggle.GetComponent<Toggle>().isOn)
            {
                if ("" == mInputRoomPWComponent.text)
                {
                    mEnterButtonComponent.interactable = false;
                }
                else
                {
                    mEnterButtonComponent.interactable = true;
                }
            }

        }
    }

    public void SetPrivateORPublicRoom()
    {
        mInputRoomPWComponent.interactable = mPrivateToggle.GetComponent<Toggle>().isOn;
        if (false == mInputRoomPWComponent.interactable)
        {
            mInputRoomPWComponent.text = "";
        }
    }

    public void CancleEnterRoom()
    {
        CheckState.ChangeState(State.ClientChannelMenu);
    }

    public void EnterRoom()
    {
        string number = mInputRoomNumberComponent.text;
        if (number == "" || number == null)
        {
            return;
        }
        bool isPrivateRoom = false;
        string pw = mInputRoomPWComponent.text;
        if (true == mPrivateToggle.GetComponent<Toggle>().isOn) // 비공개방 요청임
        {
            if (pw == "" || pw == null)
            {
                return;
            }
            isPrivateRoom = true;
        }else
        {
            isPrivateRoom = false;
        }
        State curState = CheckState.GetCurState();
        if (State.ClientRequestSpecialEnterRoom != curState)
        {
            number = number.Replace(" ", "");
            if (isPrivateRoom)
            {
                pw = pw.Replace(" ", "");

            }
            else
            {
                pw = ConstValue.RoomPWNone;
            }
            string numberPw = number + '/' + pw;
            Debug.Log("numberPw = " + numberPw);
            RequestEnterRoom(numberPw);
            CheckState.ChangeState(State.ClientRequestSpecialEnterRoom);
        }
    }

    void RequestEnterRoom(string pw)
    {
        DataPacketInfo enterPacket = new DataPacketInfo((int)ProtocolInfo.ServerCommend, (int)ProtocolDetail.EnterSpecialRoom, (int)ProtocolTagNull.Null, pw);
        mSender.Sendn(ref enterPacket);
    }

    public void CheckFailEnterRoom()
    {
        CheckState.ChangeState(State.ClientEnterSpecialRoom);
    }

}
