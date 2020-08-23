using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemSpecialAttack : MonoBehaviour
{
    PlayerController Player;
    public GameObject Hand;
    GameObject currentRock;


    float timer;

    void Start()
    {
        Player = FindObjectOfType<PlayerController>();

        
    }

    private void Update()
    {
        if(Vector3.Distance(Player.transform.position, transform.position) >= 10.0f)
        {
            timer += Time.deltaTime;
            if(timer >= 3.0f)
            {
                GetComponent<Animator>().SetBool("SpecialAttack", true);
                GetComponent<EnemyController>().isMove = false;

            }
            else
                GetComponent<EnemyController>().isMove = true;

        }
    }

    

    public void CreatRock()
    {
        StartCoroutine(CreatCoroutine());
    }

    private IEnumerator CreatCoroutine()
    {
        yield return null;
        GameObject t_object = ObjectPoolingManager.instance.GetQueue();
        if(t_object)
        {

            t_object.transform.position = Hand.transform.position;
            t_object.transform.parent = Hand.transform;
            t_object.GetComponent<MeshCollider>().enabled = false;
            t_object.GetComponent<Rigidbody>().useGravity = false;

            currentRock = t_object;
        }
    }

    public void Throw()
    {
        currentRock.GetComponent<Rigidbody>().useGravity = true;
        currentRock.GetComponent<MeshCollider>().enabled = true;

        currentRock.transform.parent = null;
        currentRock.GetComponent<Rigidbody>().AddForce(transform.forward * Vector3.Distance(Player.transform.position, transform.position) * 60, ForceMode.Acceleration);
        timer = 0.0f;
        GetComponent<Animator>().SetBool("SpecialAttack", false);

    }
}
