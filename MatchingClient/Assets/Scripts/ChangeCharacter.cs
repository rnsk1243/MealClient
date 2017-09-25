using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConstValueInfo;

public class ChangeCharacter : MonoBehaviour {

    CSender mSender;

    // Use this for initialization
    void Awake () {
        mSender = CSender.GetInstance();
    }
	

    public void SelectTofu()
    {
        SelectCharacter(ProtocolCharacterImageNameIndex.Tofu);
    }

    public void SelectMandu()
    {
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
            protocolDetail = (int)ProtocolDetail.NotReadyGame;
            CheckState.ChangeState(State.ClientReady);
        }else
        {
            protocolDetail = (int)ProtocolDetail.SetReadyGame;
            CheckState.ChangeState(State.ClientNotReady);
        }
        DataPacketInfo dataInfo = new DataPacketInfo((int)ProtocolInfo.ServerCommend, protocolDetail, 0, null);
        mSender.Sendn(ref dataInfo);
        Debug.Log("ReadyButton누름");
    }

    void SelectCharacter(ProtocolCharacterImageNameIndex characterIndex)
    {
        DataPacketInfo dataInfo = new DataPacketInfo((int)ProtocolInfo.ServerCommend, (int)ProtocolDetail.ChangeCharacter, (int)characterIndex, null);
        mSender.Sendn(ref dataInfo);
    }


}
