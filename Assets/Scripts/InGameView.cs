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

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip backSe;
    [SerializeField] private AudioClip restartSe;

    [SerializeField] private Text[] highScoreText;
    private GameObject bgm;

    //ゲームクリア
    [SerializeField] private Transform gameClearDialog;

    [SerializeField] private PlayerPrefsKeys.KindofData stageData = PlayerPrefsKeys.KindofData.HIGHSCORE01;
    /// <summary>
    /// DOTweenアニメーション用の初期化
    /// </summary>
    public void Initialize()
    {
        bgm = GameObject.Find("BgmManager");
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
        Destroy(bgm);
        audioSource.PlayOneShot(backSe);
        DOVirtual.DelayedCall(0.3f, () => SceneController.Instance.LoadHomeScene());
    }

    public void RestartStageButton()
    {
        audioSource.PlayOneShot(restartSe);
        
        DOVirtual.DelayedCall(0.3f, () => SceneController.Instance.LoadCurrentScene());
    }

    public void SetGameOverAnimation()
    {
        gameOverDialog.transform.DOScale(1f, 0.3f).SetEase(Ease.OutSine);
        var score = PlayerPrefs.GetInt(stageData.ToString());
        highScoreText[1].text = score.ToString();
    }

    public void SetGameClearAnimation()
    {
        var score = PlayerPrefs.GetInt(stageData.ToString());
        highScoreText[0].text = score.ToString();
        DOVirtual.DelayedCall(0.7f, () => gameClearDialog.transform.DOScale(1f, 0.3f).SetEase(Ease.OutSine));
    }

    

}
