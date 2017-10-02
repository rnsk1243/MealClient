using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConstValueInfo;
using UnityEngine.SceneManagement;


public class CheckState : MonoBehaviour {

    static ProtocolSceneName mCurrentSceneState;
    static State mCurrentState;
    static GameObject mChannelPanel;   // ChannelScene
    static GameObject mMatchingPanel;   // ChannelScene
    static bool mIsSceneChangeStart;    // Scene 변경 예정
    static bool mIsSceneChanged;        // Scene 변경 완료
    static bool mIsStateChanged;
    // Use this for initialization
    void Awake () {
        DontDestroyOnLoad(this.gameObject);
        mCurrentSceneState = ProtocolSceneName.FrontScene;
        mIsSceneChangeStart = false;
        mIsSceneChanged = false;
        mIsStateChanged = false;
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

    void Update()
    {
        if(mIsSceneChanged) // 씬이 바뀌고 난 후 처음 해주는 일 (한번만 함)
        {
            switch (mCurrentSceneState)
            {
                case ProtocolSceneName.FrontScene:
                    ChangeState(State.ClientFrontMenu);
                    //mLoginStatePanels = GameObject.FindGameObjectsWithTag("LoginPanel");
                    //mLoginStateScripts = GameObject.FindGameObjectsWithTag("LoginScript");
                    break;
                case ProtocolSceneName.ChannelScene:
                    ChangeState(State.ClientChannelMenu);// 채널 메뉴
                    break;
                case ProtocolSceneName.RoomScene:
                    ChangeState(State.ClientNotReady);
                    //mRoomStatePanels = GameObject.FindGameObjectsWithTag("RoomPanel");
                    //mRoomStateScripts = GameObject.FindGameObjectsWithTag("RoomScript");
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
                case State.ClientMatching:
                    mMatchingPanel.SetActive(true);
                    break;
                case State.ClientChannelMenu:
                    mChannelPanel = GameObject.FindGameObjectWithTag("LoginSuccessPanel");
                    mMatchingPanel = mChannelPanel.GetComponentInChildren<Transform>().FindChild("MatchingPanel").gameObject;
                    mMatchingPanel.SetActive(false);
                    break;
                case State.ClientReady:
                    Debug.Log("Ready 성공");
                    break;
                case State.ClientNotReady:
                    Chatting ChatScript;
                    ChatScript = GameObject.FindGameObjectWithTag("ChatPanel").GetComponent<Chatting>();
                    if (null != ChatScript)
                    {
                        ChatScript.AddDialogue(ConstValue.NoticeNotReadyState);
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
}
