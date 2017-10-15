using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConstValueInfo;

public class MyInfoClass
{
    private static MyInfoClass instance;

    public int MyCharNumb = ConstValue.WrongValue;
    public int MyGameNumb = ConstValue.WrongValue;
    public string HostIP = null;
    public string MyName = null;
    public int PlayerLimit = 6;
    //public NetworkMgr MyNetwork;

    public static MyInfoClass GetInstance()
    {
        if (instance == null)
        {
            instance = new MyInfoClass();
        }
        return instance;
    }

    public static bool IsMyInfoInit()
    {
        if((MyInfoClass.GetInstance().MyCharNumb != ConstValue.WrongValue)
            && (MyInfoClass.GetInstance().MyGameNumb != ConstValue.WrongValue)
            && (MyInfoClass.GetInstance().MyName != null))
        {
            //Debug.Log("MyCharaNum = " + MyInfoClass.GetInstance().MyCharNumb);
            //Debug.Log("MyGameNumb = " + MyInfoClass.GetInstance().MyGameNumb);
            //Debug.Log("MyName = " + MyInfoClass.GetInstance().MyName);
            return true;
        }else
        {
            return false;
        }
    }
}