using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowtoBulletMoving : MonoBehaviour
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
        transform.Translate(new Vector2(0, 3) * speed * Time.deltaTime);

        //howtoplay用3.4
        if (transform.localPosition.y > 13)
        {
            poolContent.HideFromStage();
        }
    }
}
