using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGameEngine : MonoBehaviour
{
    public GameObject m_goPlayer;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = m_goPlayer.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(Vector2.right);
    }
}
