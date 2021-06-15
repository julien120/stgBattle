using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int hitPoint = 1;
    [SerializeField] private bool isBoss = false;
    [SerializeField] private int scorePoint = 0;
    private CoinAnimation coinAnimation;

    private Renderer flashMaterial = null;

    private ObjectPool bulletpool;
    private ObjectPool explosionpool;
    private Color color;

    // Start is called before the first frame update
    void Start()
    {
        flashMaterial = transform.GetComponentInChildren<Renderer>();
        color = flashMaterial.material.color;
        
        bulletpool = StageController.Instance.enemyBulletPool;
        explosionpool = StageController.Instance.explosionPool;
        coinAnimation = FindObjectOfType<CoinAnimation>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HideFromStage()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            var poolobj = other.GetComponent<PoolContent>();
            poolobj.HideFromStage();
            hitPoint -= 1;
            coinAnimation.AddCoins(transform.position, 2);
            StageController.Instance.AddScore(scorePoint);
            if (hitPoint <= 0)
            {
                StageController.Instance.isStageBossDead = isBoss;
                StageController.Instance.AddScore(scorePoint);
                explosionpool.Launch(transform.position,
0).GetComponent<ExplosionParticle>().PlayParticle();
               // coinAnimation.AddCoins(transform.position, 2);
                HideFromStage();
            }
            else
            {
                StartCoroutine(FlashTimeWait());
            }
        }
    }

    private IEnumerator FlashTimeWait()
    {
        Color color = flashMaterial.material.color;
        color.r = 208;  // RGBのR(赤)値
        //color.g = 141f; // RGBのG(緑)値
        //color.b = 141f; // RGBのB(青)値
        //color.a = 255f;
        flashMaterial.material.color = color;
        yield return new WaitForSeconds(0.05f);
        flashMaterial.material.color = Color.white;
    }

    public void Shot(EnemyBulletPattern _o)
    {
        //var obj = bulletpool.Launch(transform.position + Vector3.up * 0.2f, 180);
        //if (obj != null) obj.GetComponent<BulletMoving>().speed = 4;

        var angleOffset = (_o.Count - 1) / 2.0f;
        float baseDirection = 0;
        if (_o.IsAimPlayer)
        {
            //向きをプレイヤーにrotate
            baseDirection = Vector2.SignedAngle(
                StageController.Instance.playerObj.transform.localPosition - transform.localPosition,
                Vector3.up
                );
        }
        else
        {
            baseDirection = _o.Direction;
        }
        for (int i = 0; i < _o.Count; i++)
        {
            var d = ((i - angleOffset) * _o.OpenAngle);
            var obj = bulletpool.Launch(transform.position + Vector3.up * 0.2f, -d + -baseDirection);
            if (obj != null) obj.GetComponent<BulletMoving>().speed = _o.Speed;
        }



    }



}
