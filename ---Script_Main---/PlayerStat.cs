using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    private PlayerController Player;
    public static PlayerStat instance;

    public bool isHit = false;

    public int Hp;
    public int currentHp;

    public int offensePower;

    public int intellectPower;

    public int defense;


    private void Start()
    {
        instance = this;
        Player = FindObjectOfType<PlayerController>();
        currentHp = Hp;
    }

    public void SetStat()
    {
        if(Player.currentLeftWeapon == PlayerController.weaponState.Hand && Player.currentRightWeapon == PlayerController.weaponState.Hand)
        {
            Hp = 100;

            offensePower = 10;

            intellectPower = 15;

            defense = 8;
        }

        
    }


    public void PickSword(int plsAtk)
    {
        Debug.Log("공격력이" + plsAtk + "만큼 증가합니다.");
        offensePower += plsAtk;
    }
    public void PickDownSword(int misAtk)
    {
        Debug.Log("공격력이" + misAtk + "만큼 감소합니다.");
        offensePower -= misAtk;
    }


    public void PickStaff(int plsInt)
    {
        Debug.Log("마법력이" + plsInt + "만큼 증가합니다.");
        intellectPower += plsInt;
    }
    public void PickDownStaff(int minInt)
    {
        Debug.Log("마법력이" + minInt + "만큼 감소합니다.");
        intellectPower -= minInt;
    }


    public void PlsDef(int plsDef)
    {
        defense += plsDef;
    }

    public int Hit(float _enemyAtk)
    {

        float enemyAtk = _enemyAtk;
        float dmg = _enemyAtk - defense;




        if (dmg <= 0)
        {
            dmg = 1;
        }

        currentHp -= (int)dmg;


        if (currentHp <= 0)
        {
            Debug.Log("체력이 0 미만입니다.");
        }

        isHit = true;

        if(Player.currentAttack)
        {
            if(GetComponent<HandController>().enabled)
            {
                Player.currentAttack = false;
                GetComponent<Animator>().SetBool("Punch", false);

                GetComponent<Animator>().SetInteger("State", 0);
                GetComponent<HandController>().attackState = HandController.AttackState.NONE;
            }
            else if(GetComponent<SwordController>().enabled)
            {
                GetComponent<SwordController>().attackState = SwordController.AttackState.NONE;

                Player.currentAttack = false;

                GetComponent<Animator>().SetInteger("State", 0);
            }

            else if(GetComponent<ElementMagicController>().enabled)
            {
                GetComponent<ElementMagicController>().StopMagic();
                GetComponent<ElementMagicController>().ElementEnd();

                //if(Player.currentAnim == PlayerController.MagicAnim.LONG)
                //    GetComponent<Animator>().SetBool("ElementMagic2", false);
                //else
                //    GetComponent<Animator>().SetBool("ElementMagic_HandUp", false);


            }

        }


        return (int)dmg;
    }
}
