using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RightScale : MonoBehaviour
{
    //put this code in the area to detect value of the box
    public Text rightScaleText;
    public float rightScaleValue;

    // Start is called before the first frame update
    void Start()
    {
        rightScaleText.text = "Value: " + rightScaleValue;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.GetComponent<Box>())
        {
            //debug, and I don't know why this bug happens
            return;
        }
        else if (other.gameObject.GetComponent<Box>().boxTagNumber == 1) // (+)
        {
            rightScaleValue += other.gameObject.GetComponent<Box>().value;
            rightScaleText.text = "Value: " + rightScaleValue;
        }
        else if (other.gameObject.GetComponent<Box>().boxTagNumber == 2) // (-)
        {
            rightScaleValue -= other.gameObject.GetComponent<Box>().value;
            rightScaleText.text = "Value: " + rightScaleValue;
        }
        else if (other.gameObject.GetComponent<Box>().boxTagNumber == 3) // (*)
        {
            rightScaleValue *= other.gameObject.GetComponent<Box>().value;
            rightScaleText.text = "Value: " + rightScaleValue;
        }
        else if (other.gameObject.GetComponent<Box>().boxTagNumber == 4) // (/)
        {
            rightScaleValue /= other.gameObject.GetComponent<Box>().value;
            rightScaleText.text = "Value: " + rightScaleValue;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.gameObject.GetComponent<Box>())
        {
            //debug, and I don't know why this bug happens
            return;
        }
        else if (other.gameObject.GetComponent<Box>().boxTagNumber == 1) // (+)
        {
            rightScaleValue -= other.gameObject.GetComponent<Box>().value;
            rightScaleText.text = "Value: " + rightScaleValue;
        }
        else if (other.gameObject.GetComponent<Box>().boxTagNumber == 2) // (-)
        {
            rightScaleValue += other.gameObject.GetComponent<Box>().value;
            rightScaleText.text = "Value: " + rightScaleValue;
        }
        else if (other.gameObject.GetComponent<Box>().boxTagNumber == 3) // (*)
        {
            rightScaleValue /= other.gameObject.GetComponent<Box>().value;
            rightScaleText.text = "Value: " + rightScaleValue;
        }
        else if (other.gameObject.GetComponent<Box>().boxTagNumber == 4) // (/)
        {
            rightScaleValue *= other.gameObject.GetComponent<Box>().value;
            rightScaleText.text = "Value: " + rightScaleValue;
        }
    }
}
