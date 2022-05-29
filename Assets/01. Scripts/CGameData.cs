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


    private int m_nPlayerIndex = 0;

    private int m_nState = 0;


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
}
