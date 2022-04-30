using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGameEngine : MonoBehaviour
{
    // public GameObject m_goPlayer;

    public GameObject m_goPlayer;

    public GameObject[] m_listPlayer = new GameObject[2];

    private GameObject m_goActivePlayer;

    private Rigidbody2D rb;

    private int m_nState = 0;

    private float m_fPlayerXPoz;
    private Vector3 m_vecCameraPoz;

    // Start is called before the first frame update
    void Start()
    {
        // rb = m_goPlayer.GetComponent<Rigidbody2D>();

        GameStart();
    }

    // Update is called once per frame
    void Update()
    {
        // rb.transform.position += new Vector3(1, 0, 0) * 0.4f * Time.deltaTime;
        if( m_nState != 1 )
            return;

        m_fPlayerXPoz = m_goActivePlayer.transform.position.x;
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

    public void GameStart()
    {
        SetActivePlayer(0);

        m_nState = 1;
    }
}
