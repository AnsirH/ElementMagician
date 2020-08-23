using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    public bool isHit = false;

    public int hp;
    public int currentHp;
    public int atk;
    public int def;

    public ParticleSystem FireHit;
    public ParticleSystem WaterHit;
    public ParticleSystem ThunderHit;
    public ParticleSystem HealHit;


    private void Start()
    {
        currentHp = hp;
    }

    private void Update()
    {
        
    }

    public int Hit(float _playerAtk)
    {

        float playerAtk = _playerAtk;
        float dmg = playerAtk - def;
        

        

        if(dmg <= 0)
        {
            dmg = 1;
        }

        currentHp -= (int)dmg;


        if(currentHp <= 0)
        {
            Debug.Log("체력이 0 미만입니다.");
        }

        isHit = true;


        return (int)dmg;
    }

    

    //private void OnParticleCollision(GameObject other)
    //{
    //    if(other.CompareTag("Effect"))
    //    {
    //        Debug.Log("충돌");
    //    }
    //}


    //private void OnCollisionEnter(GameObject coll)
    //{
    //    if(coll.transform.CompareTag("Effect"))
    //    {
    //        Debug.Log("충돌");
    //    }
    //}

    //private void OnCollisionEnter()
    //{
    //    if(other.CompareTag("Effect"))
    //    {
    //        Debug.Log("충돌");

    //    }
    //}
}
