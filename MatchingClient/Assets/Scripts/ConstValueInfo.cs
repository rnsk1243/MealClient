using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConstValueInfo
{
    //public enum State
    //{
    //    Login,
    //    Join,
    //    Room
    //}

    public enum ProtocolInfo       // 대분류
    {
        None,               // 초기화 값
        ServerCommend,      // 서버에게 명령
        ChattingMessage,    // 채팅 메세지
        ClientCommend
        //PlayerInfo          // 플레이어 정보( 캐릭터, 이름 )
    }

    public enum ProtocolDetail     // 중분류
    {
        NoneDetail,               // 초기화 값
        Message,            // 메세지
        ImageChange,              // 플레이어 캐릭터 이미지 변경
        NameChange,               // 이름 변경
        EnterRoom,
        EnterChanel,
        MakeRoom,
        OutRoom,
        ReadyGame,           // 게임준비
        Login,
        MatchingSuccess     // 매칭 성공 방 UI 표시
    }


    public enum ProtocolCharacterImageName
    {
        Tofu, Mandu, Tangsuyuk
    }

    // 이후 밑으로는 대상 Tag 인덱스 
    public enum ProtocolCharacterTagIndex   // CharacterImageTag 배열과 CharacterNameTag 배열의 인덱스
    {
        Red01, Red02, Red03, Blue01, Blue02, Blue03
    }

    public enum ProtocolMessageTag
    {
        Text
    }

    public enum ProtocolFrontManuTag
    {
        LoginManu, JoinManu, GuestManu, CancleManu
    }

    public enum ProtocolSceneName
    {
        FrontScene, RoomScene
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
        public static readonly string[] ProtocolCharacterImageName = { "Tofu", "Mandu", "Tangsuyuk" };
        public static readonly string[] ProtocolCharacterTagIndexImage = { "RedImage01", "RedImage02", "RedImage03", "BlueImage01", "BlueImage02", "BlueImage03" };
        public static readonly string[] ProtocolCharacterTagIndexName = { "RedName01", "RedName02", "RedName03", "BlueName01", "BlueName02", "BlueName03" };
        public static readonly string[] ProtocolMessageTag = { "TextView" };
        public static readonly string[] ProtocolSceneName = { "FrontScene", "RoomScene" };
        public const int CharacterKind = 3;
    }
}



