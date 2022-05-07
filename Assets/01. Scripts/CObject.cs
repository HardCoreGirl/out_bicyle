using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CObject : MonoBehaviour
{
    public int m_nIndex;    // 0 : Star

    // private bool m_bIsActive;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateObject(Vector3 vecPoz)
    {
        transform.position = vecPoz;
    }

    public void HideObject()
    {
        if( transform.position.x < CGameEngine.Instance.GetPlayer().transform.position.x - 15 )
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("OnTrigger");
        // Debug.Log(other.name);

        if(other.tag.Equals("Player"))
        {
            if( m_nIndex == 0 )
            {
                CGameEngine.Instance.AddStarPoint();
            }else if( m_nIndex == 1 )
            {
                CGameEngine.Instance.AddHP();
            } else if (m_nIndex == 2 )
            {
                
            } else if(m_nIndex >= 10 )
            {
                CGameEngine.Instance.Damage();
            }

            if( m_nIndex < 10 )
                gameObject.SetActive(false);
        }
    }
}


