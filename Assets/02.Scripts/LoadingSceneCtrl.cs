using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneCtrl : MonoBehaviour
{
    int nextScene = GameManager.instance.sceneNum + 1;
    [SerializeField]
    Image loadingBar;

    void Start()
    {
        StartCoroutine(LoadSceneProcess());
    }


    public static void LoadScene(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }

    IEnumerator LoadSceneProcess()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float timer = 0f;
        while (!op.isDone)
        {
            yield return null;

            if(op.progress < 0.9f)
            {
                loadingBar.fillAmount = op.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                loadingBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
                if(loadingBar.fillAmount >= 1f)
                {
                    op.allowSceneActivation = true;
                    GameManager.instance.ChangeScene();
                    yield break;
                }
            }
        }
    }
}
