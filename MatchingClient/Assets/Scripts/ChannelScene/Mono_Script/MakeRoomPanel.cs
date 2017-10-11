﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ConstValueInfo;

public class MakeRoomPanel : MonoBehaviour {

    CSender mSender;
    GameObject mInputRoomPW;
    InputField mInputRoomPWComponent; // PW 입력 창 컴포넌트
    GameObject mSelectPublicToggle;
    GameObject mSelectTeamToggle;
    Toggle mOneTeamToggle;
    Toggle mTwoTeamToggle;
    Toggle mThreeTeamToggle;
    ProtocolTeamAmount mTeamAmount;

    // Use this for initialization
    void Awake () {
        //Debug.Log("Awake MakeRoom");
        mSender = CSender.GetInstance();
        mInputRoomPW = GameObject.FindGameObjectWithTag("TagInputRoomPW");
        mInputRoomPWComponent = mInputRoomPW.GetComponent<InputField>();
        mInputRoomPWComponent.characterLimit = ConstValue.CharacterLimitPW;
        mInputRoomPWComponent.text = "";
        mSelectPublicToggle = GameObject.FindGameObjectWithTag("TagSelectPublicToggle");
        mSelectPublicToggle.GetComponent<Toggle>().isOn = true;
        mInputRoomPWComponent.interactable = !mSelectPublicToggle.GetComponent<Toggle>().isOn;
        mSelectTeamToggle = GameObject.FindGameObjectWithTag("TagSelectTeamToggle");
        mOneTeamToggle = mSelectTeamToggle.GetComponentInChildren<Transform>().FindChild("One").gameObject.GetComponent<Toggle>();
        //mTwoTeamToggle = mSelectTeamToggle.GetComponentInChildren<Transform>().FindChild("Two").gameObject.GetComponent<Toggle>();
        //mThreeTeamToggle = mSelectTeamToggle.GetComponentInChildren<Transform>().FindChild("Three").gameObject.GetComponent<Toggle>();
        SelectOneTeam();
        mOneTeamToggle.isOn = true;
    }

    public void SetPublicRoom()
    {
        mInputRoomPWComponent.interactable = !mSelectPublicToggle.GetComponent<Toggle>().isOn;
        if(false == mInputRoomPWComponent.interactable)
        {
            mInputRoomPWComponent.text = "";
        }
    }

    public void SelectOneTeam()
    {
        //Debug.Log("OneTeam");
        mTeamAmount = ProtocolTeamAmount.OneTeam;
    }

    public void SelectTwoTeam()
    {
        //Debug.Log("TwoTeam");
        mTeamAmount = ProtocolTeamAmount.TwoTeam;
    }

    public void SelectThreeTeam()
    {
        //Debug.Log("ThreeTeam");
        mTeamAmount = ProtocolTeamAmount.ThreeTeam;
    }

    public void MakeRoom()
    {
        State curState = CheckState.GetCurState();
        if (State.ClientRequestMakeRoom != curState)
        {
           // Debug.Log("MakeRoom 요청");
            if (false == mSelectPublicToggle.GetComponent<Toggle>().isOn)
            {
                string pw = mInputRoomPWComponent.text;
                if (pw == "" || pw == null)
                {
                   // Debug.Log("방 비번 없음");
                    return;
                }
                pw = pw.Replace(" ", "");
                RequestMakeRoom(pw);
            }
            else
            {
                RequestMakeRoom(ConstValue.RoomPWNone);
            }
            CheckState.ChangeState(State.ClientRequestMakeRoom);
        }
    }

    void RequestMakeRoom(string pw)
    {
        DataPacketInfo roomPacket = new DataPacketInfo((int)ProtocolInfo.ServerCommend, (int)ProtocolDetail.MakeRoom, (int)mTeamAmount, pw);
        mSender.Sendn(ref roomPacket);
    }

    public void CancleMakeRoom()
    {
        CheckState.ChangeState(State.ClientChannelMenu);
    }

}
