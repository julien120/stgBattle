using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

public class InGameModel : MonoBehaviour
{
   [SerializeField] private GameState gamestate = GameState.TITLE;
    delegate void gameProc();
    //ステイトが多いとSwitchが長くなるので
    //dictionaryでdelegate型の関数とステイトの値を対応させる
    Dictionary<GameState, gameProc> gameProcList;


    //gameOver
    private readonly Subject<Unit> setGameoverUI = new Subject<Unit>();
    public IObservable<Unit> IOsetGameoverUI => setGameoverUI;

    //gameClear
    private readonly Subject<Unit> setGameclearUI = new Subject<Unit>();
    public IObservable<Unit> IOsetGameclearUI => setGameclearUI;

    //todo:これも属性拡張で2-5しか選択できないようにしたい
    [SerializeField] private int nextStageCount = 2;

    [SerializeField] private AudioSource dialogAudioSource;

    //開始時のボイス
    [SerializeField] private AudioClip clearSE;

    //終了時のボイス
    [SerializeField] private AudioClip gameOverSE;


    private enum StateSE
    {
        Clear,
        GameOver,
    }

    void Start()
    {
        gameProcList = new Dictionary<GameState, gameProc> {
            { GameState.TITLE, Title },
            { GameState.GAMEMAIN, GameMain},
            { GameState.CLEAR, Clear},
            { GameState.GAMEOVER, GameOver},
            };
        gamestate = GameState.TITLE;

        this.UpdateAsObservable().Subscribe(_ => gameProcList[gamestate]());
    }

    
    void Update()
    {
       // gameProcList[gamestate]();
    }

    private void Title()
    {
        gamestate = GameState.GAMEMAIN;
        StageController.Instance.StageStart();
    }
    private void GameMain()
    {
        if (!StageController.Instance.isPlaying)
        {
            if (StageController.Instance.playStopCode ==
StageController.PlayStopCodeDef.PlayerDead)
            {
                gamestate = GameState.GAMEOVER;
            }
            else
            {
                gamestate = GameState.CLEAR;
            }
        }

    }
    private void Clear()
    {
        dialogAudioSource.PlayOneShot(clearSE);
        UpdateStageLevelSelection();
        setGameclearUI.OnNext(Unit.Default);
    }

    /// <summary>
    /// UIを表示し、
    /// 
    /// </summary>
    private void GameOver()
    {
        dialogAudioSource.PlayOneShot(gameOverSE);
        setGameoverUI.OnNext(Unit.Default);
    }

    private void UpdateStageLevelSelection()
    {
       
        if (nextStageCount > PlayerPrefs.GetInt(PlayerPrefsKeys.LevelCount))
        {
            PlayerPrefs.SetInt(PlayerPrefsKeys.LevelCount, nextStageCount);
        }
    }

    private void SetGameSE(AudioClip audioClip)
    {
        dialogAudioSource.clip = audioClip;
        dialogAudioSource.Play();
    }

    private void SetDialogSE(StateSE se)
    {
        switch (se)
        {
            case StateSE.Clear:
                SetGameSE(clearSE);
                break;

            case StateSE.GameOver:
                SetGameSE(gameOverSE);
                break;

        }

    }
}
