using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeftScale : MonoBehaviour
{
    //put this code in the area to detect value of the box
    public Text leftScaleText;
    public float leftScaleValue;

    // Start is called before the first frame update
    void Start()
    {
        leftScaleText.text = "Value: " + leftScaleValue;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.GetComponent<Box>())
        {
            return;
        }
        else if (other.gameObject.GetComponent<Box>().boxTagNumber == 1) // (+)
        {
            leftScaleValue += other.gameObject.GetComponent<Box>().value;
            leftScaleText.text = "Value: " + leftScaleValue;
        }
        else if (other.gameObject.GetComponent<Box>().boxTagNumber == 2) // (-)
        {
            leftScaleValue -= other.gameObject.GetComponent<Box>().value;
            leftScaleText.text = "Value: " + leftScaleValue;
        }
        else if (other.gameObject.GetComponent<Box>().boxTagNumber == 3) // (*)
        {
            leftScaleValue *= other.gameObject.GetComponent<Box>().value;
            leftScaleText.text = "Value: " + leftScaleValue;
        }
        else if (other.gameObject.GetComponent<Box>().boxTagNumber == 4) // (/)
        {
            leftScaleValue /= other.gameObject.GetComponent<Box>().value;
            leftScaleText.text = "Value: " + leftScaleValue;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.gameObject.GetComponent<Box>())
        {
            return;
        }
        else if (other.gameObject.GetComponent<Box>().boxTagNumber == 1) // (+)
        {
            leftScaleValue -= other.gameObject.GetComponent<Box>().value;
            leftScaleText.text = "Value: " + leftScaleValue;
        }
        else if (other.gameObject.GetComponent<Box>().boxTagNumber == 2) // (-)
        {
            leftScaleValue += other.gameObject.GetComponent<Box>().value;
            leftScaleText.text = "Value: " + leftScaleValue;
        }
        else if (other.gameObject.GetComponent<Box>().boxTagNumber == 3) // (*)
        {
            leftScaleValue /= other.gameObject.GetComponent<Box>().value;
            leftScaleText.text = "Value: " + leftScaleValue;
        }
        else if (other.gameObject.GetComponent<Box>().boxTagNumber == 4) // (/)
        {
            leftScaleValue *= other.gameObject.GetComponent<Box>().value;
            leftScaleText.text = "Value: " + leftScaleValue;
        }
    }
}
