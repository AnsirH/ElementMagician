using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemyParticle : MonoBehaviour
{
    //private ParticleSystem ps;

    //private List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();

    bool firstDmg = true;

    public string Magictype;

    private PlayerController Player;

    public float dmgRatio;

    float timer;

    public PlayerController.ElementMagicList currentMagic = new PlayerController.ElementMagicList();


    //private void OnEnable()
    //{
    //    ps = GetComponent<ParticleSystem>();

    //}


    private void Start()
    {
        Player = FindObjectOfType<PlayerController>();


    }


    //private void OnParticleTrigger()
    //{
    //    int enterParticleNumber = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);

    //    for (int i = 0; i < enterParticleNumber; i++)
    //    {
    //        Debug.Log("데미지");
    //    }
    //}


    private void OnParticleCollision(GameObject other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            if(firstDmg)
            {
                int dmg = other.gameObject.GetComponent<EnemyStat>().Hit(Player.GetComponent<PlayerStat>().intellectPower * dmgRatio);
                other.gameObject.GetComponent<Animator>().SetTrigger("OnHit");
                other.gameObject.GetComponent<EnemyController>().isHit = true;
                switch(Magictype)
                {
                case "Fire":
                other.gameObject.GetComponent<EnemyStat>().FireHit.Play();
                break;

                case "Water":
                other.gameObject.GetComponent<EnemyStat>().WaterHit.Play();
                break;

                case "Thunder":
                other.gameObject.GetComponent<EnemyStat>().ThunderHit.Play();
                break;

                case "Hael":
                other.gameObject.GetComponent<EnemyStat>().HealHit.Play();
                break;
                }

                firstDmg = false;
            }

            timer += Time.deltaTime;


            if(timer > 0.05f)
            {

                int dmg = other.gameObject.GetComponent<EnemyStat>().Hit(Player.GetComponent<PlayerStat>().intellectPower * dmgRatio);
                other.gameObject.GetComponent<Animator>().SetTrigger("OnHit");
                other.gameObject.GetComponent<EnemyController>().isHit = true;
                switch(Magictype)
                {
                case "Fire":
                other.gameObject.GetComponent<EnemyStat>().FireHit.Play();
                break;

                case "Water":
                other.gameObject.GetComponent<EnemyStat>().WaterHit.Play();
                break;

                case "Thunder":
                other.gameObject.GetComponent<EnemyStat>().ThunderHit.Play();
                break;

                case "Hael":
                other.gameObject.GetComponent<EnemyStat>().HealHit.Play();
                break;
                }
                timer = 0.0f;

            }

            



            //switch(currentMagic)
            //{
            //case PlayerController.ElementMagicList.FIRE1:
            //Fire1Hurt(other);
            //break;

            //case PlayerController.ElementMagicList.WATER1:
            //Water1Hurt(other);
            //break;

            //case PlayerController.ElementMagicList.THUNDER1:
            //Thunder1Hurt(other);
            //break;
            //}

        }



    }

    //private void Fire1Hurt(GameObject enemy)
    //{
    //    enemy.gameObject.GetComponent<Animator>().SetTrigger("OnHit");
    //    int dmg = enemy.gameObject.GetComponent<EnemyStat>().Hit(ThePlayerStat.GetComponent<PlayerStat>().intellectPower * 0.5f);
    //    enemy.gameObject.GetComponent<EnemyController>().isHit = true;
    //    StartCoroutine(FireHitDelay(enemy));


    //}

    //private void Water1Hurt(GameObject enemy)
    //{
    //    enemy.gameObject.GetComponent<Animator>().SetTrigger("OnHit");
    //    int dmg = enemy.gameObject.GetComponent<EnemyStat>().Hit(ThePlayerStat.GetComponent<PlayerStat>().intellectPower * 0.3f);
    //    enemy.gameObject.GetComponent<EnemyController>().isHit = true;
    //    StartCoroutine(WaterHitDelay(enemy));
    //}
    //private void Thunder1Hurt(GameObject enemy)
    //{
    //    enemy.gameObject.GetComponent<Animator>().SetTrigger("OnHit");
    //    int dmg = enemy.gameObject.GetComponent<EnemyStat>().Hit(ThePlayerStat.GetComponent<PlayerStat>().intellectPower * 0.7f);
    //    enemy.gameObject.GetComponent<EnemyController>().isHit = true;
    //    StartCoroutine(ThunderHitDelay(enemy));
    //}



    //private IEnumerator DmgDelay(GameObject enemy)
    //{
    //    enemy.gameObject.GetComponent<Animator>().SetTrigger("OnHit");

    //    int dmg = enemy.gameObject.GetComponent<EnemyStat>().Hit(ThePlayerStat.intellectPower);

    //    Debug.Log(enemy.gameObject.GetComponent<EnemyStat>().currentHp);
        
    //    yield return new WaitForSeconds(1.0f);

    //}

    //private void HitEffect(ParticleSystem element)
    //{
    //    enemy.gameObject.GetComponent<EnemyStat>().FireHit.Play();

    //}


    //private IEnumerator FireHitDelay(GameObject enemy)
    //{
    //    enemy.gameObject.GetComponent<EnemyStat>().FireHit.Play();


    //    yield return new WaitForSeconds(0.7f);

    //    enemy.gameObject.GetComponent<EnemyStat>().FireHit.Stop();

    //    enemy.gameObject.GetComponent<EnemyController>().isHit = false;
    //}

    //private IEnumerator WaterHitDelay(GameObject enemy)
    //{
    //    enemy.gameObject.GetComponent<EnemyStat>().WaterHit.Play();


    //    yield return new WaitForSeconds(0.7f);

    //    enemy.gameObject.GetComponent<EnemyStat>().WaterHit.Stop();

    //    enemy.gameObject.GetComponent<EnemyController>().isHit = false;
    //}

    //private IEnumerator ThunderHitDelay(GameObject enemy)
    //{
    //    enemy.gameObject.GetComponent<EnemyStat>().ThunderHit.Play();


    //    yield return new WaitForSeconds(0.7f);

    //    enemy.gameObject.GetComponent<EnemyStat>().ThunderHit.Stop();

    //    enemy.gameObject.GetComponent<EnemyController>().isHit = false;
    //}
}
