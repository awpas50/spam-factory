using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeftScale : MonoBehaviour
{
    //put this code in the area to detect value of the box
    public TextMeshProUGUI leftScaleText;
    public float leftScaleValue;
    public float leftScaleValue_Pre;

    [Range(0f, 1f)] public float t;
    private float a;
    private bool triggered = false;

    void Start()
    {
        a = leftScaleValue;
        leftScaleText.text = leftScaleValue.ToString();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (!other.gameObject.GetComponent<Box>())
        {
            return;
        }
        leftScaleValue_Pre = leftScaleValue;
        t = 0;
        triggered = true;
        if (other.gameObject.GetComponent<Box>().boxTagNumber == 1) // (+)
        {
            leftScaleValue += other.gameObject.GetComponent<Box>().value;
        }
        else if (other.gameObject.GetComponent<Box>().boxTagNumber == 2) // (-)
        {
            leftScaleValue -= other.gameObject.GetComponent<Box>().value;
        }
        else if (other.gameObject.GetComponent<Box>().boxTagNumber == 3) // (*)
        {
            leftScaleValue *= other.gameObject.GetComponent<Box>().value;
        }
        else if (other.gameObject.GetComponent<Box>().boxTagNumber == 4) // (/)
        {
            leftScaleValue /= other.gameObject.GetComponent<Box>().value;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        
        if (!other.gameObject.GetComponent<Box>())
        {
            return;
        }
        leftScaleValue_Pre = leftScaleValue;
        t = 0;
        triggered = true;
        if (other.gameObject.GetComponent<Box>().boxTagNumber == 1) // (+)
        {
            leftScaleValue -= other.gameObject.GetComponent<Box>().value;
        }
        else if (other.gameObject.GetComponent<Box>().boxTagNumber == 2) // (-)
        {
            leftScaleValue += other.gameObject.GetComponent<Box>().value;
        }
        else if (other.gameObject.GetComponent<Box>().boxTagNumber == 3) // (*)
        {
            leftScaleValue /= other.gameObject.GetComponent<Box>().value;
        }
        else if (other.gameObject.GetComponent<Box>().boxTagNumber == 4) // (/)
        {
            leftScaleValue *= other.gameObject.GetComponent<Box>().value;
        }
    }

    private void Update()
    {
        if(triggered)
        {
            t += Time.deltaTime * 5;
            a = Mathf.Lerp(leftScaleValue_Pre, leftScaleValue, t);
            if(t >= 1)
            {
                triggered = false;
            }
        }
        // not Mathf.Round, but Math.Round. This is the default round up system in C#.
        if (Mathf.Abs(a) < 10)
        {
            leftScaleText.text = Math.Round(a, 3).ToString();
        }
        else if (Mathf.Abs(a) >= 10 && Mathf.Abs(a) < 100)
        {
            leftScaleText.text = Math.Round(a, 2).ToString();
        }
        else if (Mathf.Abs(a) >= 100)
        {
            leftScaleText.text = Math.Round(a, 1).ToString();
        }
    }
}
