using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetStartPoint : MonoBehaviour
{
    private PlayerController Player;

    private CameraController Camera;

    void Start()
    {
        Player = FindObjectOfType<PlayerController>();
        Camera = FindObjectOfType<CameraController>();

        Player.isMove = false;
        Player.transform.position = transform.position;
        Camera.transform.position = transform.position;
        Destroy(gameObject);
    }

    
}
