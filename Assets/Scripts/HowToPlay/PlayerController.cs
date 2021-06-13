﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]private ObjectPool bulletPool;

    private float shootInterval = 0;
    public bool isDead;

    private Vector3 restartPos;
    private Vector3 restartRot;

    private void Awake()
    {
        restartPos = transform.localPosition;
        restartRot = transform.localEulerAngles;
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        shootInterval -= Time.deltaTime;
    }

    //inputと移動の分離
    //外部から入力できる方が再利用性ある

    public void Move(Vector3 _movevec)
    {
        transform.Translate(_movevec * 3 * Time.deltaTime);
        var nowpos = transform.localPosition; //画面外に行かないように移動制限
        nowpos.x = Mathf.Clamp(nowpos.x, -1.97f, 2.12f); //引数2,引数3で範囲制限
        nowpos.y = Mathf.Clamp(nowpos.y, -1.758645f, 3.6f);
        transform.localPosition = nowpos;
    }

    public void Shot()
    {
        if (shootInterval <= 0)
        {
            var obj = bulletPool.Launch(transform.position + Vector3.up * 1.4f, 0);
            if (obj != null) obj.GetComponent<BulletMoving>().speed = 0.1f;
            shootInterval = 0.1f;
        }
    }

    private void OnTriggerEnter(Collider _other) //追加
    {
        if (_other.CompareTag("EnemyBullet"))
        {
            _other.GetComponent<PoolContent>().HideFromStage();
            isDead = true;
        }
    }


}
