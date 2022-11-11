using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    [Header("Identificador Objeto")]
    [SerializeField]
    bool isDrag;
    [SerializeField]
    Transform goSelected;

    [Header("Rayo")]
    [SerializeField]
    Ray ray;
    [SerializeField]
    RaycastHit hitInfo;

    [Header("Localización y Desplazamiento")]
    [SerializeField]
    Vector3 screenPos;
    [SerializeField]
    Vector3 offsetMousePointer;

    [Header("Camara")]
    Camera cam;
    SelectedObject selectedObject;

    // Start is called before the first frame update
    void Start()
    {
        isDrag = false;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        DragnDrop();
    }

    void DragnDrop()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = cam.ScreenPointToRay(Input.mousePosition); //asignamos el punto desde donde lanzaremos el rayo

            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo, Mathf.Infinity))
            {
               if(hitInfo.collider.tag == "Pickable")
                {
                    goSelected = hitInfo.collider.transform;    //asignaremos el transform del objeto con el que impacta el rayo
                    selectedObject = goSelected.GetComponent<SelectedObject>();
                    print("Click " + goSelected.name);

                    screenPos = cam.WorldToScreenPoint(goSelected.position); //registramos la posicion del objeto
                    offsetMousePointer = goSelected.position - cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPos.z));  //offset de la posicion en la que estamos respecto a la que queremos desplazar el objeto, para mantenerlo centrado
                    isDrag = true;  //está clicado
                }
            }
        }
        else if (Input.GetMouseButtonUp(0) && isDrag == true)   //dejamos de desplazar/arrastras el objeto
        {
            isDrag = false; //NO está clicado
            selectedObject.isSelected = false;
            Cursor.visible = true;
        }
        else if (isDrag)    //desplazaremos el objeto mientras que está clicado
        {
            selectedObject.isSelected = true;
            Cursor.visible = false;

            Vector3 currentScreenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPos.z);  //
            Vector3 currentPos = Camera.main.ScreenToWorldPoint(currentScreenPos) + offsetMousePointer; //donde queremos que esté el objeto

            goSelected.position = currentPos;   //desplazamos el objeto
        }
    }
}