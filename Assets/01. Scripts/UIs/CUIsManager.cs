using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CUIsManager : MonoBehaviour
{
    public GameObject[] m_listUI = new GameObject[2];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HideAllUI()
    {
        for(int i = 0; i < m_listUI.Length; i++)
        {
            m_listUI[i].SetActive(false);
        }
    }

    public void ShowUI(int nIndex)
    {
        HideAllUI();

        m_listUI[nIndex].SetActive(true);

        switch(nIndex)
        {
            case 0:
                m_listUI[nIndex].GetComponent<CUITitle>().InitUI();
                break;
            case 1:
                m_listUI[nIndex].GetComponent<CUIInGame>().InitUI();
                break;
        }
    }

    public void ShowPopupGameOver()
    {
        m_listUI[1].GetComponent<CUIInGame>().ShowPopupGameOver();
    }
}
