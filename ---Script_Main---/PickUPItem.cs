using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditorInternal;
using UnityEngine;

public class PickUPItem : MonoBehaviour
{
    private PlayerStat ThePlayerStat;
    private PlayerController Player;

    private bool canPickSword = true;
    private bool canPickStaff = true;

    GameObject weapon;

    // 오른손
    public Vector3 customWeaponRightPosition = new Vector3();
    public Vector3 customWeaponRightRotation = new Vector3();

    // 왼손
    public Vector3 customWeaponLeftPosition = new Vector3();
    public Vector3 customWeaponLeftRotation = new Vector3();
    //public Vector3 customWeaponRotation = new Vector3(81.497f, -84.119f, -64.734f);

    Transform RightWeapon;
    Transform LeftWeapon;

    private void Start()
    {
        ThePlayerStat = FindObjectOfType<PlayerStat>();
        Player = FindObjectOfType<PlayerController>();
    }


    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Z) && weapon != null && weapon.transform.tag == "Weapon")
        {
            
            PickUpItem();
            SetPickUpWeapon();
            Player.SetCurrentWeapon();
            Debug.Log("아이템을 줍습니다.");

        }

        if (Input.GetKeyDown(KeyCode.X) && RightWeapon != GameObject.Find("Bip001 R Hand").transform)
        {
            PickDownRightItem();
            SetCurrentRightWeapon();
            Player.SetCurrentWeapon();

        }

        if (Input.GetKeyDown(KeyCode.C) && LeftWeapon != GameObject.Find("Bip001 L Hand").transform)
        {
            PickDownLeftItem();
            SetCurrentLeftWeapon();
            Player.SetCurrentWeapon();

        }



    }

    //private void CheckItem()
    //{
    //    Ray ray = new Ray();
    //    RaycastHit hit;

    //    ray.origin = transform.position + new Vector3(0, 0.3f, 0);
    //    ray.direction = -transform.up;

    //    if (Physics.Raycast(ray, out hit, 0.5f) && hit.transform.CompareTag("Weapon"))
    //    {
    //        weapon = hit.transform.gameObject;
    //    }

    //    else
    //        weapon = null;

    //    Debug.DrawLine(ray.origin, ray.origin + ray.direction * 0.5f, Color.white);

    //}


    private void PickDownRightItem()
    {
        RightWeapon.parent = null;
        RightWeapon.position = new Vector3(transform.position.x, 0.028f, transform.position.z);
        RightWeapon.eulerAngles = new Vector3(90.0f, transform.eulerAngles.y, 0.0f);
        RightWeapon.GetComponent<BoxCollider>().enabled = true;
        ThePlayerStat.PickDownSword(RightWeapon.GetComponent<SwordStat>().Atk);

        RightWeapon = GameObject.Find("Bip001 R Hand").transform;
        canPickSword = true;

    }

    private void PickDownLeftItem()
    {
        LeftWeapon.parent = null;
        LeftWeapon.position = new Vector3(transform.position.x, 0.028f, transform.position.z);
        //LeftWeapon.rotation = Quaternion.Euler(90.0f, transform.rotation.y, transform.rotation.z);
        LeftWeapon.eulerAngles = new Vector3(
            90.0f, 
            transform.eulerAngles.y,
            0.0f);
        LeftWeapon.GetComponent<BoxCollider>().enabled = true;
        ThePlayerStat.PickDownStaff(LeftWeapon.GetComponent<StaffStat>().Int);




        LeftWeapon = GameObject.Find("Bip001 L Hand").transform;
        canPickStaff = true;

    }

    private void PickUpItem()
    {
        if(weapon.transform.GetChild(0).tag == "Sword" && canPickSword)
        {
            weapon.transform.parent = GameObject.Find("Bip001 R Hand").transform;
            weapon.transform.localPosition = customWeaponRightPosition;
            weapon.transform.localRotation = Quaternion.Euler(customWeaponRightRotation);

            ThePlayerStat.PickSword(weapon.GetComponent<SwordStat>().Atk);
            
            RightWeapon = weapon.transform;
            canPickSword = false;

            weapon.GetComponent<BoxCollider>().enabled = false;


        }

        else if (weapon.transform.GetChild(0).tag == "Staff" && canPickStaff)
        {

            weapon.transform.parent = GameObject.Find("Bip001 L Hand").transform;
            weapon.transform.localPosition = customWeaponLeftPosition;
            weapon.transform.localRotation = Quaternion.Euler(customWeaponLeftRotation);
            weapon.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            ThePlayerStat.PickStaff(weapon.GetComponent<StaffStat>().Int);


            LeftWeapon = weapon.transform;
            canPickStaff = false;

            weapon.GetComponent<BoxCollider>().enabled = false;

        }

    }



    private void OnTriggerStay(Collider other)
    {
        if(canPickSword)
        {
            if(other.CompareTag("Weapon"))
                weapon = other.gameObject;

        }


        else if(canPickStaff)
        {
            if(other.CompareTag("Weapon"))
                weapon = other.gameObject;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        weapon = null;

    }





    private void SetPickUpWeapon()
    {
        


        


        if (weapon.transform.GetChild(0).tag == "Staff")
        {
            transform.GetComponent<PlayerController>().currentLeftWeapon = PlayerController.weaponState.Staff;

        }

        //else
        //    transform.parent.GetComponent<PlayerController>().currentLeftWeapon = PlayerController.weaponState.Hand;



        if (weapon.transform.GetChild(0).tag == "Sword")
        {
            transform.GetComponent<PlayerController>().currentRightWeapon = PlayerController.weaponState.Sword;

        }


        //else
        //    transform.parent.GetComponent<PlayerController>().currentRightWeapon = PlayerController.weaponState.Hand;
    }

    private void SetCurrentRightWeapon()
    {
        if (RightWeapon.GetChild(0).tag == "Sword")
        {
            transform.transform.GetComponent<PlayerController>().currentRightWeapon = PlayerController.weaponState.Sword;
        }
        else if(RightWeapon.tag == "Hand")
        {
            transform.transform.GetComponent<PlayerController>().currentRightWeapon = PlayerController.weaponState.Hand;
        }

        


        
    }

    private void SetCurrentLeftWeapon()
    {
        if (LeftWeapon.GetChild(0).tag == "Staff")
        {
            transform.transform.GetComponent<PlayerController>().currentLeftWeapon = PlayerController.weaponState.Staff;
        }

        else if (LeftWeapon.tag == "Hand")
        {
            transform.transform.GetComponent<PlayerController>().currentLeftWeapon = PlayerController.weaponState.Hand;
        }
    }
}
