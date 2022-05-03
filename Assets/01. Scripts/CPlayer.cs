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
    private float m_fMaxSpeed = 10f;

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
        transform.position += new Vector3(1, 0, 0) * m_fSpeed * Time.deltaTime;
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

    public void Jump()
    {
        rd.velocity = Vector2.zero;
        Vector2 JumpVelocity = new Vector2(0, 5f);

        rd.AddForce(JumpVelocity, ForceMode2D.Impulse);
    }
}
