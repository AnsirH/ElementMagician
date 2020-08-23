using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SwordController : MonoBehaviour
{


    // 공격 범위
    [SerializeField]
    private GameObject attackCollision;



    private PlayerController Player;

    // 공격 시작 가능 체크
    //private bool attackStartable = true;


    // 공격 상태
    public enum AttackState
    {
        NONE,
        ATTACK1,
        ATTACK2
    }

    public AttackState attackState = AttackState.NONE;


    // 공격 방향
    private Quaternion playerRot;
    private Vector3 lookAtTarget;

    // 방향 전환 체크
    private bool isRotate = false;

    // 방향 전환 속도
    public float rotateSpeed = 18.0f;

    private float rotateSeconds = 0.2f;


    
    Animator anim;


    private bool getCanElementMagic;

    private void Awake()
    {
        Player = FindObjectOfType<PlayerController>();
        anim = transform.GetComponent<Animator>();
    }

    private void Update()
    {
        getCanElementMagic = Player.canElementMagic;

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

    private void StopAttack()
    {
        if (Player.currentAttack && Player.isMove)
        {

            
            anim.Play("Idle");
            anim.SetInteger("State", 0);
            Player.currentAttack = false;
            Player.attackStartable = true;
            
            

        }
    }



    private void TryAttack()
    {

        // 현재 행동 중인지 체크
        if (!Player.currentAttack)
        {
            

            // 공격 가능한 상태인지 확인
            // 공격 가능한 상태가 아니라면
            if (!Player.attackStartable)
                return;

            // 공격 가능한 상태라면
            else
            {
                // 이동 불가능으로 설정
                //Player.canMove = false;
                Player.isMove = false;


                // 공격 불가능으로 설정
                Player.attackStartable = false;

                // 현재 행동 중으로 설정
                Player.currentAttack = true;
                anim.SetInteger("State", 1);

                // 공격 상태를 공격1로 설정
                attackState = AttackState.ATTACK1;
            }
        }

        // 현재 공격 중이라면
        else
        {

            // 현재 공격 상태가 공격2인지 확인
            if (attackState == AttackState.ATTACK2)
            {
                return;
            }

            else
            {
                attackState += 1;
            }
        }
    }

    

    // 애니메이션 이벤트들
    public void AttackStart()
    {
        attackState = AttackState.NONE;
        anim.SetInteger("State", 0);
    }

    public void AttackEnd()
    {
        if(attackState == AttackState.ATTACK2)
        {
            anim.SetInteger("State", 2);

        }

        else if(attackState != AttackState.ATTACK2)
        {
            Player.currentAttack = false;

            anim.SetInteger("State", 0);

            attackState = AttackState.NONE;

            // 공격 딜레이
            StartCoroutine(AttackStartDelay());
        }
    }

    public void AttackToIdle()
    {
        Player.currentAttack = false;
        anim.SetInteger("State", 0);

        attackState = AttackState.NONE;

        // 공격 딜레이
        StartCoroutine(AttackStartDelay());

    }

    private IEnumerator AttackStartDelay()
    {
        yield return new WaitForSeconds(0.3f);

        //Player.canMove = true;

        yield return new WaitForSeconds(0.2f);

        Player.attackStartable = true;
    }

    public void OnAttackCollision()
    {
        attackCollision.SetActive(true);
    }

    







    private void SetAttackTarget()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            lookAtTarget = new Vector3(hit.point.x - transform.position.x, 0.0f, hit.point.z - transform.position.z);
            playerRot = Quaternion.LookRotation(lookAtTarget);
            isRotate = true;
        }
    }

    private void Rotate()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, playerRot, rotateSpeed * Time.deltaTime);

        StartCoroutine(RotateCoroutine());
    }

    IEnumerator RotateCoroutine()
    {
        yield return new WaitForSeconds(rotateSeconds);
        isRotate = false;
    }
}
