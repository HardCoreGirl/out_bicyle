using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayer : MonoBehaviour
{
    // public WheelJoint2D wheelRear;
    // public WheelJoint2D wheelBack;

    // public Rigidbody2D rdRear;
    
    private Rigidbody2D rd;
    private float m_fSpeed = 5f;

    // private int m_nHP = 0;

    // 점프 단계
    private int m_nJumpStep = 0;

    private int m_nState = 0;

    // Start is called before the first frame update
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        // wheelRear.useMotor = true;
        // wheelBack.useMotor = true;
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

        GetComponent<Animator>().Play("Jump");

        rd.velocity = Vector2.zero;
        Vector2 JumpVelocity = new Vector2(0, CGameData.Instance.GetJumpPower());

        rd.AddForce(JumpVelocity, ForceMode2D.Impulse);

        m_nJumpStep++;
    }

    public void FinishJump()
    {
        if( m_nJumpStep > 0 )
        {
            // Debug.Log("FinishJump");
            GetComponent<Animator>().Play("Run");
            m_nJumpStep = 0;
        }
    }
}
