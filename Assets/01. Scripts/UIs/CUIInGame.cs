using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using UnityEngine.SceneManagement;

using TMPro;

public class CUIInGame : MonoBehaviour
{
    #region SingleTon
    public static CUIInGame _instance = null;

    public static CUIInGame Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log("CUIInGame install null");

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

    public GameObject m_goPopupGameStart;

    public GameObject m_goPopupGameOver;

    public Text m_txtHP;

    public Text m_txtStarPoint;

    public Text m_txtKeyCount;

    public GameObject m_goPopupPause;

    public GameObject m_goPopupGetKeyword;
    public Text m_txtGetKeyIndex;
    public Text m_txtGetKeyMessage;

    public GameObject m_goPopupFinish;
    public Text m_txtFinishStage;
    public Text m_txtFinishKeyCnt;
    public Text m_txtFinishGetKeyCnt;
    public Text m_txtFinishPercent;


    public Slider m_sliderPlayer;
    public GameObject m_goHandler;

    public GameObject m_goBoardTutoiral;
    public GameObject[] m_listTutorial = new GameObject[2];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitUI()
    {
        HideTutorial();
        HidePopupGameStart();
        m_goPopupGameOver.SetActive(false);
        m_goPopupGetKeyword.SetActive(false);
        HidePopupFinish();
        HidePopupPause();
    }

    public void UpdateHP(int nHP)
    {
        m_txtHP.text = "X " + nHP.ToString();
    }

    public void UpdateKeyCount()
    {
        m_txtKeyCount.text = CGameEngine.Instance.GetKeyCount().ToString() + " / " + CGameData.Instance.GetStageKeyCount(CGameEngine.Instance.GetStage());
    }

    public void UpdateStarPoint()
    {
        m_txtStarPoint.text = CGameData.Instance.GetStar().ToString();
    }

    public void UpdatePlayBar(float fDist)
    {
        m_sliderPlayer.value = fDist;
        m_goHandler.transform.localPosition = new Vector3((525 * fDist) - 250, 8, 0);
    }

    public void ShowPopupGameStart()
    {
        m_goPopupGameStart.SetActive(true);
    }

    public void HidePopupGameStart()
    {
        m_goPopupGameStart.SetActive(false);
    }

    // Pause -----------------------------
    public void ShowPopupPause()
    {
        m_goPopupPause.SetActive(true);
        CGameEngine.Instance.Pause();
    }

    public void HidePopupPause()
    {
        m_goPopupPause.SetActive(false);
        // Time.timeScale = 1;
        CGameEngine.Instance.Restart();
    }

    public void OnClickPause()
    {
        CAudioManager.Instance.PlayButton();

        ShowPopupPause();
    }

    public void OnClickReplayGameInPause()
    {
        CAudioManager.Instance.PlayButton();
        HidePopupPause();
    }

    public void OnClickStartInPause()
    {
        CAudioManager.Instance.PlayButton();
        HidePopupPause();

        CGameData.Instance.SetState(2);
        CGameEngine.Instance.GetPlayer().GetComponent<CPlayer>().GameOver();

        SceneManager.LoadScene("InGame");
    }

    public void OnClickHomeInPause()
    {
        CAudioManager.Instance.PlayButton();

        HidePopupPause();

        CGameData.Instance.SetState(2);
        CGameEngine.Instance.GetPlayer().GetComponent<CPlayer>().GameOver();

        SceneManager.LoadScene("Lobby");
    }

    // Keyword ---------------------------
    public void ShowPopupGetKeyword(int nIndex)
    {
        m_goPopupGetKeyword.SetActive(true);
        m_txtGetKeyIndex.text = CGameData.Instance.GetKeyIndex(nIndex);
        m_txtGetKeyMessage.text = CGameData.Instance.GetKeyMessage(CGameData.Instance.GetStage(), nIndex);
        m_txtFinishGetKeyCnt.text = CGameEngine.Instance.GetKeyCount().ToString() + " / " + CGameData.Instance.GetStageKeyCount(CGameEngine.Instance.GetStage()).ToString();
        m_txtFinishPercent.text = ((int)((float)CGameEngine.Instance.GetKeyCount() / (float)CGameData.Instance.GetStageKeyCount(CGameEngine.Instance.GetStage()) * 100)).ToString() + "% 달성";
        // CGameData.instance.GetStage()
    }

    public void HidePopupGetKeyword()
    {
        m_goPopupGetKeyword.SetActive(false);
    }

    public void ShowPopupFinish()
    {
        m_goPopupFinish.SetActive(true);
        m_txtFinishStage.text = "STAGE " + CGameData.Instance.GetStage().ToString() + " 결과";
        m_txtFinishKeyCnt.text = "[Mission] 성경을 신학적으로 읽어야 하는 " + CGameData.Instance.GetStageKeyCount(CGameData.Instance.GetStage()).ToString() + "가지 이유를 찾아라!"; 

    }

    public void HidePopupFinish()
    {
        m_goPopupFinish.SetActive(false);
    }

    public void OnClickJump()
    {
        if( CGameData.Instance.GetStage() == 0 )
        {
            if( !CGameData.Instance.IsTutorialJump() )
                return;
        }
        CGameEngine.Instance.GetPlayer().GetComponent<CPlayer>().Jump();
    }

    public void OnClickRestart()
    {
        CAudioManager.Instance.PlayButton();
        SceneManager.LoadScene("InGame");
    }

    public void OnClickNextStage()
    {
        CAudioManager.Instance.PlayButton();
        int nNextStage = CGameData.Instance.GetStage() + 1;
        if( nNextStage > 10 )
            nNextStage = 10;
        CGameData.Instance.SetStage(nNextStage);

        SceneManager.LoadScene("InGame");
    }

    public void OnClickFinishGotoLobby(int nIndex)
    {
        CAudioManager.Instance.PlayButton();
        CGameData.Instance.SetLobbyIndex(nIndex);
        SceneManager.LoadScene("Lobby");
    }

    public void ShowPopupGameOver()
    {
        m_goPopupGameOver.SetActive(true);
    }

    // Tutorial -----------------------
    public void ShowTutorial(int nIndex)
    {
        m_goBoardTutoiral.SetActive(true);

        for(int i = 0; i < m_listTutorial.Length; i++)
        {
            m_listTutorial[i].SetActive(false);
        }

        m_listTutorial[nIndex].SetActive(true);
    }

    public void HideTutorial()
    {
        m_goBoardTutoiral.SetActive(false);
    }

    public void OnClickJumpInTutorial()
    {
        Debug.Log("OnClickJumpInTutorial");
        CGameEngine.Instance.Restart();
        // HideTutorial();
        OnClickJump();
    }
}
