using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hatch : MonoBehaviour
{
    public Animator animator;
    public bool debugTest = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!debugTest)
        {
            CloseGate();
        }
        if (debugTest)
        {
            OpenGate();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<Box>() || other.gameObject.tag == "Player")
        {
            Destroy(other.gameObject);
        }
    }

    void OpenGate()
    {
        animator.SetBool("GateTrigger", true);
        gameObject.GetComponent<Collider2D>().enabled = true;
    }

    void CloseGate()
    {
        animator.SetBool("GateTrigger", false);
        gameObject.GetComponent<Collider2D>().enabled = false;
    }
}
