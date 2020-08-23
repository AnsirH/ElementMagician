using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class HandController : MonoBehaviour
{
    [SerializeField]
    private GameObject attackcollision_Right;
    [SerializeField]
    private GameObject attackcollision_Left;
    


    public enum AttackState
    {
        NONE,
        ATTACK1,
        ATTACK2
    }

    // 공격 상태
    public AttackState attackState = AttackState.NONE;




    // 공격 시작 가능 체크
    //private bool attackStartable = true;

    // 현재 공격 중인지 체크, true라면 이동 불가
    // private bool currentAttack = false;

    // 공격 방향
    private Quaternion playerRot;
    private Vector3 lookAtTarget;

    // 방향 전환 체크
    private bool isRotate = false;

    // 방향 전환 속도
    public float rotateSpeed = 18.0f;

    private float rotateSeconds = 0.2f;

    PlayerController Player;



    // 애니메이션
    Animator anim;



    private void Awake()
    {
        Player = FindObjectOfType<PlayerController>();
        anim = transform.GetComponent<Animator>();

    }

    private void Update()
    {

        StopAttack();

        if (Input.GetMouseButtonDown(0) && !Player.currentAction)
        {
            SetAttackTarget();
            
            TryAttack();

        }

        


        if (isRotate)
        {
            Rotate();

        }
    }

    

    // 일반 공격 시도
    private void TryAttack()
    {

        if(!Player.currentAttack)
        {
            if(!Player.attackStartable)
            {
                return;
            }

            else
            {

                // 이동 중단
                Player.isMove = false;

                // 이동 불가능으로 설정
                //Player.canMove = false;

                // 공격 불가능으로 설정
                Player.attackStartable = false;

                // 현재 공격 중으로 설정
                Player.currentAttack = true;
                anim.SetBool("Punch", true);
                anim.SetInteger("State", 1);

                attackState = AttackState.ATTACK1;




            }
        }

        else
        {
            if (attackState == AttackState.ATTACK2)
            {
                return;
            }

            else
            {
                attackState += 1;
            }
        }
            return;

        
    }

    public void AttackStart_Hand()
    {

        attackState = AttackState.NONE;
        anim.SetInteger("State", 0);
    }

    public void AttackEnd_Hand()
    {
        if (attackState == AttackState.ATTACK2)
        {
            anim.SetInteger("State", 2);

        }

        else if (attackState != AttackState.ATTACK2)
        {
            Player.currentAttack = false;
            anim.SetBool("Punch", false);

            anim.SetInteger("State", 0);

            attackState = AttackState.NONE;

            // 공격 딜레이
            StartCoroutine(AttackStartDelay());
        }
    }


    // 일반 공격(펀치)
    public void OnHandAttackCollision_Right()
    {



        attackcollision_Right.SetActive(true);

    }

    public void OnHandAttackCollision_Left()
    {



        attackcollision_Left.SetActive(true);

    }

    // 일반 공격(펀치) 후 행동 상태 비활성화
    public void PunchAttackToIdle()
    {
        Player.currentAttack = false;
        anim.SetBool("Punch", false);

        anim.SetInteger("State", 0);

        attackState = AttackState.NONE;

        StartCoroutine(AttackStartDelay());
    }

    // 일반 공격(펀치) 딜레이
    private IEnumerator AttackStartDelay()
    {
        yield return new WaitForSeconds(0.2f);

        //Player.canMove = true;

        yield return new WaitForSeconds(0.05f);

        Player.attackStartable = true;

    }





    
    
    // 공격 도중 이동 시 공격 중단
    private void StopAttack()
    {
        if (Player.currentAttack && Player.isMove)
        {
            
            
            anim.Play("Idle");
            anim.SetBool("Punch", false);
            anim.SetInteger("State", 0);
            Player.currentAttack = false;
            Player.attackStartable = true;
            

        }
    }

    
    

    

    

    // 공격하는 방향 설정
    private void SetAttackTarget()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            lookAtTarget = new Vector3(hit.point.x - transform.position.x, 0.0f, hit.point.z - transform.position.z);
            playerRot = Quaternion.LookRotation(lookAtTarget);
            isRotate = true;
        }
    }

    // 설정한 방향으로 방향 전환
    private void Rotate()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, playerRot, rotateSpeed * Time.deltaTime);

        StartCoroutine(RotateCoroutine());
    }


    // 일정 시간 후 방향 전환 종료
    IEnumerator RotateCoroutine()
    {
        yield return new WaitForSeconds(rotateSeconds);
        isRotate = false;
    }



}
