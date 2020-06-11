using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckBalance : MonoBehaviour
{
    public static bool equal = false;
    public bool gateOpenFirstTime = false;
    public Animator animator;

    public List<Scales> scales_list;
    public List<float> scales_val;
    public GameObject gate;

    void Update()
    {
        for (int i = 0; i < scales_list.Count; i++)
        {
            scales_val[i] = scales_list[i].realValue;
        }
        equal = CheckScale();
        Debug.Log(equal);
        if (equal)
        {
            //open gate
            animator.SetBool("GateTrigger", true);
            AudioManager.soundConditionDict[SoundList.GateClose] = false;
            gateOpenFirstTime = true;
            AudioManager.instance.PlayOnce(SoundList.GateOpen);
            gate.layer = LayerMask.NameToLayer("Default");
        }
        else
        {
            //close gate
            animator.SetBool("GateTrigger", false);
            AudioManager.soundConditionDict[SoundList.GateOpen] = false;
            if(gateOpenFirstTime)
            {
                AudioManager.instance.PlayOnce(SoundList.GateClose);
            }
            gate.layer = LayerMask.NameToLayer("Wall");
        }
    }

    public bool CheckScale()
    {
        for (int i = 0; i < scales_list.Count - 1; i++)
        {
            if (scales_val[i] != scales_val[i + 1])
            {
                return false;
            }
        }
        return true;
    }
}
