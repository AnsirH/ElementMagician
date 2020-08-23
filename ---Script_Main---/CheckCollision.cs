using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    public GameObject HitEffectPrefab = null;

    private void OnTriggerEnter(Collider other)
    {

            Destroy(transform.parent.gameObject);
            Destroy(transform.parent.parent.GetChild(1).gameObject);
            GameObject HitPrefab = Instantiate(HitEffectPrefab, transform.position, transform.rotation, transform);

            StartCoroutine(DestroyHitPrefab(HitPrefab));

        
    }

    private IEnumerator DestroyHitPrefab(GameObject hitPrefab)
    {
        yield return new WaitForSeconds(1.5f);

        Destroy(hitPrefab);
    }
}
