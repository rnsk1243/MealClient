﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConstValueInfo;

public class ChangeCharacter : MonoBehaviour {

    CSender mSender;
    Chatting ChatScript;

    // Use this for initialization
    void Awake () {
        mSender = CSender.GetInstance();
        ChatScript = GameObject.FindGameObjectWithTag("ChatPanel").GetComponent<Chatting>();
        //CheckState.ChangeState(State.ClientNotReady);
    }
	

    public void SelectTofu()
    {
        SelectCharacter(ProtocolCharacterImageNameIndex.Tofu);
    }

    public void SelectMandu()
    {
//        Debug.Log("만두선택");
        SelectCharacter(ProtocolCharacterImageNameIndex.Mandu);
    }

    public void SelectTangsuyuk()
    {
        SelectCharacter(ProtocolCharacterImageNameIndex.Tangsuyuk);
    }

    public void ReadyButton()
    {
        int protocolDetail;
        if(CheckState.GetCurState() == State.ClientNotReady)
        {
            protocolDetail = (int)ProtocolDetail.SetReadyGame;
            CheckState.ChangeState(State.ClientRequestGaemReady); // 요청 상태로 변경
            //CheckState.ChangeState(State.ClientReady);
        }
        else
        {
            protocolDetail = (int)ProtocolDetail.NotReadyGame;
            CheckState.ChangeState(State.ClientRequestGaemNotReady); // 요청 상태로 변경
            //CheckState.ChangeState(State.ClientNotReady);
        }
        DataPacketInfo dataInfo = new DataPacketInfo((int)ProtocolInfo.ServerCommend, protocolDetail, 0, null);
        mSender.Sendn(ref dataInfo);
        
   //     Debug.Log("ReadyButton누름");
    }

    void SelectCharacter(ProtocolCharacterImageNameIndex characterIndex)
    {
        //Debug.Log("내 상태 = " + CheckState.GetCurState());
        State curState = CheckState.GetCurState();
        if (State.ClientNotReady == curState && State.ClientRequestCharacterChange != curState)
        {
            DataPacketInfo dataInfo = new DataPacketInfo((int)ProtocolInfo.ServerCommend, (int)ProtocolDetail.ChangeCharacter, (int)characterIndex, null);
            mSender.Sendn(ref dataInfo);
            CheckState.ChangeState(State.ClientRequestCharacterChange);
        }
        else
        {
            if(null == ChatScript)
            {
 //               Debug.Log("ChatScript가 null임");
                return;
            }
            ChatScript.AddDialogue(ConstValue.NoticeReadyNoChangeCharacter);
        }
    }


}
