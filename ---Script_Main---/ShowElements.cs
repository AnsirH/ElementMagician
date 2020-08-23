using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class ShowElements : MonoBehaviour
{

    private PlayerController Player;



    // 원소 리스트를 초기화할 지 체크
    public bool delAll = false;

    [SerializeField] GameObject ElementList;


    GameObject playerElementList;

    Camera m_elementListCam;





    int currentImage = 0;

    private void Awake()
    {
        Player = FindObjectOfType<PlayerController>();
        //DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {



        m_elementListCam = Camera.main;
        
        playerElementList = Instantiate(ElementList, Player.transform.position, Quaternion.identity, transform);


    }

    private void Update()
    {

        ShowElement();



        playerElementList.transform.position = m_elementListCam.WorldToScreenPoint(Player.transform.position + new Vector3(2.0f, -2.2f, 0));
    }
    

    private void ShowElement()
    {
        
        if(currentImage < 4)
        {
            if(Player.OnFire)
            {


                playerElementList.transform.GetChild(currentImage).gameObject.SetActive(true);
                playerElementList.transform.GetChild(currentImage).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/S_fire_furious");
                playerElementList.transform.GetChild(currentImage).transform.GetComponent<Animator>().SetTrigger("OnApear");

                //GameObject.Find("ElementList(Clone)").transform.GetChild(currentImage).gameObject.SetActive(true);
                //GameObject.Find("ElementList(Clone)").transform.GetChild(currentImage).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/S_fire_furious");
                //GameObject.Find("ElementList(Clone)").transform.GetChild(currentImage).transform.GetComponent<Animator>().SetTrigger("OnApear");
                transform.GetChild(0).GetComponent<Animator>().SetBool("OnIdle", false);

                currentImage++;
                Player.OnFire = false;
                /// 
                // transform.GetChild(currentImage).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/S_fire_furious");


            }


            else if(Player.OnWater)
            {

                playerElementList.transform.GetChild(currentImage).gameObject.SetActive(true);
                playerElementList.transform.GetChild(currentImage).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/S_chilly_kick");
                playerElementList.transform.GetChild(currentImage).transform.GetComponent<Animator>().SetTrigger("OnApear");


                //GameObject.Find("ElementList(Clone)").transform.GetChild(currentImage).gameObject.SetActive(true);
                //GameObject.Find("ElementList(Clone)").transform.GetChild(currentImage).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/S_chilly_kick");
                //GameObject.Find("ElementList(Clone)").transform.GetChild(currentImage).transform.GetComponent<Animator>().SetTrigger("OnApear");
                transform.GetChild(1).GetComponent<Animator>().SetBool("OnIdle", false);


                currentImage++;

                Player.OnWater = false;

            }

            else if(Player.OnThunder)
            {
                playerElementList.transform.GetChild(currentImage).gameObject.SetActive(true);
                playerElementList.transform.GetChild(currentImage).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/S_Forward_thurder");
                playerElementList.transform.GetChild(currentImage).transform.GetComponent<Animator>().SetTrigger("OnApear");


                //GameObject.Find("ElementList(Clone)").transform.GetChild(currentImage).gameObject.SetActive(true);
                //GameObject.Find("ElementList(Clone)").transform.GetChild(currentImage).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/S_Forward_thurder");
                //GameObject.Find("ElementList(Clone)").transform.GetChild(currentImage).transform.GetComponent<Animator>().SetTrigger("OnApear");
                transform.GetChild(2).GetComponent<Animator>().SetBool("OnIdle", false);


                currentImage++;

                Player.OnThunder = false;

            }

            else if(Player.OnHeal)
            {
                playerElementList.transform.GetChild(currentImage).gameObject.SetActive(true);
                playerElementList.transform.GetChild(currentImage).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/S_spell03");
                playerElementList.transform.GetChild(currentImage).transform.GetComponent<Animator>().SetTrigger("OnApear");


                //GameObject.Find("ElementList(Clone)").transform.GetChild(currentImage).gameObject.SetActive(true);
                //GameObject.Find("ElementList(Clone)").transform.GetChild(currentImage).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/S_spell03");
                //GameObject.Find("ElementList(Clone)").transform.GetChild(currentImage).transform.GetComponent<Animator>().SetTrigger("OnApear");
                transform.GetChild(3).GetComponent<Animator>().SetBool("OnIdle", false);


                currentImage++;

                Player.OnHeal = false;

            }

        }

        

        if(delAll)
        {
            for (int i = 0; i < currentImage; i++)
            {
                
                
                //GameObject.Find("ElementList(Clone)").transform.GetChild(i).gameObject.SetActive(false);
                playerElementList.transform.GetChild(i).gameObject.SetActive(false);
                
            }

            for(int i = 0; i < 4; i++)
            {
                transform.GetChild(i).GetComponent<Animator>().SetBool("OnIdle", true);

            }

            currentImage = 0;
            delAll = false;
        }
        

    }



}
