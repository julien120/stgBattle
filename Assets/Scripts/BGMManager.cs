using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    //[SerializeField] AudioClip audioClip;
    private void Awake()
    {
        BGMManager[] bgms = FindObjectsOfType<BGMManager>();
        if (bgms.Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this);

    }
}
