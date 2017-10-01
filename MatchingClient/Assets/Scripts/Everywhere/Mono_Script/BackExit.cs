﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConstValueInfo;

public class BackExit : MonoBehaviour {

    CSender mSender;
    Chatting ChatScript;

    void Awake()
    {
        mSender = CSender.GetInstance();
        ChatScript = GameObject.FindGameObjectWithTag("ChatPanel").GetComponent<Chatting>();
    }

    public void ExitButton()
    {
        Debug.Log("뒤로가기 버튼 클릭 / 상태에 따른 씬변경 요청");
        
        if(CheckState.GetCurState() != State.ClientReady)
        {
            Debug.Log("나가기 요청 함");

        }else
        {
            if (null == ChatScript)
            {
                Debug.Log("ChatScript가 null임");
                return;
            }
            ChatScript.AddDialogue(ConstValue.NoticeReadyNoBackExit);
        }

        //CheckState.ChangeState(State.ClientChannelMenu);
        //DataPacketInfo dataOutRoom = new DataPacketInfo((int)ProtocolInfo.ServerCommend, (int)ProtocolDetail.OutRoom, (int)ProtocolChannelMenuTag.MatchingCancel, null);
        //mSender.Sendn(ref dataOutRoom);
    }

}