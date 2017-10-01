using System.Runtime.InteropServices;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConstValueInfo;

//[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
//public struct DataPacket
//{
//    [MarshalAs(UnmanagedType.I4)]
//    public int Grade;

//    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
//    public string Memo;

//    // Calling this method will return a byte array with the contents
//    // of the struct ready to be sent via the tcp socket.
//    public byte[] Serialize()
//    {
//        // allocate a byte array for the struct data
//        var buffer = new byte[Marshal.SizeOf(typeof(DataPacket))];

//        // Allocate a GCHandle and get the array pointer
//        var gch = GCHandle.Alloc(buffer, GCHandleType.Pinned);
//        var pBuffer = gch.AddrOfPinnedObject();

//        // copy data from struct to array and unpin the gc pointer
//        Marshal.StructureToPtr(this, pBuffer, false);
//        gch.Free();

//        return buffer;
//    }

//    // this method will deserialize a byte array into the struct.
//    public void Deserialize(ref byte[] data)
//    {
//        var gch = GCHandle.Alloc(data, GCHandleType.Pinned);
//        this = (DataPacket)Marshal.PtrToStructure(gch.AddrOfPinnedObject(), typeof(DataPacket));
//        gch.Free();
//    }
//}

//public class CReadyNetWork {

//    int Grade = 77;
//    string Memo = "니미";

//    public DataPacket func()
//    {
//        // 2. 구조체 데이타를 바이트 배열로 변환
//        DataPacket packet = new DataPacket();
//        //packet.Name = Name;
//        //packet.Subject = Subject;
//        packet.Grade = Grade;
//        packet.Memo = Memo;

//        byte[] buffer = packet.Serialize();

//        // 3. 서버에 접속
//        TcpClient client = new TcpClient();
//        client.Connect("127.0.0.1", 9000);

//        NetworkStream stream = client.GetStream();

//        // 4. 데이타 전송
//        stream.Write(buffer, 0, buffer.Length);

//        byte[] buffer2 = new byte[1024];
//        stream.Read(buffer2, 0, 1024);

//        DataPacket packet2 = new DataPacket();
//        packet2.Deserialize(ref buffer2);

//        // 5. 스트림&소켓 닫기
//        stream.Close();
//        client.Close();

//        return packet2;
//    }

//}

public class CReadyNetWork
{
    private static CReadyNetWork mInstance;
    private TcpClient mClient;// = new TcpClient();
    private NetworkStream mStream;

    private CReadyNetWork()
    {
        mClient = new TcpClient();
        mClient.Connect(ConstValue.IP, ConstValue.Port);
        mStream = mClient.GetStream();
    }
    ~CReadyNetWork()
    {
        UnityEngine.Debug.Log("CReadyNetWork 소멸자 호출");
        mStream.Close();
        mClient.Close();
    }
    public static CReadyNetWork GetInstance()
    {
        if(null == mInstance)
        {
            mInstance = new CReadyNetWork();
        }
        return mInstance;
    }
    
    public bool IsConnected()
    {
        return mClient.Connected;
    }

    public NetworkStream GetStream()
    {
        return mStream;
    }

    public void CloseStream()
    {
        UnityEngine.Debug.Log("mStream Close() 호출");
        mStream.Close();
    }

    public void CloseClient()
    {
        UnityEngine.Debug.Log("mClient Close() 호출");
        mClient.Close();
    }

}

