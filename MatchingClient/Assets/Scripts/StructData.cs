using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConstValueInfo;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
public struct DataPacketInt
{
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = ConstValue.IntSize)]
    public int Number;

    public DataPacketInt(int number)
    {
        Number = number;
    }
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
public struct DataPacketString
{
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = ConstValue.BufSize)]
    public string Message;

    public DataPacketString(string message)
    {
        Message = message;
    }
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
public struct DataPacketPlayerInfo
{
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = ConstValue.BufSize)]
    public int InfoProtocol;        // 값의 종류 ( 캐릭터 이미지, 캐릭터 닉네임 등.. )
    public string InfoTag;          // 적용 대상 오브젝트 tag
    public string InfoValue;        // 값 (이름 혹은 이미지 이름(tofu, mandu, tangsuyuk))

    public DataPacketPlayerInfo(int infoProtocol, string infoTag, string infoValue)
    {
        InfoProtocol = infoProtocol;
        InfoTag = infoTag;
        InfoValue = infoValue;
    }
}