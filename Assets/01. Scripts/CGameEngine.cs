using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGameEngine : MonoBehaviour
{
    #region SingleTon
    public static CGameEngine _instance = null;

    public static CGameEngine Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log("CGameEngine install null");

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
    // public GameObject m_goPlayer;

    public GameObject m_goPlayer;

    public GameObject[] m_listPlayer = new GameObject[3];

    public GameObject m_goActivePlayer;

    public GameObject m_goBGManager;

    public GameObject m_goRoadParents;

    public GameObject[] m_listStageRoad = new GameObject[12];
    public GameObject[] m_listBG = new GameObject[3];


    private CUIsManager m_uiManager;
    private Rigidbody2D rb;

    private int m_nState = 0;

    private float m_fPlayerXPoz;
    private Vector3 m_vecCameraPoz;

    private int m_nPlayerPozIndex = 0;

    private int m_nHP = 0;
    // private int[,] m_listGetKey = new int[11,7];

    private int m_nStage = 1;

    private int m_nGetKeyCnt = 0;

    // private float m_fGetKeyTime = 0;
    private long m_lGetKeyTime = 0;

    private int m_nGetKeyIndex = 0;

    private float m_fStageTime = 60f;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("GameStart");

        CGameData.Instance.InitData();

        m_uiManager = GetComponent<CUIsManager>();
        // m_uiManager.ShowUI(0);

        GetComponent<CObjectManager>().InitObjects();

        // for(int i = 0; i <= 15; i++)
        // {
        //     GameObject goRoad = Instantiate(Resources.Load("Prefabs/Roads/road01") as GameObject);
        //     // m_goRoadParents.transform = goRoad.transform.parent;
        //     goRoad.transform.SetParent(m_goRoadParents.transform);
        //     goRoad.transform.position = new Vector3(i, 0, 0);

        // }

        CAudioManager.Instance.PlayBGInGame();

        GameStart();
        
    }

    // Update is called once per frame
    void Update()
    {
        if( Time.timeScale == 0 )
        {
            if( System.DateTime.Now.Ticks > m_lGetKeyTime )
            {
                if (CGameData.Instance.GetStage() == 0)
                {
                    CUIInGame.Instance.HideTutorial();
                }

                Time.timeScale = 1;

                CUIInGame.Instance.HidePopupGetKeyword();
            }
        }
        // rb.transform.position += new Vector3(1, 0, 0) * 0.4f * Time.deltaTime;
        // if( m_nState != 1 )
        if( CGameData.Instance.GetState() != 1 )
            return;

        
    
        if( CGameData.Instance.AddPlayTime(Time.deltaTime) >= m_fStageTime )
        {
            CGameData.Instance.SetClearStage(CGameData.Instance.GetStage(), 1);
            if( CGameData.Instance.GetStage() < 11  )
            {
                if( CGameData.Instance.GetClearStage(CGameData.Instance.GetStage() + 1) < 0 ) 
                    CGameData.Instance.SetClearStage(CGameData.Instance.GetStage() + 1, 0);
            }
            CUIInGame.Instance.ShowPopupGameClear();
//            CUIInGame.Instance.ShowPopupFinish();
//            CGameData.Instance.SetState(2);
            return;
        }

        CUIInGame.Instance.UpdatePlayBar(CGameData.Instance.GetPlayTime() / m_fStageTime);

        m_fPlayerXPoz = m_goActivePlayer.transform.position.x;

        if( (int)m_fPlayerXPoz > m_nPlayerPozIndex )
        {
            m_nPlayerPozIndex = (int)m_fPlayerXPoz;

            GetComponent<CObjectManager>().CreateObject(new Vector3(m_nPlayerPozIndex + 14, 0, 0));
            GetComponent<CObjectManager>().HideObject();
            m_goBGManager.GetComponent<CBGManager>().UpdateBG();
        }

        if( CGameData.Instance.GetStage() == 0 )
        {
            if( m_nPlayerPozIndex == 46 )
            {
                if( !CGameData.Instance.IsTutorialJump() )
                {
                    CGameData.Instance.SetIsTutorialJump(true);
                    CUIInGame.Instance.ShowTutorial(1);
                    Pause();
                }
            }
        }

        // m_vecCameraPoz = Camera.main.transform.position;
        // m_vecCameraPoz.x = m_fPlayerXPoz + 3;
        // Camera.main.transform.position = m_vecCameraPoz;

        Camera.main.transform.position += new Vector3(1, 0, 0) * CGameData.Instance.GetBicyleSpeed() * Time.deltaTime;
    }

    public void SetState(int nState)
    {
        m_nState = nState;
    }

    public int GetState()
    {
        return m_nState;
    }

    public void SetActivePlayer(int nIndex)
    {
        for(int i = 0; i < m_listPlayer.Length; i++)
        {
            m_listPlayer[i].SetActive(false);
        }

        m_listPlayer[nIndex].SetActive(true);
        m_goActivePlayer = m_listPlayer[nIndex];
    }

    public GameObject GetPlayer()
    {
        return m_goActivePlayer;
    }

    public void GameStart()
    {
        CGameData.Instance.SetPlayTime(0);

        m_nStage = CGameData.Instance.GetStage();

        if (CGameData.Instance.GetStage() == 0)
        {
            m_fStageTime = 20f;
            CGameData.Instance.InitTutorial();
        } else
            m_fStageTime = 60f;

        for(int i = 0; i < m_listStageRoad.Length; i++)
        {
            m_listStageRoad[i].GetComponent<SpriteRenderer>().sprite = CObjectManager.Instance.GetRoad(CGameData.Instance.GetStage()).GetComponent<SpriteRenderer>().sprite;
        }

        for(int i = 0; i < m_listBG.Length; i++)
        {
            m_listBG[i].GetComponent<SpriteRenderer>().sprite = CObjectManager.Instance.GetBG(CGameData.Instance.GetStage()).GetComponent<SpriteRenderer>().sprite;
        }


        m_uiManager.ShowUI(1);
        CObjectManager.Instance.InitKeyItem(m_nStage);        

        // SetActivePlayer(2);
        SetActivePlayer(CGameData.Instance.GetPlayerIndex());

        SetHP(3);
        SetKeyCount(0);
        // CGameData.Instance.
        // SetStarPoint(0);

        CUIInGame.Instance.UpdateStarPoint();
        CUIInGame.Instance.UpdateKeyCount();

        // CUIInGame.Instance.ShowPopupGameStart();

        StartCoroutine("ProcessGameStart");

        // GetPlayer().GetComponent<CPlayer>().Run();

        // // m_nState = 1;
        // CGameData.Instance.SetState(1);
    }

    IEnumerator ProcessGameStart()
    {
        CUIInGame.Instance.ShowPopupGameStart();

        yield return new WaitForSeconds(4f);

        GetPlayer().GetComponent<CPlayer>().Run();
        CGameData.Instance.SetState(1);

        // yield return new WaitForSeconds(0.5f);
        CUIInGame.Instance.HidePopupGameStart();
    }

    public void PauseGetKeyItem(int nKeyIndex)
    {
        m_nGetKeyIndex = nKeyIndex;
        
        CUIInGame.Instance.ShowPopupGetKeyword(m_nGetKeyIndex);
        m_lGetKeyTime = System.DateTime.Now.Ticks + 15000000;

        Time.timeScale = 0;
    }

    public void PauseGetKeyTutorial()
    {
        m_lGetKeyTime = System.DateTime.Now.Ticks + 30000000;

        Time.timeScale = 0;
    }

    public void Pause()
    {
        m_lGetKeyTime = System.DateTime.Now.Ticks + 99999000000;

        Time.timeScale = 0;
    }

    public void Restart()
    {
        m_lGetKeyTime = 0;
    }

    public void SetStage(int nStage)
    {
        m_nStage = nStage;
    }

    public int GetStage()
    {
        return m_nStage;
    }

    public void SetHP(int nHP)
    {
        m_nHP = nHP;
        CUIInGame.Instance.UpdateHP(m_nHP);
    }

    public int AddHP(int nHP = 1)
    {
        m_nHP += nHP;
        CUIInGame.Instance.UpdateHP(m_nHP);
        return m_nHP;
    }

    public int GetHP()
    {
        return m_nHP;
    }

    public void ResetPause()
    {
        m_lGetKeyTime = 0;
    }

    public void SetKeyCount(int nCount)
    {
        m_nGetKeyCnt = nCount;
    }

    public int GetKeyCount()
    {
        return m_nGetKeyCnt;
    }

    public int AddKeyCount()
    {
        int nAddCount = GetKeyCount() + 1;
        SetKeyCount(nAddCount);
        return nAddCount;
    }

    public void AddStarPoint(int nPoint = 10)
    {
        if( CGameData.Instance.GetStage() == 0 )
        {
            if( !CGameData.Instance.IsTutorialGetStar() )
            {
                CGameData.Instance.SetIsTutorialGetStar(true);
                CUIInGame.Instance.ShowTutorial(0);
                m_lGetKeyTime = System.DateTime.Now.Ticks + 30000000;
                Time.timeScale = 0;
            }
        }
        // m_nStarPoint += nPoint;
        // if( m_nStarPoint > 9999)
        //     m_nStarPoint = 9999;

        CGameData.Instance.AddStar(nPoint);

        // CUIInGame.Instance.UpdateStarPoint(m_nStarPoint);
        CUIInGame.Instance.UpdateStarPoint();
    }

    public void Unbeatable()
    {
        GetPlayer().GetComponent<CPlayer>().Unbeatable();
    }

    public int Damage()
    {
        if( GetPlayer().GetComponent<CPlayer>().IsUnbeatable() )
            return m_nHP;

        m_nHP--;
        CUIInGame.Instance.UpdateHP(m_nHP);

        GetPlayer().GetComponent<CPlayer>().Damage();

        if( m_nHP <= 0 )
        {
            // m_nState = 2;
            CGameData.Instance.SetState(2);
            GetPlayer().GetComponent<CPlayer>().GameOver();
            m_uiManager.ShowPopupGameOver();
        }

        return m_nHP;
    }
}
