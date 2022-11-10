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
    float timer, timeMin, timeMax, smoothRepeating;

    void Start()
    {
        InvokeRepeating("SpawnCans", 2f, Random.Range(1f, 4f));
    }

    void Update()
    {
        smoothRepeating += Time.deltaTime;
        timeMin = Random.Range(5f,10f);
        timeMax = Random.Range(10.5f,15f);
        timer = Random.Range(timeMin, timeMax);


        if (smoothRepeating >= Random.Range(3f, 5f))
        {
            smoothRepeating = 0f;
            //InvokeRepeating("SpawnCans", 1.5f, timer);
        }
        /*timer += Time.deltaTime;

        if (timer >= 5f)
            SpawnCans();

        if (timer <= 0f)
            timer = 0f;*/
    }

    void SpawnCans()
    {
        //timer = 0f;
        spawnPos = new Vector3(Random.Range(-8f, 8f), Random.Range(10f, 15f), 2f);
        GameObject clone = Instantiate(lataGO, spawnPos, Quaternion.Euler(-90f,0f,0f));
    }
}
