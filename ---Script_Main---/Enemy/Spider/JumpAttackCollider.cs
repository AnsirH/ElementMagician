using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAttackCollider : MonoBehaviour
{



    private EnemyStat TheEnemyStat;
    //private EnemyController Enemy;

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
        TheEnemyStat = transform.parent.GetComponent<EnemyStat>();

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && other.GetComponent<PlayerStat>().currentHp > 0)
        {

            int dmg = other.gameObject.GetComponent<PlayerStat>().Hit(TheEnemyStat.atk);


            other.gameObject.GetComponent<PlayerController>().canMove = false;
            other.gameObject.GetComponent<PlayerController>().isStumble = true;
            other.gameObject.GetComponent<Animator>().SetTrigger("Stumble");

        }
    }

}
