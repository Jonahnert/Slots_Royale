using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class PlayerControl : MonoBehaviour
{
    public static float clickCooldown = 0f;

    public delegate void OwnerRegisteration(PlayerControl owner);
    public delegate void ButtonAction(string color, int level);
    public static event ButtonAction OnPress;

    public void Press(string color, int level)
    {
        if(OnPress != null)
        {
            OnPress(color, level);
        }
        else
        {
            Debug.Log("Nothing has subscribed to the OnPress event");
        }
    }

    private void Update()
    {
        
        if(clickCooldown > 0)
        {
            clickCooldown -= Time.deltaTime;
        }
        else if(clickCooldown < 0)
        {
            clickCooldown = 0;
        }
    }
}
