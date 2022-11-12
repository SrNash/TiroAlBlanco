using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragNDrop : MonoBehaviour
{
    [SerializeField]
    bool isDragging = false;
    [SerializeField]
    Transform selectedItem;

    [SerializeField]
    Vector3 screenPosition;
    [SerializeField]
    Vector3 offset;

    [SerializeField]
    Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        Ray ray;
        RaycastHit hitInfo;

        ray = cam.ScreenPointToRay(Input.mousePosition);
        ray.origin = cam.transform.position;

        if (Physics.Raycast(ray.origin, ray.direction, out hitInfo, Mathf.Infinity))
        {
            Debug.DrawLine(ray.origin, ray.direction, Color.yellow);
            ClickNDrag();
        }
    }

    void ClickNDrag()
    {
        Ray ray;
        RaycastHit hitInfo;

        ray = cam.ScreenPointToRay(Input.mousePosition);
        
        if(Input.GetMouseButtonDown(0))
        {
            if(Physics.Raycast(ray.origin, ray.direction, out hitInfo, Mathf.Infinity))
            {
                if(hitInfo.collider.tag == "Pickable")
                {
                    Debug.Log("LLegue");

                    selectedItem = hitInfo.collider.transform;
                    selectedItem.GetComponent<Collider>().enabled = false;
                    selectedItem.GetComponent<Collider>().enabled = true;

                    print("Saliendo");
                }
            }
        }
    }
}
