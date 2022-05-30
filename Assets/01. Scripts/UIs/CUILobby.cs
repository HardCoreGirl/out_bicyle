using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class CUILobby : MonoBehaviour
{
    public GameObject[] m_goUI = new GameObject[5];

    public GameObject m_goPopupPlay;

    public GameObject m_goPopupSelect;

    private int m_nSelectBicycleIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        CGameData.Instance.InitData();
        
        ShowUI(0);   
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
        }
        else if( nIndex == 4 )
        {
            m_goPopupSelect.SetActive(false);
        }
    }
}
