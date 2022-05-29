using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBGManager : MonoBehaviour
{
    public GameObject[] m_listBG = new GameObject[3];

    public GameObject m_goBackgrounds;
    public GameObject[] m_listBackground = new GameObject[3];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateBG()
    {
        GameObject m_goPlayer = CGameEngine.Instance.GetPlayer();

        for(int i = 0; i < m_listBG.Length; i++)
        {
            if( m_listBG[i].transform.position.x + 8 < m_goPlayer.transform.position.x )
            {
                Vector3 vecNew = m_listBG[i].transform.position;
                vecNew.x += 24;
                m_listBG[i].transform.position = vecNew;
            }
        }


        
        for(int i = 0; i < m_listBackground.Length; i++)
        {
            if( m_listBackground[i].transform.position.x + 8 < m_goPlayer.transform.position.x )
            {
                Vector3 vecNew = m_listBackground[i].transform.position;
                vecNew.x += 48;
                m_listBackground[i].transform.position = vecNew;
            }
        }

        

        // Vector3 vecBackgrounds = m_goBackgrounds.transform.position;
        // vecBackgrounds.x = Camera.main.transform.position.x;
        // m_goBackgrounds.transform.position = vecBackgrounds;
    }

    public void FixedUpdate()
    {
        // if( CGameEngine.Instance.GetState() != 1 )
        if( CGameData.Instance.GetState() != 1 )
            return;

        m_goBackgrounds.transform.localPosition += new Vector3(-1, 0, 0) * (CGameData.Instance.GetBicyleSpeed() * 0.1f)* Time.deltaTime;
    }
}
