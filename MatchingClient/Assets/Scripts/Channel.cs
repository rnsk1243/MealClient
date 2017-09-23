using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConstValueInfo;


public class Channel : MonoBehaviour {

    GameObject mMatchingPanel;

	// Use this for initialization
	void Awake () {
        mMatchingPanel = GameObject.FindGameObjectWithTag("MatchingPanel");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RequestMatching()
    {
        Debug.Log("매칭 중...");
        CheckState.ChangeState(State.ClientMatching);
    }

    public void CancleMatching()
    {
        Debug.Log("매칭 취소.");
        CheckState.ChangeState(State.ClientChannelMenu);
    }

}
