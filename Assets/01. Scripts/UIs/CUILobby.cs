using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

using UnityEngine.UI;

public class CUILobby : MonoBehaviour
{
    public GameObject[] m_goUI = new GameObject[5];

    public GameObject[] m_listLock = new GameObject[11];
    public GameObject[] m_listClear = new GameObject[11];
    public GameObject[] m_listPerfect = new GameObject[11];
    public GameObject[] m_listKeyIcon = new GameObject[11];
    public Text[] m_txtGetKey = new Text[11];

    public GameObject m_goPopupPlay;
    public Text m_txtStageTitle;
    public Text m_txtStageMsg;

    // Keyword ------------------------------
    public GameObject m_goKeywordMain;
    public GameObject[] m_listKeywordKey = new GameObject[10];
    public GameObject[] m_listKeywordLock = new GameObject[10];
    public Text[] m_listKeywordMainKeyCnt = new Text[10];

    public GameObject m_goKeywordDetail;
    public Text m_txtKeywordDetailTitle;
    public Text m_txtKeywordKeyCnt;
    public GameObject[] m_listKeywordDetailKeyword = new GameObject[7];

    // Storage -------------------------------
    public GameObject m_goPopupSelect;



    private int m_nSelectBicycleIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        CGameData.Instance.InitData();
        
        ShowUI(CGameData.Instance.GetLobbyIndex());   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("InGame");
    }

    public void OnClickStart()
    {
        // StartGame();
        ShowUI(1);
    }

    public void OnClickBackInState()
    {
        ShowUI(0);
    }

    public void OnClickKeywordInStage()
    {
        ShowUI(2);
    }

    public void OnClickShopInStage()
    {
        ShowUI(3);
    }

    public void OnClickStorageBoxInStage()
    {
        ShowUI(4);
    }

    public void OnClickPlayInStage(int nIndex)
    {
        // StartGame();
        CGameData.Instance.SetStage(nIndex);
        m_goPopupPlay.SetActive(true);
        m_txtStageTitle.text = CGameData.Instance.GetStageTitle(nIndex);
        m_txtStageMsg.text = CGameData.Instance.GetStageMsg(nIndex);
    }

    public void OnClickPlayInStagePopup()
    {
        m_goPopupPlay.SetActive(false);
        StartGame();
    }

    public void OnClickCanceInStagePopup()
    {
        m_goPopupPlay.SetActive(false);
    }

    // Keyword -------------------------
    public void OnClickBackInKeyword()
    {
        ShowUI(1);
    }

    public void OnClickDetailInKeyword(int nIndex)
    {
        m_goKeywordMain.SetActive(false);
        m_goKeywordDetail.SetActive(true);

        int nStage = nIndex + 1;
        m_txtKeywordDetailTitle.text = CGameData.Instance.GetStageTitle(nStage);
        m_txtKeywordKeyCnt.text = CGameData.Instance.GetKeyCountInStage(nStage).ToString() + " / " + CGameData.Instance.GetStageKeyCount(nStage).ToString();

        for(int i = 0; i < m_listKeywordDetailKeyword.Length; i++)
        {
            if( i < CGameData.Instance.GetStageKeyCount(nStage) )
            {
                m_listKeywordDetailKeyword[i].SetActive(true);
                if( CGameData.Instance.GetKeyItem(nStage, i ) < 0)
                    m_listKeywordDetailKeyword[i].GetComponentInChildren<Text>().text = "?";
                else
                    m_listKeywordDetailKeyword[i].GetComponentInChildren<Text>().text = (i + 1).ToString() + ". " + CGameData.Instance.GetKeyMessage(nStage, i);
            } else {
                m_listKeywordDetailKeyword[i].SetActive(false);
            }
        }
    }

    public void OnClickBackInKeywordDetail()
    {
        m_goKeywordMain.SetActive(true);
        m_goKeywordDetail.SetActive(false);
    }

    // Storage -------------------------
    public void OnClickBackInStorageBox()
    {
        ShowUI(1);
    }

    public void OnClickSelectBicycleInStorageBox(int nIndex)
    {
        m_nSelectBicycleIndex = nIndex;
        // CGameData.Instance.SetPlayerIndex(nIndex);
        // ShowUI(1);
        m_goPopupSelect.SetActive(true);
    }

    public void OnClickSelectInStorageBox()
    {
        CGameData.Instance.SetPlayerIndex(m_nSelectBicycleIndex);
        m_goPopupSelect.SetActive(false);
        ShowUI(1);
    }

    public void OnClickCancelBicycleInStorageBox()
    {
        m_goPopupSelect.SetActive(false);
    }


    public void HideAllUI()
    {
        for(int i = 0; i < m_goUI.Length; i++)
        {
            m_goUI[i].SetActive(false);
        }
    }

    public void ShowUI(int nIndex)
    {
        HideAllUI();

        m_goUI[nIndex].SetActive(true);

        if( nIndex == 1 )
        {
            m_goPopupPlay.SetActive(false);

            for(int i = 0; i < m_txtGetKey.Length; i++)
            {
                if( CGameData.Instance.GetClearStage(i) >= 0 )
                {
                    m_listLock[i].SetActive(false);
                    if( CGameData.Instance.GetClearStage(i) == 0 )
                    {
                        m_listPerfect[i].SetActive(false);
                        m_listClear[i].SetActive(false);
                    } else if ( CGameData.Instance.GetClearStage(i) == 1 )
                    {
                        if( CGameData.Instance.GetKeyCountInStage(i) == CGameData.Instance.GetStageKeyCount(i) )
                        {
                            m_listPerfect[i].SetActive(true);
                            m_listClear[i].SetActive(false);
                        } else {
                            m_listPerfect[i].SetActive(false);
                            m_listClear[i].SetActive(true);
                        }
                    }
                    m_listKeyIcon[i].SetActive(true);
                    m_txtGetKey[i].text = CGameData.Instance.GetKeyCountInStage(i) + " / " + CGameData.Instance.GetStageKeyCount(i).ToString();
                } else {
                    m_listLock[i].SetActive(true);
                    m_listPerfect[i].SetActive(false);
                    m_listClear[i].SetActive(false);
                    m_listKeyIcon[i].SetActive(false);
                }
            }
        }
        else if( nIndex == 2 )
        {
            m_goKeywordMain.SetActive(true);
            m_goKeywordDetail.SetActive(false);

            for(int i = 0; i < m_listKeywordLock.Length; i++ )
            {
                int nStage = i + 1;
                if( CGameData.Instance.GetClearStage(nStage) >= 0 )
                {
                    m_listKeywordLock[i].SetActive(false);
                    m_listKeywordKey[i].SetActive(true);

                    m_listKeywordMainKeyCnt[i].text = CGameData.Instance.GetKeyCountInStage(nStage).ToString() + " / " + CGameData.Instance.GetStageKeyCount(nStage).ToString();
                } else {
                    m_listKeywordLock[i].SetActive(true);
                    m_listKeywordKey[i].SetActive(false);
                }
            }
        }
        else if( nIndex == 4 )
        {
            m_goPopupSelect.SetActive(false);
        }
    }
}
