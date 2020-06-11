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
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.gameObject.tag == "Player" && CheckBalance.equal)
        {
            levelLoader.LoadNextLevel();
        }
    }
}
