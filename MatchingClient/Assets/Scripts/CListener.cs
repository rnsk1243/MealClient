using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using ConstValueInfo;
using System;
using System.Threading;

public class CListener {

    private static CListener mInstance;
    private Thread mThreadListen;
    private NetworkStream mStream;
    private CReadyNetWork mNetWork;
    private Queue<DataChatMessage> mRecvMessage;
    private Queue<DataMatchInfo> mRecvMatchInfoQueue;
    ///

    private CListener()
    {
        mRecvMessage = new Queue<DataChatMessage>();
        mRecvMatchInfoQueue = new Queue<DataMatchInfo>();
        mNetWork = CReadyNetWork.GetInstance();
        mStream = mNetWork.GetStream();
        mThreadListen = new Thread(new ThreadStart(Listen));
        Debug.Log("listen 시작");
        mThreadListen.Start();
    }

    ~CListener()
    {
        Debug.Log("CListener 소멸자 호출");
        mThreadListen.Abort();
    }

    public void TerminaterThread()
    {
        Debug.Log("mThreadListen 종료 호출");
        mThreadListen.Abort();
    }

    public static CListener GetInstance()
    {
        if(null == mInstance)
        {
            Debug.Log("Listener 객체 생성");
            mInstance = new CListener();
        }
        return mInstance;
    }

    public DataChatMessage GetRecvMessage()
    {
        if(0 != mRecvMessage.Count)
        {
            return mRecvMessage.Dequeue();
        }
        return null;
    }

    public DataMatchInfo GetRecvDataMatchInfo()
    {
        if(0 != mRecvMatchInfoQueue.Count)
        {
            Debug.Log("플레이어 정보 꺼냄");
            return mRecvMatchInfoQueue.Dequeue();
        }
        return null;
    }

  

    public void Listen()
    {
        while(true)
        {
            try
            {
                if(!mNetWork.IsConnected())
                {
                    Debug.Log("연결 끊김 Listen스레드 종료중..");
                    mThreadListen.Abort();
                }

                byte[] dataBuffer = new byte[ConstValue.BufSizeRecv];
                int goalSize = Marshal.SizeOf(typeof(DataPacketInfo));
                //Debug.Log("받을 목표 사이즈 = " + goalSize);
                int curRecvedSize = 0;
                while (true)
                {
                    curRecvedSize += mStream.Read(dataBuffer, 0, goalSize);
                    if (curRecvedSize >= goalSize)
                    {
                        break;
                    }
                }
                DataPacketInfo dataPacket = new DataPacketInfo();
                dataPacket.Deserialize(ref dataBuffer);
                //System.Text.Encoding utf8 = System.Text.Encoding.UTF8;

                //byte[] utf8Bytes = utf8.GetBytes(mMessage.Message);
                //string decodedStringByUTF8 = utf8.GetString(utf8Bytes);

                //byte[] byteFromStr = System.Text.Encoding.Unicode.GetBytes(mMessage.Message);
                //string result = System.Text.Encoding.Unicode.GetString(byteFromStr);
                
                DataCategorize(ref dataPacket);
                Thread.Sleep(100);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                Debug.Log("Listen스레드 종료중..");
                mThreadListen.Abort();
            }
        }
    }

    void DataCategorize(ref DataPacketInfo dataPacket)
    {
        switch(dataPacket.InfoProtocol)
        {
            case (int)ProtocolInfo.ChattingMessage:
                mRecvMessage.Enqueue(new DataChatMessage((ProtocolDetail)dataPacket.InfoProtocolDetail, dataPacket.InfoValue));
                break;
            case (int)ProtocolInfo.ClientCommend:
                switch(dataPacket.InfoProtocolDetail)
                {
                    case (int)ProtocolDetail.ImageChange:
                    case (int)ProtocolDetail.NameChange:
                        mRecvMatchInfoQueue.Enqueue(new DataMatchInfo((ProtocolDetail)dataPacket.InfoProtocolDetail, (ProtocolCharacterTagIndex)dataPacket.InfoTagNumber, dataPacket.InfoValue));
                        break;
                    case (int)ProtocolDetail.MatchingSuccess:
                        CheckState.ChangeSceneState(ProtocolSceneName.RoomScene);
                        CheckState.ChangeState(State.ClientRoomIn);
                        break;
                    case (int)ProtocolDetail.LoginSuccess:
                        CheckState.ChangeSceneState(ProtocolSceneName.ChannelScene);
                        CheckState.ChangeState(State.ClientChannelMenu);// 채널 메뉴
                        break;
                    default:
                        Debug.Log("분류 할 수 없는 enum InfoProtocolDetail에 등록 되어 있지 않음");
                        break;
                }
                break;
            default:
                Debug.Log("분류 할 수 없는 enum ProtocolInfo에 등록 되어 있지 않음");
                break;
        }
    }

}
