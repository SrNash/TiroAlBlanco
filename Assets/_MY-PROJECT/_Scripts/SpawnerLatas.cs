using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerLatas : MonoBehaviour
{

    [SerializeField]
    GameObject lataGO;
    [SerializeField]
    Vector3 spawnPos;


    [SerializeField]
    float timer;

    void Start()
    {
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 5f)
            SpawnCans();

        if (timer <= 0f)
            timer = 0f;
    }

    void SpawnCans()
    {
        timer = 0f;
        spawnPos = new Vector3(Random.Range(-8f, 8f), Random.Range(10f, 15f), 2f);
        GameObject clone = Instantiate(lataGO, spawnPos, Quaternion.identity);
    }
}
