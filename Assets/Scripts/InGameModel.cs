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
        UpdateStageLevelSelection();
        setGameclearUI.OnNext(Unit.Default);
    }

    /// <summary>
    /// UIを表示し、
    /// 
    /// </summary>
    private void GameOver()
    {

        setGameoverUI.OnNext(Unit.Default);
    }

    private void UpdateStageLevelSelection()
    {
        var nextStageCount = 2;
        if (nextStageCount > PlayerPrefs.GetInt(PlayerPrefsKeys.LevelCount))
        {
            PlayerPrefs.SetInt(PlayerPrefsKeys.LevelCount, nextStageCount);
        }
    }
}
