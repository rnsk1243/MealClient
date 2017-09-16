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
    private Queue<string> mRecvMessage;

    ///

    private CListener()
    {
        mRecvMessage = new Queue<string>();
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

    public string GetRecvMessage()
    {
        if(0 != mRecvMessage.Count)
        {
            return mRecvMessage.Dequeue();
        }
        return null;
    }

    void Deserialize<T>(ref T outTemp, ref byte[] data)
    {
        //int RawSize = Marshal.SizeOf(outTemp);
        //IntPtr buffer = Marshal.AllocHGlobal(RawSize);
        //Marshal.Copy(data, 0, buffer, RawSize);
        //object retobj = Marshal.PtrToStructure(buffer, typeof(T));
        //Marshal.FreeHGlobal(buffer);
        //outTemp = (T)retobj;

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
                if(!mNetWork.IsConnected())
                {
                    Debug.Log("연결 끊김 Listen스레드 종료중..");
                    mThreadListen.Abort();
                }
                DataPacketString mMessage = new DataPacketString();
                mMessage.Message = "";
                byte[] sizeBuffer = new byte[ConstValue.IntSize];
                int isSuccess = mStream.Read(sizeBuffer, 0, ConstValue.IntSize);

                DataPacketInt size = new DataPacketInt();
                Deserialize(ref size, ref sizeBuffer);

                byte[] messageBuffer = new byte[ConstValue.BufSize];
                int curRecvedSize = 0;
                Debug.Log("받을 size = " + size.Number);
                while (size.Number > 0)
                {
                    curRecvedSize += mStream.Read(messageBuffer, 0, size.Number);
                    if (curRecvedSize >= size.Number)
                        break;
                }
                Deserialize(ref mMessage, ref messageBuffer);

                //System.Text.Encoding utf8 = System.Text.Encoding.UTF8;

                //byte[] utf8Bytes = utf8.GetBytes(mMessage.Message);
                //string decodedStringByUTF8 = utf8.GetString(utf8Bytes);

                //byte[] byteFromStr = System.Text.Encoding.Unicode.GetBytes(mMessage.Message);
                //string result = System.Text.Encoding.Unicode.GetString(byteFromStr);

                Debug.Log("받은 메세지 = " + mMessage.Message);
                mRecvMessage.Enqueue(mMessage.Message);
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

}
