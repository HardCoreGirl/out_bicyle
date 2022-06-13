using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGameData : MonoBehaviour
{
    #region SingleTon
    public static CGameData _instance = null;

    public static CGameData Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log("CGameData install null");

            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else 
        {
            DestroyImmediate(this.gameObject);
        }
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            _instance = null;
        }
    }
    #endregion

    private float m_fJumpPower = 5f;

    private int m_nMaxRate = 100000;

    // 최대 확률 100000
    private int[] m_nRate = new int[5];      

    private float m_fBicyleSpeed = 7f;

    // 열쇠 아이템 개수
    private int[] m_listStageKeyCnt = new int[11];

    // 스테이지별 자전거 속도
    private float[] m_listStageSpeed = new float[11];

    private string[] m_listStageTitle = new string[11];
    private string[] m_listStageMsg = new string[11];
    private string[] m_listKeyIndex = new string[10];
    private string[,] m_listKeyMessage = new string[11, 10];

    private int m_nPlayerIndex = 0;

    private int m_nState = 0;
    private int m_nStage = 0;

    private float m_fPlayTime = 0;

    private bool m_bIsLoad = false;

    private int[] m_listClearStage = new int[11];
    private int[,] m_listGetKey = new int[11, 7];

    private int[] m_listBicycle = new int[3];

    private int m_nStar = 0;

    private int m_nLobbyIndex = 0;

    private bool m_bIsTutorialGetStar = false;
    private bool m_bIsTutorialJump = false;
    private bool m_bIsTutorialKey = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitData()
    {
        if(m_bIsLoad)
            return;

        m_bIsLoad = true;

        SetStage(0);

        //SetStar(99999);

        
        m_nStar = PlayerPrefs.GetInt("Star", 0);
        
        string strKey = "";
        for(int i = 0; i < 11; i++)
        {
            strKey = "Clear" + i.ToString("00");
            m_listClearStage[i] = PlayerPrefs.GetInt(strKey, -1);

            for(int j = 0; j < 7; j++)
            {
                strKey = "Key" + i.ToString("00") + j.ToString("00");
                m_listGetKey[i, j] = PlayerPrefs.GetInt(strKey, -1);
            }
        }

        if( GetClearStage(0) == -1 )
            SetClearStage(0, 0);

        for(int i = 0; i < 3; i++)
        {
            strKey = "Bicycle" + i.ToString("00");
            m_listBicycle[i] = PlayerPrefs.GetInt(strKey, 0);
        }

        m_nRate[0] = 10000;
        m_nRate[1] = 1000;
        m_nRate[2] = 1000;
        // m_nRate[3] = 1000;
        m_nRate[3] = 5000;
        m_nRate[4] = 5000;

        m_listStageKeyCnt[0] = 4;
        m_listStageKeyCnt[1] = 6;
        m_listStageKeyCnt[2] = 5;
        m_listStageKeyCnt[3] = 5;
        m_listStageKeyCnt[4] = 6;
        m_listStageKeyCnt[5] = 3;
        m_listStageKeyCnt[6] = 5;
        m_listStageKeyCnt[7] = 4;
        m_listStageKeyCnt[8] = 4;
        m_listStageKeyCnt[9] = 4;
        m_listStageKeyCnt[10] = 7;

        m_listStageSpeed[0] = 6f;
        m_listStageSpeed[1] = 6f;
        m_listStageSpeed[2] = 6f;
        m_listStageSpeed[3] = 6f;
        m_listStageSpeed[4] = 7f;
        m_listStageSpeed[5] = 7f;
        m_listStageSpeed[6] = 7f;
        m_listStageSpeed[7] = 8f;
        m_listStageSpeed[8] = 8f;
        m_listStageSpeed[9] = 8f;
        m_listStageSpeed[10] = 10f;

        m_listStageTitle[0] = "튜토리얼";
        m_listStageMsg[0] = "열쇠 4개 속에 숨겨진 슬로건을 찾아보세요";
        m_listStageTitle[1] = "Stage 1. 성경신학개론";
        m_listStageMsg[1] = "성경을 신학적으로 읽어야 하는 6가지 이유를 찾아보세요";
        m_listStageTitle[2] = "Stage2. 창세기로 보는 구속사";
        m_listStageMsg[2] = "하나님께서 명령하신 정복명령 5가지는 무엇일까요?";
        m_listStageTitle[3] = "Stage3. 베리트, 하나님의 언약";
        m_listStageMsg[3] = "하나님이 아브라함을 통해 메시아를 보내겠다는\n언약이 담긴 창세기 15:4을 완성하세요";
        m_listStageTitle[4] = "Stage4. 하나님의 왕권, 거룩한 계승";
        m_listStageMsg[4] = "아담으로 시작된 하나님의 왕권 계승의 역사는 어떻게 될까요?";
        m_listStageTitle[5] = "Stage5. 지혜로우신 예수그리스도";
        m_listStageMsg[5] = "우리를 구원하기 위한 하나님의 3가지 지혜를 찾아보세요";
        m_listStageTitle[6] = "Stage6. 이 시대의 선지자, 그리스도인";
        m_listStageMsg[6] = "분량이 많은 선지서를 대선지서라고 합니다.\n열쇠 속에 숨긴 대선지서 5권을 찾아보세요";
        m_listStageTitle[7] = "Stage7. 구속사의 정점, 메시아 공생애";
        m_listStageMsg[7] = "4복음서의 수신자는 누구일까요?";
        m_listStageTitle[8] = "Stage8. 성도의 참 위로자이신 예수님";
        m_listStageMsg[8] = "바울이 감옥에서 쓴 서신을 옥중서신이라고 합니다.\n열쇠 속에 숨겨진 옥중서신 4권과 그 특징을 찾아보세요.";
        m_listStageTitle[9] = "Stage9. 반드시 다시 오실 예수님";
        m_listStageMsg[9] = "초림을 먼저 알았던 사람들은 누구일까요?";
        m_listStageTitle[10] = "Stage10. 말씀으로 담대하고 거침없이";
        m_listStageMsg[10] = "담대하고 거침없이 산다는 것은 어떤 삶일까요?";

        m_listKeyIndex[0] = "첫번째 키워드";
        m_listKeyIndex[1] = "두번째 키워드";
        m_listKeyIndex[2] = "세번째 키워드";
        m_listKeyIndex[3] = "네번째 키워드";
        m_listKeyIndex[4] = "다섯번째 키워드";
        m_listKeyIndex[5] = "여섯번째 키워드";
        m_listKeyIndex[6] = "일곱번째 키워드";
        m_listKeyIndex[7] = "여덜번째 키워드";
        m_listKeyIndex[8] = "아홉번째 키워드";
        m_listKeyIndex[9] = "열번째 키워드";

        m_listKeyMessage[0, 0] = "영의 건강";
        m_listKeyMessage[0, 1] = "육의 건강";
        m_listKeyMessage[0, 2] = "영육 건강";
        m_listKeyMessage[0, 3] = "성경 일주";

        m_listKeyMessage[1, 0] = "성경이 증언하고 있다";
        m_listKeyMessage[1, 1] = "예수님이 명령하셨다";
        m_listKeyMessage[1, 2] = "사도들이 그렇게 가르쳤다";
        m_listKeyMessage[1, 3] = "성도들이 그렇게 가르쳤다";
        m_listKeyMessage[1, 4] = "예수님이 교회를 세우셨다";
        m_listKeyMessage[1, 5] = "성령이 그렇게 가르쳤다";

        m_listKeyMessage[2, 0] = "생육하라";
        m_listKeyMessage[2, 1] = "번성하라";
        m_listKeyMessage[2, 2] = "충만하라";
        m_listKeyMessage[2, 3] = "정복하라";
        m_listKeyMessage[2, 4] = "다스려라";

        m_listKeyMessage[3, 0] = "말씀";
        m_listKeyMessage[3, 1] = "임하여";
        m_listKeyMessage[3, 2] = "상속자";
        m_listKeyMessage[3, 3] = "네 몸에서";
        m_listKeyMessage[3, 4] = "상속자";

        m_listKeyMessage[4, 0] = "출애굽기: 제사장 나라 출범";
        m_listKeyMessage[4, 1] = "레위기: 하나님의 백성이 되는 과정";
        m_listKeyMessage[4, 2] = "민수기: 다음세대에게 신앙 전수";
        m_listKeyMessage[4, 3] = "여호수아: 하나님의 진정한 백성이 되기 위한 고군분투";
        m_listKeyMessage[4, 4] = "사사기: 진정한 왕을 찾아가는 여정";
        m_listKeyMessage[4, 5] = "룻기: 다윗을 통한 하나님의 본격적인 왕위 계승";

        m_listKeyMessage[5, 0] = "육신으로 이 땅에 오심";
        m_listKeyMessage[5, 1] = "우리의 죄를 대속함으로 죽으심";
        m_listKeyMessage[5, 2] = "부활하셔서 구원의 길로 인도하심";

        m_listKeyMessage[6, 0] = "이사야";
        m_listKeyMessage[6, 1] = "예레미야";
        m_listKeyMessage[6, 2] = "예레미야애가";
        m_listKeyMessage[6, 3] = "에스겔";
        m_listKeyMessage[6, 4] = "다니엘";

        m_listKeyMessage[7, 0] = "마태복음: 유대인";
        m_listKeyMessage[7, 1] = "마가복음: 약하고 소외된 자들";
        m_listKeyMessage[7, 2] = "누가복음: 유대인이 아닌 이방인";
        m_listKeyMessage[7, 3] = "요한복음: 예수님을 알지도 못하는 이방인";

        m_listKeyMessage[8, 0] = "에베소서: 에베소 교회를 향한 사랑의 서신";
        m_listKeyMessage[8, 1] = "빌립보서: 기쁨과 겸손, 천국 소망을 쓴 서신";
        m_listKeyMessage[8, 2] = "골로새서: 세상의 가르침을 거부하고 성도다운 삶을 권면하는 서신";
        m_listKeyMessage[8, 3] = "빌레몬서: 사랑과 용서, 화해를 당부하는 서신";

        m_listKeyMessage[9, 0] = "동방박사";
        m_listKeyMessage[9, 1] = "양 치던 목자들";
        m_listKeyMessage[9, 2] = "신실한 유대인 시므온";
        m_listKeyMessage[9, 3] = "경건한 과부 안나";

        m_listKeyMessage[10, 0] = "소명에 집중하는 삶";
        m_listKeyMessage[10, 1] = "영원한 생명을 소망하는 삶";
        m_listKeyMessage[10, 2] = "기도와 말씀으로 지혜를 열망하는 삶";
        m_listKeyMessage[10, 3] = "말씀 앞에 실패해도 순종하는 삶";
        m_listKeyMessage[10, 4] = "우리의 연약함을 인정하는 삶";
        m_listKeyMessage[10, 5] = "사명을 비교하지 않는 삶";
        m_listKeyMessage[10, 6] = "예수님과 동행하는 삶";
    }


    public float GetJumpPower()
    {
        return m_fJumpPower;
    }

    public int GetMaxRate()
    {
        return m_nMaxRate;

    }

    public int GetRateCnt()
    {
        return m_nRate.Length;
    }

    public void SetRate(int nIndex, int nValue)
    {
        m_nRate[nIndex] = nValue;
    }

    public int GetRate(int nIndex)
    {
        return m_nRate[nIndex];
    }

    public void SetBicyleSpeed(float fSpeed)
    {
        m_fBicyleSpeed = fSpeed;
    }

    public float GetBicyleSpeed()
    {
        return m_fBicyleSpeed;
    }

    public int GetStageKeyCount(int nStage)
    {
        return m_listStageKeyCnt[nStage];
    }

    public float GetStageSpeed(int nStage)
    {
        return m_listStageSpeed[nStage];
    }

    public int GetDistance(float fSpeed)
    {
        return (int)(fSpeed * 49f);
    }

    public string GetKeyIndex(int nIndex)
    {
        return m_listKeyIndex[nIndex];
    }

    public string GetKeyMessage(int nStage, int nIndex)
    {
        return m_listKeyMessage[nStage, nIndex];
    }

    public string GetStageTitle(int nStage)
    {
        return m_listStageTitle[nStage];
    }

    public string GetStageMsg(int nStage)
    {
        return m_listStageMsg[nStage];
    }

    //-------------------------------------------
    public void SetStar(int nStar)
    {
        m_nStar = nStar;
        PlayerPrefs.SetInt("Star", nStar);
    }

    public int GetStar()
    {
        return m_nStar;
    }

    public int AddStar(int nStar)
    {
        int nAddStar = GetStar() + nStar;
        if( nAddStar > 99999 )
            nAddStar = 99999;
            
        SetStar(nAddStar);
        return nAddStar;
    }

    public int UseStar(int nStar)
    {
        int nUseStar = GetStar() - nStar;
        if(nUseStar < 0)
            return -1;

        SetStar(nUseStar);

        return nUseStar;
    }

    public void SetClearStage(int nStage, int nClear)
    {
        string strKey = "Clear" + nStage.ToString("00");

        m_listClearStage[nStage] = nClear;

        PlayerPrefs.SetInt(strKey, nClear);
    }

    public int GetClearStage(int nStage)
    {
        return m_listClearStage[nStage];
    }

    public void SetKeyItem(int nStage, int nIndex, int nState)
    {
        m_listGetKey[nStage, nIndex] = nState;

        string strKey = "Key" + nStage.ToString("00") + nIndex.ToString("00");
        PlayerPrefs.SetInt(strKey, nState);
    }

    public int GetKeyItem(int nState, int nIndex)
    {
        return m_listGetKey[nState, nIndex];
    }

    public int GetKeyCountInStage(int nStage)
    {
        int nCnt = 0;
        for(int i = 0; i < 7; i++)   
        {
            if( m_listGetKey[nStage, i] >= 0 )
                nCnt++;
        }

        return nCnt;
    }

    public void SetBicycle(int nIndex, int nState)
    {
        m_listBicycle[nIndex] = nState;

        string strKey = "Bicycle" + nIndex.ToString("00");
        PlayerPrefs.SetInt(strKey, nState);
    }

    public int GetBicycle(int nIndex)
    {
        if( nIndex == 0 )
            return 1;

        return m_listBicycle[nIndex];
    }

    public void SetPlayerIndex(int nIndex)
    {
        m_nPlayerIndex = nIndex;
    }

    public int GetPlayerIndex()
    {
        return m_nPlayerIndex;
    }

    public void SetState(int nState)
    {
        m_nState = nState;
    }

    public int GetState()
    {
        return m_nState;
    }

    public void SetStage(int nStage)    
    {
        m_nStage = nStage;
    }

    public int GetStage()
    {
        return m_nStage;
    }

    public void SetPlayTime(float fTime)
    {
        m_fPlayTime = fTime;
    }

    public float GetPlayTime()
    {
        return m_fPlayTime;
    }

    public float AddPlayTime(float fTime)
    {
        float fAddTime = GetPlayTime() + fTime;
        SetPlayTime(fAddTime);
        return fAddTime;
    }

    public void SetLobbyIndex(int nIndex)
    {
        m_nLobbyIndex = nIndex;
    }

    public int GetLobbyIndex()
    {
        return m_nLobbyIndex;
    }
    
    public void SetIsTutorialGetStar(bool bIsTutorial)
    {
        m_bIsTutorialGetStar = bIsTutorial;
    }

    public bool IsTutorialGetStar()
    {
        return m_bIsTutorialGetStar;
    }

    public void SetIsTutorialJump(bool bIsTutorial)
    {
        m_bIsTutorialJump = bIsTutorial;
    }

    public bool IsTutorialJump()
    {
        return m_bIsTutorialJump;
    }

    public void SetIsTutorialKey(bool bIsTutorial)
    {
        m_bIsTutorialKey = bIsTutorial;
    }    

    public bool IsTutorialKey()
    {
        return m_bIsTutorialKey;
    }

    public void InitTutorial()
    {
        m_bIsTutorialGetStar = false;
        m_bIsTutorialJump = false;
        m_bIsTutorialKey = false;
    }
}

