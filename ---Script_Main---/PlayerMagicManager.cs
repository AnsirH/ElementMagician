using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Sockets;
using UnityEngine;

public class PlayerMagicManager : MonoBehaviour
{

    public GameObject Fire1Prefab = null;

    public GameObject Fire2Prefab = null;


    public GameObject Water1Prefab = null;

    public GameObject Water2Prefab = null;


    public GameObject Thunder1Prefab = null;

    public GameObject Thunder2Prefab = null;


    public GameObject HealPrefab = null;




    public GameObject SteamPrefab = null;

    public GameObject Laser_FirePrefab = null;

    public GameObject Fire_HealPrefab = null;


    public GameObject Hot_WaterPrefab = null;

    public GameObject Laser_WaterPrefab = null;

    public GameObject Water_HealPrefab = null;


    public GameObject ExplosionPrefab = null;

    public GameObject Thunder_WaterPrefab = null;

    public GameObject Laser_HealPrefab = null;

// ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ

    public GameObject Heal_FirePrefab = null;

    public GameObject Heal_WaterPrefab = null;

    public GameObject Heal_ThunderPrefab = null;

    public GameObject Water_ThunderWavePrefab = null;

    public GameObject WaterSmashPrefab = null;

    public GameObject FireMix1Prefab = null;

    public GameObject WaterMix1Prefab = null;

    public GameObject ThunderMix1Prefab = null;

    public GameObject HealMix1Prefab = null;







    public GameObject Fire3Prefab = null;

    public GameObject Water3Prefab = null;

    public GameObject Thunder3Prefab = null;

    public GameObject SteamFire1Prefab = null;

    public GameObject SteamWater1Prefab = null;

    public GameObject SteamThunder1Prefab = null;

    public GameObject SteamHealPrefab = null;






    public GameObject Fire4Prefab = null;

    public GameObject Water4Prefab = null;

    public GameObject Thunder4Prefab = null;

    public GameObject LavaPrefab = null;

    public GameObject WaterFireWallPrefab = null;

    public GameObject Fire2Thunder2Prefab = null;

    public GameObject Steam2Prefab = null;

    public GameObject SteamFire2Prefab = null;

    public GameObject SteamWater2Prefab = null;

    public GameObject SteamThunder2Prefab = null;

    public GameObject FireMix2Prefab = null;

    public GameObject WaterMix2Prefab = null;

    public GameObject ThunderMix2Prefab = null;

    public GameObject HealMix2Prefab = null;




    public GameObject StartPointMagic = null;



    private PlayerController Player;










    private void Start()
    {
        Player = FindObjectOfType<PlayerController>();
    }

    


    public void LoadElementMagic(GameObject position)
    {
        switch(Player.currentMagic)
        {
        case PlayerController.ElementMagicList.FIRE1:
                ElementParticle(Fire1Prefab, position);
        break;

        case PlayerController.ElementMagicList.WATER1:
                ElementParticle(Water1Prefab, position); 

                break;

        case PlayerController.ElementMagicList.THUNDER1:
                ElementParticle(Thunder1Prefab, position);

                break;

        case PlayerController.ElementMagicList.HEAL1:
        Heal(HealPrefab, position);

                break;



        case PlayerController.ElementMagicList.FIRE2:
                ElementParticle(Fire2Prefab, position);

                break;

        case PlayerController.ElementMagicList.WATER2:
                ElementParticle(Water2Prefab, position);

                break;

        case PlayerController.ElementMagicList.THUNDER2:
                ElementParticle(Thunder2Prefab, position);

                break;

        case PlayerController.ElementMagicList.HEAL2:
                ElementParticle(HealPrefab, position);



                break;



        case PlayerController.ElementMagicList.STEAM:
                ElementParticle(SteamPrefab, position);

                break;

        case PlayerController.ElementMagicList.LASER_FIRE:
                ElementParticle(Laser_FirePrefab, position);

                break;

        case PlayerController.ElementMagicList.FIRE_HEAL:
                ElementParticle(Fire_HealPrefab, position);

                break;


        case PlayerController.ElementMagicList.HOT_WATER:
                ElementParticle(Hot_WaterPrefab, position);

                break;

        case PlayerController.ElementMagicList.LASER_WATER:
                ElementParticle(Laser_WaterPrefab, position);

                break;

        case PlayerController.ElementMagicList.WATER_HEAL:
                ElementParticle(Water_HealPrefab, position);
        break;


        case PlayerController.ElementMagicList.EXPLOSION:
                ElementParticle(ExplosionPrefab, position);

                break;

        case PlayerController.ElementMagicList.THUNDER_WATER:
                ElementParticle(Thunder_WaterPrefab, position);

                break;

        case PlayerController.ElementMagicList.LASER_HEAL:
                ElementParticle(Laser_HealPrefab, position);

                break;


        case PlayerController.ElementMagicList.HEAL_FIRE:
                Heal(Heal_FirePrefab, position);

                break;

        case PlayerController.ElementMagicList.HEAL_WATER:
        Heal(Heal_WaterPrefab, position);

                break;

        case PlayerController.ElementMagicList.HEAL_THUNDER:
        Heal(Heal_ThunderPrefab, position);

                break;


        // ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ 3개 이상
        case PlayerController.ElementMagicList.STEAM_FIRE1:
        ElementParticle(SteamFire1Prefab, position);
        break;

        case PlayerController.ElementMagicList.STEAM_WATER1:
        ElementParticle(SteamWater1Prefab, position);
        break;

        case PlayerController.ElementMagicList.STEAM_THUNDER1:
        ElementParticle(SteamThunder1Prefab, position);
        break;

        case PlayerController.ElementMagicList.STEAM_HEAL1:
        ElementParticle(SteamHealPrefab, position);
        break;

        case PlayerController.ElementMagicList.FIRE3:
        ElementParticle(Fire3Prefab, position);
        break;

        case PlayerController.ElementMagicList.WATER3:
        ElementParticle(Water3Prefab, position);
        break;

        case PlayerController.ElementMagicList.THUNDER3:
        ElementParticle(Thunder3Prefab, position);
        break;

        case PlayerController.ElementMagicList.HEAL3:
        ElementParticle(HealPrefab, position);
        break;

        case PlayerController.ElementMagicList.WATER_THUNDERWAVE:
        ElementGameObject(Water_ThunderWavePrefab, position);
        break;

        case PlayerController.ElementMagicList.WATERSMASH:
        ElementGameObject(WaterSmashPrefab, position);
        break;

        case PlayerController.ElementMagicList.FIREMIX1:
            ElementGameObject(FireMix1Prefab, position);
            break;

            case PlayerController.ElementMagicList.WATERMIX1:
                ElementGameObject(WaterMix1Prefab, position);
                break;

            case PlayerController.ElementMagicList.THUNDERMIX1:
                ElementGameObject(ThunderMix1Prefab, position);
                break;

            case PlayerController.ElementMagicList.HEALMIX1:
                ElementGameObject(HealMix1Prefab, position);
                break;




            case PlayerController.ElementMagicList.FIRE4:
        ElementParticle(Fire4Prefab, position);
        break;

        case PlayerController.ElementMagicList.WATER4:
        ElementParticle(Water4Prefab, position);
        break;

        case PlayerController.ElementMagicList.THUNDER4:
        ElementParticle(Thunder4Prefab, position);
        break;

        case PlayerController.ElementMagicList.HEAL4:
        Heal(HealPrefab, position);
        break;

        case PlayerController.ElementMagicList.FIRE2_THUNDER2:
        ElementParticle(Fire2Thunder2Prefab, position);
        break;

        case PlayerController.ElementMagicList.WATER_FIREWALL:
        ElementParticle(WaterFireWallPrefab, position);
        break;
            
        case PlayerController.ElementMagicList.LAVA:
        ElementParticle(LavaPrefab, position);
        break;

        case PlayerController.ElementMagicList.STEAM_FIRE2:
        ElementParticle(SteamFire2Prefab, position);
        break;

        case PlayerController.ElementMagicList.STEAM_WATER2:
        ElementParticle(SteamWater2Prefab, position);
        break;

        case PlayerController.ElementMagicList.STEAM_THUNDER2:
        ElementParticle(SteamThunder2Prefab, position);
        break;

        case PlayerController.ElementMagicList.STEAM_HEAL2:
        ElementParticle(SteamHealPrefab, position);
        break;

        case PlayerController.ElementMagicList.STEAM2:
        ElementParticle(Steam2Prefab, position);
        break;

            case PlayerController.ElementMagicList.FIREMIX2:
                ElementGameObject(FireMix2Prefab, position);
                break;

            case PlayerController.ElementMagicList.WATERMIX2:
                ElementGameObject(WaterMix2Prefab, position);
                break;

            case PlayerController.ElementMagicList.THUNDERMIX2:
                ElementGameObject(ThunderMix2Prefab, position);
                break;

            case PlayerController.ElementMagicList.HEALMIX2:
                ElementGameObject(HealMix2Prefab, position);
                break;

        }
    }

    private void ElementGameObject(GameObject element, GameObject position)
    {
        transform.gameObject.GetComponent<ElementMagicController>().currentMagic =
        Instantiate(element, position.transform.position, Player.transform.rotation, transform);
    }

    private void ElementParticle(GameObject element, GameObject position)
    {
        transform.gameObject.GetComponent<ElementMagicController>().currentMagic =
        Instantiate(element, position.transform.position, Player.transform.rotation, transform);
        element.GetComponent<ParticleSystem>().Play();

    }

    

    private void Heal(GameObject element, GameObject position)
    {
        element.GetComponent<ParticleSystem>().Play();
        transform.gameObject.GetComponent<ElementMagicController>().currentMagic =
        Instantiate(element, position.transform.position + new Vector3(0, 0.2f, 0), Quaternion.identity, transform);
    }


    

    public void EndElementMagic()
    {
        Player.currentElements.Clear();
        Player.elementMagicNumber.Clear();



        Player.FireElementNum = 0;
        Player.WaterElementNum = 0;
        Player.ThunderElementNum = 0;
        Player.HealElementNum = 0;
        Player.canReceiveElements = true;
        Player.canSetElements = true;
        GameObject.Find("GameUI").GetComponent<ShowElements>().delAll = true;
    }
}
