using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewStart : MonoBehaviour
{
    [Header("Objeto Seleccionado")]
    [SerializeField]
    GameObject selectedGO;

    [SerializeField]
    Camera cam;

    [Header("Booleans de Comprobación")]
    [SerializeField]
    bool isDraged;
    [SerializeField]
    bool isRotated;
    [SerializeField]
    bool isScaled;

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


    private void Start()
    {
        //Registramos la camara
        cam = Camera.main;
    }

    void Update()
    {
        switch(currentState)
        {
            /*case SelectorState.Waiting:
                
                break;*/
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
                RotatorObject(selectedGO);
                break;
            case SelectorState.Scaled:
                ScaleUp(selectedGO);
                break;
        }
    }

    /// <summary>
    /// Funciones
    /// </summary>

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
                    //currentState = SelectorState.Dragin;

                    if (isDraged)
                        currentState = SelectorState.Dragin;
                    else if (isScaled)
                        currentState = SelectorState.Scaled;
                    else if (isRotated)
                        currentState = SelectorState.Rotator;


                    switch (currentState)
                    {
                        /*case SelectorState.Waiting:
                            ResetDef();
                            break;*/
                        case SelectorState.Dragin:
                            break;
                        case SelectorState.Rotator:
                            RotatorObject(selectedGO);
                            break;
                        case SelectorState.Scaled:
                            ScaleUp(selectedGO);
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
        }

        selectedGO.SetActive(true);

        if ((Input.touchCount >= 1 && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetMouseButtonUp(0))
        {
            currentState = SelectorState.Droped;
        }
        isDraged = false;
    }
    void ScaleUp(GameObject goToScale)
    {
        GameObject go;
        go = goToScale;
        Vector3 scaleUp = new Vector3(Input.mouseScrollDelta.y, Input.mouseScrollDelta.y, Input.mouseScrollDelta.y);
        go.transform.localScale += scaleUp;

        var vChecker = 1f; ;

        if (go.transform.localScale.y <= vChecker)
            go.transform.localScale = new Vector3(1f, 1f, 1f);

        isScaled = false;
        //selectedGO.transform.localScale += scaleUp;
    }

    void RotatorObject(GameObject goToRotate)
    {
        GameObject go;
        go = goToRotate;
        Vector3 rot = new Vector3(0f, Input.GetAxis("Mouse X"), 0f);
        go.transform.Rotate(rot);

        isRotated = false;
    }
  
    /// <summary>
    /// Control de ESTADOS
    /// </summary>

    public void MovedObject()
    {
        isDraged = true;
        currentState = SelectorState.SelectObjectDrag;
    }
    public void RotatedObject()
    {
        isRotated = true;
        currentState = SelectorState.SelectObjectDrag;
    }
    public void ScaledObject()
    {
        isScaled = true;
        currentState = SelectorState.SelectObjectDrag;
    }

    /// <summary>
    /// Float de la altura del collider
    /// </summary>

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
