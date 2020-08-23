using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderSpecialAttack : MonoBehaviour
{
    PlayerController Player;

    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(Player.transform.position, transform.position) >= 7 && Vector3.Distance(Player.transform.position, transform.position) < 9)
        {
            Debug.Log("확인 2");
            timer += Time.deltaTime;
            if (timer >= 1.0f)
            {

                GetComponent<SpiderController>().OnJump = true;
                GetComponent<SpiderController>().OnMove = false;
                GetComponent<SpiderController>().OnAttack = false;

            }
            else
                GetComponent<SpiderController>().OnJump = false;

        }
    }
}
