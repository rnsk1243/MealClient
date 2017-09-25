using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConstValueInfo;
using UnityEngine.SceneManagement;


public class CheckState : MonoBehaviour {

    static ProtocolSceneName mCurrentSceneState;
    static State mCurrentState;
    //static GameObject[] mLoginStatePanels;
    //static GameObject[] mLoginStateScripts;

    //static GameObject[] mRoomStatePanels;
    //static GameObject[] mRoomStateScripts;
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
        
        //foreach (GameObject g in mLoginStatePanels)
        //{
        //    g.SetActive(true);
        //}
        //foreach (GameObject g in mLoginStateScripts)
        //{
        //    g.SetActive(true);
        //}
        //foreach (GameObject g in mRoomStateScripts)
        //{
        //    g.SetActive(false);
        //}
        //foreach (GameObject g in mRoomStatePanels)
        //{
        //    g.SetActive(false);
        //}
    }

    public static void ChangeSceneState(ProtocolSceneName state)
    {
        mCurrentSceneState = state;
        mIsSceneChangeStart = true;
        //ActiveObject(state);
    }

    public static void ChangeState(State state)
    {
        mCurrentState = state;
        mIsStateChanged = true;
        //ActiveObject(state);
    }

    public static State GetCurState()
    {
        return mCurrentState;
    }

    void Update()
    {

        if(mIsSceneChanged)
        {
            switch (mCurrentSceneState)
            {
                case ProtocolSceneName.FrontScene:
                    //mLoginStatePanels = GameObject.FindGameObjectsWithTag("LoginPanel");
                    //mLoginStateScripts = GameObject.FindGameObjectsWithTag("LoginScript");
                    break;
                case ProtocolSceneName.ChannelScene:
                    mChannelPanel = GameObject.FindGameObjectWithTag("LoginSuccessPanel");
                    if(null == mChannelPanel)
                    {
                        break;
                    }
                    mMatchingPanel = mChannelPanel.GetComponentInChildren<Transform>().FindChild("MatchingPanel").gameObject;
                    mMatchingPanel.SetActive(false);
                    break;
                case ProtocolSceneName.RoomScene:
                    //mRoomStatePanels = GameObject.FindGameObjectsWithTag("RoomPanel");
                    //mRoomStateScripts = GameObject.FindGameObjectsWithTag("RoomScript");
                    break;
                default:
                    break;
            }
            mIsSceneChanged = false;
        }

        if(mIsStateChanged)
        {
            Debug.Log("상태 변경되어 할 작업 mCurrentState = " + mCurrentState);
            switch (mCurrentState)
            {
                case State.ClientMatching:
                    if(mMatchingPanel != null)
                    {
                        mMatchingPanel.SetActive(true);
                    }
                    break;
                case State.ClientChannelMenu:
                    if (mMatchingPanel != null)
                    {
                        mMatchingPanel.SetActive(false);
                    }
                    break;
                case State.ClientReady:

                    break;
                default:
                    break;
            }
            mIsStateChanged = false;
        }

        if(mIsSceneChangeStart)
        {
            SceneManager.LoadScene(ConstValue.ProtocolSceneName[(int)mCurrentSceneState]);
            mIsSceneChangeStart = false;
            mIsSceneChanged = true;
        }
    }



    //static void ActiveObject(State curState)
    //{
    //    switch (curState)
    //    {
    //        case State.Login:
    //            foreach (GameObject g in mLoginStatePanels)
    //            {
    //                g.SetActive(true);
    //            }
    //            foreach (GameObject g in mLoginStateScripts)
    //            {
    //                g.SetActive(true);
    //            }
    //            foreach (GameObject g in mRoomStateScripts)
    //            {
    //                g.SetActive(false);
    //            }
    //            foreach (GameObject g in mRoomStatePanels)
    //            {
    //                g.SetActive(false);
    //            }
    //            break;
    //        case State.Join:
    //            break;
    //        case State.Room:
    //            foreach (GameObject g in mRoomStatePanels)
    //            {
    //                g.SetActive(true);
    //            }
    //            foreach (GameObject g in mRoomStateScripts)
    //            {
    //                g.SetActive(true);
    //            }
    //            foreach (GameObject g in mLoginStateScripts)
    //            {
    //                g.SetActive(false);
    //            }
    //            foreach (GameObject g in mLoginStatePanels)
    //            {
    //                g.SetActive(false);
    //            }
    //            break;
    //    }
    //}

}
