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


    private CUIsManager m_uiManager;
    private Rigidbody2D rb;

    private int m_nState = 0;

    private float m_fPlayerXPoz;
    private Vector3 m_vecCameraPoz;

    private int m_nPlayerPozIndex = 0;

    private int m_nHP = 0;
    private int m_nStarPoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("GameStart");
        // rb = m_goPlayer.GetComponent<Rigidbody2D>();

        // GameStart();

        CGameData.Instance.InitData();

        m_uiManager = GetComponent<CUIsManager>();
        m_uiManager.ShowUI(0);

        GetComponent<CObjectManager>().InitObjects();

        for(int i = 0; i <= 15; i++)
        {
            GameObject goRoad = Instantiate(Resources.Load("Prefabs/Roads/road01") as GameObject);
            // m_goRoadParents.transform = goRoad.transform.parent;
            goRoad.transform.SetParent(m_goRoadParents.transform);
            goRoad.transform.position = new Vector3(i, 0, 0);

        }
        
    }

    // Update is called once per frame
    void Update()
    {
        // rb.transform.position += new Vector3(1, 0, 0) * 0.4f * Time.deltaTime;
        if( m_nState != 1 )
            return;

        m_fPlayerXPoz = m_goActivePlayer.transform.position.x;

        if( (int)m_fPlayerXPoz > m_nPlayerPozIndex )
        {
            m_nPlayerPozIndex = (int)m_fPlayerXPoz;
            GameObject goRoad = Instantiate(Resources.Load("Prefabs/Roads/road01") as GameObject);
            // m_goRoadParents.transform = goRoad.transform.parent;
            goRoad.transform.SetParent(m_goRoadParents.transform);
            goRoad.transform.position = new Vector3(14 + m_nPlayerPozIndex, 0, 0);

            GetComponent<CObjectManager>().CreateObject(new Vector3(m_nPlayerPozIndex + 14, 0, 0));

            GetComponent<CObjectManager>().HideObject();

            m_goBGManager.GetComponent<CBGManager>().UpdateBG();
        }

        m_vecCameraPoz = Camera.main.transform.position;
        m_vecCameraPoz.x = m_fPlayerXPoz + 3;
        Camera.main.transform.position = m_vecCameraPoz;
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
        m_uiManager.ShowUI(1);

        SetActivePlayer(0);

        SetHP(3);
        SetStarPoint(0);

        GetPlayer().GetComponent<CPlayer>().Run();

        m_nState = 1;
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

    public void SetStarPoint(int nPoint)
    {
        m_nStarPoint = nPoint;
        CUIInGame.Instance.UpdateStarPoint(m_nStarPoint);
    }

    public int AddStarPoint(int nPoint = 10)
    {
        m_nStarPoint += nPoint;
        if( m_nStarPoint > 9999)
            m_nStarPoint = 9999;

        CUIInGame.Instance.UpdateStarPoint(m_nStarPoint);
        return m_nStarPoint;
    }

    public int GetStarPoint()
    {
        return m_nStarPoint;
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
            m_nState = 2;
            GetPlayer().GetComponent<CPlayer>().GameOver();
            m_uiManager.ShowPopupGameOver();
        }

        return m_nHP;
    }

}
