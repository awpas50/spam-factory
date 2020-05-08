using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hatch : MonoBehaviour
{
    public Animator animator;
    public bool debugTest = false;

    public float freezeTime;
    public float hatchOpenTime;
    public float hatchCloseTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(HatchInteraction());
    }

    // Update is called once per frame
    void Update()
    {
        //if (!debugTest)
        //{
        //    CloseGate();
        //}
        //if (debugTest)
        //{
        //    OpenGate();
        //}
    }

    IEnumerator HatchInteraction()
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(freezeTime);
        while (true)
        {
            OpenHatch();
            yield return new WaitForSeconds(hatchOpenTime);
            CloseHatch();
            yield return new WaitForSeconds(hatchCloseTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<Box>() || other.gameObject.tag == "Player")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    void OpenHatch()
    {
        animator.SetBool("GateTrigger", true);
        gameObject.GetComponent<Collider2D>().enabled = true;
    }

    void CloseHatch()
    {
        animator.SetBool("GateTrigger", false);
        gameObject.GetComponent<Collider2D>().enabled = false;
    }
}
