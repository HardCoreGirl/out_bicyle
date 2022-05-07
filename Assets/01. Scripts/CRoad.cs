using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("Road Trigger : " + other.tag);
        if(other.tag.Equals("Player"))
        {
            other.GetComponent<CPlayer>().FinishJump();
        }
    }
}
