using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuadController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            var poolobj = other.GetComponent<PoolContent>();
            poolobj.HideFromStage();
        }
    }
}
