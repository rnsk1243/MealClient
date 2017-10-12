using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConstValueInfo;


public class Channel : MonoBehaviour {

    //GameObject mMatchingPanel;
    CSender mSender;
    // Use this for initialization
    void Awake () {
        //mMatchingPanel = GameObject.FindGameObjectWithTag("MatchingPanel");
        mSender = CSender.GetInstance();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RequestMatching()
    {
        State curState = CheckState.GetCurState();
        if (State.ClientChannelMenu == curState)
        {
            CheckState.ChangeState(State.ClientRequestMatching);
            DataPacketInfo dataEnterRoom = new DataPacketInfo((int)ProtocolInfo.ServerCommend, (int)ProtocolDetail.EnterRoom, (int)ProtocolChannelMenuTag.MatchingStart, null);
            mSender.Sendn(ref dataEnterRoom);
        }
    }

    public void CancleMatching()
    {
        State curState = CheckState.GetCurState();
        if (State.ClientRequestCancleMactching != curState)
        {
            CheckState.ChangeState(State.ClientRequestCancleMactching);
            DataPacketInfo dataOutRoom = new DataPacketInfo((int)ProtocolInfo.ServerCommend, (int)ProtocolDetail.OutRoom, (int)ProtocolChannelMenuTag.MatchingCancel, null);
            mSender.Sendn(ref dataOutRoom);
        }
    }

    public void MakeRoomButton()
    {
        State curState = CheckState.GetCurState();
        if(State.ClientChannelMenu == curState)
        {
            CheckState.ChangeState(State.ClientMakeRoom);
        }
    }

    public void EnterRoomButton()
    {
        State curState = CheckState.GetCurState();
        if (State.ClientChannelMenu == curState)
        {
            CheckState.ChangeState(State.ClientEnterSpecialRoom);
        }
    }

    public void ExitButton()
    {
        Application.Quit();
    }

}
