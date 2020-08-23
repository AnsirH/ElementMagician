using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemy : MonoBehaviour
{

    private PlayerStat ThePlayerStat;
    private PlayerController Player;
    


    private void OnEnable()
    {
        StartCoroutine(OutoDisable());
    }


    private IEnumerator OutoDisable()
    {
        yield return new WaitForSeconds(0.1f);

        gameObject.SetActive(false);

    }

    void Start()
    {
        ThePlayerStat = FindObjectOfType<PlayerStat>();
        Player = FindObjectOfType<PlayerController>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy" && other.GetComponent<EnemyStat>().currentHp > 0)
        {
            


            if(Player.currentLeftWeapon == PlayerController.weaponState.Staff)
            {
                Debug.Log("마법 공격");
                int dmg = other.gameObject.GetComponent<EnemyStat>().Hit(ThePlayerStat.intellectPower);
            }

            else if(Player.currentLeftWeapon == PlayerController.weaponState.Hand)
            {
                Debug.Log("물리 공격");

                int dmg = other.gameObject.GetComponent<EnemyStat>().Hit(ThePlayerStat.offensePower);
                if(Player.currentRightWeapon == PlayerController.weaponState.Hand)
                    SoundManager.instance.PlaySoundEffect("Punch2");
                else
                {
                    if(Player.GetComponent<SwordController>().attackState == SwordController.AttackState.ATTACK1)
                        SoundManager.instance.PlaySoundEffect("Sword1");
                    else if(Player.GetComponent<SwordController>().attackState == SwordController.AttackState.ATTACK2)
                        SoundManager.instance.PlaySoundEffect("Sword2");

                }


            }

            if (other.name == "Spider_Brown")
            {
                other.gameObject.GetComponent<SpiderController>().OnHit = true;
                other.gameObject.GetComponent<SpiderController>().HitAnim();

            }
            else
                other.gameObject.GetComponent<Animator>().SetTrigger("OnHit");
            Debug.Log(other.gameObject.GetComponent<EnemyStat>().currentHp);
        }
    }

}
