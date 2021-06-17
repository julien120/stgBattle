using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private static SceneController instance;
    public static SceneController Instance
    {

        get
        {
            if (instance == null)
            {

                GameObject single = new GameObject();
                //instanceに格納されてる値を管理する
                instance = single.AddComponent<SceneController>();
                //scene跨いでもインスタンスが残るのでnull処理に行かない
                DontDestroyOnLoad(instance);

            }
            return instance;

        }
    }


    public void LoadHomeScene()
    {
        SceneManager.LoadScene(SceneName.Title);
    }

    public void LoadCurrentScene()
    {
        var currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void LoadInGame1()
    {
        SceneManager.LoadScene(SceneName.Stage01);
    }

    public void LoadInGame2()
    {
        SceneManager.LoadScene(SceneName.Stage02);
    }

    public void LoadInGame3()
    {
        SceneManager.LoadScene(SceneName.Stage03);
    }

    public void LoadInGame4()
    {
        SceneManager.LoadScene(SceneName.Stage04);
    }

    public void LoadInGame5()
    {
        SceneManager.LoadScene(SceneName.Stage05);
    }


    public void LoadRestartStage(SceneName sceneName)
    {
        SceneManager.LoadScene(sceneName.ToString());
    }
}
