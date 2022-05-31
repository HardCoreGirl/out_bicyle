using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CObject : MonoBehaviour
{
    public int m_nIndex;    // 0 : Star

    private int m_nValue;

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

    public void SetValue(int nValue)
    {
        m_nValue = nValue;
    }

    public int GetValue()
    {
        return m_nValue;
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
                CGameEngine.Instance.Unbeatable();
            } else if (m_nIndex == 3 )
            {
                Debug.Log("Key Index : " + m_nValue);

                CGameData.Instance.SetKeyItem(CGameEngine.Instance.GetStage(), m_nValue, 1);
                CGameEngine.Instance.AddKeyCount();
                // CUIInGame.Instance.ShowPopupGetKeyword(m_nValue);
                CUIInGame.Instance.UpdateKeyCount();

                // CGameEngine.Instance.GetPlayer().GetComponent<CPlayer>().GetKey();
                // CGameEngine.Instance.SetState(3);

                CGameEngine.Instance.PauseGetKeyItem(m_nValue);
                
                // CGameEngine.Instance.Unbeatable();

            } else if(m_nIndex >= 10 )
            {
                CGameEngine.Instance.Damage();
            }

            if( m_nIndex < 10 )
                gameObject.SetActive(false);
        }
    }
}


