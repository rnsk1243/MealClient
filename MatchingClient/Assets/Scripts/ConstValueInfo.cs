using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConstValueInfo
{
    public enum ProtocolInfo       // 대분류
    {
        None,               // 초기화 값
        ServerCommend,      // 서버에게 명령
        ChattingMessage,    // 채팅 메세지
        PlayerInfo          // 플레이어 정보( 캐릭터, 이름 )
    }

    public enum ProtocolDetail     // 소분류
    {
        NoneDetail,               // 초기화 값
        Message,            // 메세지
        Image,              // 이미지
        Name,               // 이름
        EnterRoom,
        EnterChanel,
        MakeRoom,
        OutRoom,
        ReadyGame           // 게임준비
    }

    public enum ProtocolCharacter
    {
        Tofu, Mandu, Tangsuyuk
    }

    public enum ProtocolCharacterTagIndex   // CharacterImageTag 배열과 CharacterNameTag 배열의 인덱스
    {
        Red01, Red02, Red03, Blue01, Blue02, Blue03
    }

    public enum ProtocolMessageTag
    {
        Text
    }

    //enum ProtocolTeam
    //{
    //    Red, Blue
    //}

    static public class ConstValue
    {
        public const int Port = 9000;
        public const string IP = "127.0.0.1";
        public const int BufSizeRecv = 1024;
        public const int BufSizeSend = 1024;
        //public const int BufSizeTag = 64;  // 오브젝트 Tag값
        public const int BufSizeValue = 128; // 채팅 메세지, 혹은 값
        public const int IntSize = 4;
        public static readonly string[] CharacterImageName = { "Tofu", "Mandu", "Tangsuyuk" };
        public static readonly string[] CharacterImageTag = { "RedImage01", "RedImage02", "RedImage03", "BlueImage01", "BlueImage02", "BlueImage03" };
        public static readonly string[] CharacterNameTag = { "RedName01", "RedName02", "RedName03", "BlueName01", "BlueName02", "BlueName03" };
        public static readonly string[] MessageTag = { "TextView" };
        public const int CharacterKind = 3;
    }
}



