using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using UnityEngine.SceneManagement;

using TMPro;

public class CUITitle : MonoBehaviour
{

    public TMP_InputField m_ifSpeeed;

    public TMP_Text[] m_txtKey = new TMP_Text[11];
    public TMP_InputField m_ifStage;

    
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
        // for(int i = 0; i < m_ifRate.Length; i++)
        // {
        //     m_ifRate[i].text = CGameData.Instance.GetRate(i).ToString();
        // }

        m_ifSpeeed.text = CGameData.Instance.GetBicyleSpeed().ToString();

        m_ifStage.text = "1";

        for(int i = 0; i < 11; i++)
        {
            int nCount = CGameData.Instance.GetStageKeyCount(i);
            string strKeyCount = "";
            for(int j = 0; j < nCount; j++)
            {
                if( CGameData.Instance.GetKeyItem(i, j) == -1 )
                    strKeyCount += "X ";
                else
                    strKeyCount += "O ";
            }
            m_txtKey[i].text = strKeyCount;
        }
    }

    public void OnClickStart()
    {
        // for(int i = 0; i < m_ifRate.Length; i++)
        // {
        //     CGameData.Instance.SetRate(i, System.Convert.ToInt32(m_ifRate[i].text));
        // }

        CGameEngine.Instance.SetStage(System.Convert.ToInt32 (m_ifStage.text));
        // CGameData.Instance.SetBicyleSpeed((float)System.Convert.ToDouble(m_ifSpeeed.text));
        CGameData.Instance.SetBicyleSpeed(CGameData.Instance.GetStageSpeed(System.Convert.ToInt32 (m_ifStage.text)));
        CGameEngine.Instance.GameStart();
    }

    public void OnClickClearKey()
    {
        for(int i = 0; i < 11; i++)
        {
            for(int j = 0; j < 7; j++)
            {
                CGameData.Instance.SetKeyItem(i, j, -1);
            }
        }       
        SceneManager.LoadScene("InGame");
    }
}
