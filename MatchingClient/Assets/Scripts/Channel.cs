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
        Debug.Log("매칭 중...");
        CheckState.ChangeState(State.ClientMatching);
        DataPacketInfo dataEnterRoom = new DataPacketInfo((int)ProtocolInfo.ServerCommend, (int)ProtocolDetail.EnterRoom, (int)State.ClientMatching, null);
        mSender.Sendn(ref dataEnterRoom);
    }

    public void CancleMatching()
    {
        Debug.Log("매칭 취소.");
        CheckState.ChangeState(State.ClientChannelMenu);
    }

}
