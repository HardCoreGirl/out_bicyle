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

    private string[] m_listKeyIndex = new string[10];
    private string[,] m_listKeyMessage = new string[11, 10];

    private int m_nPlayerIndex = 0;

    private int m_nState = 0;
    private int m_nStage = 0;

    private float m_fPlayTime = 0;

    private bool m_bIsLoad = false;

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
        
        SetStage(1);

        m_nRate[0] = 10000;
        m_nRate[1] = 1000;
        m_nRate[2] = 1000;
        // m_nRate[3] = 1000;
        m_nRate[3] = 5000;
        m_nRate[4] = 5000;

        m_listStageKeyCnt[0] = 2;
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

        m_listKeyIndex[0] = "첫번째 이유";
        m_listKeyIndex[1] = "두번째 이유";
        m_listKeyIndex[2] = "세번째 이유";
        m_listKeyIndex[3] = "네번째 이유";
        m_listKeyIndex[4] = "다섯번째 이유";
        m_listKeyIndex[5] = "여섯번째 이유";
        m_listKeyIndex[6] = "일곱번째 이유";
        m_listKeyIndex[7] = "여덜번째 이유";
        m_listKeyIndex[8] = "아홉번째 이유";
        m_listKeyIndex[9] = "열번째 이유";

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
        m_listKeyMessage[4, 4] = "사사기: 긴정한 왕을 찾아가는 여정";
        m_listKeyMessage[4, 5] = "룻기: 다윗을 통한 하나님의 본격적인 왕위 계승";

        m_listKeyMessage[5, 0] = "육신으로 이 땅에 오심";
        m_listKeyMessage[5, 1] = "우리의 죄를 대속함으로 죽으심";
        m_listKeyMessage[5, 2] = "부활하셔서 구원의 길로 인도하심";

        m_listKeyMessage[6, 0] = "이사야";
        m_listKeyMessage[6, 1] = "예레미야";
        m_listKeyMessage[6, 2] = "예레미아야애가";
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
        return (int)(fSpeed * 60f);
    }

    public string GetKeyIndex(int nIndex)
    {
        return m_listKeyIndex[nIndex];
    }

    public string GetKeyMessage(int nStage, int nIndex)
    {
        return m_listKeyMessage[nStage, nIndex];
    }

    //-------------------------------------------

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
}
