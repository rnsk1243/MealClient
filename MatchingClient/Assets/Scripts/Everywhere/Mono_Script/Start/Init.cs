using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConstValueInfo;

public class Init : MonoBehaviour {

    // Use this for initialization
    CReadyNetWork readyNetWork;
    CListener listener;
    CSender sender;

    void Awake () {
        DontDestroyOnLoad(this.gameObject);
//        Debug.Log("Init시작");
        readyNetWork = CReadyNetWork.GetInstance();
        listener = CListener.GetInstance();
        sender = CSender.GetInstance();
    }

    //void Update()
    //{
    //}

    private void OnApplicationQuit()
    {
        //DataPacketInfo quitInfo = new DataPacketInfo((int)ProtocolInfo.ExitGameProcess, (int)ProtocolDetail.Message, (int)ProtocolMessageTag.Text, "으악 나 죽네");
        //sender.Sendn(ref quitInfo);
        //Debug.Log("OnApplicationQuit 호출");
        readyNetWork.CloseStream();
        readyNetWork.CloseClient();
        //Debug.Log("readyNetWork null로 초기화 시킴");
        readyNetWork = null;
        listener.TerminaterThread();
        listener = null;
        sender = null;
    }

}

