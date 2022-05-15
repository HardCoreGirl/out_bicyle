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
            _instance = this;
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            _instance = null;
        }
    }
    #endregion

    private float m_fJumpPower = 7f;

    private int m_nMaxRate = 100000;

    // 최대 확률 100000
    private int[] m_nRate = new int[6];      

    private float m_fBicyleSpeed = 7f;

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
        m_nRate[3] = 1000;
        m_nRate[4] = 5000;
        m_nRate[5] = 5000;
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
}
