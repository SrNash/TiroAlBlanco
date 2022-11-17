using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewStart : MonoBehaviour
{
    [Header("Objeto Seleccionado")]
    [SerializeField]
    GameObject selectedGO;

    public enum SelectorState
    {
        Waiting,
        SelectObjectDrag,
        SelectObjectRotate,
        Dragin,
        Droped,
        Scaled,
        Rotator
    }

    [SerializeField]
    SelectorState currentState = SelectorState.Waiting;

    Camera cam;

    private void Start()
    {
        //Registramos la camara
        cam = Camera.main;
    }

    void Update()
    {
        switch(currentState)
        {
            case SelectorState.Waiting:
                ResetDef();
                break;
            case SelectorState.SelectObjectDrag:
                SelectObject();
                break;
            case SelectorState.Dragin:
                DragObject();
                break;
            case SelectorState.Droped:
                DeselectObject();
                break;
            case SelectorState.Rotator:
                RotatedObject();
                break;
            case SelectorState.Scaled:
                ScaleUp();
                break;
        }
    }

    public void SelectObject()
    {
        if ((Input.touchCount >= 1 && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetMouseButtonUp(0))
        {             
            Debug.Log("Funcion de Selección");
            Ray ray;
            ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if ((Physics.Raycast(ray, out hit) == true))
            {
                if (hit.collider.CompareTag("Pickable"))
                {   
                    selectedGO = hit.collider.gameObject;
                    currentState = SelectorState.Dragin;

                    switch(currentState)
                    {
                        case SelectorState.Waiting:
                            ResetDef();
                            break;
                        case SelectorState.Dragin:
                            break;
                        case SelectorState.Rotator:
                            RotatedObject();
                            break;
                        case SelectorState.Scaled:
                            ScaleUp();
                            break;
                    }
                }
            }
        }
    }

    public void DeselectObject()
    {
        selectedGO = null;
        currentState = SelectorState.Waiting;
    }

    public void DragObject()
    {
        Ray ray;
        ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        selectedGO.SetActive(false);

        if ((Physics.Raycast(ray, out hit) == true))
        {
            selectedGO.SetActive(true);
            selectedGO.transform.position = hit.point + new Vector3(0f, Vector3.up.y * (altura() / 2f), 0f);
            //selectedGO.transform.position = hit.point + ((Vector3.up * selectedGO.transform.localScale.y) / 2f);
        }

        selectedGO.SetActive(true);

        if ((Input.touchCount >= 1 && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetMouseButtonUp(0))
        {
            currentState = SelectorState.Droped;
        }
    }

    public void MovedObject()
    {
        currentState = SelectorState.SelectObjectDrag;
    }
    public void RotatedObject()
    {
        currentState = SelectorState.Rotator;
        SelectObject();
    }
    public void ScaledObject()
    {
        currentState = SelectorState.Scaled;
        SelectObject();
    }

    void ScaleUp()
    {
        Vector3 scaleUp = Input.mouseScrollDelta;
        selectedGO.transform.localScale += scaleUp;
    }

    void ResetDef()
    {
        Vector3 scaleDef = new Vector3(1f, 1f, 1f);
        //selectedGO.transform.localScale = scaleDef;
    }

    float altura()
    {
        float h = 0;
        BoxCollider boxCol;
        SphereCollider sphereCol;
        CapsuleCollider capsuleCol;

        if (selectedGO.name == "CubeDrag")
        {
            boxCol = selectedGO.GetComponent<BoxCollider>();
            h = boxCol.size.y;
        }
        else if (selectedGO.name == "SphereDrag")
        {
            sphereCol = selectedGO.GetComponent<SphereCollider>();
            h = sphereCol.radius;
        }
        else if (selectedGO.name == "CylinderDrag")
        {
            capsuleCol = selectedGO.GetComponent<CapsuleCollider>();
            h = capsuleCol.height;
        }

        return h;
    }
}
