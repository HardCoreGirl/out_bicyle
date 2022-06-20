using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAudioManager : MonoBehaviour
{
    #region SingleTon
    public static CAudioManager _instance = null;

    public static CAudioManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log("CAudioManager install null");

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

    public AudioSource m_asGetKey;   
    public AudioSource m_asGetStar;
    public AudioSource m_asGetShield;
    public AudioSource m_asGetHeart;
    public AudioSource m_asWood;
    public AudioSource m_asWater;

    public AudioSource m_asJump;

    public AudioSource m_asButton;

    public AudioSource m_asBGLobby;
    public AudioSource m_asBGInGame;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGetKey()
    {
        if (!CGameData.Instance.IsSound())
            return;

        m_asGetKey.Play();
    }

    public void PlayGetStar()
    {
        if (!CGameData.Instance.IsSound())
            return;

        m_asGetStar.Play();
    }

    public void PlayGetShield()
    {
        if (!CGameData.Instance.IsSound())
            return;

        m_asGetShield.Play();
    }

    public void PlayGetHeart()
    {
        if (!CGameData.Instance.IsSound())
            return;

        m_asGetHeart.Play();
    }

    public void PlayWood()
    {
        if (!CGameData.Instance.IsSound())
            return;

        m_asWood.Play();
    }

    public void PlayWater()
    {
        if (!CGameData.Instance.IsSound())
            return;

        m_asWater.Play();
    }

    public void PlayJump()
    {
        if (!CGameData.Instance.IsSound())
            return;

        m_asJump.Play();
    }

    public void PlayButton()
    {
        if (!CGameData.Instance.IsSound())
            return;

        m_asButton.Play();
    }

    public void PlayBGLobby()
    {
        if (!CGameData.Instance.IsSound())
            return;

        m_asBGLobby.Play();
    }

    public void StopBGLobby()
    {
        m_asBGLobby.Stop();
    }

    public void PlayBGInGame()
    {
        if (!CGameData.Instance.IsSound())
            return;

        m_asBGInGame.Play();
    }
}
