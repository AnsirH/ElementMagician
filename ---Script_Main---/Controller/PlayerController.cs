using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using TreeEditor;
using UnityEditorInternal;
using UnityEngine;

public class PlayerController:MonoBehaviour
{
    

    private Rigidbody rb;


    [SerializeField]
    private GameObject SetElementsPrefab = null;

    [SerializeField]
    private GameObject ChargeElementsPrefab = null;

    [SerializeField]
    private GameObject CancelElementsPrefab = null;

    public GameObject PortalPrefab = null;

    // 속도 변수
    float magicRunSpeed = 9.0f;
    float runSpeed = 5.0f;

    // 적용된 이동 속도
    public float applySpeed;

    // 회전 속도
    float rotateSpeed = 13.0f;

    // 원소를 입력받을 수 있는 상태인지 체크 -> CheckElements에서 현재 원소가 4개 이상이라면 false, 마법 실행 하면서 true 반환
    [HideInInspector]
    public bool canReceiveElements = true;

    // 원소를 확정할 수 있는지 체크 -> 한 번 원소를 확정한다면 false 반환, 마법을 사용하면 true반환
    [HideInInspector]
    public bool canSetElements = true;

    // 원소 마법을 사용할 수 있는 상태인지 체크합니다.
    [HideInInspector]
    public bool canElementMagic = false;

    public Animator[] elementImageAnim;


    private bool isJump = false;


    public GameObject Potal;

    [HideInInspector]
    public string currentMagicSoundName;

    public enum MagicAnim
    {
        SHORT, LONG, HAND_UP, JUMPSMASH_GROUND, GROUND, KEEPING, UPPERCUT
    }



    public MagicAnim currentAnim = MagicAnim.SHORT;

    // 원소 종류
    public enum Elements
    {
        FIRE,
        WATER,
        THUNDER,
        HEAL
    }


    public enum ElementMagicList
    {
        // 원소 1개
        FIRE1,
        WATER1,
        THUNDER1,
        HEAL1,
        
        // 원소 2개
        FIRE2,
        WATER2,
        THUNDER2,
        HEAL2,

        STEAM,
        LASER_FIRE,
        FIRE_HEAL,

        HOT_WATER,
        LASER_WATER,
        WATER_HEAL,

        EXPLOSION,
        THUNDER_WATER,
        LASER_HEAL,
        
        HEAL_FIRE,
        HEAL_WATER,
        HEAL_THUNDER,

        // 원소 3개
        FIREMIX1,
        WATERMIX1,
        THUNDERMIX1,
        HEALMIX1,
        FIRE3,
        WATER3,
        THUNDER3,
        HEAL3,


        // 증기 마법
        STEAM_FIRE1,
        STEAM_WATER1,
        STEAM_THUNDER1,
        STEAM_HEAL1,
        WATERSMASH,
        WATER_THUNDERWAVE,



        // 원소 4개
        FIREMIX2,
        WATERMIX2,
        THUNDERMIX2,
        HEALMIX2,
        FIRE4,
        WATER4,
        THUNDER4,
        HEAL4,

        // 증기 마법
        STEAM_FIRE2,
        STEAM_WATER2,
        STEAM_THUNDER2,
        STEAM_HEAL2,
        STEAM2,

        WATER_FIREWALL,
        LAVA,
        FIRE2_THUNDER2


    }

    public ElementMagicList currentMagic;

    // 각 원소의 활성화 여부
    [HideInInspector]
    public bool OnFire = false;
    [HideInInspector]
    public bool OnWater = false;
    [HideInInspector]
    public bool OnThunder = false;
    [HideInInspector]
    public bool OnHeal = false;


    // 입력받은 불 원소의 개수
    [HideInInspector]
    public int FireElementNum = 0;
    [HideInInspector]
    public int WaterElementNum = 0;
    [HideInInspector]
    public int ThunderElementNum = 0;
    [HideInInspector]
    public int HealElementNum = 0;


    // 현재 입력받은 원소의 배열
    [HideInInspector]
    public List<Elements> currentElements = new List<Elements>();

    [HideInInspector]
    public List<int> elementMagicNumber = new List<int>();


    private ElementMagicController elementMagicController;



    // 마우스 이동 변수
    private Vector3 targetPosition;
    private Quaternion playerRot;
    private Vector3 lookAtTarget;

    public bool isStumble = false;

    // 이동 가능 여부
    [HideInInspector]
    public bool canMove = true;

    // 현재 이동 체크
    [HideInInspector]
    public bool isMove = false;

    // 달리기 상태 체크
    private bool isRun = false;

    // 플레이어 행동 상태 체크
    [HideInInspector]
    public bool currentAction = false;

    [HideInInspector]
    public bool currentAttack = false;

    [HideInInspector]
    public bool attackStartable = true;

    // 기모으기 상태 체크
    [HideInInspector]
    public bool isCharge = false;

    // 레이어 확인
    public LayerMask layerMask_Move = -1;

    public LayerMask layerMask_Obtacle = -1;



    // 애니메이션
    Animator anim;

    // 장애물 체크 거리
    public float obtacleRayDistance = 1.0f;
    
    // 무기 종류
    public enum weaponState { Hand, Sword, Staff }

    // 현재 무기
    public weaponState currentRightWeapon = weaponState.Hand;
    public weaponState currentLeftWeapon = weaponState.Hand;

    // 장애물 체크 레이 생성 위치
    private Transform obtacleRayOrigin;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {

        rb = GetComponent<Rigidbody>();
        elementMagicController = transform.GetComponent<ElementMagicController>();
        anim = transform.GetComponent<Animator>();
        applySpeed = runSpeed;
        obtacleRayOrigin = transform.GetChild(2);
    }


    private void Update()
    {


        // 현재 무기에 따른 공격 유형 설정
        //SetCurrentWeapon();

        


        ShiftRunning();

        if(Input.GetKeyDown(KeyCode.Space) && !isJump && !currentAttack)
        {
            SetTargetPosition();
            transform.rotation = Quaternion.Slerp(transform.rotation, playerRot, 5.0f);

            isJump = true;
            isMove = false;
            canMove = false;
            anim.SetTrigger("Jump");
            Potal = Instantiate(PortalPrefab, transform.position, transform.rotation);
        }

        if (isJump)
        {

            Jump();
        }


        // 원소를 받을 수 있는 상태라면
        if (canReceiveElements)
        {
            // 원소 입력받은 후 원소 리스트에 추가
            ReceiveElements();


            // 원소 검사 후 활성화(불필요?)
            // CheckElements();


        }



        // 원소를 활성화할 것인지 취소할 것인지 확인합니다.
        SetOrCancelElements();


    }

    // ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ


    private void Jump()
    {

        transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * 10.0f);

        //rb.velocity = transform.forward * 10.0f;



    }



    public IEnumerator DestroyPotal(GameObject potal)
    {
        yield return new WaitForSeconds(0.6f);
        for(int i = 0; i < potal.transform.childCount; i++)
        {
            for (int j = 0; j < potal.transform.GetChild(i).childCount; j++)
            {
                for(int x = 0; x < potal.transform.GetChild(i).GetChild(j).childCount; x++)
                {
                    potal.transform.GetChild(i).GetChild(j).GetChild(x).GetComponent<ParticleSystem>().Stop();

                }
            }
        }

        yield return new WaitForSeconds(3.0f);
        Destroy(potal);
    }

    public void Teleport()
    {



        //transform.position = Vector3.Lerp(transform.position, Potal.transform.GetChild(1).transform.position + new Vector3(0, -0.8f, 0), 50.0f);

        StartCoroutine(DelayVelocity());

        StartCoroutine(DestroyPotal(Potal));
        isJump = false;

    }

    public void TeleportEnd()
    {

        canMove = true;
    }

    
    IEnumerator DelayVelocity()
    {
        rb.velocity = transform.forward * 65.0f;
        yield return new WaitForSeconds(0.1f);

        rb.velocity = Vector3.zero;
    }

    // ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ


    private void LateUpdate()
    {


        // 현재 행동 비활성화 일 때
        if (!currentAction)
        {

            if (Input.GetMouseButton(1) && canMove)
            {

                // 목적지 설정
                SetTargetPosition();
            }
        }

        if (isMove && canMove)
        {

            // 이동 시도
            Move();
        }


        MoveAnim();
    }

    // 목표 설정(마우스 좌표)
    private void SetTargetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        

        RaycastHit[] hits;

        hits = Physics.RaycastAll(ray, Mathf.Infinity, layerMask_Move);
        targetPosition = hits[0].point;
        lookAtTarget = new Vector3(targetPosition.x - transform.position.x, 
                                   0.0f, 
                                   targetPosition.z - transform.position.z);
        playerRot = Quaternion.LookRotation(lookAtTarget);

        isMove = true;
    }

    // 이동 실행
    private void Move()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, playerRot, rotateSpeed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, applySpeed * Time.deltaTime);




        if (transform.position == targetPosition)
        {
            isMove = false;

        }

        if(Input.GetKeyDown(KeyCode.S))
        {
            isMove = false;
        }

        
    }

    // 걷기 애니메이션 실행
    private void MoveAnim()
    {



        if(isMove)
        {
            if(currentLeftWeapon == weaponState.Staff)
                anim.SetBool("RunWithWeapon", true);

            else
                anim.SetBool("Run", true);


        }

        else
        {
            if(currentLeftWeapon == weaponState.Staff)
                anim.SetBool("RunWithWeapon", false);

            else
                anim.SetBool("Run", false);

        }
        //anim.SetBool("Run", false);


        //if (isMove && currentLeftWeapon == weaponState.Staff)
        //{
        //    anim.SetBool("RunWithWeapon", true);

        //}

        //else
        //    anim.SetBool("RunWithWeapon", false);


        

        
    }

    

    // 이동속도 변환
    public void ShiftRunning()
    {



        if(isRun)
        {
            applySpeed = magicRunSpeed;
        }

        else
            applySpeed = runSpeed;

        
    }

    


    // 현재 무기 상태 반환
    public void SetCurrentWeapon()
    {
        transform.GetComponent<ElementMagicController>().enabled = false;
        switch(currentRightWeapon)
        {
        case weaponState.Hand:
                transform.GetComponent<HandController>().enabled = true;
                transform.GetComponent<SwordController>().enabled = false;

        break;

        case weaponState.Sword:
                transform.GetComponent<HandController>().enabled = false;
                transform.GetComponent<SwordController>().enabled = true;
                transform.GetComponent<StaffController>().enabled = false; 

        break;


        }

        if(currentRightWeapon == weaponState.Hand)
        {
            switch (currentLeftWeapon)
            {
                case weaponState.Hand:
                    transform.GetComponent<HandController>().enabled = true;
                    transform.GetComponent<StaffController>().enabled = false;
                    break;


                case weaponState.Staff:
                    transform.GetComponent<HandController>().enabled = false;
                    transform.GetComponent<StaffController>().enabled = true;
                    break;
            }
        }

        
    }


    // 원소 입력받기
    private void ReceiveElements()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            OnFire = true;
            SoundManager.instance.PlaySoundEffect("GetElement");

            currentElements.Add(Elements.FIRE);
            ChargeElementsPrefab.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
            elementImageAnim[0].SetTrigger("OnFire");

        }

        else if(Input.GetKeyDown(KeyCode.W))
        {
            OnWater = true;
            SoundManager.instance.PlaySoundEffect("GetElement");

            currentElements.Add(Elements.WATER);
            ChargeElementsPrefab.transform.GetChild(1).GetComponent<ParticleSystem>().Play();
            elementImageAnim[1].SetTrigger("OnWater");



        }

        else if(Input.GetKeyDown(KeyCode.E))
        {
            OnThunder = true;
            SoundManager.instance.PlaySoundEffect("GetElement");

            currentElements.Add(Elements.THUNDER);
            ChargeElementsPrefab.transform.GetChild(2).GetComponent<ParticleSystem>().Play();
            elementImageAnim[2].SetTrigger("OnThunder");


        }

        else if(Input.GetKeyDown(KeyCode.R))
        {
            OnHeal = true;
            SoundManager.instance.PlaySoundEffect("GetElement");

            currentElements.Add(Elements.HEAL);
            ChargeElementsPrefab.transform.GetChild(3).GetComponent<ParticleSystem>().Play();
            elementImageAnim[3].SetTrigger("OnHeal");


        }

        // 현재 입력받은 원소가 4개가 되었다면 입력을 그만 받게합니다.
        if (currentElements.Count >= 4)
        {
            canReceiveElements = false;
        }

    }

    private void ElementMagicControllerAble()
    {
        transform.GetComponent<HandController>().enabled = false;
        transform.GetComponent<SwordController>().enabled = false;
        transform.GetComponent<StaffController>().enabled = false;
        transform.GetComponent<ElementMagicController>().enabled = true;

    }

    // ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
    // 원소 확정 또는 취소
    private void SetOrCancelElements()
    {

        if (canSetElements)
        {
            if (Input.GetKeyDown(KeyCode.D) && currentElements.Count > 0)
            {

                // 원소 마법 사용 가능
                canElementMagic = true;

                // D키 사용 불가능
                canSetElements = false;

                canReceiveElements = false;

                // 배열에 각각의 원소가 몇 개씩있는지 검사
                CountElement();

                // 원소 확정 애니메이션
                anim.SetTrigger("ChargeSet");

                SetElementMagic();

                // 원소 확정 이펙트
                StartCoroutine(DelayEffect());


                ElementMagicControllerAble();

            }
        }
        

        if (Input.GetKeyDown(KeyCode.F) && currentElements.Count > 0)
        {
            // D키 사용 가능
            canSetElements = true;

            // 원소 마법 사용 불가능
            canElementMagic = false;

            canReceiveElements = true;


            currentElements.Clear();
            elementMagicNumber.Clear();
            


            FireElementNum = 0;
            WaterElementNum = 0;
            ThunderElementNum = 0;
            HealElementNum = 0;

            canReceiveElements = true;
            GameObject.Find("GameUI").GetComponent<ShowElements>().delAll = true;
            CancelElementsPrefab.GetComponent<ParticleSystem>().Play();
            SetCurrentWeapon();
            currentAnim = MagicAnim.SHORT;
            SoundManager.instance.PlaySoundEffect("CancelMagic");

        }



    }


    // 원소 확정 시 발동 이펙트
    IEnumerator DelayEffect()
    {
        yield return new WaitForSeconds(0.5f);

        //SetElementsPrefab.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
        //SetElementsPrefab.transform.GetChild(1).GetComponent<ParticleSystem>().Play();
        //SetElementsPrefab.GetComponent<ParticleSystem>().Play();
        SetElementsPrefab.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        SetElementsPrefab.SetActive(false);

    }

    // 현재 원소 리스트에 있는 각 원소의 개수를 측정합니다. -> 마법의 위력 결정
    private void CountElement()
    {


        foreach(Elements element in currentElements)
        {
            

            switch (element)
            {
                case Elements.FIRE:
                    FireElementNum += 1;
                    elementMagicNumber.Add(1);
            break;

                case Elements.WATER:
                    WaterElementNum += 1;
                    elementMagicNumber.Add(2);


                    break;

            case Elements.THUNDER:
                    ThunderElementNum += 1;
                    elementMagicNumber.Add(3);

                    break;

            case Elements.HEAL:
                    HealElementNum += 1;
                    elementMagicNumber.Add(4);

                    break;
            }

            

        }

        

    }

    
    private void SetElementMagic()
    {

        // 1개
        if(currentElements.Count == 1)
        {
            if(elementMagicNumber[0] == 1)
            {
                currentMagic = ElementMagicList.FIRE1;
                currentMagicSoundName = "Fire1";
            }


            else if(elementMagicNumber[0] == 2)
            {
                currentMagic = ElementMagicList.WATER1;
                currentMagicSoundName = "Water1";

            }

            else if(elementMagicNumber[0] == 3)
            {
                currentMagic = ElementMagicList.THUNDER1;
                currentMagicSoundName = "Thunder1";

            }

            else if(elementMagicNumber[0] == 4)
            {
                currentMagic = ElementMagicList.HEAL1;
                elementMagicController.isHealMagic = true;
                currentMagicSoundName = "Heal1";

            }

        }

        // 2개
        else if(currentElements.Count == 2)
        {
            if(elementMagicNumber[0] == 1 && elementMagicNumber[1] == 1)
            {
                currentMagic = ElementMagicList.FIRE2;
                currentMagicSoundName = "Fire2";

            }

            else if(elementMagicNumber[0] == 1 && elementMagicNumber[1] == 2)
            {
                currentMagic = ElementMagicList.STEAM;
                currentMagicSoundName = "Steam";

            }

            else if(elementMagicNumber[0] == 1 && elementMagicNumber[1] == 3)
            {
                currentMagic = ElementMagicList.LASER_FIRE;
                currentMagicSoundName = "Laser_Fire";

            }

            else if(elementMagicNumber[0] == 1 && elementMagicNumber[1] == 4)
            {
                currentMagic = ElementMagicList.FIRE_HEAL;
                currentMagicSoundName = "Fire_Heal";

            }


            if(elementMagicNumber[0] == 2 && elementMagicNumber[1] == 1)
                currentMagic = ElementMagicList.HOT_WATER;

            else if(elementMagicNumber[0] == 2 && elementMagicNumber[1] == 2)
                currentMagic = ElementMagicList.WATER2;

            else if(elementMagicNumber[0] == 2 && elementMagicNumber[1] == 3)
                currentMagic = ElementMagicList.LASER_WATER;

            else if(elementMagicNumber[0] == 2 && elementMagicNumber[1] == 4)
                currentMagic = ElementMagicList.WATER_HEAL;


            if(elementMagicNumber[0] == 3 && elementMagicNumber[1] == 1)
                currentMagic = ElementMagicList.EXPLOSION;

            else if(elementMagicNumber[0] == 3 && elementMagicNumber[1] == 2)
                currentMagic = ElementMagicList.THUNDER_WATER;

            else if(elementMagicNumber[0] == 3 && elementMagicNumber[1] == 3)
                currentMagic = ElementMagicList.THUNDER2;

            else if(elementMagicNumber[0] == 3 && elementMagicNumber[1] == 4)
                currentMagic = ElementMagicList.LASER_HEAL;


            if(elementMagicNumber[0] == 4 && elementMagicNumber[1] == 1)
            {
                elementMagicController.isHealMagic = true;
                currentMagic = ElementMagicList.HEAL_FIRE;

            }

            else if(elementMagicNumber[0] == 4 && elementMagicNumber[1] == 2)
            {
                elementMagicController.isHealMagic = true;
                currentMagic = ElementMagicList.HEAL_WATER;

            }

            else if(elementMagicNumber[0] == 4 && elementMagicNumber[1] == 3)
            {
                elementMagicController.isHealMagic = true;
                currentMagic = ElementMagicList.HEAL_THUNDER;

            }

            else if(elementMagicNumber[0] == 4 && elementMagicNumber[1] == 4)
            {
                elementMagicController.isHealMagic = true;
                currentMagic = ElementMagicList.HEAL2;

            }
        }



        // 3개
        else if(currentElements.Count == 3)
        {
            if(elementMagicNumber[0] == 1 && elementMagicNumber[1] == 1 && elementMagicNumber[2] == 1)
            {
                currentMagic = ElementMagicList.FIRE3;
                currentAnim = MagicAnim.LONG;
                currentMagicSoundName = "Fire3";
            }




            else if(elementMagicNumber[0] == 2 && elementMagicNumber[1] == 2 && elementMagicNumber[2] == 2)
                currentMagic = ElementMagicList.WATER3;



            else if(elementMagicNumber[0] == 3 && elementMagicNumber[1] == 3 && elementMagicNumber[2] == 3)
            {
                currentMagic = ElementMagicList.THUNDER3;
                currentAnim = MagicAnim.LONG;


            }


            else if(elementMagicNumber[0] == 4 && elementMagicNumber[1] == 4 && elementMagicNumber[2] == 4)
                currentMagic = ElementMagicList.HEAL3;


            else if (elementMagicNumber[0] == 2 && elementMagicNumber[1] == 1 && elementMagicNumber[2] == 3)
            {
                currentMagic = ElementMagicList.WATERSMASH;
                currentAnim = MagicAnim.JUMPSMASH_GROUND;
            }

            else if (elementMagicNumber[0] == 3 && elementMagicNumber[1] == 2 && elementMagicNumber[2] == 3)
            {
                currentMagic = ElementMagicList.WATER_THUNDERWAVE;
                currentAnim = MagicAnim.UPPERCUT;
            }

            // 증기 마법
            else if (elementMagicNumber[0] == 1 && elementMagicNumber[1] == 2)
            {
                if(elementMagicNumber[2] == 1)
                {
                    currentMagic = ElementMagicList.STEAM_FIRE1;
                    currentAnim = MagicAnim.KEEPING;
                }

                else if(elementMagicNumber[2] == 2)
                {
                    currentMagic = ElementMagicList.STEAM_WATER1;
                    currentAnim = MagicAnim.HAND_UP;
                }

                else if(elementMagicNumber[2] == 3)
                {
                    currentMagic = ElementMagicList.STEAM_THUNDER1;
                    currentAnim = MagicAnim.GROUND;
                }

                else if(elementMagicNumber[2] == 4)
                {
                    currentMagic = ElementMagicList.STEAM_HEAL1;
                    currentAnim = MagicAnim.GROUND;
                }
            }

            else
            {
                if(elementMagicNumber[0] == 1)
                {
                    currentMagic = ElementMagicList.FIREMIX1;
                }
                
                else if (elementMagicNumber[0] == 2)
                {
                    currentMagic = ElementMagicList.WATERMIX1;

                }

                else if (elementMagicNumber[0] == 3)
                {
                    currentMagic = ElementMagicList.THUNDERMIX1;

                }

                else if (elementMagicNumber[0] == 4)
                {
                    currentMagic = ElementMagicList.HEALMIX1;

                }
            }
        }





        // 4개
        else if(currentElements.Count == 4)
        {
            if(elementMagicNumber[0] == 1 && elementMagicNumber[1] == 1 && elementMagicNumber[2] == 1 && elementMagicNumber[3] == 1)
            {
                currentMagic = ElementMagicList.FIRE4;
                currentAnim = MagicAnim.HAND_UP;


            }

            else if(elementMagicNumber[0] == 2 && elementMagicNumber[1] == 2 && elementMagicNumber[2] == 2 && elementMagicNumber[3] == 2)
            {
                currentMagic = ElementMagicList.WATER4;
                currentAnim = MagicAnim.GROUND;


            }

            else if(elementMagicNumber[0] == 3 && elementMagicNumber[1] == 3 && elementMagicNumber[2] == 3 && elementMagicNumber[3] == 3)
            {
                currentMagic = ElementMagicList.THUNDER4;
                currentAnim = MagicAnim.GROUND;
            }

            else if(elementMagicNumber[0] == 1 && elementMagicNumber[1] == 2 && elementMagicNumber[2] == 2 && elementMagicNumber[3] == 1)
            {
                currentMagic = ElementMagicList.WATER_FIREWALL;
                currentAnim = MagicAnim.GROUND;
            }

            else if(elementMagicNumber[0] == 1 && elementMagicNumber[1] == 3 && elementMagicNumber[2] == 1 && elementMagicNumber[3] == 3)
            {
                currentMagic = ElementMagicList.FIRE2_THUNDER2;
                currentAnim = MagicAnim.LONG;
            }

            else if(elementMagicNumber[0] == 2 && elementMagicNumber[1] == 2 && elementMagicNumber[2] == 1 && elementMagicNumber[3] == 1)
            {
                currentMagic = ElementMagicList.LAVA;
                currentAnim = MagicAnim.GROUND;
            }

            // 증기 마법
            else if(elementMagicNumber[0] == 1 && elementMagicNumber[1] == 2)
            {
                if(elementMagicNumber[2] == 1 && elementMagicNumber[3] == 1)
                {
                    currentMagic = ElementMagicList.STEAM_FIRE2;
                    currentAnim = MagicAnim.HAND_UP;
                }

                else if(elementMagicNumber[2] == 2 && elementMagicNumber[3] == 2)
                {
                    currentMagic = ElementMagicList.STEAM_WATER2;
                    currentAnim = MagicAnim.HAND_UP;
                }

                else if(elementMagicNumber[2] == 3 && elementMagicNumber[3] == 3)
                {
                    currentMagic = ElementMagicList.STEAM_THUNDER2;
                    currentAnim = MagicAnim.HAND_UP;
                }

                else if(elementMagicNumber[2] == 4 && elementMagicNumber[3] == 4)
                {
                    currentMagic = ElementMagicList.STEAM_HEAL2;
                    currentAnim = MagicAnim.GROUND;
                }

                else if(elementMagicNumber[2] == 1 && elementMagicNumber[3] == 2)
                {
                    currentMagic = ElementMagicList.STEAM2;
                    currentAnim = MagicAnim.GROUND;
                }

                else
                {
                    if(elementMagicNumber[0] == 1)
                    {
                        currentMagic = ElementMagicList.FIREMIX2;
                    }

                    else if(elementMagicNumber[0] == 2)
                    {
                        currentMagic = ElementMagicList.WATERMIX2;

                    }

                    else if(elementMagicNumber[0] == 3)
                    {
                        currentMagic = ElementMagicList.THUNDERMIX2;

                    }

                    else if(elementMagicNumber[0] == 4)
                    {
                        currentMagic = ElementMagicList.HEALMIX2;

                    }
                }
            }

            else
            {
                if (elementMagicNumber[0] == 1)
                {
                    currentMagic = ElementMagicList.FIREMIX2;
                }

                else if (elementMagicNumber[0] == 2)
                {
                    currentMagic = ElementMagicList.WATERMIX2;

                }

                else if (elementMagicNumber[0] == 3)
                {
                    currentMagic = ElementMagicList.THUNDERMIX2;

                }

                else if (elementMagicNumber[0] == 4)
                {
                    currentMagic = ElementMagicList.HEALMIX2;

                }
            }


            

            
        }

        Debug.Log(currentMagic);

    }

    public void Stumble()
    {

        rb.AddForce((-transform.forward) * 190.0f, ForceMode.Acceleration);
    }


   

    public void EffectSound()
    {

        SoundManager.instance.PlaySoundEffect("SetMagic");

    }
}
