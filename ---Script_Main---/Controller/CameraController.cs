using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    PlayerController Player;
    private Transform tr;

    public Vector3 center;
    public Vector3 size;

    [Range(0.0f, 1.0f)]
    public float SmoothFactor = 0.5f;

    public float dist;
    public float height;

    float limitWidth;
    float limitHeight;

    private void Start()
    {
        Player = FindObjectOfType<PlayerController>();
        limitHeight = Camera.main.orthographicSize;
        limitWidth = limitWidth * Screen.width / Screen.height;
        tr = GetComponent<Transform>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, size);
    }

    private void LateUpdate()
    {

        // 카메라 이동
        tr.position = Vector3.Slerp(tr.position, Player.transform.position - (Vector3.forward * dist) + (Vector3.up * height), SmoothFactor);

        // 카메라가 플레이어를 바라보게 하는 코드
        //tr.LookAt(playerTransform);

        // x축 제한 설정
        float lx = size.x * 0.5f - limitWidth;
        float clampX = Mathf.Clamp(tr.position.x, - lx + center.x, lx + center.x);

        // y축 제한 설정
        float lz = size.z * 0.5f - limitHeight;
        float clampZ = Mathf.Clamp(tr.position.z, -lz + center.z, lz + center.z);

        // 제한된 좌표를 넘어갔을 경우 위치 재설정
        tr.position = new Vector3(clampX, tr.position.y, clampZ);
    }

}
