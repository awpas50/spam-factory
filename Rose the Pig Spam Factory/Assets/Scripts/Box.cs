using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [Header("Barrier interaction")]
    [SerializeField] public int value; // value of the box
    public int boxTagNumber; // 1: addition 2: subtraction 3: multiplication 4: division
    private Rigidbody2D rb;

    public bool overOtherBoxes = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    //DEBUG: avoid box stacking

}
