﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolContent : MonoBehaviour
{
    private ObjectPool pool;

    void Start()
    {
        pool = transform.parent.GetComponent<ObjectPool>();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowInStage(Vector3 _position, float _angle)
    {
        transform.position = _position;
        transform.eulerAngles = new Vector3(0, 0, _angle);
    }

    public void HideFromStage()
    {
        Debug.Assert(gameObject.activeInHierarchy);
        pool.Collect(this);
    }
}
