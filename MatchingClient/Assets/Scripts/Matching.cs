using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConstValueInfo;
using UnityEngine.UI;


public class Matching : MonoBehaviour {

    public Texture mTofu;
    public Texture mMandu;
    public Texture mTangsuyuk;
    CListener mListener;

    List<GameObject> mPlayerInfo;
    List<Transform> mPlayerImageInfoList;
    List<Transform> mPlayerNameInfoList;

    Texture[] mTextureArray;
    


    void Awake()
    {
        mListener = CListener.GetInstance();
        mTextureArray = new Texture[ConstValue.CharacterKind];
        mTextureArray[(int)ProtocolCharacter.Tofu] = mTofu;
        mTextureArray[(int)ProtocolCharacter.Mandu] = mMandu;
        mTextureArray[(int)ProtocolCharacter.Tangsuyuk] = mTangsuyuk;
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
                case ProtocolDetail.Image:
                    UpdateImage(dataInfo.DataTagNumber, dataInfo.DataValue);
                    break;
                case ProtocolDetail.Name:
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
        Transform targetTr = SearchTargetPlayerImage(ConstValue.CharacterImageTag[(int)tagIndex]);
        if(targetTr != null)
        {
            int index = 0;
            foreach(string name in ConstValue.CharacterImageName)
            {   // 이름이 일치하고 배열 범위를 벗어나지 않으면 
                for(int i=0; i<name.Length; ++i)
                {
                    Debug.Log("name[" + i + "] = " + name[i]);
                }
                for (int i = 0; i < imageProtocol.Length; ++i)
                {
                    Debug.Log("imageProtocol[" + i + "] = " + imageProtocol[i]);
                }

                //Debug.Log("imageProtocol = " + imageProtocol);
                //Debug.Log("name = " + name);
                //Debug.Log("imageProtocol == name == " + string.Equals(imageProtocol, name, System.StringComparison.OrdinalIgnoreCase));
                //Debug.Log("((mTextureArray.Length - 1) >= index) = " + ((mTextureArray.Length - 1) >= index));
                if (imageProtocol == name && ((mTextureArray.Length - 1) >= index))
                {
                    Debug.Log("이미지 변경 = " + imageProtocol);
                    targetTr.GetComponent<RawImage>().texture = mTextureArray[index];
                    return true;
                }
                ++index;
            }
            Debug.Log("전부 일치 하지 않음");
        }
        else
        {
            Debug.Log(ConstValue.CharacterImageTag[(int)tagIndex] + " 이름의 tag를 찾지 못함");
        }

        return false;
    }

    void UpdateName(ProtocolCharacterTagIndex tagIndex, string name)
    {
        Transform targetTr = SearchTargetPlayerName(ConstValue.CharacterNameTag[(int)tagIndex]);
        if(targetTr != null)
        {
            targetTr.GetComponent<Text>().text = name;
        }
    }

}
