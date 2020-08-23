using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class StaffController : MonoBehaviour
{
    [SerializeField]
    private GameObject StaffAttackPrefab = null;

    [SerializeField]
    private GameObject StaffAttackCollision = null;

    // 공격 시작 가능한지 체크
    //private bool attackStartable = true;


    // 공격 위치
    private Vector3 explosionLocation;

    // 폭발 가능 체크
    private bool canexplosion = true;

    // 공격 방향 설정 변수
    private Quaternion playerRot;
    private Vector3 lookAtTarget;

    // 방향 전환 체크
    private bool isRotate = false;

    // 방향 전환 속도
    public float rotateSpeed = 18.0f;

    // 방향 전환 딜레이
    private float rotateDelay = 0.2f;

    // 기모으기 체크
    private bool isCharge;

    PlayerController Player;

    

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
            // 공격할 방향 설정
            SetAttackTarget();

            
            // 기본 공격 시도
            TryAttack();
            SetExplosionLocation();

            


        }

        if(isRotate)
        {
            Rotate();
        }

    }

    

    private void StopAttack()
    {
        if (Player.currentAttack && Player.isMove)
        {
            anim.Play("Idle");
            Player.isCharge = false;
            Player.currentAttack = false;
            Player.attackStartable = true;

        }
    }



    private void TryAttack()
    {
        if (!Player.currentAttack)
        {
            if (!Player.attackStartable)
            {
                return;
            }

            else
            {
                //Player.canMove = false;

                Player.isMove = false;

                Player.attackStartable = false;

                Player.currentAttack = true;
                anim.SetTrigger("StaffAttack");
            }
        }

        else
            return;
    }

    public void SetExplosionLocation()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, 30.0f))
        {
            explosionLocation = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            

        }

        else
            canexplosion = false;
        
    }

    public void OnStaffAttackCollision()
    {
        StaffAttackCollision.transform.position = explosionLocation;
        StaffAttackCollision.SetActive(true);
    }

    public void StaffAttackEffect()
    {
        if(canexplosion)
        {
            GameObject.Instantiate(StaffAttackPrefab, explosionLocation + new Vector3(0, 1, 0), Quaternion.identity);
            StartCoroutine(DestroyStaffAttackEffect());

        }

        else Debug.Log("사정거리를 벗어났습니다.");


        canexplosion = true;
    }

    IEnumerator DestroyStaffAttackEffect()
    {
        yield return new WaitForSeconds(1f);
        Destroy(GameObject.Find("EnergyExplosion(Clone)"));
    }

    public void StaffAttackToIdle()
    {
        Player.currentAttack = false;
        StartCoroutine(StaffAttackStartDelay());
    }

    private IEnumerator StaffAttackStartDelay()
    {
        yield return new WaitForSeconds(0.3f);

        //Player.canMove = true;

        yield return new WaitForSeconds(0.1f);
        Player.attackStartable = true;
    }

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

    private void Rotate()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, playerRot, rotateSpeed * Time.deltaTime);

        StartCoroutine(RotateCoroutine());
    }

    IEnumerator RotateCoroutine()
    {
        yield return new WaitForSeconds(rotateDelay);
        isRotate = false;
    }
}
