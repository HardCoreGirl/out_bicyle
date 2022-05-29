using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayer : MonoBehaviour
{
    // public WheelJoint2D wheelRear;
    // public WheelJoint2D wheelBack;

    // public Rigidbody2D rdRear;
    public GameObject m_goUnbeatable;
    
    private Rigidbody2D rd;
    private Animator m_ani;

    private float m_fSpeed = 5f;

    // private int m_nHP = 0;

    // 점프 단계
    private int m_nJumpStep = 0;

    private int m_nState = 0;

    private float m_fRefreshInterval = 0.1f;

    private bool m_bIsUnbeatable = false;

    // Start is called before the first frame update
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        m_ani = GetComponentInChildren<Animator>();

        m_goUnbeatable.SetActive(false);
        // wheelRear.useMotor = true;
        // wheelBack.useMotor = true;
        // SpriteRenderer[] spr = GetComponentsInChildren<SpriteRenderer>();
        // for(int i = 0; i < spr.Length; i++)
        // {
        //     spr[i].color = new Color(1, 1, 1, 0);
        // }
    }

    // Update is called once per frame
    void Update()
    {
        if( m_nState != 1 )
            return;

        transform.position += new Vector3(1, 0, 0) * CGameData.Instance.GetBicyleSpeed() * Time.deltaTime;

        // transform.position += new Vector3(1, 0, 0) * 7f * Time.deltaTime;
        // Rigidbody2D rd = GetComponent<Rigidbody2D>();

        // rd.velocity = Vector2.zero;
        // Vector2 JumpVelocity = new Vector2(5f, 0);

        // rd.AddForce(JumpVelocity, ForceMode2D.Impulse);
    }

    void FixedUpdate()
    {
        // rd.AddForce(Vector2.right, ForceMode2D.Impulse);

        // // Max Speed
        // if(rd.velocity.x > m_fMaxSpeed)
        // {
        //     rd.velocity = new Vector2(m_fMaxSpeed, rd.velocity.y);
        // }
    }

    public void Run()
    {
        m_nState = 1;
    }

    public void GameOver()
    {
        m_nState = 2;
    }

    public void Jump()
    {
        if( m_nJumpStep > 1 )
            return;

        m_ani.Rebind();

        m_ani.Play("Jump003");

        rd.velocity = Vector2.zero;
        Vector2 JumpVelocity = new Vector2(0, CGameData.Instance.GetJumpPower());

        rd.AddForce(JumpVelocity, ForceMode2D.Impulse);

        m_nJumpStep++;
    }

    public void FinishJump()
    {
        Debug.Log("Finish Jump");
        if( m_nJumpStep > 0 )
        {
            m_ani.Play("Run003");
            m_nJumpStep = 0;
        }
    }

    public void Damage()
    {
        m_ani.Rebind();

        m_ani.Play("Damage003");
    }

    public bool IsUnbeatable()
    {
        return m_bIsUnbeatable;
    }

    public void Unbeatable()
    {
        StartCoroutine("ProcessUnbeatable");
    }

    IEnumerator ProcessUnbeatable()
    {
        m_bIsUnbeatable = true;

        m_goUnbeatable.SetActive(true);

        yield return new WaitForSeconds(5f);

        m_goUnbeatable.SetActive(false);

        // float fTime = 0;
        // SpriteRenderer[] spr = GetComponentsInChildren<SpriteRenderer>();

        // float fType = 0f;
        // while(true)
        // {
        //     if( fTime > 5 )
        //         break;

        //     for(int i = 0; i < spr.Length; i++)
        //     {
        //         spr[i].color = new Color(1, 1, 1, fType);
        //     }

        //     if( fType == 0 )
        //         fType = 1;
        //     else
        //         fType = 0;

        //     yield return new WaitForSeconds(m_fRefreshInterval);

        //     fTime += m_fRefreshInterval;
        // }

        // for(int i = 0; i < spr.Length; i++)
        // {
        //     spr[i].color = new Color(1, 1, 1, 1);
        // }

        m_bIsUnbeatable = false;
    }
}
