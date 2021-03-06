using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMoving : MonoBehaviour
{
    public float speed;
    private PoolContent poolContent;
    [SerializeField] private AudioSource audeioSource;

    // Start is called before the first frame update
    void Start()
    {
        poolContent = transform.GetComponent<PoolContent>();
        audeioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(0,3) * speed * Time.deltaTime);

        //howtoplay用3.4
        if (transform.localPosition.y > 8.4|| transform.localPosition.y<-14)
        {
            poolContent.HideFromStage();
        }
    }
}
