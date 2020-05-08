using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchLevel : MonoBehaviour
{
    public string levelToSwitch;
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.collider.gameObject.tag == "Player" && CheckBalance.equal)
        {
            SceneManager.LoadScene(levelToSwitch);
        }
    }
}
