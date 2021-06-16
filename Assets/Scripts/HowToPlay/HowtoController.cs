using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class HowtoController : MonoBehaviour
{
    [SerializeField] private HowtoPlayerController playerObj;

    private Interface interfacetype;

    private void Start()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            interfacetype = new InputOnMobile();
        }
        else if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {
            interfacetype = new InputOnPC();
        }
        else
        {
            interfacetype = new InputOnPC();
        }
    }

    void Update()
    {
        var xaxis = interfacetype.Horizontal();
        var yaxis = interfacetype.Vertical();
        playerObj.Move(new Vector2(xaxis,yaxis));


        Observable.Interval(System.TimeSpan.FromSeconds(0.3))
            .Subscribe(x =>
            {
                playerObj.Shot();
            }
            ).AddTo(this);
    }


}
