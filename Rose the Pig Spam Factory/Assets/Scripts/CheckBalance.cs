using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckBalance : MonoBehaviour
{
    public List<Scales> scales_list;
    public List<float> scales_val;
    public GameObject gate;

    private void Update()
    {
        for (int i = 0; i < scales_list.Count; i++)
        {
            scales_val[i] = scales_list[i].realValue;
        }
        bool equal = CheckScale();
        Debug.Log(equal);
        if (equal)
        {
            //oepn gate
            gate.GetComponent<Renderer>().enabled = false;
            gate.SetActive(false);
        }
        else
        {
            //close gate
            gate.GetComponent<Renderer>().enabled = true;
            gate.SetActive(true);
        }
    }

    public bool CheckScale()
    {
        for (int i = 0; i < scales_list.Count - 1; i++)
        {
            if (scales_val[i] != scales_val[i + 1])
            {
                return false;
            }
        }
        return true;
    }
}
