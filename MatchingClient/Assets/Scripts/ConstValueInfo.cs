using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConstValueInfo
{
    enum ProtocolCharacter
    {
        Tofu, Mandu, Tangsuyuk
    }
    enum ProtocolTeam
    {
        Red, Blue
    }
    enum ProtocolImageName
    {
        Image, Name
    }

    static public class ConstValue
    {
        public const int Port = 9000;
        public const string IP = "127.0.0.1";
        public const int BufSize = 1024;
        public const int IntSize = 4;
        public static readonly string[] CharacterName = { "Tofu", "Mandu", "Tangsuyuk" };
        public const int CharacterKind = 3;
    }
}



