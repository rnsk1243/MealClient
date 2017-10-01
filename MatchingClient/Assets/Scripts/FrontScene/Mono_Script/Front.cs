using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ConstValueInfo;
using System.Text;

public class Front : MonoBehaviour {

    CSender mSender;
    CListener mListener;

    GameObject mInputID;
    GameObject mInputPW;
    InputField mInputIDComponent; // ID 입력 창 컴포넌트
    InputField mInputPWComponent; // PW 입력 창 컴포넌트

    // Use this for initialization
    void Awake () {
        mSender = CSender.GetInstance();
        mListener = CListener.GetInstance();
        mInputID = GameObject.FindGameObjectWithTag("InputIDTag");
        mInputPW = GameObject.FindGameObjectWithTag("InputPWTag");
        mInputIDComponent = mInputID.GetComponent<InputField>();
        mInputIDComponent.text = "";
        mInputPWComponent = mInputPW.GetComponent<InputField>();
        mInputPWComponent.text = "";
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SendID_PW()
    {
        string id = mInputIDComponent.text;
        string pw = mInputPWComponent.text;

        if ((id != null && id != "") && (pw != null && pw != ""))
        {
            string idpw = id + '/' + pw;
            Debug.Log("idpw = " + idpw);
            DataPacketInfo dataIDPWString = new DataPacketInfo((int)ProtocolInfo.ServerCommend, (int)ProtocolDetail.FrontMenu, (int)ProtocolFrontMenuTag.LoginMenu, idpw);
            mSender.Sendn(ref dataIDPWString);
        }
        else
        {
            Debug.Log("아이디 혹은 비밀번호를 입력해 주세요.");
        }
    }
}
