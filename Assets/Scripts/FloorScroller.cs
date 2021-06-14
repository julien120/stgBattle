using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScroller : MonoBehaviour
{
    //3.68-7.36
    private readonly float areaSizeY = 10f * 2;
    readonly float areaSizeZ = 1.62f * 2;

    private Vector2 basepos;
   [SerializeField] private StageController stage;


    void Start()
    {
       
        basepos = transform.position;

    }

    void Update()
    {
        //前に進んだ距離と自分の距離をエリアサイズで割ることで何枚目のタイルかわかる)
        var posy = Mathf.Round((stage.transform.position.y - basepos.y) / areaSizeY);
        var nowpos = transform.position;
        nowpos.y = areaSizeY * posy + basepos.y;
        transform.position = nowpos;
    }

}
