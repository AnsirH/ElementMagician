using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scenes
{
    TUTORIAL,
    HOUSE,
    STAGE1,
    STAGE2,
    STAGE3,
    STAGE4
}

public class SceneMove : MonoBehaviour
{
    [SerializeField]
    Animator canvasAnim;

    CameraController Camera;


    public static Scenes currentSceen = Scenes.TUTORIAL;



    private void Start()
    {
        Camera = FindObjectOfType<CameraController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        switch(currentSceen)
        {
            case Scenes.TUTORIAL:
                currentSceen = Scenes.HOUSE;
                StartCoroutine(ChangeScene("House"));
                break;


            case Scenes.HOUSE:
                currentSceen = Scenes.STAGE1;
                StartCoroutine(ChangeScene("Main"));
                break;

        case Scenes.STAGE1:
        currentSceen = Scenes.STAGE2;
        StartCoroutine(ChangeScene("Stage_1"));
        break;

        case Scenes.STAGE2:
        currentSceen = Scenes.STAGE3;
        StartCoroutine(ChangeScene("Stage_2"));
        break;

        case Scenes.STAGE3:
        currentSceen = Scenes.STAGE4;
        StartCoroutine(ChangeScene("Stage_3"));
        break;


        }

    }

    IEnumerator ChangeScene(string sceneName)
    {
        canvasAnim.SetTrigger("Fade_Out");

        yield return new WaitForSeconds(1.5f);

        LoadingBar.LoadScene(sceneName);

        
    }
}
