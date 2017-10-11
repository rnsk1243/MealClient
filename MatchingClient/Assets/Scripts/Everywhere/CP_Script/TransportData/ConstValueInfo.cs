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
        ClientCommend,       // 클라이언트 명령
        RequestResult,       // 요청 결과
        SceneChange         // 씬 변경
        //PlayerInfo          // 플레이어 정보( 캐릭터, 이름 )
    }

    public enum ProtocolDetail     // 중분류
    {
        NoneDetail,               // 초기화 값
        Message,            // 메세지
        ImageChange,              // 이미지
        NameChange,               // 이름
        EnterRoom,
        EnterSpecialRoom,
        EnterChanel,
        MakeRoom,
        OutRoom,
        OutMakeRoom,
        SetReadyGame,           // 게임준비
        FrontMenu,              // 메뉴(로그인, 회원가입, 게스트 로그인 )
        ChangeCharacter,        // 캐릭터 변경
        NotReadyGame,           // 게임 준비 취소
        StartGame,
        GetHostIP,
        SuccessRequest,       // 요청 성공
        FailRequest,            // 요청 실패
        RemovePanel             // 나간 사람 패널 지우기
    }


    public enum ProtocolCharacterImageNameIndex
    {
        Tofu, Mandu, Tangsuyuk
    }

    public enum ProtocolTagNull
    {
        Null = -1
    }

    // 이후 밑으로는 대상 Tag 인덱스 
    public enum ProtocolCharacterTagIndex   // CharacterImageTag 배열과 CharacterNameTag 배열의 인덱스
    {
        Red01, Blue01, Red02, Blue02, Red03, Blue03
    }

    public enum ProtocolMessageTag
    {
        Text
    }

    public enum ProtocolFrontMenuTag
    {
        LoginMenu, JoinMenu, GuestMenu, CancleMenu
    }

    public enum ProtocolChannelMenuTag
    {
        MatchingStart, MatchingCancel
    }

    public enum ProtocolSceneName
    {
        FrontScene, ChannelScene, RoomMakeScene, RoomScene, MainScene
    }

    public enum ProtocolTeamAmount
    {
        OneTeam, TwoTeam, ThreeTeam
    };


    public enum State
    {
        ClientNone, ClientFrontMenu/*front씬에서의 기본 상태*/,
        ClientLogin/*login을 선택한 상태*/, ClientJoin/*회원가입을 선택한 상태*/, ClientGuest/*guest 로그인 상태*/,
        ClientChannelMenu/*채널씬에서의 기본 상태*/,
        ClientMakeRoom, ClientOption, 
        ClientGame,/*0926추가됨*/
        ClientRequestGaemReady, ClientReady,       // 쌍으로 세트임
        ClientRequestGaemNotReady, ClientNotReady/*룸에서의 기본 상태*/, // 1001추가됨
        ClientRequestMatching, ClientMatching,
        ClientRequestCancleMactching, /*채널 기본*/
        ClientRequestCharacterChange, /*룸기본*/
        ClientRequestBackExit, /*씬 기본*/
<<<<<<< HEAD
        ClientRequestMakeRoom, /*룸기본*/
        ClientEnterSpecialRoom, // 방입장 버튼 누른 상태
        ClientRequestSpecialEnterRoom, ClientFailEnterRoom // room 입장 실패
=======
        ClientRequestMakeRoom /*룸기본*/
>>>>>>> origin/WOOHEE
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
        public const int CharacterLimitChatting = 40; // 채팅 InputField 글자수 제한
        public const int CharacterLimitID = 8; //  로그인 id 글자수 제한
        public const int CharacterLimitPW = 15; // 로그인 pw 글자수 제한
<<<<<<< HEAD
        public const int CharacterLimitRoomNumber = 4; // 방입장 요청 방번호 글자수 제한
=======
>>>>>>> origin/WOOHEE
        public const int CharacterLimitGuestName = CharacterLimitID; // Guest로그인 name 글자수 제한
        public const int IntSize = 4;
        public static readonly string[] ProtocolCharacterImageName = { "Tofu", "Mandu", "Tangsuyuk" };
        public static readonly string[] ProtocolCharacterTagIndexImage = { "RedImage01", "BlueImage01", "RedImage02", "BlueImage02", "RedImage03", "BlueImage03" };
        public static readonly string[] ProtocolCharacterTagIndexName = { "RedName01", "BlueName01", "RedName02", "BlueName02", "RedName03", "BlueName03" };
        public static readonly string[] ProtocolMessageTag = { "TextView" };
        public static readonly string[] ProtocolSceneName = { "FrontScene", "ChannelScene", "RoomScene", "Main" };
        public const int CharacterKind = 3;
        public const string NoticeReadyNoChangeCharacter = "이미 요청중이거나 준비 상태이므로 캐릭터를 변경할 수 없습니다. 먼저 준비를 풀어주세요.";
        public const string NoticeReadyNoBackExit = "이미 요청중이거나 준비 상태이므로 방을 나갈 수 없습니다. 먼저 준비를 풀어주세요.";
        public const string NoticeNotReadyState = "변화가 발생하여 준비가 풀렸습니다. 다시 준비 해주세요.";
        public const string RoomPWNone = "None";
    }
}