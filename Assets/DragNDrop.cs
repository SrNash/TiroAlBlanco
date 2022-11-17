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
        if (Input.touchCount >= 1 && Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetMouseButton(0))
        {
            //Indicamos desde donde lanzaremos el rayo
            ray = cam.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo, Mathf.Infinity))
            {
                if (hitInfo.collider.CompareTag("Pickable"))
                {
                    //Registraremos tanto la Transform como El GameObject con el 
                    selectedTransform = hitInfo.collider.GetComponent<Transform>();
                    selection = hitInfo.collider.GetComponent<SelectedObject>();

                    //Prueba guardar Posición inicial
                    ResetPos(selectedTransform);

                    selectedGO = hitInfo.collider.gameObject;

                    //Desactivamos el objeto
                    selectedGO.SetActive(false);
                    isClicked = true;
                   
                    if (selectedGO != null || selectedTransform != null)
                    {
                        if (isClicked)
                        {
                            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
                            {
                                selection.SelectedItem();
                                //mOffset = new Vector3(0f, Vector3.up.y * selectedTransform.localScale.y / 2f, -.25f);
                                mOffset = new Vector3(0f, Vector3.up.y * selection.scaleUp.y / 2f, -.50f);

                                //Variable Temporal que registra la posicion actual del objeto seleccionado 
                                //y le suma un offset
                                var tmpPos = mOffset;
                                //Cursor.visible = false;
                                //Activamos el objeto
                                selectedGO.SetActive(true);

                                //Desplazaremos el objeto
                                selectedTransform.position = hitInfo.point + tmpPos;

                                if(selectedTransform.position.y < .5f)
                                    selectedTransform.position = selection.initPos;
                            }
                            else
                            {
                                selectedGO.SetActive(true);

                                if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
                                {
                                    //mOffset = new Vector3(0f,Vector3.up.y *  selectedTransform.localScale.y / 2f, -.25f);
                                    mOffset = new Vector3(0f,Vector3.up.y * selection.scaleUp.y / 2f, -.50f);
                                    
                                    //Variable Temporal que registra la posicion actual del objeto seleccionado 
                                    //y le suma un offset
                                    var tmpPos = mOffset;

                                    //Cursor.visible = false;

                                    //Activamos el objeto
                                    selectedGO.SetActive(true);

                                    //Desplazaremos el objeto
                                    //selectedTransform.position = new Vector3(selection.initPos.x, Vector3.up.y * selectedTransform.localScale.y / 2f, selection.initPos.z);
                                    selectedTransform.position = new Vector3(selection.initPos.x, Vector3.up.y * selection.scaleUp.y / 2f, selection.initPos.z);
                                }
                            }
                        }
                    }
                }
            }
        }
        else
        {
            Cursor.visible = true;
            selection.DeselectedItem();
        }
    }

    Vector3 ResetPos(Transform tr)
    {
        Vector3 initPos = tr.position;

        return initPos;
    }
}