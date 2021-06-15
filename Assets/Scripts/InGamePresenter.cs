using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class InGamePresenter : MonoBehaviour
{
    [SerializeField] private InGameModel inGameModel;
    [SerializeField] private InGameView inGameView;

    void Start()
    {
        Initialize();   
    }

    private void Initialize()
    {
        inGameView.Initialize();
        inGameModel.IOsetGameoverUI.Subscribe(_ => inGameView.SetGameOverAnimation());
        inGameModel.IOsetGameclearUI.Subscribe(_ => inGameView.SetGameClearAnimation());
    }
}
