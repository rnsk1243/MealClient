using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using ConstValueInfo;
using System;

public class CSender
{

    void Serialize<T>(T targetStruct, ref byte[] data)
    {
        // allocate a byte array for the struct data
        var buffer = new byte[Marshal.SizeOf(typeof(T))];

        // Allocate a GCHandle and get the array pointer
        var gch = GCHandle.Alloc(buffer, GCHandleType.Pinned);
        var pBuffer = gch.AddrOfPinnedObject();

        // copy data from struct to array and unpin the gc pointer
        Marshal.StructureToPtr(targetStruct, pBuffer, false);
        gch.Free();

        data = buffer;
    }

    public void Sendn(ref DataPacketString message)
    {
        CReadyNetWork netWork = CReadyNetWork.GetInstance();
        NetworkStream stream = netWork.GetStream();
        try
        {
            byte[] sizeBuffer = new byte[ConstVaue.IntSize];

            DataPacketInt size = new DataPacketInt(message.GetStrLen());
            Serialize(size, ref sizeBuffer);

            stream.Write(sizeBuffer, 0, ConstVaue.IntSize);

            byte[] messageBuffer = new byte[ConstVaue.BufSize];
            Serialize(message, ref messageBuffer);

            stream.Write(messageBuffer, 0, message.GetStrLen());
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            message.Message = "Exception";
        }
    }


}
