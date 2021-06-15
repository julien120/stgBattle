using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;

public class InGameView : MonoBehaviour
{
    //ゲームオーバー
    [SerializeField] private Transform gameOverDialog;
    [SerializeField] private Button[] backHomeButton;
    [SerializeField] private Button[] restartStageButton;
    [SerializeField] private Text[] highScoreText;

    //ゲームクリア
    [SerializeField] private Transform gameClearDialog;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    /// <summary>
    /// DOTweenアニメーション用の初期化
    /// </summary>
    public void Initialize()
    {
        gameOverDialog.transform.localScale = Vector3.zero;
        gameClearDialog.transform.localScale = Vector3.zero;

        
        for (var i=0; i< backHomeButton.Length; i++)
        {
            backHomeButton[i].onClick.AddListener(BackHomeButton);
            restartStageButton[i].onClick.AddListener(RestartStageButton);
            
        }
    }

    public void BackHomeButton()
    {
        SceneController.Instance.LoadHomeScene();
    }

    public void RestartStageButton()
    {
        SceneController.Instance.LoadInGame1();
    }

    public void SetGameOverAnimation()
    {
        gameOverDialog.transform.DOScale(1f, 0.3f).SetEase(Ease.OutSine);
        var score = PlayerPrefs.GetInt(PlayerPrefsKeys.HighScoreData);
        highScoreText[1].text = score.ToString();
    }

    public void SetGameClearAnimation()
    {
        var score = PlayerPrefs.GetInt(PlayerPrefsKeys.HighScoreData);
        highScoreText[0].text = score.ToString();
        DOVirtual.DelayedCall(0.7f, () => gameClearDialog.transform.DOScale(1f, 0.3f).SetEase(Ease.OutSine));
    }

    

}
