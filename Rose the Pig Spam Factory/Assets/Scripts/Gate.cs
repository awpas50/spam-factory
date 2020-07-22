using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    // Allow player to proceed to next level.
    SwitchLevel levelLoader;

    private void Start()
    {
        levelLoader = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<SwitchLevel>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && CheckBalance.equal)
        {
            levelLoader.LoadNextLevel();
            // fade out player model
            other.gameObject.GetComponent<SpriteRenderer>().sortingOrder -= 10;
        }
    }
}
