using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CObjectManager : MonoBehaviour
{
    #region SingleTon
    public static CObjectManager _instance = null;

    public static CObjectManager Instance
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

    public GameObject m_goStarParent;
    public GameObject[] m_listStar = new GameObject[20];

    public GameObject m_goHeartParent;
    public GameObject[] m_listHeart = new GameObject[20];

    public GameObject m_goBibleParent;
    public GameObject[] m_listBible = new GameObject[20];

    public GameObject m_goKeyParent;
    public GameObject[] m_listKey = new GameObject[20];

    public GameObject m_goWoodParent;
    public GameObject[] m_listWood = new GameObject[20];

    public GameObject m_goPuddieParent;
    public GameObject[] m_listPuddie = new GameObject[20];

    private int m_nStarCombo = 0;
    private int m_nStarHeight = 0;

    private int[] m_listKeyPoz = new int[7];


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

        for(int i = 0; i < m_listKey.Length; i++)
        {
            m_listKey[i] = Instantiate(Resources.Load("Prefabs/Objects/Key") as GameObject);
            m_listKey[i].transform.SetParent(m_goBibleParent.transform);
            m_listKey[i].SetActive(false);
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

    public void InitKeyItem(int nStage)
    {
        for(int i = 0; i < m_listKeyPoz.Length; i++ )
            m_listKeyPoz[i] = -1;

        int nKeyCount = 0;
        int nDist = 0;

        nKeyCount = CGameData.Instance.GetStageKeyCount(nStage);
        nDist = CGameData.Instance.GetDistance(CGameData.Instance.GetStageSpeed(nStage));
        
        // switch(nStage)
        // {
        //     case 0: nKeyCount = 2; nDist = 360; break;
        //     case 1: nKeyCount = 6; nDist = 360; break;
        //     case 2: nKeyCount = 5; nDist = 360; break;
        //     case 3: nKeyCount = 5; nDist = 360; break;
        //     case 4: nKeyCount = 6; nDist = 425; break;
        //     case 5: nKeyCount = 3; nDist = 425; break;
        //     case 6: nKeyCount = 5; nDist = 425; break;
        //     case 7: nKeyCount = 4; nDist = 485; break;
        //     case 8: nKeyCount = 4; nDist = 485; break;
        //     case 9: nKeyCount = 4; nDist = 485; break;
        //     case 10:    nKeyCount = 7;  nDist = 600; break;
        // }

        int nIndex = 0;
        while(true)
        {
            int nRandomValue = Random.Range(0, nDist);

            bool bIsExist = false;
            for(int i = 0; i < nIndex; i++ )
            {
                if(m_listKeyPoz[i] == nRandomValue)
                    bIsExist = true;

            }

            if(bIsExist)
                continue;

            m_listKeyPoz[nIndex] = nRandomValue;

            nIndex++;

            if( nIndex >= nKeyCount )
                break;
        }

        for(int i = 0; i < m_listKeyPoz.Length; i++)
        {
            Debug.Log(m_listKeyPoz[i]);
        }
    }

    public bool IsKeyItemPoz(int nPoz, bool bIsCheckStar = false) // 별인지 체크
    {
        for(int i = 0; i < m_listKeyPoz.Length; i++)
        {
            if( bIsCheckStar )
            {
                if( m_listKeyPoz[i] -5 <= nPoz || m_listKeyPoz[i] <= nPoz )
                {
                    return true;
                }
            } else {
                if( m_listKeyPoz[i] == nPoz )   
                    return true;
            }
        }
        return false;
    }

    public int GetKeyIndex(int nPoz)
    {
        for(int i = 0; i < m_listKeyPoz.Length; i++)
        {
            if( m_listKeyPoz[i] == nPoz )   
                return i;
        }

        return -1;
    }


    public void CreateObject(Vector3 vecPoz)
    {
        Vector3 vecCreatePoz = vecPoz;

        int nHeight = 0;
        int nActiveIndex = 0;        

        int nNowPoz = (int)vecPoz.x;

        if( IsKeyItemPoz(nNowPoz) )
        {
            int nKeyIndex = GetKeyIndex(nNowPoz);
            nHeight = Random.Range(0, 3);
            vecCreatePoz.y = 0.7f + ((float)nHeight * 1.2f);

            for(int i = 0; i < m_listKey.Length; i++)
            {
                if(!m_listKey[i].activeSelf)
                {
                    nActiveIndex = i;
                    break;
                }
            }
            m_listKey[nActiveIndex].SetActive(true);
            m_listKey[nActiveIndex].GetComponent<CObject>().CreateObject(vecCreatePoz);
            m_listKey[nActiveIndex].GetComponent<CObject>().SetValue(nKeyIndex);
            return;
        }

        int nRandomValue = Random.Range(0, CGameData.Instance.GetMaxRate());

        int nObjectIndex = -1;
        int nTotalRate = 0;
        
        if( m_nStarCombo > 0 )
        {
            nObjectIndex = 0;
            nHeight = m_nStarHeight;
        } else {
            for(int i = 0; i < CGameData.Instance.GetRateCnt(); i++)
            {
                nTotalRate += CGameData.Instance.GetRate(i);
                if( nRandomValue < nTotalRate )
                {
                    nObjectIndex = i;
                    break;
                }
            }

            if( nObjectIndex < 0 )
                return;
            
            if( nObjectIndex < 2)
                nHeight = Random.Range(0, 3);
        }

        vecCreatePoz.y = 0.7f + ((float)nHeight * 1.2f);

        
        if( nObjectIndex == 0 )
        {
            if( m_nStarCombo <= 0 )
            {
                m_nStarCombo = Random.Range(3, 5);
                m_nStarHeight = nHeight;
            }

            for(int i = 0; i < m_listStar.Length; i++)
            {
                if(!m_listStar[i].activeSelf)
                {
                    nActiveIndex = i;
                    break;
                }
            }
            m_nStarCombo--;
            m_listStar[nActiveIndex].SetActive(true);
            // vecCreatePoz.y = 1 + ((float)m_nStarHeight * 2);
            m_listStar[nActiveIndex].GetComponent<CObject>().CreateObject(vecCreatePoz);
        } else if ( nObjectIndex == 1 )
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
            m_listHeart[nActiveIndex].GetComponent<CObject>().CreateObject(vecCreatePoz);
        } else if ( nObjectIndex == 2 )
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
            m_listBible[nActiveIndex].GetComponent<CObject>().CreateObject(vecCreatePoz);
        // } else if ( nObjectIndex == 3 )
        // {
        //     for(int i = 0; i < m_listKey.Length; i++)
        //     {
        //         if(!m_listKey[i].activeSelf)
        //         {
        //             nActiveIndex = i;
        //             break;
        //         }
        //     }
        //     m_listKey[nActiveIndex].SetActive(true);
        //     m_listKey[nActiveIndex].GetComponent<CObject>().CreateObject(vecCreatePoz);
        } else if ( nObjectIndex == 3 )
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
            vecCreatePoz.y = 0.5f;                        
            m_listWood[nActiveIndex].GetComponent<CObject>().CreateObject(vecCreatePoz);
        } else if ( nObjectIndex == 4 )
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
            vecCreatePoz.y = 0f;                        
            m_listPuddie[nActiveIndex].GetComponent<CObject>().CreateObject(vecCreatePoz);
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
        
        for(int i = 0; i < m_listKey.Length; i++)
            if(m_listKey[i].activeSelf)
                m_listKey[i].GetComponent<CObject>().HideObject();

        for(int i = 0; i < m_listWood.Length; i++)
            if(m_listWood[i].activeSelf)
                m_listWood[i].GetComponent<CObject>().HideObject();

        for(int i = 0; i < m_listPuddie.Length; i++)
            if(m_listPuddie[i].activeSelf)
                m_listPuddie[i].GetComponent<CObject>().HideObject();
    }
}

