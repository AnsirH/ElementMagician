using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
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
        //Enemy = FindObjectOfType<EnemyController>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && other.GetComponent<PlayerStat>().currentHp > 0)
        {
            int dmg = other.gameObject.GetComponent<PlayerStat>().Hit(TheEnemyStat.atk);


            
            if(!other.GetComponent<PlayerController>().isStumble)
                other.gameObject.GetComponent<Animator>().SetTrigger("OnHit");




            other.gameObject.GetComponent<PlayerController>().attackStartable = true;
            Debug.Log(other.gameObject.GetComponent<PlayerStat>().currentHp);
        }
    }

}
