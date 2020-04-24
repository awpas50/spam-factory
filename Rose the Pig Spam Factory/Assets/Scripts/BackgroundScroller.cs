using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public GameObject scrolledBackground;
    public Renderer backgroundRend;
    [Header("X")]
    public bool scrollX;
    public float scrollSpeedX;
    
    [Header("Y")]
    public bool scrollY;
    public float scrollSpeedY;
    private void Start()
    {
        backgroundRend = scrolledBackground.GetComponent<Renderer>();
    }

    void Update()
    {
        if(scrollX && !scrollY)
        {
            backgroundRend.material.mainTextureOffset += new Vector2(scrollSpeedX * Time.deltaTime, 0f);
        }
        if(scrollY && !scrollX)
        {
            backgroundRend.material.mainTextureOffset += new Vector2(0f, scrollSpeedY * Time.deltaTime);
        }
    }
}
