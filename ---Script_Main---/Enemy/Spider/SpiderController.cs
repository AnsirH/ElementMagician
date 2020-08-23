using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderController : MonoBehaviour
{
    PlayerController Player;

    private Animation anim;


    private Vector3 lookAtTarget;
    private Quaternion enemyRot;

    public GameObject AttackCollider;
    public GameObject JumpAttackCollider;

    float timer;
    public float MoveSpeed = 0.0f;

    public bool OnIdle = true;
    public bool OnMove = false;
    public bool OnAttack = false;
    public bool OnHit = false;
    public bool OnDeath = false;
    public bool OnJump = false;

    public float limitDistance = 0.0f;


    private void Start()
    {
        anim = GetComponent<Animation>();

        Player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {

        if(OnJump)
        {
            Debug.Log("확인");
        }
        if(OnMove)
        {
            Move();

            anim.Play("run");
        }

        else if(OnAttack)
        {
            anim.Play("attack1");
        }

        else if (OnHit)
        {
            OnMove = false;
            OnAttack = false;
        }

        else if (OnDeath)
        {

            anim.Play("death1");
        }

        else if (OnJump)
        {
            OnMove = false;
            OnAttack = false;
            anim.Play("jump", PlayMode.StopAll);
        }
        else
        {
            anim.Play("idle");
        }



        //if (isMove)
        //{
        //    Move();

        //    anim.Play("run");
        //}
        //else if(!isMove && !(Vector3.Distance(Player.transform.position, transform.position) <= limitDistance) && !(transform.GetComponent<EnemyStat>().currentHp <= 0))
        //    anim.Play("idle");

        if (!OnDeath && !OnJump)
            CheckDistance();

        Death();

    }

    public void HitAnim()
    {
        anim.Stop();
        anim.Play("hit1");
        OnAttack = false;
        timer = 1.5f;

    }

    private void Death()
    {
        if (transform.GetComponent<EnemyStat>().currentHp <= 0)
        {
            OnMove = false;
            OnDeath = true;
        }
    }

    private void CheckDistance()
    {
        if (Vector3.Distance(Player.transform.position, transform.position) <= limitDistance)
        {
            OnMove = false;
            timer += Time.deltaTime;
            if (timer < 0.7f)
            {
                Rotate();
                OnAttack = true;



            }
            else if (timer > 2.5f)
            {
                timer = 0.0f;
            }

            else
                OnAttack = false;

        }

        else
        {
            OnMove = true;



        }

    }

    private void Move()
    {
        lookAtTarget = new Vector3(Player.transform.position.x - transform.position.x, 0.0f, Player.transform.position.z - transform.position.z);
        enemyRot = Quaternion.LookRotation(lookAtTarget);

        transform.rotation = Quaternion.Slerp(transform.rotation, enemyRot, 13.0f * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, MoveSpeed * Time.deltaTime);
    }

    private void Rotate()
    {

        lookAtTarget = new Vector3(Player.transform.position.x - transform.position.x, 0.0f, Player.transform.position.z - transform.position.z);

        enemyRot = Quaternion.LookRotation(lookAtTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, enemyRot, 13.0f * Time.deltaTime);


    }

    public void OnAttackCollision()
    {
        AttackCollider.SetActive(true);
    }
    public void OnJumpAttackCollision()
    {
        JumpAttackCollider.SetActive(true);

    }


    public void EndHit()
    {
        Debug.Log("히트 끝");
        OnHit = false;
    }

    public void AddJumpForce()
    {
        GetComponent<Rigidbody>().AddForce((transform.forward + new Vector3(0, 1, 0)) * Mathf.Sqrt(Vector3.Distance(Player.transform.position, transform.position)) * 40 * 3.1f, ForceMode.Acceleration);
    }


    public void EndJump()
    {
        OnJump = false;
        GetComponent<SpiderSpecialAttack>().timer = 0.0f;

    }
}
