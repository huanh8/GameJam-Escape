using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootArrow : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform arrowSpawn;
    public float timeGap = 1.0f;

    void Start()
    {
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeGap);
            Instantiate(arrowPrefab, arrowSpawn.position, arrowSpawn.rotation);
        }
    }
}
