using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    [SerializeField] public ObjectPool playerBulletPool;
     [SerializeField] public ObjectPool enemyBulletPool;
    [SerializeField] public PlayerController playerObj;

    [SerializeField] private StageSequencer sequencer;
    [SerializeField] public Transform enemyPool;

    [SerializeField] public ObjectPool explosionPool;
    [SerializeField] UnityEngine.UI.Text ScoreValue;
    private int highScore;

    public float stageSpeed = 1;
    float stageProgressTime = 0;

    public bool isPlaying;

    public bool isStageBossDead;

    int score= 0; //追加

    private static StageController instance;
    public static StageController Instance { get => instance; }

    private void Awake()
    {
        instance = GetComponent<StageController>();
    }

    public enum PlayStopCodeDef
    {
        PlayerDead,
        BossDefeat,
    }
    public PlayStopCodeDef playStopCode;

 
    void Start()
    {
        sequencer.Load();
        sequencer.Reset();
        stageProgressTime = 0;
        isPlaying = false;
        SetScore(0);
        playerObj.SetupForTitle();
        highScore = PlayerPrefs.GetInt(PlayerPrefsKeys.HighScoreData);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerObj.isDead)
        {
            playStopCode = PlayStopCodeDef.PlayerDead; //追加
            isPlaying = false;
        }
        if (isStageBossDead)
        {
            playStopCode = PlayStopCodeDef.BossDefeat; //追加
            isPlaying = false;
        }

        if (!isPlaying) return;
       sequencer.Step(stageProgressTime);
        stageProgressTime += Time.deltaTime;

        transform.Translate(Vector2.up * Time.deltaTime * stageSpeed);

        var xaxis = Input.GetAxisRaw("Horizontal");
        var yaxis = Input.GetAxisRaw("Vertical");
        playerObj.Move(new Vector2(xaxis, yaxis));

        if (Input.GetButton("Fire1"))
        {
            playerObj.Shot();
        }
        playerObj.Shot();
        //Debug.Log(transform.position.y);
    }

    public void StageStart() //追加
    {
        isPlaying = true;
        stageProgressTime = 0;
        stageSpeed = 0;
        sequencer.Reset();
        isStageBossDead = false;
        playerObj.SetupForPlay(); //追加
        SetScore(0); 

    }

    public void ResetStage() //追加
    {
        playerObj.SetupForTitle();
        BroadcastMessage("HideFromStage", SendMessageOptions.DontRequireReceiver);
        transform.position = Vector3.zero;
    }
    public void AddScore(int _val)
    {
        SetScore(score + _val);
    }
    public void SetScore(int _val)
    {
        score = _val;
        ScoreValue.text = $"{score:0}";

        SetHighScore(score);
    }

    private void SetHighScore(int score)
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt(PlayerPrefsKeys.HighScoreData, highScore);
            //todo:inGameViewに伝えるrx
        }
    }

}
