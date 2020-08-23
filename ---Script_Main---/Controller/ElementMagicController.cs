using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// 원소를 확정했을 때 사용되는 컴포넌트
public class ElementMagicController : MonoBehaviour
{

    PlayerController Player;

    PlayerMagicManager MagicManager;

    Animator anim;

    // 현재 사용할 마법을 가리키는 변수
    public GameObject currentMagic = null;

    // 사용할 마법이 회복 마법인지 확인하는 변수
    public bool isHealMagic = false;

    // 사용할 마법의 모션이 손을 올리는 모션인지 확인하는 변수
    public bool HandUpMagicMotion = false;

    // 회전을 할 것인지 확인하는 변수
    bool isRotate = false;

    // 폭발 마법이 사용되었는지 확인하는 변수
    bool isExplosion = false;

    Vector3 lookAtTarget;
    Quaternion playerRot;

    float timer;

    string currentAnimation;
























    private void Awake()
    {
        Player = GetComponent<PlayerController>();
        MagicManager = GetComponent<PlayerMagicManager>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {


        if (Input.GetMouseButton(0))
        {

            // 클릭한 좌표를 공격 좌표로 설정
            if (!(anim.GetCurrentAnimatorStateInfo(0).IsName("GroundAreaMagic") || anim.GetCurrentAnimatorStateInfo(0).IsName("JumpMagic")))
                SetAttackTarget();


            // 애니메이션 모션 확인(짧은 마법)
            if(Player.currentAnim == PlayerController.MagicAnim.SHORT || Player.currentAnim == PlayerController.MagicAnim.GROUND 
                || Player.currentAnim == PlayerController.MagicAnim.JUMPSMASH_GROUND || Player.currentAnim == PlayerController.MagicAnim.UPPERCUT)
            {
                ElementMagic1();

            }

            // 애니메이션 모션 확인(유지 마법)
            else if(Player.currentAnim == PlayerController.MagicAnim.KEEPING)
            {
                ElementMagic2();
                CheckLongMagicTime("ElementMagic_Keeping");
                currentAnimation = "ElementMagic_Keeping_In";
            }



            // 애니메이션 모션 확인(손을 올려든 모션)
            else if(Player.currentAnim == PlayerController.MagicAnim.HAND_UP)
            {
                ElementMagic2();
                CheckLongMagicTime("ElementMagic_HandUp");
                currentAnimation = "ElementMagic_HandUp_In";

            }

            // 애니메이션 모션 확인(긴 마법)
            else if(Player.currentAnim == PlayerController.MagicAnim.LONG)
            {
                ElementMagic2();
                CheckLongMagicTime("ElementMagic2");
                currentAnimation = "ElementMagic2";


            }





        }

            // 마우스 우클릭을 뗐을 경우 마법 중지
        if (Player.currentAttack && !Input.GetMouseButton(0))
        {
            StopMagic();



        }



        if(isRotate)
            if(Player.currentAnim == PlayerController.MagicAnim.LONG || Player.currentAnim == PlayerController.MagicAnim.HAND_UP || Player.currentAnim == PlayerController.MagicAnim.KEEPING)
                RotateWithMagic();
            else
                Rotate();

        if(isExplosion)
            FallDown();
    }

    public void StopMagic()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).IsName(currentAnimation))
        {
            if(Player.currentAnim == PlayerController.MagicAnim.HAND_UP)
                anim.SetBool("ElementMagic_HandUp", false);

            else if(Player.currentAnim == PlayerController.MagicAnim.KEEPING)
                anim.SetBool("ElementMagic_Keeping", false);

            else
                anim.SetBool("ElementMagic2", false);
        }
    }

    // 짧은 마법을 실행하는 함수.
    private void ElementMagic1()
    {
        
        // 플레이어가 공격을 하고 있지 않을 때만 실행합니다.
        if (!Player.currentAttack)
        {
            // 마법을 사용할 수 없는 상태라면 return
            if (!Player.canElementMagic)
            {
                return;
            }

            else
            {

                // 이동 중단
                Player.isMove = false;

                // 이동 불가능으로 설정
                Player.canMove = false;


                // 현재 공격 중으로 설정
                Player.currentAttack = true;

                // 힐 마법인지 공격 마법인지 확인합니다. 
                if(isHealMagic)
                {
                    anim.SetTrigger("Heal");
                }
                else
                {
                    if(Player.currentAnim == PlayerController.MagicAnim.GROUND)
                        anim.SetTrigger("GroundMagic");

                    else if(Player.currentAnim == PlayerController.MagicAnim.SHORT)
                        anim.SetTrigger("ElementMagic1");

                    else if (Player.currentAnim == PlayerController.MagicAnim.JUMPSMASH_GROUND)
                    {
                        anim.SetTrigger("JumpMagic");
                        anim.applyRootMotion = true;
                    }

                    else if (Player.currentAnim == PlayerController.MagicAnim.UPPERCUT)
                        anim.SetTrigger("UppercutMagic");

                }



            }
        }

        else return;



    }

    // 긴 마법을 실행하는 함수.
    private void ElementMagic2()
    {

        // 플레이어가 공격을 하고 있지 않을 때만 실행합니다.
        if (!Player.currentAttack)
        {
            // 마법을 실행할 수 없는 상태라면 return
            if (!Player.canElementMagic)
            {
                return;
            }

            else
            {
                // 이동 중단
                Player.isMove = false;

                // 이동 불가능으로 설정
                Player.canMove = false;


                // 현재 공격 중으로 설정
                Player.currentAttack = true;
                if(isHealMagic)
                {
                    anim.SetTrigger("Heal");
                }
                else
                {
                    if(Player.currentAnim == PlayerController.MagicAnim.HAND_UP)
                        anim.SetBool("ElementMagic_HandUp", true);

                    else if(Player.currentAnim == PlayerController.MagicAnim.KEEPING)
                        anim.SetBool("ElementMagic_Keeping", true);

                    else
                        anim.SetBool("ElementMagic2", true);
                    //StartCoroutine(CheckTime_ElementMagic2());

                }



            }
        }

        else return;
    }

    // 긴 마법을 사용할 수 있는 최대 시간을 설정합니다.
    // 긴 마법을 사용하는 도중 우클릭을 떼면 마법을 중단할 수 있지만
    // 중단하지 않았을 경우 최대 시간이 지났을 때 마법을 중단합니다.(마법을 계속 지속하는 것을 막는 용도)
    private void CheckLongMagicTime(string magicName)
    {
        timer += Time.deltaTime;

        if(timer > 5.0f)
            anim.SetBool(magicName, false);
    }

    
    // ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
    #region animation clip event

    // (애니메이션 이벤트) 마법을 생성하는 함수
    public void Element()
    {
        SoundManager.instance.PlaySoundEffect(Player.currentMagicSoundName);
        // 사용할 마법이 회복 마법이라면 좌표를 플레이어로 설정합니다.
        if(isHealMagic)
        {
            MagicManager.LoadElementMagic(transform.gameObject);

        }

        // 사용할 마법이 공격 마법이라면 좌표를 StartPoint로 설정합니다.
        else
        {
            MagicManager.LoadElementMagic(MagicManager.StartPointMagic);

            if(Player.currentMagic == PlayerController.ElementMagicList.FIRE4)
            {
                FindObjectOfType<CameraController>().SmoothFactor = 0.035f;
                FindObjectOfType<CameraController>().dist = 15;
                FindObjectOfType<CameraController>().height = 22;
            }

            // 사용할 마법이 폭발 마법이라면 넘어지는 애니메이션을 재생하는 코루틴을 실행합니다.
            if(Player.currentMagic == PlayerController.ElementMagicList.EXPLOSION)
            {
                StartCoroutine(CheckExplosion());
            }

            if (!currentMagic.GetComponent<ParticleSystem>())
            {
                currentMagic.transform.parent = null;

            }

        }

    }

    // (애니메이션 이벤트) 마법을 끝내는 함수
    public void ElementEnd()
    {


        if(isHealMagic)
        {
            EndElementMagic();

            StartCoroutine(DestroyHeal(currentMagic));
        }

        

        else
        {
            if (currentMagic.GetComponent<ParticleSystem>())
            {
                currentMagic.transform.parent = null;

                if (Player.currentMagic == PlayerController.ElementMagicList.FIRE4)
                {
                    FindObjectOfType<CameraController>().dist = 9;
                    FindObjectOfType<CameraController>().height = 15;
                }

                // 사용한 마법이 폭발 마법이 아닌 경우에만 마법을 종료하는 함수를 실행해줍니다.
                // 폭발 마법이 사용된 경우에는 넘어지는 동안 움직일 수 없도록 설정하기 위함
                if(Player.currentMagic != PlayerController.ElementMagicList.EXPLOSION)
                {


                    EndElementMagic();

                }

                currentMagic.GetComponent<ParticleSystem>().Stop();


                StartCoroutine(DestroyMagic_Particle(currentMagic));

            }
            else
            {
                //currentMagic.GetComponentInChildren<ParticleSystem>().Stop();

                EndElementMagic();


                currentMagic.transform.parent = null;

                anim.applyRootMotion = false;


                StartCoroutine(DestroyMagic_GameObejct(currentMagic));

            }

        }
        Player.currentAnim = PlayerController.MagicAnim.SHORT;
        timer = 0.0f;
        
    }

    // 플레이어가 일어날 때 마법을 종료하는 함수를 실행해줍니다.
    public void GetUpEnd()
    {
        Player.isStumble = false;
        EndElementMagic();
    }

    #endregion 

    // 마법을 종료하는 함수(-> 이동 가능, 마법 실행 불가능, 공격 종료, 원소 초기화, 공격 상태 초기화)
    public void EndElementMagic()
    {
        SoundManager.instance.StopSE(Player.currentMagicSoundName);

        Player.canMove = true;
        Player.canElementMagic = false;
        Player.currentAttack = false;
        MagicManager.EndElementMagic();
        Player.SetCurrentWeapon();
        if(isHealMagic)
            isHealMagic = false;

    }

    // 힐 마법 파티클을 종료합니다.
    private IEnumerator DestroyHeal(GameObject magic)
    {
        yield return new WaitForSeconds(0.5f);

        currentMagic.GetComponent<ParticleSystem>().Stop();

        yield return new WaitForSeconds(1.2f);
        Destroy(magic);
    }

    // 짧은 마법 파티클을 종료합니다.
    private IEnumerator DestroyMagic_Particle(GameObject magic)
    {

        yield return new WaitForSeconds(2.6f);



        if(Player.currentMagic == PlayerController.ElementMagicList.FIRE4)
            FindObjectOfType<CameraController>().SmoothFactor = 0.1f;

        Destroy(magic);
    }

    private IEnumerator DestroyMagic_GameObejct(GameObject magic)
    {
        yield return new WaitForSeconds(5.3f);

        Destroy(magic);


    }

    // 넘어지는 애니메이션을 재생하고 폭발상태를 true로 바꿉니다.
    private IEnumerator CheckExplosion()
    {
        yield return new WaitForSeconds(0.1f);

        
        anim.SetTrigger("FallDown");

        isExplosion = true;
        Player.isStumble = true;
    }
        


    // 마우스 좌표를 받아와 공격할 방향으로 설정합니다.
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

    // 설정한 방향으로 방향 전환(짧은 마법일 때) 빠른 방향전환
    private void Rotate()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, playerRot, 18.0f * Time.deltaTime);
        StartCoroutine(RotateCoroutine());
    }

    // 설정한 방향으로 방향 전환(긴 마법일 때)
    private void RotateWithMagic()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, playerRot, 1.0f * Time.deltaTime);
        StartCoroutine(RotateCoroutine());
    }


    // 방향 전환 후 방향 전환 false로 설정
    IEnumerator RotateCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        isRotate = false;
    }

    // 넘어지는 애니메이션의 이벤트
    private void FallDown()
    {
        StartCoroutine(FallDownMoveEnd());
    }

    // 넘어지면서 뒤로 밀려나는 기능을 합니다.
    private IEnumerator FallDownMoveEnd()
    {
        yield return new WaitForSeconds(0.1f);

        Player.transform.Translate(new Vector3(0, 0, -1) * Time.deltaTime * 5.0f);


        yield return new WaitForSeconds(0.4f);

        isExplosion = false;
    }

}
