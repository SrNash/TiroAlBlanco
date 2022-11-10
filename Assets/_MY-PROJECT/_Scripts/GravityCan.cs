using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityCan : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb;
    [SerializeField]
    float gravIntensity;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        gravIntensity = Random.Range(5f, 7.5f);
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(-Vector3.up * gravIntensity * .25f);
    }
}
