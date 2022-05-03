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
}
