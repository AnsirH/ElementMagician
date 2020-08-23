using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    

    [SerializeField] GameObject m_goHpBarPrefab = null;
    //[SerializeField] GameObject m_goElementListPrefab = null;

    PlayerController Player;
    GameObject playerHpBar;
    //GameObject playerElementList;

    List<Transform> m_objectList = new List<Transform>();
    List<GameObject> m_hpBarList = new List<GameObject>();

    Camera m_hpBarCam = null;

    
    private void Start()
    {
        Player = FindObjectOfType<PlayerController>();
        m_hpBarCam = Camera.main;

        GameObject[] t_objects = GameObject.FindGameObjectsWithTag("Enemy");

        playerHpBar = Instantiate(m_goHpBarPrefab, Player.transform.position, Quaternion.identity, transform);

        for(int i = 0; i < t_objects.Length;i++)
        {
            m_objectList.Add(t_objects[i].transform);
            GameObject t_hpBar = Instantiate(m_goHpBarPrefab, t_objects[i].transform.position, Quaternion.identity, transform);

            m_hpBarList.Add(t_hpBar);
        }


    }

    

    public void EnemyHpBarAnim(int index)
    {
        m_hpBarList[index].GetComponent<Animator>().SetTrigger("Idle 0");
    }

    public void PlayerHpBarAnim()
    {
        playerHpBar.GetComponent<Animator>().SetTrigger("Idle 0");
    }

    private void Update()
    {
        


        for (int i = 0; i < m_objectList.Count; i++)
        {
            m_hpBarList[i].transform.position = m_hpBarCam.WorldToScreenPoint(m_objectList[i].transform.position + new Vector3(0, -1.15f, 0));
            m_hpBarList[i].transform.GetChild(0).GetComponent<Image>().fillAmount = 
                (float)m_objectList[i].transform.GetComponent<EnemyStat>().currentHp / m_objectList[i].transform.GetComponent<EnemyStat>().hp;


            if (m_objectList[i].GetComponent<EnemyStat>().isHit)
            {
                m_hpBarList[i].GetComponent<Animator>().SetTrigger("Idle 0");
                m_objectList[i].GetComponent<EnemyStat>().isHit = false;
            }
            



            //if(m_objectList[i].transform.GetComponent<EnemyStat>().currentHp == m_objectList[i].transform.GetComponent<EnemyStat>().hp)
            //{
            //    m_objectList[i].transform.GetComponent<Animator>().SetTrigger("DisapearHpbar");

            //    StartCoroutine(SetActiveHpbar(i, false));
            //}
        }
        playerHpBar.transform.position = m_hpBarCam.WorldToScreenPoint(Player.transform.position + new Vector3(0, -1.15f, 0));
        playerHpBar.transform.GetChild(0).GetComponent<Image>().fillAmount =
                (float)Player.transform.GetComponent<PlayerStat>().currentHp / Player.transform.GetComponent<PlayerStat>().Hp;


        if (Player.transform.GetComponent<PlayerStat>().isHit)
        {
            playerHpBar.GetComponent<Animator>().SetTrigger("Idle 0");
            Player.transform.GetComponent<PlayerStat>().isHit = false;
        }
        


    }

    //private IEnumerator SetActiveHpbar(int index, bool setBool)
    //{
    //    yield return new WaitForSeconds(1.0f);

    //    m_objectList[index].transform.gameObject.SetActive(setBool);
    //}

    

    IEnumerator isVisualTime_Plaeyer(Transform hpBar)
    {
        yield return new WaitForSecondsRealtime(3.0f);
        hpBar.GetComponent<PlayerStat>().isHit = false;
    }

    
}
