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
    public GameObject m_goPopupGameClear;

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
    public GameObject m_goGetKeyIcon;
    public GameObject m_goBtnKeyword;
    public GameObject m_goBtnNextStage;

    public GameObject m_goPopupStage;
    public Text m_txtPopupStageTitle;
    public Text m_txtPopupStageMsg;

    private int m_nPopupStageType;  // 0 : realy, 1 : next stage


    public Slider m_sliderPlayer;
    public GameObject m_goHandler;

    public GameObject m_goBoardTutoiral;
    public GameObject[] m_listTutorial = new GameObject[3];

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
        HidePopupGameClear();
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

        CGameData.Instance.SetLobbyIndex(1);
        SceneManager.LoadScene("Lobby");
    }

    // Keyword ---------------------------
    public void ShowPopupGetKeyword(int nIndex)
    {
        m_goPopupGetKeyword.SetActive(true);
        m_txtGetKeyIndex.text = CGameData.Instance.GetKeyIndex(nIndex);
        m_txtGetKeyMessage.text = CGameData.Instance.GetKeyMessage(CGameData.Instance.GetStage(), nIndex);
        //m_txtFinishGetKeyCnt.text = CGameEngine.Instance.GetKeyCount().ToString() + " / " + CGameData.Instance.GetStageKeyCount(CGameEngine.Instance.GetStage()).ToString();
        //m_txtFinishPercent.text = ((int)((float)CGameEngine.Instance.GetKeyCount() / (float)CGameData.Instance.GetStageKeyCount(CGameEngine.Instance.GetStage()) * 100)).ToString() + "% 달성";
        // CGameData.instance.GetStage()
    }

    public void HidePopupGetKeyword()
    {
        if( m_goPopupGetKeyword.activeSelf )
        {
            if(CGameData.Instance.GetStage() == 0)
            {
                if( CGameData.Instance.IsTutorialGetStar() && !CGameData.Instance.IsTutorialKey() )
                {
                    CGameData.Instance.SetIsTutorialKey(true);
                    CGameEngine.Instance.PauseGetKeyTutorial();
                    ShowTutorial(2);
                }
            }
        }
        m_goPopupGetKeyword.SetActive(false);
    }

    public void ShowPopupFinish()
    {
        m_goPopupFinish.SetActive(true);
        m_txtFinishGetKeyCnt.text = CGameEngine.Instance.GetKeyCount().ToString() + " / " + CGameData.Instance.GetStageKeyCount(CGameEngine.Instance.GetStage()).ToString();
        m_txtFinishPercent.text = "(" + ((int)((float)CGameEngine.Instance.GetKeyCount() / (float)CGameData.Instance.GetStageKeyCount(CGameEngine.Instance.GetStage()) * 100)).ToString() + "%)";
        m_txtFinishStage.text = "STAGE " + CGameData.Instance.GetStage().ToString() + " 결과";
        //m_txtFinishKeyCnt.text = "성경을 신학적으로 읽어야 하는\n" + CGameData.Instance.GetStageKeyCount(CGameData.Instance.GetStage()).ToString() + "가지 이유를 찾아라!";

        m_txtFinishKeyCnt.text = CGameData.Instance.GetStageMsg(CGameData.Instance.GetStage());

        if( CGameData.Instance.GetStage() == 0 )
        {
            m_goBtnKeyword.SetActive(false);
            m_goGetKeyIcon.transform.localPosition = new Vector3(-140, 0, 0);
        } else
        {
            m_goBtnKeyword.SetActive(true);
            m_goGetKeyIcon.transform.localPosition = new Vector3(-240, 0, 0);

            if (CGameData.Instance.GetStage() == 10)
            {
                m_goBtnNextStage.SetActive(false);
            }
            else
            {
                m_goBtnNextStage.SetActive(true);
            }
        }


        

        HidePopupStage();

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

        m_nPopupStageType = 0;
        ShowPopupStage();

        m_txtPopupStageTitle.text = CGameData.Instance.GetStageTitle(CGameData.Instance.GetStage());
        m_txtPopupStageMsg.text = CGameData.Instance.GetStageMsg(CGameData.Instance.GetStage());


        //SceneManager.LoadScene("InGame");
    }

    public void OnClickNextStage()
    {
        CAudioManager.Instance.PlayButton();

        m_nPopupStageType = 1;

        ShowPopupStage();


        m_txtPopupStageTitle.text = CGameData.Instance.GetStageTitle(CGameData.Instance.GetStage() + 1);
        m_txtPopupStageMsg.text = CGameData.Instance.GetStageMsg(CGameData.Instance.GetStage() + 1);
        //int nNextStage = CGameData.Instance.GetStage() + 1;
        //if( nNextStage > 10 )
        //    nNextStage = 10;
        //CGameData.Instance.SetStage(nNextStage);

        //SceneManager.LoadScene("InGame");
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

    public void ShowPopupGameClear()
    {
        m_goPopupGameClear.SetActive(true);

        StartCoroutine("ProcessGameClear");
        //            CUIInGame.Instance.ShowPopupFinish();
        //            CGameData.Instance.SetState(2);
    }

    IEnumerator ProcessGameClear()
    {
        yield return new WaitForSeconds(1.5f);
        HidePopupGameClear();
        ShowPopupFinish();
        CGameData.Instance.SetState(2);
    }

    public void HidePopupGameClear()
    {
        m_goPopupGameClear.SetActive(false);
    }

    public void ShowPopupStage()
    {
        m_goPopupStage.SetActive(true);
    }

    public void HidePopupStage()
    {
        m_goPopupStage.SetActive(false);
    }

    public void OnClickPlayInPopupStage()
    {
        CAudioManager.Instance.PlayButton();

        if ( m_nPopupStageType == 1 )
        {
            int nNextStage = CGameData.Instance.GetStage() + 1;
            if (nNextStage > 10)
                nNextStage = 10;
            CGameData.Instance.SetStage(nNextStage);
        }
        
        SceneManager.LoadScene("InGame");
    }

    public void OnClickCancelInPopupStage()
    {
        CAudioManager.Instance.PlayButton();

        HidePopupStage();
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
