using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using ConstValueInfo;
using System;

public class CSender
{
    void Serialize(DataPacketInt targetStruct, ref byte[] data)
    {
        // allocate a byte array for the struct data
        var buffer = new byte[Marshal.SizeOf(typeof(DataPacketInt))];

        // Allocate a GCHandle and get the array pointer
        var gch = GCHandle.Alloc(buffer, GCHandleType.Pinned);
        var pBuffer = gch.AddrOfPinnedObject();

        // copy data from struct to array and unpin the gc pointer
        Marshal.StructureToPtr(targetStruct, pBuffer, false);
        gch.Free();

        data = buffer;
    }


    int Serialize<T>(T targetStruct, ref byte[] data)
    {
        // allocate a byte array for the struct data
        var buffer = new byte[Marshal.SizeOf(typeof(T))];

        // Allocate a GCHandle and get the array pointer
        var gch = GCHandle.Alloc(buffer, GCHandleType.Pinned);
        var pBuffer = gch.AddrOfPinnedObject();

        // copy data from struct to array and unpin the gc pointer
        Marshal.StructureToPtr(targetStruct, pBuffer, false);
        gch.Free();

        int i = 0;
        while(0 != buffer[i])
        {
            ++i;
        }
        Debug.Log("보낼 크기 = " + i);
        byte[] fixedData = new byte[i];
        Array.Copy(buffer, fixedData, i);

        data = fixedData;
        return i;
    }

    public void Sendn(ref DataPacketString message)
    {
        CReadyNetWork netWork = CReadyNetWork.GetInstance();
        NetworkStream stream = netWork.GetStream();
        try
        {
            // 보낼 byte 크기를 알아내기 위해 우선 Serialize해본다.
            byte[] messageBuffer = null;
            int sendSize = Serialize(message, ref messageBuffer);   // 보낼 문자 시리얼라이즈!
            // 보낼 크기 저장 buffer준비
            byte[] sizeBuffer = new byte[ConstVaue.IntSize];
            DataPacketInt size = new DataPacketInt(sendSize); // 보낼 크기 구조체에 담고
            Serialize(size, ref sizeBuffer);    // 보낼 크기 시리얼라이즈!
            stream.Write(sizeBuffer, 0, ConstVaue.IntSize);
            stream.Write(messageBuffer, 0, sendSize);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            //message.Message = "Exception";
        }
    }


}
