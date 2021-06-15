using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]private ObjectPool bulletPool;
    [SerializeField] private CoinAnimation coinAnimation;

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
        transform.Translate(_movevec * 7 * Time.deltaTime);
        var nowpos = transform.localPosition; //画面外に行かないように移動制限
        nowpos.x = Mathf.Clamp(nowpos.x, -3.2f, 3.3f); //引数2,引数3で範囲制限
        nowpos.y = Mathf.Clamp(nowpos.y, -4.068038f, 3.9f);
        transform.localPosition = nowpos;
    }

    public void Shot()
    {
        if (shootInterval <= 0)
        {
            var obj = bulletPool.Launch(transform.position + Vector3.up * 1.4f, 0);
            if (obj != null) obj.GetComponent<BulletMoving>().speed = 4f;
            shootInterval = 0.2f;
        }
    }

    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.CompareTag("EnemyBullet"))
        {
            _other.GetComponent<PoolContent>().HideFromStage();
            isDead = true;
            
        }
       // coinAnimation.AddCoins(_other.transform.position, 2);
    }

    public void SetupForPlay() //追加
    {
        shootInterval = 0;
        isDead = false;
        transform.localPosition = new Vector3(0, -4.06f, 5f);
        transform.localEulerAngles = new Vector3(0, 0, 0);
    }

    public void SetupForTitle() //追加
    {
        transform.localPosition = restartPos;
        transform.localEulerAngles = restartRot;
    }


}
