using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputOnMobile : Interface
{
    public float Horizontal()
    {
        if (Input.touchCount > 0)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                return touch.deltaPosition.x* 0.04f; 
            }
        }

        return 0;
    }

    public float Vertical()
    {
        if (Input.touchCount > 0)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                return touch.deltaPosition.y*0.04f;
            }
        }

        return 0;
    }
}
