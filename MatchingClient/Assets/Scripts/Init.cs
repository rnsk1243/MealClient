using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init : MonoBehaviour {

    // Use this for initialization
    CReadyNetWork readyNetWork;


    void Awake () {
        DontDestroyOnLoad(this.gameObject);
        Debug.Log("Init시작");
        readyNetWork = CReadyNetWork.GetInstance();
    }

    //void Update()
    //{
    //}

    private void OnApplicationQuit()
    {
        readyNetWork.CloseStream();
        readyNetWork.CloseClient();
        Debug.Log("readyNetWork null로 초기화 시킴");
        readyNetWork = null;
    }

}

