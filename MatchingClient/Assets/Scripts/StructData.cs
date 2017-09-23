using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConstValueInfo;
using System.Text;
using System;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

//[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
//public struct DataPacketInt
//{
//    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = ConstValue.IntSize)]
//    public int Number;

//    public DataPacketInt(int number)
//    {
//        Number = number;
//    }
//}

//[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
//public struct DataPacketString
//{
//    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = ConstValue.BufSize)]
//    public string Message;

//    public DataPacketString(string message)
//    {
//        Message = message;
//    }
//}

//[Serializable]
//public class DataPacket
//{
//    public int InfoProtocol;        //(대분류) 어떤 데이터 인가? (0: 채팅 메세지 / 1: 캐릭터&이름 UI 내용)
//    public int InfoProtocolDetail;  //(소분류) 데이터중에 어떤 종류 인가?(캐릭터 이미지, 캐릭터 닉네임 등.. )
//    public string InfoTag;          // 적용 대상 오브젝트 tag
//    public string InfoValue;        // 값 (이름 혹은 이미지 이름(tofu, mandu, tangsuyuk) 또는 채팅 메세지)

//    public DataPacket(int infoProtocol, int infoProtocolDetail, string infoTag, string infoValue)
//    {
//        InfoProtocol = infoProtocol;
//        InfoProtocolDetail = infoProtocolDetail;
//        InfoTag = infoTag;
//        InfoValue = infoValue;
//    }

//    public byte[] Serialize(NetworkStream stream)
//    {
//        MemoryStream MS = new MemoryStream();
//        BinaryFormatter BF = new BinaryFormatter();
//        BF.Serialize(MS, this);
//        //IFormatter formatter = new BinaryFormatter();
//        //formatter.Serialize(stream, this);

//    }

//}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
public struct DataPacketInfo
{
    [MarshalAs(UnmanagedType.I4)]
    public int InfoProtocol;        //(대분류) 어떤 데이터 인가? (0: 채팅 메세지 / 1: 캐릭터&이름 UI 내용)
    [MarshalAs(UnmanagedType.I4)]
    public int InfoProtocolDetail;  //(소분류) 데이터중에 어떤 종류 인가?(캐릭터 이미지, 캐릭터 닉네임 등.. )
    [MarshalAs(UnmanagedType.I4)]
    public int InfoTagNumber;          // 적용 대상 오브젝트 tag 인덱스 번호
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = ConstValue.BufSizeValue)]
    public string InfoValue;        // 값 (이름 혹은 이미지 이름(tofu, mandu, tangsuyuk) 또는 채팅 메세지)

    public DataPacketInfo(int infoProtocol, int infoProtocolDetail, int infoTagNumber, string infoValue)
    {
        InfoProtocol = infoProtocol;
        InfoProtocolDetail = infoProtocolDetail;
        InfoTagNumber = infoTagNumber;
        InfoValue = infoValue;
    }

    public byte[] Serialize()
    {
        // allocate a byte array for the struct data
        var buffer = new byte[Marshal.SizeOf(typeof(DataPacketInfo))];

        // Allocate a GCHandle and get the array pointer
        var gch = GCHandle.Alloc(buffer, GCHandleType.Pinned);
        var pBuffer = gch.AddrOfPinnedObject();

        // copy data from struct to array and unpin the gc pointer
        Marshal.StructureToPtr(this, pBuffer, false);

        gch.Free();

        return buffer;
    }

    public void Deserialize(ref byte[] data)
    {
        //if (BitConverter.IsLittleEndian)
        //    Array.Reverse(data);
        //Encoding enc = Encoding.GetEncoding("euc-kr");
        this.InfoProtocol = BitConverter.ToInt32(data, 0);
        //Debug.Log("받은 InfoProtocol = " + InfoProtocol);
        this.InfoProtocolDetail = BitConverter.ToInt32(data, 4);
        //Debug.Log("받은 InfoProtocolDetail = " + InfoProtocolDetail);
        this.InfoTagNumber = BitConverter.ToInt32(data, 8); //Encoding.Default.GetString(data, 8, ConstValue.BufSizeTag);
        //Debug.Log("받은 Tag = " + InfoTag);
        this.InfoValue = Encoding.Default.GetString(data, 12, ConstValue.BufSizeValue);
        RemoveNullString(ref this.InfoValue);
        //Debug.Log("받은 value = " + InfoValue);
        //var gch = GCHandle.Alloc(data, GCHandleType.Pinned);
        //this = (DataPacketInfo)Marshal.PtrToStructure(gch.AddrOfPinnedObject(), typeof(DataPacketInfo));
        //gch.Free();
    }

    // null 문자 제거 함수
    void RemoveNullString(ref string str)
    {
        StringBuilder strBuilder = new StringBuilder(); ; // 문자열 더하고 빼는데 이용함
        for (int i = 0; i < str.Length; ++i)
        {
            if ('\0' == str[i])
            {
                break;
            }
            strBuilder.Append(str[i]);
        }
        //return strBuilder.ToString();
        this.InfoValue = strBuilder.ToString();
    }
}

    // 매칭 정보
public class DataMatchInfo
{
    public ProtocolDetail DataInfo;
    public ProtocolCharacterTagIndex DataTagNumber;
    public string DataValue;

    public DataMatchInfo(ProtocolDetail dataInfo, ProtocolCharacterTagIndex dataTagNumber, string dataValue)
    {
        DataInfo = dataInfo;
        DataTagNumber = dataTagNumber;
        DataValue = dataValue;
        
    }
}
// 채팅
public class DataChatMessage
{
    public ProtocolDetail DataInfo;
    public string DataValue;

    public DataChatMessage(ProtocolDetail dataInfo, string dataValue)
    {
        DataInfo = dataInfo;
        DataValue = dataValue;
    }
}