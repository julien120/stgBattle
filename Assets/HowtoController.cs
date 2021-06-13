using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowtoController : MonoBehaviour
{
    [SerializeField] private PlayerController playerObj;
    // Start is called before the first frame update
    void Update()
    {
        var xaxis = Input.GetAxisRaw("Horizontal");
        var yaxis = Input.GetAxisRaw("Vertical");
        playerObj.Move(new Vector2(xaxis,yaxis));

        //if (Input.GetButton("Fire1"))
        //{
        //    playerObj.Shot();
        //}
        playerObj.Shot();
    }


}
