using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConstValueInfo;

public class MainScene : MonoBehaviour {

    CSender mSender;

    void Awake()
    {
        mSender = CSender.GetInstance();
        Debug.Log("MyCharaNum = " + MyInfoClass.GetInstance().MyCharNumb);
        Debug.Log("MyGameNumb = " + MyInfoClass.GetInstance().MyGameNumb);
        Debug.Log("MyName = " + MyInfoClass.GetInstance().MyName);
    }

    public void GameOver()
    {
        DataPacketInfo gameOverPacket = new DataPacketInfo((int)ProtocolInfo.ServerCommend, (int)ProtocolDetail.OutMainGameScene, (int)ProtocolTagNull.Null, null);
        mSender.Sendn(ref gameOverPacket);
    }

}
