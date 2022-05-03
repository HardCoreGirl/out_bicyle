using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CObjectManager : MonoBehaviour
{
    public GameObject m_goStarParent;
    public GameObject[] m_listStar = new GameObject[20];

    public GameObject m_goHeartParent;
    public GameObject[] m_listHeart = new GameObject[20];

    public GameObject m_goBibleParent;
    public GameObject[] m_listBible = new GameObject[20];

    public GameObject m_goWoodParent;
    public GameObject[] m_listWood = new GameObject[20];

    public GameObject m_goPuddieParent;
    public GameObject[] m_listPuddie = new GameObject[20];


    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitObjects()
    {
        for(int i = 0; i < m_listStar.Length; i++)
        {
            m_listStar[i] = Instantiate(Resources.Load("Prefabs/Objects/Star") as GameObject);
            m_listStar[i].transform.SetParent(m_goStarParent.transform);
            m_listStar[i].SetActive(false);
        }

        for(int i = 0; i < m_listHeart.Length; i++)
        {
            m_listHeart[i] = Instantiate(Resources.Load("Prefabs/Objects/Heart") as GameObject);
            m_listHeart[i].transform.SetParent(m_goHeartParent.transform);
            m_listHeart[i].SetActive(false);
        }

        for(int i = 0; i < m_listBible.Length; i++)
        {
            m_listBible[i] = Instantiate(Resources.Load("Prefabs/Objects/Bible") as GameObject);
            m_listBible[i].transform.SetParent(m_goBibleParent.transform);
            m_listBible[i].SetActive(false);
        }

        for(int i = 0; i < m_listWood.Length; i++)
        {
            m_listWood[i] = Instantiate(Resources.Load("Prefabs/Objects/Wood") as GameObject);
            m_listWood[i].transform.SetParent(m_goWoodParent.transform);
            m_listWood[i].SetActive(false);
        }

        for(int i = 0; i < m_listPuddie.Length; i++)
        {
            m_listPuddie[i] = Instantiate(Resources.Load("Prefabs/Objects/Puddie") as GameObject);
            m_listPuddie[i].transform.SetParent(m_goPuddieParent.transform);
            m_listPuddie[i].SetActive(false);
        }

    }

    public void CreateObject(Vector3 vecPoz)
    {
        int nRandom = Random.Range(0, 10);

        int nActiveIndex = 0;        
        if( nRandom == 0 )
        {
            for(int i = 0; i < m_listStar.Length; i++)
            {
                if(!m_listStar[i].activeSelf)
                {
                    nActiveIndex = i;
                    break;
                }
            }
            m_listStar[nActiveIndex].SetActive(true);
            m_listStar[nActiveIndex].GetComponent<CObject>().CreateObject(vecPoz);
        } else if ( nRandom == 1 )
        {
            for(int i = 0; i < m_listHeart.Length; i++)
            {
                if(!m_listHeart[i].activeSelf)
                {
                    nActiveIndex = i;
                    break;
                }
            }
            m_listHeart[nActiveIndex].SetActive(true);
            m_listHeart[nActiveIndex].GetComponent<CObject>().CreateObject(vecPoz);
        } else if ( nRandom == 2 )
        {
            for(int i = 0; i < m_listBible.Length; i++)
            {
                if(!m_listBible[i].activeSelf)
                {
                    nActiveIndex = i;
                    break;
                }
            }
            m_listBible[nActiveIndex].SetActive(true);
            m_listBible[nActiveIndex].GetComponent<CObject>().CreateObject(vecPoz);
        } else if ( nRandom == 3 )
        {
            for(int i = 0; i < m_listWood.Length; i++)
            {
                if(!m_listWood[i].activeSelf)
                {
                    nActiveIndex = i;
                    break;
                }
            }
            m_listWood[nActiveIndex].SetActive(true);
            m_listWood[nActiveIndex].GetComponent<CObject>().CreateObject(vecPoz);
        } else if ( nRandom == 4 )
        {
            for(int i = 0; i < m_listPuddie.Length; i++)
            {
                if(!m_listPuddie[i].activeSelf)
                {
                    nActiveIndex = i;
                    break;
                }
            }
            m_listPuddie[nActiveIndex].SetActive(true);
            m_listPuddie[nActiveIndex].GetComponent<CObject>().CreateObject(vecPoz);
        }
    }

    public void HideObject()
    {
        for(int i = 0; i < m_listStar.Length; i++)
            if(m_listStar[i].activeSelf)
                m_listStar[i].GetComponent<CObject>().HideObject();

        for(int i = 0; i < m_listHeart.Length; i++)
            if(m_listHeart[i].activeSelf)
                m_listHeart[i].GetComponent<CObject>().HideObject();

        for(int i = 0; i < m_listBible.Length; i++)
            if(m_listBible[i].activeSelf)
                m_listBible[i].GetComponent<CObject>().HideObject();

        for(int i = 0; i < m_listWood.Length; i++)
            if(m_listWood[i].activeSelf)
                m_listWood[i].GetComponent<CObject>().HideObject();

        for(int i = 0; i < m_listPuddie.Length; i++)
            if(m_listPuddie[i].activeSelf)
                m_listPuddie[i].GetComponent<CObject>().HideObject();
    }
}
