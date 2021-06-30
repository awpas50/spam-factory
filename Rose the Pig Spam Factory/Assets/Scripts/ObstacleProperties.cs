using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleProperties : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D colExt)
    {
        if (colExt.gameObject.layer == 10 || colExt.gameObject.layer == 11 || colExt.gameObject.layer == 12)
        {
            colExt.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }
    }
    void OnCollisionStay2D(Collision2D colExt)
    {
        if (colExt.gameObject.layer == 10 || colExt.gameObject.layer == 11 || colExt.gameObject.layer == 12)
        {
            colExt.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }
    }
    void OnCollisionExit2D(Collision2D colExt)
    {
        if (colExt.gameObject.layer == 10 || colExt.gameObject.layer == 11 || colExt.gameObject.layer == 12)
        {
            colExt.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }
    }
}
