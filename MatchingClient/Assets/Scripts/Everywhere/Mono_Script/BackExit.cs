using System.Collections;
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
//        Debug.Log("뒤로가기 버튼 클릭 / 상태에 따른 씬변경 요청");
        State curState = CheckState.GetCurState();
        DataPacketInfo dataInfo = new DataPacketInfo();
        if (curState != State.ClientReady && curState != State.ClientRequestBackExit)
        {
            switch(curState)
            {
                case State.ClientNotReady:
                    dataInfo = new DataPacketInfo((int)ProtocolInfo.ServerCommend, (int)ProtocolDetail.OutRoom, (int)State.ClientRequestBackExit, null);
                    break;
                case State.ClientMakeRoom:
                    break;
                case State.ClientChannelMenu:
                    break;
                default:
                    break;
            }
            mSender.Sendn(ref dataInfo);
            CheckState.ChangeState(State.ClientRequestBackExit);
 //           Debug.Log("나가기 요청 함");
        }else
        {
            if (null == ChatScript)
            {
//                Debug.Log("ChatScript가 null임");
                return;
            }
            ChatScript.AddDialogue(ConstValue.NoticeReadyNoBackExit);
        }

        //CheckState.ChangeState(State.ClientChannelMenu);
        //DataPacketInfo dataOutRoom = new DataPacketInfo((int)ProtocolInfo.ServerCommend, (int)ProtocolDetail.OutRoom, (int)ProtocolChannelMenuTag.MatchingCancel, null);
        //mSender.Sendn(ref dataOutRoom);
    }

}
