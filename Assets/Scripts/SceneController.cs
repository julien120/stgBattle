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
    public void LoadInGame1()
    {
        SceneManager.LoadScene(SceneName.Stage01);
    }

    public void LoadRestartStage(SceneName sceneName)
    {
        SceneManager.LoadScene(sceneName.ToString());
    }
}
