using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public int barrierTagNumber; 
    private GameManager gamecontroller;

    [Header("VFX")]
    public GameObject glassEffect;
    public GameObject boxEffect;
    public float vfxSpawnDelay;

    void Start()
    {
        // just a short form... otherwise the code below will be too long
        gamecontroller = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
    }

    IEnumerator delayInstantiate(GameObject VFX, Vector3 position, Quaternion rotation)
    {
        // delat instantiate.
        yield return new WaitForSeconds(vfxSpawnDelay);
        GameObject VFXPrefab = Instantiate(VFX, position, rotation);
        // Destroy the Prefab after 5 sec to save memory.
        Destroy(VFXPrefab, 5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // just a short form... otherwise the code below will be too long
        if(!other.GetComponent<Box>())
        {
            return;
        }
        Box box = other.GetComponent<Box>(); 
        // 1: addition 2: subtraction 3: multiplication 4: division
        if (barrierTagNumber != box.boxTagNumber)
        {
            // ---------------- VFX part ----------------
            StartCoroutine(delayInstantiate(glassEffect, transform.position, Quaternion.identity));
            StartCoroutine(delayInstantiate(boxEffect, transform.position, Quaternion.identity));

            if (barrierTagNumber == 1)
            {
                // change the sprite and the property (boxTagNumber) of the box
                // 1: addition 2: subtraction 3: multiplication 4: division
                box.boxTagNumber = barrierTagNumber;
                box.GetComponent<SpriteRenderer>().sprite = gamecontroller.box_sprite_addition[box.value - 1]; // array starts from 0 so -1 is necessary
            }
            if (barrierTagNumber == 2)
            {
                box.boxTagNumber = barrierTagNumber;
                box.GetComponent<SpriteRenderer>().sprite = gamecontroller.box_sprite_subtraction[box.value - 1];
            }
            if (barrierTagNumber == 3)
            {
                box.boxTagNumber = barrierTagNumber;
                box.GetComponent<SpriteRenderer>().sprite = gamecontroller.box_sprite_multi[box.value - 1];
            }
            if (barrierTagNumber == 4)
            {
                box.boxTagNumber = barrierTagNumber;
                box.GetComponent<SpriteRenderer>().sprite = gamecontroller.box_sprite_division[box.value - 1];
            }
        }
    }
}
