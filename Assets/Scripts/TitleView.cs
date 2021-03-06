using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UniRx;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;

public class TitleView : MonoBehaviour
{
    [SerializeField] private RectTransform charaPannel;
    [SerializeField] private RectTransform homePannel;
    [SerializeField] private RectTransform titlePannel;
    [SerializeField] private RectTransform titleChara;
    [SerializeField] private RectTransform titleCharaPapa;
    [SerializeField] private Button titlePannelButton;
    [SerializeField] private RectTransform howToPlayPannel;
    [SerializeField] private Button howToPlayButton;
    [SerializeField] private Button howOKButton;
    [SerializeField] private GameObject howtoplayChara;

    [SerializeField] private Text touchScreenText;

    //stageLevelSelection
    //button
    [SerializeField] private Button[] levelSelectionButton;

    //se
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip levelSelectionSe;
    [SerializeField] private AudioClip otherSe;

    void Start()
    {
        touchScreenText.DOFade(0.0f, 1).SetEase(Ease.InCubic).SetLoops(-1, LoopType.Yoyo);
    

         //buttonの設定
        titlePannelButton.onClick.AsObservable().First().Subscribe(x => ToHomeFromTitle().Forget());

        howOKButton.onClick.AsObservable().Subscribe(x => ToHomeFromHow());
        howToPlayButton.onClick.AsObservable().Subscribe(x => SetHowToPlay());

        //levelSelectionButton
        levelSelectionButton[0].onClick.AsObservable().Subscribe( x =>
        {
            audioSource.PlayOneShot(levelSelectionSe);
            DOVirtual.DelayedCall(0.3f, () => SceneController.Instance.LoadInGame1());
        });

        levelSelectionButton[1].onClick.AsObservable().Subscribe(x =>
        {
            audioSource.PlayOneShot(levelSelectionSe);
            DOVirtual.DelayedCall(0.3f, () => SceneController.Instance.LoadInGame2());
        });

        levelSelectionButton[2].onClick.AsObservable().Subscribe(x =>
        {
            audioSource.PlayOneShot(levelSelectionSe);
            DOVirtual.DelayedCall(0.3f, () => SceneController.Instance.LoadInGame3());
        });

        levelSelectionButton[3].onClick.AsObservable().Subscribe(x =>
        {
            audioSource.PlayOneShot(levelSelectionSe);
            DOVirtual.DelayedCall(0.3f, () => SceneController.Instance.LoadInGame4());
        });

        levelSelectionButton[4].onClick.AsObservable().Subscribe(x =>
        {
            audioSource.PlayOneShot(levelSelectionSe);
            DOVirtual.DelayedCall(0.3f, () => SceneController.Instance.LoadInGame5());
        });

        //title
        SetTitle().Forget();

        //homeの攻略可能なステージ
        SetStageLevelSelection();

    }

    private async UniTaskVoid SetTitle()
    {
        await charaPannel.DOAnchorPos(Vector2.zero, 0.2f).AsyncWaitForCompletion();
        titleChara.DOAnchorPos(new Vector2(-117, -138), 0.4f).SetEase(Ease.InOutBack);
        
    }

    private async UniTaskVoid ToHomeFromTitle()
    {
        audioSource.PlayOneShot(otherSe);
        await titlePannel.DOAnchorPos(new Vector2(-844, 0), 0.1f).AsyncWaitForCompletion();
        DOVirtual.DelayedCall(0.2f, () => titleCharaPapa.DOAnchorPos(new Vector2(-844, 0), 0.4f));
        
    }

    private void SetStageLevelSelection()
    {
        int levelCount = PlayerPrefs.GetInt(PlayerPrefsKeys.LevelCount, 1);
        for (int i = 0; i < levelSelectionButton.Length; i++)
        {
            if (i + 1 > levelCount)
            {
                levelSelectionButton[i].gameObject.SetActive(false);
            }
        }
    }

    private void SetHowToPlay()
    {
        audioSource.PlayOneShot(otherSe);
        howToPlayPannel.DOAnchorPos(Vector2.zero, 0.3f);
        howtoplayChara.SetActive(true);
    }

    private void ToHomeFromHow()
    {
        audioSource.PlayOneShot(otherSe);
        howtoplayChara.SetActive(false);
        howToPlayPannel.DOAnchorPos(new Vector2(0, -1888), 0.3f);
    }


}
