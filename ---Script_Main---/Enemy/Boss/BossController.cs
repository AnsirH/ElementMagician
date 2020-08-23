using System.Collections;
using System.Collections.Generic;
using System.Data;
using TreeEditor;
using UnityEditorInternal;
using UnityEngine;

public class BossController : MonoBehaviour
{
    PlayerController Player;

    float randomSkillNum;
    
    enum Skills
    {
        SKILL1,
        SKILL2,
        SKILL3,
        SKILL4,
        SKILL5
    }

    

    Skills currentPickSkill;

    bool canPickSkill = true;

    bool canSkill = true;

    bool canAttack = true;

    GameObject currentSkillPrefab;

    GameObject OutPutSkillPrefab;

    public GameObject SkillStartPoint;
    public GameObject Skill3StartPoint;
    public GameObject Skill4StartPoint;
    public GameObject Skill5StartPoint;
    public GameObject AttackStartPoint;

    public GameObject Skill1Prefab;
    public GameObject Skill2Prefab;
    public GameObject Skill3Prefab;
    public GameObject Skill4Prefab;
    public GameObject Skill5Prefab;


    public GameObject AttackPrefab;

    private Vector3 lookAtTarget;
    private Quaternion enemyRot;

    public GameObject AttackCollider;


    public float MoveSpeed = 0.0f;
    public bool isMove = true;
    public bool isHit = false;
    public float limitProximityDistance = 0.0f;

    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        Player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        Rotate();

        if(isMove && !isHit)
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
        if (transform.GetComponent<EnemyStat>().currentHp <= 0)
        {
            isMove = false;
            anim.SetBool("Death", true);
        }
    }

    private void CheckDistance()
    {
        if(Vector3.Distance(Player.transform.position, transform.position) <= limitProximityDistance)
        {
            isMove = false;
            if(canAttack)
            {
                anim.SetTrigger("Attack");
                isMove = false;
                canAttack = false;
            }



        }



        else if(Vector3.Distance(Player.transform.position, transform.position) >= 8 && Vector3.Distance(Player.transform.position, transform.position) <= 15)
        {

            if(canPickSkill)
            {
                RandomPickSkill();


            }

            if(canSkill)
            {
                isMove = false;
                CheckSkill();
                canSkill = false;
                canAttack = false;

            }

        }

        else
            isMove = true;
    }

    private void RandomPickSkill()
    {
        randomSkillNum = Random.Range(1, 6);

        switch(randomSkillNum)
        {
            case 1:
                currentPickSkill = Skills.SKILL1;
                break;

            case 2:
                currentPickSkill = Skills.SKILL2;
                break;

            case 3:
                currentPickSkill = Skills.SKILL3;
                break;

            case 4:
                currentPickSkill = Skills.SKILL4;
                break;

            case 5:
                currentPickSkill = Skills.SKILL5;
                break;


        }

        canPickSkill = false;
    }

    private void CheckSkill()
    {
        switch (currentPickSkill)
        {
            case Skills.SKILL1:
                    anim.SetTrigger("1H_Magic");
                    currentSkillPrefab = Skill1Prefab;
               
                break;

            case Skills.SKILL2:
                    anim.SetTrigger("2H_Magic");
                    currentSkillPrefab = Skill2Prefab;
                
                break;

            case Skills.SKILL3:
                    anim.SetTrigger("SlashMagic");
                    currentSkillPrefab = Skill3Prefab;
                
                break;

            case Skills.SKILL4:
                    anim.SetTrigger("1H_Magic");
                    currentSkillPrefab = Skill4Prefab;
                
                break;

            case Skills.SKILL5:
                    anim.SetTrigger("1H_Magic");
                    currentSkillPrefab = Skill5Prefab;
                
                break;
        }

        
    }

    public void Dash()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * 20;
    }


    public void Skill()
    {
        if(currentPickSkill == Skills.SKILL3)
            OutPutSkillPrefab = Instantiate(currentSkillPrefab, Skill3StartPoint.transform.position, transform.rotation, transform);

        else if(currentPickSkill == Skills.SKILL4)
            OutPutSkillPrefab = Instantiate(currentSkillPrefab, Skill4StartPoint.transform.position, Skill4StartPoint.transform.rotation, Skill4StartPoint.transform);
        else if(currentPickSkill == Skills.SKILL5)
            OutPutSkillPrefab = Instantiate(currentSkillPrefab, Skill5StartPoint.transform.position, transform.rotation, transform);
        else
            OutPutSkillPrefab = Instantiate(currentSkillPrefab, SkillStartPoint.transform.position, transform.rotation, transform);
        if(currentPickSkill == Skills.SKILL1 || currentPickSkill == Skills.SKILL3)
            OutPutSkillPrefab.transform.parent = null;

    }

    public void SkillEnd()
    {
        canAttack = true;
        isMove = true;
        canPickSkill = true;
        if(!(currentPickSkill == Skills.SKILL1))
            OutPutSkillPrefab.transform.parent = null;

        //timer = 0.0f;
        StartCoroutine(DestroySkill(OutPutSkillPrefab));

    }

    private IEnumerator DestroySkill(GameObject skill)
    {


        yield return new WaitForSeconds(4.0f);

        canSkill = true;
        Destroy(skill);
    }

    // ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
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

    public void Attack()
    {
        AttackCollider.SetActive(true);
        OutPutSkillPrefab = Instantiate(AttackPrefab, AttackStartPoint.transform.position, transform.rotation, transform);
    }

    public void AttackEnd()
    {
        isMove = true;
        StartCoroutine(DistroyAttack());
    }

    private IEnumerator DistroyAttack()
    {
        yield return new WaitForSeconds(0.1f);
        canAttack = true;

        Destroy(OutPutSkillPrefab);

    }
}
