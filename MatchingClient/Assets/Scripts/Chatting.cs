using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 if (Event.current.isKey &&
            Event.current.keyCode == KeyCode.Return)
        {
           // Debug.Log("말하기 버튼 클릭 isSent = " + isSent);
            if(m_sendComment != "")
            {
                isSent = true;
            }
        }

        if (isSent == true)
        {
            //string message = "[" + DateTime.Now.ToString("HH:mm:ss") + "] " + m_sendComment;
            //byte[] buffer = System.Text.Encoding.UTF8.GetBytes(message);
     */

public class Chatting : MonoBehaviour {

    CSender mSender;
    CListener mListener;

    GameObject mInputFieldObj;  // 채팅 입력 컴포넌트가 포함된 오브젝트
    InputField mInputComponent; // 채팅 입력 창 컴포넌트

    GameObject mViewPort;
    RectTransform mRectTransform;
    Text mText;

    List<String> DialogueRecord;

    int mLimitDialogueWindow; // 몇줄까지 나타내는지 

    string setDialogue;
    void Start ()
    {
        mSender = CSender.GetInstance();
        mListener = CListener.GetInstance();
        mInputFieldObj = GameObject.FindGameObjectWithTag("TextInput");
        mViewPort = GameObject.FindGameObjectWithTag("TextView");
        mInputComponent = mInputFieldObj.GetComponent<InputField>();
        mInputComponent.text = "";
        mRectTransform = mViewPort.GetComponent<RectTransform>();
        mText = mViewPort.GetComponent<Text>();
        mLimitDialogueWindow = (int)(mRectTransform.position.y / 30);
        setDialogue = "";
        DialogueRecordInit();
    }
	
    void DialogueRecordInit()
    {
        DialogueRecord = new List<string>();
        for(int i=0; i< mLimitDialogueWindow; ++i)
        {
            DialogueRecord.Add(" ");
        }
    }

	// Update is called once per frame
	void Update () {
        UpdateChatting();
        if (Input.GetKeyUp("return") || Input.GetKeyUp(KeyCode.KeypadEnter))
        {
            SendMessage();
        }
    }

    void UpdateChatting()
    {
        string recvMessage = mListener.GetRecvMessage();
        if (null != recvMessage)
        {
            AddDialogue(recvMessage);
        }
    }

    public void SendMessage()
    {
        string message = mInputComponent.text;
        if(message != null && message != "")
        {
            DataPacketString dataString = new DataPacketString(message);
            //DrawText(message, new Vector2(0, 0));
            //Debug.Log("보낸 메세지 = " + message);
            mSender.Sendn(ref dataString);
            AddDialogue(message);
            mInputComponent.text = "";
            mInputComponent.ActivateInputField(); // 활성화 유지
        }
    }

    void AddDialogue(string message)
    {
        if(DialogueRecord.Count >= mLimitDialogueWindow)
        {
            //Debug.Log("꽉참 지울 문장 = " + DialogueRecord[0]);
            DialogueRecord.RemoveAt(0);
        }
        DialogueRecord.Add(message);
        MessageUpdate();
    }

    void MessageUpdate()
    {
        setDialogue = "";
        foreach (string m in DialogueRecord)
        {
            setDialogue += m;
            setDialogue += "\n";
        }
        mText.text = setDialogue;
    }
}

/*
 using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chatting : MonoBehaviour {

    CSender mSender;
    CListener mListener;

    private List<string> m_message;
    private string m_sendComment = "";
    private string m_prevComment = "";

    // 말 풍선 표시용 텍스처.
    public Texture texture_ChatBg = null;
    //public Texture texture_tofu = null;
    //public Texture texture_daizu = null;

    //private ChatState m_state = ChatState.HOST_TYPE_SELECT;

    //enum ChatState
    //{
    //    HOST_TYPE_SELECT = 0,   // 방 선택.
    //    CHATTING,               // 채팅 중.
    //    LEAVE,                  // 나가기.
    //    ERROR,                  // 오류.
    //};

    private static float KADO_SIZE = 16.0f;
    private static float FONT_SIZE = 13.0f;
    private static float FONG_HEIGHT = 18.0f;
    private static int MESSAGE_LINE = 18;
    private static int CHAT_MEMBER_NUM = 2;

    GUIStyle style;
    // Use this for initialization

    //
    GameObject mInputFieldObj;
    InputField mInputComponent;
    GameObject mSendButtonObj;
    Button mSendButtonComponent;
    //

    void Start () {
        mSender = CSender.GetInstance();
        mListener = CListener.GetInstance();
        m_message = new List<string>();
        style = new GUIStyle();
        style.fontSize = 16;
        style.normal.textColor = Color.black;
        //
        mInputFieldObj = GameObject.FindGameObjectWithTag("TextInput");
        mInputComponent = mInputFieldObj.GetComponent<InputField>();
        mInputComponent.text = "";
        mSendButtonObj = GameObject.FindGameObjectWithTag("ButtonSend");
        mSendButtonComponent = mSendButtonObj.GetComponent<Button>();
    }
	
	// Update is called once per frame
	void Update () {
        UpdateChatting();
    }

    void OnGUI()
    {
        //ChattingGUI();
    }

    void UpdateChatting()
    {
        string recvMessage = mListener.GetRecvMessage();
        if(null != recvMessage)
        {
            AddMessage(recvMessage);
        }
    }

    void SendMessage()
    {
        string message = mInputComponent.text;
        if(message != null)
        {
            DataPacketString dataString = new DataPacketString(message);
            mSender.Sendn(ref dataString);
        }
    }

    void AddMessage(string str)
    {
        //while (m_message.Count >= MESSAGE_LINE)
        //{
        //    m_message.RemoveAt(0);
        //}
        m_message.Add(str);
    }

    void DispBalloon(Vector2 position, Vector2 size, Color color, bool left)
    {
        //Debug.Log("DispBalloon호출");
        // 말풍선 테두리를 그립니다.
        //DrawBaloonFrame(position, size, color, left);
        // 채팅 문장을 표시합니다. 	
        foreach (string s in m_message)
        {
            DrawText(s, position, size);
            position.y += FONG_HEIGHT;
        }
        //while(0 != messages.Count)
        //{
        //    Debug.Log("메세지 표시");
        //    DrawText(messages.Dequeue(), position, size);
        //    position.y += FONG_HEIGHT;
        //}
    }

    //void DrawBaloonFrame(Vector2 position, Vector2 size, Color color, bool left)
    //{
    //    GUI.color = color;

    //    float kado_size = KADO_SIZE;

    //    Vector2 p, s;

    //    s.x = size.x - kado_size * 2.0f;
    //    s.y = size.y;

    //    // 한 가운데.
    //    p = position - s / 2.0f;
    //    GUI.DrawTexture(new Rect(p.x, p.y, s.x, s.y), this.texture_main);

    //    // 좌.
    //    p.x = position.x - s.x / 2.0f - kado_size;
    //    p.y = position.y - s.y / 2.0f + kado_size;
    //    GUI.DrawTexture(new Rect(p.x, p.y, kado_size, size.y - kado_size * 2.0f), this.texture_main);

    //    // 우.
    //    p.x = position.x + s.x / 2.0f;
    //    p.y = position.y - s.y / 2.0f + kado_size;
    //    GUI.DrawTexture(new Rect(p.x, p.y, kado_size, size.y - kado_size * 2.0f), this.texture_main);

    //    // 좌상.
    //    p.x = position.x - s.x / 2.0f - kado_size;
    //    p.y = position.y - s.y / 2.0f;
    //    GUI.DrawTexture(new Rect(p.x, p.y, kado_size, kado_size), this.texture_kado_lu);

    //    // 우상.
    //    p.x = position.x + s.x / 2.0f;
    //    p.y = position.y - s.y / 2.0f;
    //    GUI.DrawTexture(new Rect(p.x, p.y, kado_size, kado_size), this.texture_kado_ru);

    //    // 좌하.
    //    p.x = position.x - s.x / 2.0f - kado_size;
    //    p.y = position.y + s.y / 2.0f - kado_size;
    //    GUI.DrawTexture(new Rect(p.x, p.y, kado_size, kado_size), this.texture_kado_ld);

    //    // 우하.
    //    p.x = position.x + s.x / 2.0f;
    //    p.y = position.y + s.y / 2.0f - kado_size;
    //    GUI.DrawTexture(new Rect(p.x, p.y, kado_size, kado_size), this.texture_kado_rd);

    //    // 말풍선 기호.
    //    p.x = position.x - kado_size;
    //    p.y = position.y + s.y / 2.0f;
    //    GUI.DrawTexture(new Rect(p.x, p.y, kado_size, kado_size), this.texture_belo);

    //    GUI.color = Color.white;
    //}

    void DrawText(string message, Vector2 position, Vector2 size)
    {
        if (null == message)
        {
            //Debug.Log("message 없음");
            return;
        }
        Vector2 text_size;

        text_size.x = message.Length * FONT_SIZE;
        text_size.y = FONG_HEIGHT;

        Vector2 p;

        p.x = position.x - size.x / 2.0f + KADO_SIZE;
        p.y = position.y - size.y / 2.0f + KADO_SIZE;

        GUI.Label(new Rect(p.x, p.y, text_size.x, text_size.y), message, style);
        //Debug.Log("p.x = " + p.x + " // p.y = " + p.y + " // text_size.x = " + text_size.x + " // text_size.y = " + text_size.y);

    }

    private void OnApplicationQuit()
    {
        //Debug.Log("유니티 종료 함수 호출 됨.");
        mListener.TerminaterThread();
        mListener = null;
    }
}
     */

/*
 using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chatting : MonoBehaviour {

    CSender mSender;
    CListener mListener;

    private List<string> m_message;
    private string m_sendComment = "";
    private string m_prevComment = "";

    // 말 풍선 표시용 텍스처.
    public Texture texture_ChatBg = null;
    //public Texture texture_tofu = null;
    //public Texture texture_daizu = null;

    //private ChatState m_state = ChatState.HOST_TYPE_SELECT;

    //enum ChatState
    //{
    //    HOST_TYPE_SELECT = 0,   // 방 선택.
    //    CHATTING,               // 채팅 중.
    //    LEAVE,                  // 나가기.
    //    ERROR,                  // 오류.
    //};

    private static float KADO_SIZE = 16.0f;
    private static float FONT_SIZE = 13.0f;
    private static float FONG_HEIGHT = 18.0f;
    private static int MESSAGE_LINE = 18;
    private static int CHAT_MEMBER_NUM = 2;

    GUIStyle style;
    // Use this for initialization

    //
    GameObject mInputFieldObj;
    InputField mInputComponent;
    GameObject mSendButtonObj;
    Button mSendButtonComponent;
    //

    void Start () {
        mSender = CSender.GetInstance();
        mListener = CListener.GetInstance();
        m_message = new List<string>();
        style = new GUIStyle();
        style.fontSize = 16;
        style.normal.textColor = Color.black;
        //
        mInputFieldObj = GameObject.FindGameObjectWithTag("TextInput");
        mInputComponent = mInputFieldObj.GetComponent<InputField>();
        mInputComponent.text = "";
        mSendButtonObj = GameObject.FindGameObjectWithTag("ButtonSend");
        mSendButtonComponent = mSendButtonObj.GetComponent<Button>();
    }
	
	// Update is called once per frame
	void Update () {
        //UpdateChatting();
    }

    void OnGUI()
    {
        //ChattingGUI();
    }

    void UpdateChatting()
    {
        string recvMessage = mListener.GetRecvMessage();
        if(null != recvMessage)
        {
            AddMessage(recvMessage);
        }
    }

    void ChattingGUI()
    {
        //Debug.Log("ChattingGUI 호출");
        Rect commentRect = new Rect(220, 450, 300, 30);
        m_sendComment = GUI.TextField(commentRect, m_sendComment, 15);

        bool isSent = GUI.Button(new Rect(530, 450, 100, 30), "말하기");
        if (Event.current.isKey &&
            Event.current.keyCode == KeyCode.Return)
        {
           // Debug.Log("말하기 버튼 클릭 isSent = " + isSent);
            if(m_sendComment != "")
            {
                isSent = true;
            }
        }

        if (isSent == true)
        {
            //string message = "[" + DateTime.Now.ToString("HH:mm:ss") + "] " + m_sendComment;
            //byte[] buffer = System.Text.Encoding.UTF8.GetBytes(message);
            //m_transport.Send(buffer, buffer.Length);
            DataPacketString message = new DataPacketString(m_sendComment);
            mSender.Sendn(ref message);
            AddMessage(m_sendComment);
            m_sendComment = "";
        }

        if (GUI.Button(new Rect(700, 560, 80, 30), "나가기"))
        {
            //m_state = ChatState.LEAVE;
            Debug.Log("나가기 버튼 클릭");
        }

        // 메시지 표시.
        DispBalloon(new Vector2(200.0f, 200.0f), new Vector2(340.0f, 360.0f), Color.cyan, true);
        //GUI.DrawTexture(new Rect(50.0f, 370.0f, 145.0f, 200.0f), this.texture_tofu);
    }

    void AddMessage(string str)
    {
        while (m_message.Count >= MESSAGE_LINE)
        {
            m_message.RemoveAt(0);
        }

        m_message.Add(str);
    }

    void DispBalloon(Vector2 position, Vector2 size, Color color, bool left)
    {
        //Debug.Log("DispBalloon호출");
        // 말풍선 테두리를 그립니다.
        //DrawBaloonFrame(position, size, color, left);
        // 채팅 문장을 표시합니다. 	
        foreach (string s in m_message)
        {
            DrawText(s, position, size);
            position.y += FONG_HEIGHT;
        }
        //while(0 != messages.Count)
        //{
        //    Debug.Log("메세지 표시");
        //    DrawText(messages.Dequeue(), position, size);
        //    position.y += FONG_HEIGHT;
        //}
    }

    //void DrawBaloonFrame(Vector2 position, Vector2 size, Color color, bool left)
    //{
    //    GUI.color = color;

    //    float kado_size = KADO_SIZE;

    //    Vector2 p, s;

    //    s.x = size.x - kado_size * 2.0f;
    //    s.y = size.y;

    //    // 한 가운데.
    //    p = position - s / 2.0f;
    //    GUI.DrawTexture(new Rect(p.x, p.y, s.x, s.y), this.texture_main);

    //    // 좌.
    //    p.x = position.x - s.x / 2.0f - kado_size;
    //    p.y = position.y - s.y / 2.0f + kado_size;
    //    GUI.DrawTexture(new Rect(p.x, p.y, kado_size, size.y - kado_size * 2.0f), this.texture_main);

    //    // 우.
    //    p.x = position.x + s.x / 2.0f;
    //    p.y = position.y - s.y / 2.0f + kado_size;
    //    GUI.DrawTexture(new Rect(p.x, p.y, kado_size, size.y - kado_size * 2.0f), this.texture_main);

    //    // 좌상.
    //    p.x = position.x - s.x / 2.0f - kado_size;
    //    p.y = position.y - s.y / 2.0f;
    //    GUI.DrawTexture(new Rect(p.x, p.y, kado_size, kado_size), this.texture_kado_lu);

    //    // 우상.
    //    p.x = position.x + s.x / 2.0f;
    //    p.y = position.y - s.y / 2.0f;
    //    GUI.DrawTexture(new Rect(p.x, p.y, kado_size, kado_size), this.texture_kado_ru);

    //    // 좌하.
    //    p.x = position.x - s.x / 2.0f - kado_size;
    //    p.y = position.y + s.y / 2.0f - kado_size;
    //    GUI.DrawTexture(new Rect(p.x, p.y, kado_size, kado_size), this.texture_kado_ld);

    //    // 우하.
    //    p.x = position.x + s.x / 2.0f;
    //    p.y = position.y + s.y / 2.0f - kado_size;
    //    GUI.DrawTexture(new Rect(p.x, p.y, kado_size, kado_size), this.texture_kado_rd);

    //    // 말풍선 기호.
    //    p.x = position.x - kado_size;
    //    p.y = position.y + s.y / 2.0f;
    //    GUI.DrawTexture(new Rect(p.x, p.y, kado_size, kado_size), this.texture_belo);

    //    GUI.color = Color.white;
    //}

    void DrawText(string message, Vector2 position, Vector2 size)
    {
        if (null == message)
        {
            //Debug.Log("message 없음");
            return;
        }
        //Debug.Log("그릴 message = " + message);
        //GUIStyle style = new GUIStyle();
        style.fontSize = 16;
        style.normal.textColor = Color.black;

        Vector2 balloon_size, text_size;

        text_size.x = message.Length * FONT_SIZE;
        text_size.y = FONG_HEIGHT;

        balloon_size.x = text_size.x + KADO_SIZE * 2.0f;
        balloon_size.y = text_size.y + KADO_SIZE;

        Vector2 p;

        p.x = position.x - size.x / 2.0f + KADO_SIZE;
        p.y = position.y - size.y / 2.0f + KADO_SIZE;
        //p.x = position.x - text_size.x/2.0f;
        //p.y = position.y - text_size.y/2.0f;

        GUI.Label(new Rect(p.x, p.y, text_size.x, text_size.y), message, style);
        //Debug.Log("p.x = " + p.x + " // p.y = " + p.y + " // text_size.x = " + text_size.x + " // text_size.y = " + text_size.y);

    }

    private void OnApplicationQuit()
    {
        //Debug.Log("유니티 종료 함수 호출 됨.");
        mListener.TerminaterThread();
        mListener = null;
    }
}

     */
