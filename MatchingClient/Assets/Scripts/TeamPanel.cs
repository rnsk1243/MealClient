using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConstValueInfo;
using UnityEngine.UI;


public class TeamPanel : MonoBehaviour {

    public Texture mTofu;
    public Texture mMandu;
    public Texture mTangsuyuk;
    CListener mListener;

    List<GameObject> mPlayerInfo;
    List<Transform> mPlayerImageInfoList;
    List<Transform> mPlayerNameInfoList;

    Texture[] mCharacterTextureArray; // 캐릭터 이미지 텍스쳐 보관
    


    void Awake()
    {
        mListener = CListener.GetInstance();
        mCharacterTextureArray = new Texture[ConstValue.CharacterKind];
        mCharacterTextureArray[(int)ProtocolCharacterImageNameIndex.Tofu] = mTofu;
        mCharacterTextureArray[(int)ProtocolCharacterImageNameIndex.Mandu] = mMandu;
        mCharacterTextureArray[(int)ProtocolCharacterImageNameIndex.Tangsuyuk] = mTangsuyuk;
        mPlayerInfo = new List<GameObject>();
        mPlayerImageInfoList = new List<Transform>();
        mPlayerNameInfoList = new List<Transform>();
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("PlayerInfo"))
        {
            mPlayerInfo.Add(g);
        }
        foreach (GameObject g in mPlayerInfo)
        {
            mPlayerImageInfoList.Add(g.transform.FindChild("Image"));
            mPlayerNameInfoList.Add(g.transform.FindChild("Name"));
        }
    }

    void Update()
    {
        // 리스트에 정보가 있으면 적용 함수 호출
        UpdatePlayerInfo();
    }

    void UpdatePlayerInfo()
    {
        DataMatchInfo dataInfo = mListener.GetRecvDataMatchInfo();
        if(dataInfo != null)
        {
            switch (dataInfo.DataInfo)
            {
                case ProtocolDetail.ImageChange:
                    UpdateImage(dataInfo.DataTagNumber, dataInfo.DataValue);
                    break;
                case ProtocolDetail.NameChange:
                    UpdateName(dataInfo.DataTagNumber, dataInfo.DataValue);
                    break;
                default:
                    Debug.Log("이상한거 받음 = dataInfo.DataInfo = " + dataInfo.DataInfo + "// dataInfo.DataTagNumber = " + dataInfo.DataTagNumber + " // dataInfo.DataValue = " + dataInfo.DataValue);
                    break;
            }
        }
    }

    Transform SearchTargetPlayerImage(string tag)
    {
        foreach (Transform tr in mPlayerImageInfoList)
        {
            if(tag == tr.tag)
            {
                return tr;
            }
        }
        Debug.Log("tag = " + tag + " 와 일치하는 Image 못 찾음");
        return null;
    }

    Transform SearchTargetPlayerName(string tag)
    {
        foreach (Transform tr in mPlayerNameInfoList)
        {
            if (tag == tr.tag)
            {
                return tr;
            }
        }
        Debug.Log("tag = " + tag + " 와 일치하는 Player Name 객체 못 찾음");
        return null;
    }

    bool UpdateImage(ProtocolCharacterTagIndex tagIndex, string imageProtocol)
    {
        Debug.Log("변경 위치 = " + tagIndex);
        Transform targetTr = SearchTargetPlayerImage(ConstValue.ProtocolCharacterTagIndexImage[(int)tagIndex]);
        if(targetTr != null)
        {
            int index = 0;
            foreach(string name in ConstValue.ProtocolCharacterImageName)
            {   // 서버에서 받은 캐릭터이미지(imageProtocol)와 프로토콜 이름(name)이 일치하고 배열 범위를 벗어나지 않으면 
                if (imageProtocol == name && ((mCharacterTextureArray.Length - 1) >= index))
                {
                    Debug.Log("이미지 변경 = " + imageProtocol);
                    targetTr.GetComponent<RawImage>().texture = mCharacterTextureArray[index];
                    return true;
                }
                ++index;
            }
            Debug.Log("전부 일치 하지 않음");
        }
        else
        {
            Debug.Log(ConstValue.ProtocolCharacterTagIndexImage[(int)tagIndex] + " 이름의 tag를 찾지 못함");
        }

        return false;
    }

    void UpdateName(ProtocolCharacterTagIndex tagIndex, string name)
    {
        Transform targetTr = SearchTargetPlayerName(ConstValue.ProtocolCharacterTagIndexName[(int)tagIndex]);
        if(targetTr != null)
        {
            Debug.Log("이름 변경 = " + name);
            targetTr.GetComponent<Text>().text = name;
        }
    }

}
