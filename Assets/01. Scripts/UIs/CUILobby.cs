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

    public GameObject[] m_listSound = new GameObject[2];

    // Keyword ------------------------------
    public GameObject m_goKeywordMain;
    public GameObject[] m_listKeywordKey = new GameObject[10];
    public GameObject[] m_listKeywordLock = new GameObject[10];
    public Text[] m_listKeywordMainKeyCnt = new Text[10];

    public GameObject m_goKeywordDetail;
    public Text m_txtKeywordDetailTitle;
    public Text m_txtKeywordKeyCnt;
    public GameObject[] m_listKeywordDetailKeyword = new GameObject[7];

    // Shop ---------------------------------
    public Text m_txtStarInShop;
    public GameObject m_goPopupBuy;
    public GameObject[] m_goPrice = new GameObject[2];
    private int m_nShopSelectIndex;
    public GameObject m_goPopupConfirm;
    public Text m_txtConfirm;

    // Storage -------------------------------
    public Text m_txtStarInStorage;
    public GameObject m_goPopupSelect;
    public GameObject[] m_goLock = new GameObject[2];



    private int m_nSelectBicycleIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        CGameData.Instance.InitData();

        ShowUI(CGameData.Instance.GetLobbyIndex());   

        CAudioManager.Instance.PlayBGLobby();
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
        CAudioManager.Instance.PlayButton();
        // StartGame();
        ShowUI(1);
    }

    public void OnClickBackInState()
    {
        CAudioManager.Instance.PlayButton();
        ShowUI(0);
    }

    public void OnClickKeywordInStage()
    {
        CAudioManager.Instance.PlayButton();
        ShowUI(2);
    }

    public void OnClickShopInStage()
    {
        CAudioManager.Instance.PlayButton();
        ShowUI(3);
    }

    public void OnClickStorageBoxInStage()
    {
        CAudioManager.Instance.PlayButton();
        ShowUI(4);
    }

    public void OnClickPlayInStage(int nIndex)
    {
        if( CGameData.Instance.GetClearStage(nIndex) < 0 )
            return;

        CAudioManager.Instance.PlayButton();
        // StartGame();
        CGameData.Instance.SetStage(nIndex);
        m_goPopupPlay.SetActive(true);
        m_txtStageTitle.text = CGameData.Instance.GetStageTitle(nIndex);
        m_txtStageMsg.text = CGameData.Instance.GetStageMsg(nIndex);
    }

    public void OnClickPlayInStagePopup()
    {
        CAudioManager.Instance.PlayButton();
        m_goPopupPlay.SetActive(false);
        StartGame();
    }

    public void OnClickCanceInStagePopup()
    {
        CAudioManager.Instance.PlayButton();
        m_goPopupPlay.SetActive(false);
    }

    // Keyword -------------------------
    public void OnClickBackInKeyword()
    {
        CAudioManager.Instance.PlayButton();
        ShowUI(1);
    }

    public void OnClickDetailInKeyword(int nIndex)
    {
        if( CGameData.Instance.GetClearStage(nIndex + 1) < 0 )
            return;

        CAudioManager.Instance.PlayButton();

        m_goKeywordMain.SetActive(false);
        m_goKeywordDetail.SetActive(true);

        int nStage = nIndex + 1;
        //m_txtKeywordDetailTitle.text = CGameData.Instance.GetStageTitle(nStage);
        m_txtKeywordDetailTitle.text = CGameData.Instance.GetStageKeywordTitle(nStage);
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
        CAudioManager.Instance.PlayButton();
        m_goKeywordMain.SetActive(true);
        m_goKeywordDetail.SetActive(false);
    }

    // Shop ----------------------------
    public void UpdateShop()
    {
        m_txtStarInShop.text = CGameData.Instance.GetStar().ToString();

        if( CGameData.Instance.GetBicycle(1) == 0 )
        {
            m_goPrice[0].SetActive(true);
        } else {
            m_goPrice[0].SetActive(false);
        }

        if( CGameData.Instance.GetBicycle(2) == 0 )
        {
            m_goPrice[1].SetActive(true);
        } else {
            m_goPrice[1].SetActive(false);
        }
    }

    public void OnClickBackInShop()
    {
        CAudioManager.Instance.PlayButton();
        ShowUI(1);
    }

    public void OnClickBuyInShop(int nIndex)
    {
        CAudioManager.Instance.PlayButton();

        if (CGameData.Instance.GetBicycle(nIndex) > 0)
            return;

        int nPrice = 0;
        if (nIndex == 1)
            nPrice = 7000;
        else if (nIndex == 2)
            nPrice = 20000;

        if (CGameData.Instance.GetStar() < nPrice)
        {
            ShowPopupConfirm(1);
            return;
        }

        m_nShopSelectIndex = nIndex;

        m_goPopupBuy.SetActive(true);
    }

    public void OnClickBuyOKInShop()
    {
        int nPrice = 0;

        if (m_nShopSelectIndex == 1)
            nPrice = 7000;
        else if (m_nShopSelectIndex == 2)
            nPrice = 20000;

        Debug.Log("MyStar : " + CGameData.Instance.GetStar());

        if (CGameData.Instance.UseStar(nPrice) < 0)
        {
            Debug.Log("MyStar : " + CGameData.Instance.GetStar());
            ShowPopupConfirm(1);
            return;
        }

        CAudioManager.Instance.PlayButton();

        CGameData.Instance.SetBicycle(m_nShopSelectIndex, 1);
        UpdateShop();
        m_goPopupBuy.SetActive(false);

        ShowPopupConfirm(0);
    }

    public void OnClickCancleInShop()
    {
        CAudioManager.Instance.PlayButton();
        m_goPopupBuy.SetActive(false);
    }

    public void ShowPopupConfirm(int nIndex)
    {
        m_goPopupConfirm.SetActive(true);

        string strMsg = "구매가 완료되었습니다.";

        if( nIndex == 1 )
            strMsg = "별이 부족합니다.";

        m_txtConfirm.text = strMsg;
    }

    public void HidePopupConfirm()
    {
        m_goPopupConfirm.SetActive(false);
    }

    public void OnCliekOkInConfirm()
    {
        CAudioManager.Instance.PlayButton();
        HidePopupConfirm();
    }

    // Storage -------------------------
    public void OnClickBackInStorageBox()
    {
        CAudioManager.Instance.PlayButton();
        ShowUI(1);
    }

    public void OnClickSelectBicycleInStorageBox(int nIndex)
    {
        if( CGameData.Instance.GetBicycle(nIndex) <= 0 )
            return;

        CAudioManager.Instance.PlayButton();

        m_nSelectBicycleIndex = nIndex;
        // CGameData.Instance.SetPlayerIndex(nIndex);
        // ShowUI(1);
        m_goPopupSelect.SetActive(true);
    }

    public void OnClickSelectInStorageBox()
    {
        CAudioManager.Instance.PlayButton();

        CGameData.Instance.SetPlayerIndex(m_nSelectBicycleIndex);
        m_goPopupSelect.SetActive(false);
        ShowUI(1);
    }

    public void OnClickCancelBicycleInStorageBox()
    {
        CAudioManager.Instance.PlayButton();
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

        if( nIndex == 0 )
        {
            if (CGameData.Instance.IsSound())
            {
                m_listSound[0].SetActive(true);
                m_listSound[1].SetActive(false);
            } else
            {
                m_listSound[0].SetActive(false);
                m_listSound[1].SetActive(true);
            }
        }
        else if( nIndex == 1 )
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
        else if( nIndex == 3 )
        {
            m_goPopupBuy.SetActive(false);
            m_goPopupConfirm.SetActive(false);

            UpdateShop();
            // m_txtKeywordInShop.text = CGameData.Instance.GetKeyCountInStage(nStage).ToString() + " / " + CGameData.Instance.GetStageKeyCount(nStage).ToString();
            // m_txtKeywordInShop.text = CGameData.Instance.GetStar

        }
        else if( nIndex == 4 )
        {
            m_goPopupSelect.SetActive(false);

            m_txtStarInStorage.text = CGameData.Instance.GetStar().ToString();

            if( CGameData.Instance.GetBicycle(1) == 0 )
            {
                m_goLock[0].SetActive(true);
            } else {
                m_goLock[0].SetActive(false);
            }

            if( CGameData.Instance.GetBicycle(2) == 0 )
            {
                m_goLock[1].SetActive(true);
            } else {
                m_goLock[1].SetActive(false);
            }
        }
    }

    public void OnClickSound(int nIndex)
    {
        if( nIndex == 0)
        {
            CAudioManager.Instance.PlayButton();
            CAudioManager.Instance.StopBGLobby();
            CGameData.Instance.SetSound(false);
            m_listSound[0].SetActive(false);
            m_listSound[1].SetActive(true);
        } else
        {
            CGameData.Instance.SetSound(true);
            CAudioManager.Instance.PlayBGLobby();
            m_listSound[0].SetActive(true);
            m_listSound[1].SetActive(false);
        }
    }

    public void OnClickClearData()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Lobby");
    }
}
