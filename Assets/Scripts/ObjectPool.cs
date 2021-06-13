using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private PoolContent content = default;
    private Queue<PoolContent> objctQueue;
    [SerializeField] private int MaxObjcts = 20;

    void Start()
    {
        objctQueue = new Queue<PoolContent>(MaxObjcts);
        for (int i = 0; i < MaxObjcts; ++i)
        {
            var tmpobj = Instantiate(content);
            tmpobj.transform.parent = transform;
            tmpobj.transform.localPosition = new Vector3(100, 0, 100);//画面外
            objctQueue.Enqueue(tmpobj);
        }
    }


    public PoolContent Launch(Vector3 _position, float _angle)
    {
        if (objctQueue.Count <= 0) return null;
        var tmpobj = objctQueue.Dequeue();
        tmpobj.gameObject.SetActive(true);
        tmpobj.ShowInStage(_position, _angle);

        return tmpobj;
    }

    public void Collect(PoolContent _obj)
    {
        _obj.gameObject.SetActive(false);
        objctQueue.Enqueue(_obj);
    }

    public void ResetAll()
    {
        BroadcastMessage("HideFromStage", SendMessageOptions.DontRequireReceiver);
    }
}
