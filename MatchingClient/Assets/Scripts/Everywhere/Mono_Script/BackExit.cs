using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConstValueInfo;

public class BackExit : MonoBehaviour {

    CSender mSender;
    

    void Awake()
    {
        mSender = CSender.GetInstance();
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
                default:
                    break;
            }
            mSender.Sendn(ref dataInfo);
            CheckState.ChangeState(State.ClientRequestBackExit);
 //           Debug.Log("나가기 요청 함");
        }else
        {
            if (CheckState.GetCurScene() == ProtocolSceneName.RoomScene)
            {
                Chatting ChatScript = GameObject.FindGameObjectWithTag("ChatPanel").GetComponent<Chatting>();
                ChatScript.AddDialogue(ConstValue.NoticeReadyNoBackExit);
                return;
            }
        }

        //CheckState.ChangeState(State.ClientChannelMenu);
        //DataPacketInfo dataOutRoom = new DataPacketInfo((int)ProtocolInfo.ServerCommend, (int)ProtocolDetail.OutRoom, (int)ProtocolChannelMenuTag.MatchingCancel, null);
        //mSender.Sendn(ref dataOutRoom);
    }

}
