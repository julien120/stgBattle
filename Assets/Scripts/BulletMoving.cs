using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMoving : MonoBehaviour
{
    public float speed;
    private PoolContent poolContent;

    // Start is called before the first frame update
    void Start()
    {
        poolContent = transform.GetComponent<PoolContent>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        if (transform.localPosition.y > 5 ||
            transform.localPosition.y < -5 ||
            transform.localPosition.x > 5 ||
            transform.localPosition.x < -5)
        {
            poolContent.HideFromStage();
        }
    }
}
