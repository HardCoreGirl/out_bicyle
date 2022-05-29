using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class CUILobby : MonoBehaviour
{
    public GameObject[] m_goUI = new GameObject[5];

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

    public void OnClickPlayerInStage(int nIndex)
    {
        StartGame();
    }

    public void OnClickSelectBicycleInStorageBox(int nIndex)
    {
        CGameData.Instance.SetPlayerIndex(nIndex);
        ShowUI(1);
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
    }
}
