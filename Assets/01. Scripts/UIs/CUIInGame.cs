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

    public GameObject m_goPopupGetKeyword;
    public Text m_txtGetKeyIndex;
    public Text m_txtGetKeyMessage;

    public GameObject m_goPopupFinish;

    public Slider m_sliderPlayer;

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
        HidePopupGameStart();
        m_goPopupGameOver.SetActive(false);
        m_goPopupGetKeyword.SetActive(false);
        HidePopupFinish();
    }

    public void UpdateHP(int nHP)
    {
        m_txtHP.text = "X " + nHP.ToString();
    }

    public void UpdateKeyCount()
    {
        m_txtKeyCount.text = CGameEngine.Instance.GetKeyCount().ToString() + " / " + CGameData.Instance.GetStageKeyCount(CGameEngine.Instance.GetStage());
    }

    public void UpdateStarPoint(int nPoint)
    {
        m_txtStarPoint.text = nPoint.ToString();
    }

    public void UpdatePlayBar(float fDist)
    {
        m_sliderPlayer.value = fDist;
    }

    public void ShowPopupGameStart()
    {
        m_goPopupGameStart.SetActive(true);
    }

    public void HidePopupGameStart()
    {
        m_goPopupGameStart.SetActive(false);
    }

    public void ShowPopupGetKeyword(int nIndex)
    {
        m_goPopupGetKeyword.SetActive(true);
        m_txtGetKeyIndex.text = CGameData.Instance.GetKeyIndex(nIndex);
        m_txtGetKeyMessage.text = CGameData.Instance.GetKeyMessage(CGameData.Instance.GetStage(), nIndex);
        // CGameData.instance.GetStage()
    }

    public void HidePopupGetKeyword()
    {
        m_goPopupGetKeyword.SetActive(false);
    }

    public void ShowPopupFinish()
    {
        m_goPopupFinish.SetActive(true);
    }

    public void HidePopupFinish()
    {
        m_goPopupFinish.SetActive(false);
    }

    public void OnClickJump()
    {
        CGameEngine.Instance.GetPlayer().GetComponent<CPlayer>().Jump();
    }

    public void OnClickRestart()
    {
        SceneManager.LoadScene("InGame");
    }

    public void ShowPopupGameOver()
    {
        m_goPopupGameOver.SetActive(true);
    }
}
