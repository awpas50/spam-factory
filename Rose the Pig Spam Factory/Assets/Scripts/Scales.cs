using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scales : MonoBehaviour
{
    
    //put this code in the area to detect value of the box
    public TextMeshProUGUI ScaleText;
    public float currentValue;
    private float preValue;

    [Range(0f, 1f)] private float t;
    [HideInInspector] public float realValue;
    private bool trigger = false;

    void Awake()
    {
        realValue = currentValue;
        ScaleText.text = currentValue.ToString();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.GetComponent<Box>())
        {
            return;
        }
        preValue = currentValue;
        t = 0;
        trigger = true;
        if (other.gameObject.GetComponent<Box>().boxTagNumber == 1) // (+)
        {
            currentValue += other.gameObject.GetComponent<Box>().value;
        }
        else if (other.gameObject.GetComponent<Box>().boxTagNumber == 2) // (-)
        {
            currentValue -= other.gameObject.GetComponent<Box>().value;
        }
        else if (other.gameObject.GetComponent<Box>().boxTagNumber == 3) // (*)
        {
            currentValue *= other.gameObject.GetComponent<Box>().value;
        }
        else if (other.gameObject.GetComponent<Box>().boxTagNumber == 4) // (/)
        {
            currentValue /= other.gameObject.GetComponent<Box>().value;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {

        if (!other.gameObject.GetComponent<Box>())
        {
            return;
        }
        preValue = currentValue;
        t = 0;
        trigger = true;
        if (other.gameObject.GetComponent<Box>().boxTagNumber == 1) // (+)
        {
            currentValue -= other.gameObject.GetComponent<Box>().value;
        }
        else if (other.gameObject.GetComponent<Box>().boxTagNumber == 2) // (-)
        {
            currentValue += other.gameObject.GetComponent<Box>().value;
        }
        else if (other.gameObject.GetComponent<Box>().boxTagNumber == 3) // (*)
        {
            currentValue /= other.gameObject.GetComponent<Box>().value;
        }
        else if (other.gameObject.GetComponent<Box>().boxTagNumber == 4) // (/)
        {
            currentValue *= other.gameObject.GetComponent<Box>().value;
        }
    }

    private void Update()
    {
        if (trigger)
        {
            t += Time.deltaTime * 5;
            realValue = Mathf.Lerp(preValue, currentValue, t);
            if (t >= 1)
            {
                trigger = false;
            }
        }
        // not Mathf.Round, but Math.Round. This is the default round up system in C#.
        if (Mathf.Abs(realValue) < 10)
        {
            ScaleText.text = Math.Round(realValue, 3).ToString();
        }
        else if (Mathf.Abs(realValue) >= 10 && Mathf.Abs(realValue) < 100)
        {
            ScaleText.text = Math.Round(realValue, 2).ToString();
        }
        else if (Mathf.Abs(realValue) >= 100)
        {
            ScaleText.text = Math.Round(realValue, 1).ToString();
        }
    }
}
