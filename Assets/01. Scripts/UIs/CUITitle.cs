using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using TMPro;

public class CUITitle : MonoBehaviour
{

    public TMP_InputField[] m_ifRate = new TMP_InputField[5];

    public TMP_InputField m_ifSpeeed;

    

    
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
        for(int i = 0; i < m_ifRate.Length; i++)
        {
            m_ifRate[i].text = CGameData.Instance.GetRate(i).ToString();
        }

        m_ifSpeeed.text = CGameData.Instance.GetBicyleSpeed().ToString();
    }

    public void OnClickStart()
    {
        for(int i = 0; i < m_ifRate.Length; i++)
        {
            CGameData.Instance.SetRate(i, System.Convert.ToInt32(m_ifRate[i].text));
        }

        CGameData.Instance.SetBicyleSpeed((float)System.Convert.ToDouble(m_ifSpeeed.text));
        CGameEngine.Instance.GameStart();
    }
}
