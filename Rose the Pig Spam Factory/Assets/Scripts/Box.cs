using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [Header("Barrier interaction")]
    public int value; // value of the box
    public int boxTagNumber; // 1: addition 2: subtraction 3: multiplication 4: division
    public Transform player;
    public float magnitude = 8f; // speed

    private Vector3 force;
    private bool iceDetected = false;
    private bool positionSet = false;
    private Rigidbody2D rb;
}
