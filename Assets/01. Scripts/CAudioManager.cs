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
        m_asGetKey.Play();
    }

    public void PlayGetStar()
    {
        m_asGetStar.Play();
    }

    public void PlayGetShield()
    {
        m_asGetShield.Play();
    }

    public void PlayGetHeart()
    {
        m_asGetHeart.Play();
    }

    public void PlayWood()
    {
        m_asWood.Play();
    }

    public void PlayWater()
    {
        m_asWater.Play();
    }

    public void PlayJump()
    {
        m_asJump.Play();
    }

    public void PlayButton()
    {
        m_asButton.Play();
    }

    public void PlayBGLobby()
    {
        m_asBGLobby.Play();
    }

    public void PlayBGInGame()
    {
        m_asBGInGame.Play();
    }
}
