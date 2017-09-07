using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init : MonoBehaviour {

    // Use this for initialization
    CReadyNetWork readyNetWork;
    
    void Start () {
        Debug.Log("Init시작");
        readyNetWork = CReadyNetWork.GetInstance();
    }

}
