using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Update is called once per frame
    public static GameManager instance = null;
    public Sprite[] box_sprite_addition = new Sprite[9];
    public Sprite[] box_sprite_subtraction = new Sprite[9];
    public Sprite[] box_sprite_multi = new Sprite[9];
    public Sprite[] box_sprite_division = new Sprite[9];
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
