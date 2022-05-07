using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBGManager : MonoBehaviour
{
    public GameObject[] m_listBG = new GameObject[3];

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
            if( m_listBG[i].transform.position.x + 18 < m_goPlayer.transform.position.x )
            {
                Vector3 vecNew = m_listBG[i].transform.position;
                vecNew.x += 48;
                m_listBG[i].transform.position = vecNew;
            }
        }
    }
}
