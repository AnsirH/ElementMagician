using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoadingBar : MonoBehaviour
{
    [SerializeField]
    Image prograssBar;
    static string nextScene;

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("Loading"); 
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadSceneProcess());
    }

    IEnumerator LoadSceneProcess()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float timer = 0.0f;
        while (!op.isDone)
        {
            yield return null;

            if(op.progress < 0.7f)
            {
                prograssBar.fillAmount = op.progress;
            }
            else
            {
                timer += Time.deltaTime;
                prograssBar.fillAmount = Mathf.Lerp(0.7f, 1.0f, timer);
                if (prograssBar.fillAmount >= 1.0f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }


}
