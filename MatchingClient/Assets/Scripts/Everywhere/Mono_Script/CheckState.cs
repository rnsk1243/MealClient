using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConstValueInfo;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CheckState : MonoBehaviour {

    static ProtocolSceneName mCurrentSceneState;
    static State mCurrentState;
    static GameObject mChannelMasterPanel;
    static GameObject mFront;
    static GameObject mMapCharacterPanel;
    static GameObject[] mRoomSceneObjs;
    [SerializeField]
    Sprite[] mReadyButtonSprite;
    [SerializeField]
    Sprite[] mActiveCharacterSprite;
    [SerializeField]
    Sprite[] mCharacterSprite;
    //static GameObject mSelectLoginPanel;     // FrontScene
    //static GameObject mSelectPanel;          // FrontScene
    static bool mIsSceneChangeStart;    // Scene 변경 예정
    static bool mIsSceneChanged;        // Scene 변경 완료
    static bool mIsStateChanged;
    // Use this for initialization
    void Awake () {
        DontDestroyOnLoad(this.gameObject);
        mRoomSceneObjs = new GameObject[8];
        mCurrentSceneState = ProtocolSceneName.FrontScene;
        mIsSceneChangeStart = false;
        mIsSceneChanged = false;
        mIsStateChanged = false;
        ChangeState(State.ClientFrontMenu);
    }

    public static void ChangeSceneState(ProtocolSceneName state)
    {
        Debug.Log("씬변경 = " + state);
        mCurrentSceneState = state;
        mIsSceneChangeStart = true;
        //ActiveObject(state);
    }

    public static void ChangeState(State state)
    {
        mCurrentState = state;
        mIsStateChanged = true;
        Debug.Log("현재 상태 표시 = " + mCurrentState);
        //ActiveObject(state);
    }

    public static State GetCurState()
    {
        return mCurrentState;
    }

    public static ProtocolSceneName GetCurScene()
    {
        return mCurrentSceneState;
    }

    void Update()
    {
        if(mIsSceneChanged) // 씬이 바뀌고 난 후 처음 해주는 일 (한번만 함)
        {
            switch (mCurrentSceneState)
            {
                case ProtocolSceneName.FrontScene:
                    mFront = GameObject.FindGameObjectWithTag("TagFront");
                    ChangeState(State.ClientFrontMenu);
                    //mLoginStatePanels = GameObject.FindGameObjectsWithTag("LoginPanel");
                    //mLoginStateScripts = GameObject.FindGameObjectsWithTag("LoginScript");
                    break;
                case ProtocolSceneName.ChannelScene:
                    mChannelMasterPanel = GameObject.FindGameObjectWithTag("TagChannelMaster");
                    ChangeState(State.ClientChannelMenu);// 채널 메뉴
                    break;
                case ProtocolSceneName.RoomScene:
                    RoomSceneSet();
                    ChangeState(State.ClientNotReady);
                    //mRoomStatePanels = GameObject.FindGameObjectsWithTag("RoomPanel");
                    //mRoomStateScripts = GameObject.FindGameObjectsWithTag("RoomScript");
                    break;
                case ProtocolSceneName.MainScene:
                    Debug.Log("=================");
                    Debug.Log("MyCharaNum = " + MyInfoClass.GetInstance().MyCharNumb);
                    Debug.Log("MyGameNumb = " + MyInfoClass.GetInstance().MyGameNumb);
                    Debug.Log("MyName = " + MyInfoClass.GetInstance().MyName);
                    Debug.Log("=================");
                    break;
                default:
                    break;
            }
            mIsSceneChanged = false;
        }

        if(mIsStateChanged) // 상태가 바뀌면 처음 해주는 일 (한번만 함)
        {
            Debug.Log("상태 변경되어 할 작업 mCurrentState = " + mCurrentState);
            switch (mCurrentState)
            {
                case State.ClientFrontMenu: // Scene전환시 호출 기본
                    FrontInit(true, false);
                    break;
                case State.ClientGuest:
                    FrontInit(false, true);
                    break;
                case State.ClientRequestMatching:
                    break;
                case State.ClientMatching:
                    ChannelInit(true, false, false, false);
                    break;
                case State.ClientChannelMenu:
                    ChannelInit(false, false, false, false);
                    break;
                case State.ClientMakeRoom:
                    ChannelInit(false, true, false, false);
                    break;
                case State.ClientEnterSpecialRoom:
                    ChannelInit(false, false, true, false);
                    break;
                case State.ClientFailEnterRoom:
                    ChannelInit(false, false, true, true);
                    break;
                case State.ClientReady:
                    Debug.Log("Ready 성공");
                    RoomInit(true);
                    break;
                case State.ClientNotReady:
                    Chatting ChatScript;
                    ChatScript = GameObject.FindGameObjectWithTag("ChatPanel").GetComponent<Chatting>();
                    RoomInit(false);
                    if (null != ChatScript)
                    {
                        ChatScript.AddDialogue(ConstValue.NoticeNotReadyState);
                    }    
                    break;
                case State.ClientNotAllReady:
                    Chatting chatScript;
                    GameObject.FindGameObjectWithTag("RoomPanel").GetComponent<TeamPanel>().UpdateAllNotReady();
                    chatScript = GameObject.FindGameObjectWithTag("ChatPanel").GetComponent<Chatting>();
                    RoomInit(false);
                    if (null != chatScript)
                    {
                        chatScript.AddDialogue(ConstValue.NoticeNotReadyState);
                    }
                    break;
                default:
                    break;
            }
            mIsStateChanged = false;
        }

        if(mIsSceneChangeStart) // 씬이 바뀌는 작업
        {
            SceneManager.LoadScene(ConstValue.ProtocolSceneName[(int)mCurrentSceneState]);
            mIsSceneChangeStart = false;
            mIsSceneChanged = true;
        }
    }

    void FrontInit(bool select, bool guest)
    {
        if(mFront == null)
        {
            mFront = GameObject.FindGameObjectWithTag("TagFront");
        }
        GameObject selectLoginPanel = mFront.GetComponentInChildren<Transform>().FindChild("SelectLoginPanel").gameObject;
        GameObject guestPanel = mFront.GetComponentInChildren<Transform>().FindChild("GuestPanel").gameObject;
        GameObject loginPanel = mFront.GetComponentInChildren<Transform>().FindChild("LoginPanel").gameObject;

        selectLoginPanel.SetActive(select);
        guestPanel.SetActive(guest);
        loginPanel.SetActive(false);
    }

    void ChannelInit(bool matching, bool makeRoom, bool enterRoom, bool failRoom)
    {
        if(mChannelMasterPanel == null)
        {
            mChannelMasterPanel = GameObject.FindGameObjectWithTag("TagChannelMaster");
        }
        GameObject matchingPanel = mChannelMasterPanel.GetComponentInChildren<Transform>().FindChild("MatchingPanel").gameObject;
        matchingPanel.SetActive(matching);
        GameObject makeRoomPanel = mChannelMasterPanel.GetComponentInChildren<Transform>().FindChild("MakeRoomPanel").gameObject;
        makeRoomPanel.SetActive(makeRoom);
        GameObject enterRoomPanel = mChannelMasterPanel.GetComponentInChildren<Transform>().FindChild("EnterRoomPanel").gameObject;
        enterRoomPanel.SetActive(enterRoom);
        GameObject failRoomPanel = mChannelMasterPanel.GetComponentInChildren<Transform>().FindChild("EnterRoomFailPopPanel").gameObject;
        failRoomPanel.SetActive(failRoom);
    }
    // RoomScene의 버튼 가져오기
    void RoomSceneSet()
    {
        Debug.Log("RoomSceneSet 호출");
        mMapCharacterPanel = GameObject.FindGameObjectWithTag("TagMapCharacterPanel");
        mRoomSceneObjs[(int)ProtocolRoomSceneObj.Room] = GameObject.FindGameObjectWithTag("TagRoom");
        mRoomSceneObjs[(int)ProtocolRoomSceneObj.ButtonTofu] = mMapCharacterPanel.GetComponentInChildren<Transform>().FindChild("ButtonTofu").gameObject;
        mRoomSceneObjs[(int)ProtocolRoomSceneObj.ButtonMandu] = mMapCharacterPanel.GetComponentInChildren<Transform>().FindChild("ButtonMandu").gameObject;
        mRoomSceneObjs[(int)ProtocolRoomSceneObj.ButtonTangsuyuk] = mMapCharacterPanel.GetComponentInChildren<Transform>().FindChild("ButtonTangsuyuk").gameObject;
        mRoomSceneObjs[(int)ProtocolRoomSceneObj.ButtonBack] = GameObject.FindGameObjectWithTag("TagBackButton");
        mRoomSceneObjs[(int)ProtocolRoomSceneObj.ButtonReady] = GameObject.FindGameObjectWithTag("TagRoomReadyButton");
        mRoomSceneObjs[(int)ProtocolRoomSceneObj.ButtonLockCharacter] = mMapCharacterPanel.GetComponentInChildren<Transform>().FindChild("ButtonLockCharacter").gameObject;
        mRoomSceneObjs[(int)ProtocolRoomSceneObj.ButtonLockExit] = mRoomSceneObjs[(int)ProtocolRoomSceneObj.Room].GetComponentInChildren<Transform>().FindChild("ButtonLockExit").gameObject;
    }

    void RoomInit(bool ready)
    {
        for (int i=1; i<5; i++)
        {
            mRoomSceneObjs[i].GetComponent<Button>().interactable = !ready;
        }
        mRoomSceneObjs[(int)ProtocolRoomSceneObj.ButtonLockCharacter].SetActive(ready);
        mRoomSceneObjs[(int)ProtocolRoomSceneObj.ButtonLockExit].SetActive(ready);
        if(ready)
        {
            mRoomSceneObjs[(int)ProtocolRoomSceneObj.ButtonReady].GetComponent<Image>().sprite = mReadyButtonSprite[(int)ProtocolReady.Ready];
            
            switch(MyInfoClass.GetInstance().MyCharNumb)
            {
                case (int)ProtocolCharacterImageNameIndex.Tofu:
                    mRoomSceneObjs[(int)ProtocolRoomSceneObj.ButtonTofu].GetComponent<Image>().sprite = mActiveCharacterSprite[(int)ProtocolCharacterImageNameIndex.Tofu];
                    mRoomSceneObjs[(int)ProtocolRoomSceneObj.ButtonTofu].GetComponent<Button>().interactable = ready;
                    break;
                case (int)ProtocolCharacterImageNameIndex.Mandu:
                    mRoomSceneObjs[(int)ProtocolRoomSceneObj.ButtonMandu].GetComponent<Image>().sprite = mActiveCharacterSprite[(int)ProtocolCharacterImageNameIndex.Mandu];
                    mRoomSceneObjs[(int)ProtocolRoomSceneObj.ButtonMandu].GetComponent<Button>().interactable = ready;
                    break;
                case (int)ProtocolCharacterImageNameIndex.Tangsuyuk:
                    mRoomSceneObjs[(int)ProtocolRoomSceneObj.ButtonTangsuyuk].GetComponent<Image>().sprite = mActiveCharacterSprite[(int)ProtocolCharacterImageNameIndex.Tangsuyuk];
                    mRoomSceneObjs[(int)ProtocolRoomSceneObj.ButtonTangsuyuk].GetComponent<Button>().interactable = ready;
                    break;
            }
            Debug.Log("MyInfoClass.GetInstance().MyCharNumb = " + MyInfoClass.GetInstance().MyCharNumb);
        }
        else
        {
            mRoomSceneObjs[(int)ProtocolRoomSceneObj.ButtonTofu].GetComponent<Image>().sprite = mCharacterSprite[(int)ProtocolCharacterImageNameIndex.Tofu];
            mRoomSceneObjs[(int)ProtocolRoomSceneObj.ButtonMandu].GetComponent<Image>().sprite = mCharacterSprite[(int)ProtocolCharacterImageNameIndex.Mandu];
            mRoomSceneObjs[(int)ProtocolRoomSceneObj.ButtonTangsuyuk].GetComponent<Image>().sprite = mCharacterSprite[(int)ProtocolCharacterImageNameIndex.Tangsuyuk];
            mRoomSceneObjs[(int)ProtocolRoomSceneObj.ButtonReady].GetComponent<Image>().sprite = mReadyButtonSprite[(int)ProtocolReady.NotReady];
        }
        
    }

}
