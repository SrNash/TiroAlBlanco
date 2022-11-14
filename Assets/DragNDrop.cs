using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragNDrop : MonoBehaviour
{

    [Header("Objeto Clickeado")]
    [SerializeField]
    GameObject selectedGO;
    [SerializeField]
    Transform selectedTransform;
    [SerializeField]
    bool isClicked = false;

    Ray ray;
    RaycastHit hitInfo;

    [Header("Camera")]
    [SerializeField]
    Camera cam;

    [Header("Offsets")]
    [SerializeField]
    Vector3 mOffset;

    [Header("LeanTween")]
    [SerializeField]
    SelectedObject selection;

    private void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        Clicking();

        
    }

    void Clicking()
    {
        if (Input.GetMouseButton(0))
        {
            //Indicamos desde donde lanzaremos el rayo
            ray = cam.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo, Mathf.Infinity))
            {
                if (hitInfo.collider.CompareTag("Pickable"))
                {
                    //Registraremos tanto la Transform como El GameObject con el 
                    selectedTransform = hitInfo.collider.GetComponent<Transform>();
                    selectedGO = hitInfo.collider.gameObject;

                    //Desactivamos el objeto
                    selectedGO.SetActive(false);
                    isClicked = true;
                   
                    if (selectedGO != null || selectedTransform != null)
                    {
                        if (isClicked)
                        {
                            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo, Mathf.Infinity))
                            {
                                mOffset = new Vector3(0f, selectedTransform.position.y, -.75f);
                                //Variable Temporal que registra la posicion actual del objeto seleccionado 
                                //y le suma un offset
                                var tmpPos = mOffset;
                                Cursor.visible = false;
                                //Activamos el objeto
                                selectedGO.SetActive(true);

                                //Desplazaremos el objeto
                                selectedTransform.position = hitInfo.point + tmpPos;
                            }
                        }
                    }
                }
            }
            /*else
            { Cursor.visible = true; }*/
        }
        else
        { Cursor.visible = true; }
    }

    
}