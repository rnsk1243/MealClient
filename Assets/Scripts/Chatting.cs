using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Chatting : MonoBehaviour {

    InputField mField;
    CSender Sender;
    CListener listener;
    // Use this for initialization
    void Start () {
        mField = GetComponentInChildren<InputField>();
        Sender = new CSender();
        listener = CListener.GetInstance();
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Return))
        {
            DataPacketString message = new DataPacketString(mField.text);
            Sender.Sendn(ref message);
        }
	}
}
