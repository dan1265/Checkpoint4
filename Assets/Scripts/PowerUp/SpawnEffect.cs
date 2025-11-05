using System.Collections;
using UnityEngine;

public class SpawnEffect : MonoBehaviour
{
    public GameObject effectPrefab;
    public float timeToSpawn = 5f;

    void Start()
    {
        Spawn();
    }

    IEnumerator SpawnEffectRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeToSpawn);
            GameObject newEffect = Instantiate(effectPrefab, transform.position, Quaternion.identity);
            newEffect.transform.SetParent(transform);
        }
    }

    public void Spawn()
    {
        StartCoroutine(SpawnEffectRoutine());
    }
}
