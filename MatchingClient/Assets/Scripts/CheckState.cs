using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConstValueInfo;
using UnityEngine.SceneManagement;


public class CheckState : MonoBehaviour {

    static ProtocolSceneName mCurrentSceneState;
    //static GameObject[] mLoginStatePanels;
    //static GameObject[] mLoginStateScripts;

    //static GameObject[] mRoomStatePanels;
    //static GameObject[] mRoomStateScripts;
    static bool mIsChanged;
    // Use this for initialization
    void Awake () {
        DontDestroyOnLoad(this.gameObject);
        mCurrentSceneState = ProtocolSceneName.FrontScene;
        mIsChanged = false;
        //mLoginStatePanels = GameObject.FindGameObjectsWithTag("LoginPanel");
        //mLoginStateScripts = GameObject.FindGameObjectsWithTag("LoginScript");

        //mRoomStatePanels = GameObject.FindGameObjectsWithTag("RoomPanel");
        //mRoomStateScripts = GameObject.FindGameObjectsWithTag("RoomScript");

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

    public static void ChangeState(ProtocolSceneName state)
    {
        mCurrentSceneState = state;
        mIsChanged = true;
        //ActiveObject(state);
    }

    void Update()
    {
        if(mIsChanged)
        {
            SceneManager.LoadScene(ConstValue.ProtocolSceneName[(int)mCurrentSceneState]);
            mIsChanged = false;
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
