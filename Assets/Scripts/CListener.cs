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
    private DataPacketString mMessage;
    private NetworkStream mStream;

    private CListener()
    {
        mStream = CReadyNetWork.GetInstance().GetStream();
        mMessage = new DataPacketString();
        mThreadListen = new Thread(new ThreadStart(Listen));
        Debug.Log("listen 시작");
        mThreadListen.Start();
    }

    public static CListener GetInstance()
    {
        if(null == mInstance)
        {
            mInstance = new CListener();
        }
        return mInstance;
    }

    void Deserialize<T>(ref T outTemp, ref byte[] data)
    {
        var gch = GCHandle.Alloc(data, GCHandleType.Pinned);
        outTemp = (T)Marshal.PtrToStructure(gch.AddrOfPinnedObject(), typeof(T));
        gch.Free();
    }

    public void Listen()
    {
        while(true)
        {
            try
            {
                mMessage.Message = "";
                byte[] sizeBuffer = new byte[ConstVaue.IntSize];
                int isSuccess = mStream.Read(sizeBuffer, 0, ConstVaue.IntSize);

                DataPacketInt size = new DataPacketInt();
                Deserialize(ref size, ref sizeBuffer);

                byte[] messageBuffer = new byte[ConstVaue.BufSize];
                int curRecvedSize = 0;
                while (size.Number > 0)
                {
                    curRecvedSize += mStream.Read(messageBuffer, 0, size.Number);
                    if (curRecvedSize >= size.Number)
                        break;
                }
                Deserialize(ref mMessage, ref messageBuffer);
                Debug.Log("받은 메세지 = " + mMessage.Message);
                Thread.Sleep(100);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                mMessage.Message = "Exception";
                Debug.Log("Listen스레드 종료중..");
                mThreadListen.Abort();
                Debug.Log("Listen스레드 종료완료");
            }
        }
    }

}
