using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBalance : MonoBehaviour
{
    public LeftScale leftScale;
    public RightScale rightScale;
    public GameObject gate;

    private void Update()
    {
        if (leftScale.leftScaleValue == rightScale.rightScaleValue)
        {
            gate.GetComponent<Renderer>().enabled = false;
            gate.SetActive(false);
        }
        else
        {
            gate.GetComponent<Renderer>().enabled = true;
            gate.SetActive(true);
        }
    }
}
