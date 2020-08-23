using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GolemRock : MonoBehaviour
{


    Rigidbody rb;

    private void OnEnable()
    {
        if(rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }

        rb.velocity = Vector3.zero;

        StartCoroutine(PoolRock());
    }



    private IEnumerator PoolRock()
    {
        yield return new WaitForSeconds(5.0f);
        ObjectPoolingManager.instance.InsertQueue(gameObject);
    }
}
