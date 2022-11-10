using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityCan : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb;
    [SerializeField]
    float gravityIntensity;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        gravityIntensity = Random.Range(8f, 16f);
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(-Vector3.up * gravityIntensity * .25f);
    }
}
