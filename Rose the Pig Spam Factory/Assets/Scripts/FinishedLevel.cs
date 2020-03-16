using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishedLevel : MonoBehaviour
{
    public Text ValueText;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ValueText.text = "You passed the level";
        }
    }
}