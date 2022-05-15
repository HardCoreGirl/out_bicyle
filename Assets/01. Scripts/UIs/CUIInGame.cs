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

    public GameObject m_goPopupGameOver;

    public Text m_txtHP;

    public Text m_txtStarPoint;

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
        m_goPopupGameOver.SetActive(false);
    }

    public void UpdateHP(int nHP)
    {
        m_txtHP.text = "X " + nHP.ToString();
    }

    public void UpdateStarPoint(int nPoint)
    {
        m_txtStarPoint.text = nPoint.ToString();
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
