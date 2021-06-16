using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputOnPC : Interface
{
    public float Horizontal()
    {
        //touch GetAxisRaw
        return Input.GetAxisRaw("Horizontal");
    }
    public float Vertical()
    {
        return Input.GetAxisRaw("Vertical");
    }
}
