using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierLight : MonoBehaviour
{
    public UnityEngine.Experimental.Rendering.LWRP.Light2D neonLight;
    // Start is called before the first frame update
    void Start()
    {
        neonLight = transform.GetChild(0).GetComponent<UnityEngine.Experimental.Rendering.LWRP.Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
