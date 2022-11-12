using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    [Header("Identificador Objeto")]
    [SerializeField]
    bool isDrag;
    [SerializeField]
    Transform selectedTransform;

    Ray ray;
    RaycastHit hitInfo;

    [Header("Localización y Desplazamiento")]
    [SerializeField]
    Vector3 screenPos;
    [SerializeField]
    Vector3 offsetMousePointer;
    [SerializeField]
    float mZCoord;  //variable para ajustar el item seleccionado en el eje Z

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
                if (hitInfo.collider.CompareTag("Pickable"))
                {
                    selectedTransform = hitInfo.collider.transform;    //asignaremos el transform del objeto con el que impacta el rayo

                    selectedObject = selectedTransform.GetComponent<SelectedObject>();

                    screenPos = cam.WorldToScreenPoint(selectedTransform.position); //registramos la posicion del objeto en la pantalla

                    print(screenPos.z);
                    //offset de la posicion en la que estamos respecto a la que queremos desplazar el objeto, para mantenerlo centrado
                    offsetMousePointer = selectedTransform.position - cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPos.z));  
                    
                    isDrag = true;  //está clicado
                }
            }
        }
        else if (Input.GetMouseButtonUp(0) && isDrag == true)   //dejamos de desplazar/arrastras el objeto
        {
            isDrag = false; //NO está clicado

            //Item Deseleccionado
            selectedObject.DeselectedItem();

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else if (isDrag)    //desplazaremos el objeto mientras que está clicado
        {
            selectedObject.GetComponent<Collider>().enabled = false;
            
            //Item Seleccionado
            selectedObject.SelectedItem();
            
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;

            Vector3 currentScreenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPos.z);
            Vector3 currentPos = cam.ScreenToWorldPoint(currentScreenPos) + offsetMousePointer; //donde queremos que esté el objeto
            selectedObject.GetComponent<Collider>().enabled = true;

            selectedTransform.position = currentPos;   //desplazamos el objeto
        }


    }
}