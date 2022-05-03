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

    public GameObject[] m_listPlayer = new GameObject[2];

    public GameObject m_goActivePlayer;

    public GameObject m_goRoadParents;

    private Rigidbody2D rb;

    private int m_nState = 0;

    private float m_fPlayerXPoz;
    private Vector3 m_vecCameraPoz;

    private int m_nPlayerPozIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        // rb = m_goPlayer.GetComponent<Rigidbody2D>();

        GameStart();

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

            GetComponent<CObjectManager>().CreateObject(new Vector3(m_nPlayerPozIndex + 14, 1, 0));

            GetComponent<CObjectManager>().HideObject();
        }

        m_vecCameraPoz = Camera.main.transform.position;
        m_vecCameraPoz.x = m_fPlayerXPoz;
        Camera.main.transform.position = m_vecCameraPoz;
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
        SetActivePlayer(0);

        m_nState = 1;
    }
}
