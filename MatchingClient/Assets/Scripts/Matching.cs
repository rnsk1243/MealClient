using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConstValueInfo;
using UnityEngine.UI;


public class Matching : MonoBehaviour {

    public Texture mTofu;
    public Texture mMandu;
    public Texture mTangsuyuk;

    List<DataPacketPlayerInfo> mMatchInfoList;
    List<GameObject> mPlayerInfo;
    List<Transform> mPlayerImageInfoList;
    List<Transform> mPlayerNameInfoList;

    Texture[] mTextureArray;
    


    void Start()
    {
        mTextureArray = new Texture[ConstValue.CharacterKind];
        mTextureArray[(int)ProtocolCharacter.Tofu] = mTofu;
        mTextureArray[(int)ProtocolCharacter.Mandu] = mMandu;
        mTextureArray[(int)ProtocolCharacter.Tangsuyuk] = mTangsuyuk;
        mMatchInfoList = new List<DataPacketPlayerInfo>();
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
        //
        mMatchInfoList.Add(new DataPacketPlayerInfo((int)ProtocolImageName.Image, "RedImage01", "Tofu"));
        mMatchInfoList.Add(new DataPacketPlayerInfo((int)ProtocolImageName.Name, "RedName01", "M37"));
        mMatchInfoList.Add(new DataPacketPlayerInfo((int)ProtocolImageName.Image, "RedImage02", "Mandu"));
        mMatchInfoList.Add(new DataPacketPlayerInfo((int)ProtocolImageName.Name, "RedName02", "리엔필드"));
        mMatchInfoList.Add(new DataPacketPlayerInfo((int)ProtocolImageName.Image, "RedImage03", "Tangsuyuk"));
        mMatchInfoList.Add(new DataPacketPlayerInfo((int)ProtocolImageName.Name, "RedName03", "그로자"));
        mMatchInfoList.Add(new DataPacketPlayerInfo((int)ProtocolImageName.Image, "BlueImage01", "Tangsuyuk"));
        mMatchInfoList.Add(new DataPacketPlayerInfo((int)ProtocolImageName.Name, "BlueName01", "파업찐"));
        mMatchInfoList.Add(new DataPacketPlayerInfo((int)ProtocolImageName.Image, "BlueImage02", "Tofu"));
        mMatchInfoList.Add(new DataPacketPlayerInfo((int)ProtocolImageName.Name, "BlueName02", "꼬접충"));
        mMatchInfoList.Add(new DataPacketPlayerInfo((int)ProtocolImageName.Image, "BlueImage03", "Mandu"));
        mMatchInfoList.Add(new DataPacketPlayerInfo((int)ProtocolImageName.Name, "BlueName03", "쪼꼬바"));
        //
    }

    void Update()
    {
        // 리스트에 정보가 있으면 적용 함수 호출
        if(mMatchInfoList.Count != 0)
        {
            UpdatePlayerInfo();
        }
    }

    public void PushMatchingInfo(ref DataPacketPlayerInfo info)
    {
        mMatchInfoList.Add(info);
    }

    void UpdatePlayerInfo()
    {
        foreach(DataPacketPlayerInfo dataInfo in mMatchInfoList)
        {
            switch(dataInfo.InfoProtocol)
            {
                case (int)ProtocolImageName.Image:
                    UpdateImage(dataInfo.InfoTag, dataInfo.InfoValue);
                    break;
                case (int)ProtocolImageName.Name:
                    UpdateName(dataInfo.InfoTag, dataInfo.InfoValue);
                    break;
            }
        }
        mMatchInfoList.RemoveRange(0, mMatchInfoList.Count);
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
        Debug.Log("tag = " + tag + " 와 일치하는 Name 못 찾음");
        return null;
    }

    bool UpdateImage(string tag, string imageProtocol)
    {
        Transform targetTr = SearchTargetPlayerImage(tag);
        if(targetTr != null)
        {
            int index = 0;
            foreach(string name in ConstValue.CharacterName)
            {   // 이름이 일치하고 배열 범위를 벗어나지 않으면 
                if(imageProtocol == name && ((mTextureArray.Length - 1) >= index))
                {
                    Debug.Log("이미지 변경 = " + imageProtocol);
                    targetTr.GetComponent<RawImage>().texture = mTextureArray[index];
                    return true;
                }
                ++index;
            }
        }
        return false;
    }

    void UpdateName(string tag, string name)
    {
        Transform targetTr = SearchTargetPlayerName(tag);
        if(targetTr != null)
        {
            targetTr.GetComponent<Text>().text = name;
        }
    }

}
