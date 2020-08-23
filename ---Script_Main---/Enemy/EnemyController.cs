using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    PlayerController Player;

    

    private Vector3 lookAtTarget;
    private Quaternion enemyRot;

    public GameObject AttackCollider;

    public float MoveSpeed = 0.0f;
    public bool isMove = true;
    public bool isHit = false;
    public float limitDistance = 0.0f;

    bool canAttack = true;

    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        Player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {

        if(isMove && !isHit && canAttack)
        {
            Move();
            anim.SetBool("OnMove", true);


        }

        else 
            anim.SetBool("OnMove", false);


        CheckDistance();

        Death();

    }

    private void Death()
    {
        if(transform.GetComponent<EnemyStat>().currentHp <= 0)
        {
            isMove = false;
            anim.SetBool("Death", true);
        }
    }

    private void CheckDistance()
    {
        if (Vector3.Distance(Player.transform.position, transform.position) <= limitDistance)
        {
            isMove = false;
            Rotate();

            if(canAttack)
            {
                anim.SetTrigger("Attack");
                canAttack = false;
            }

            

        }

        else
        {
            Debug.Log("아아");   
            isMove = true;


        }

    }

    private void Move()
    {
        lookAtTarget = new Vector3(Player.transform.position.x - transform.position.x, transform.position.y, Player.transform.position.z - transform.position.z);
        enemyRot = Quaternion.LookRotation(lookAtTarget);

        transform.rotation = Quaternion.Slerp(transform.rotation, enemyRot, 13.0f * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, MoveSpeed * Time.deltaTime);
    }

    private void Rotate()
    {
        lookAtTarget = new Vector3(Player.transform.position.x - transform.position.x, transform.position.y, Player.transform.position.z - transform.position.z);

        enemyRot = Quaternion.LookRotation(lookAtTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, enemyRot, 13.0f * Time.deltaTime);


    }

    public void OnAttackCollision()
    {
        AttackCollider.SetActive(true);
    }

    public void AttackEnd()
    {
        StartCoroutine(DelayAttack());

    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(0.4f);
        canAttack = true;
    }

    public void HitToIdle()
    {
        isHit = false;
        canAttack = true;
    }
}
