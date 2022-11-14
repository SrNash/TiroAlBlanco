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
    Vector3 mousePointer;
    [SerializeField]
    Vector3 screenPos;
    [SerializeField]
    Vector3 mOffset;
    [SerializeField]
    float mZCoord;  //variable para ajustar el item seleccionado en el eje Z

    [Header("Camara")]
    Camera cam;
    SelectedObject selectedObject;


    void Start()
    {
        isDrag = false;
        cam = Camera.main;
    }

    void Update()
    {    
        DragnDrop();
    }

    void DragnDrop()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Registramos el origen del Rayo
            ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo, Mathf.Infinity))
            {
                if (hitInfo.collider.CompareTag("Pickable"))
                {
                    //Registramos el objeto clickeado.
                    selectedTransform = hitInfo.collider.transform;    
                    selectedObject = selectedTransform.GetComponent<SelectedObject>();

                    //Registramos la Pos del Puntero en el mundo
                    screenPos = cam.WorldToScreenPoint(selectedTransform.position);
                    
                    //Coordenadas en X,Y
                    mousePointer = Input.mousePosition;
                    mousePointer.z = screenPos.z;

                    //offset de la posicion en la que estamos respecto a la que queremos desplazar el objeto, para mantenerlo centrado
                    mOffset = selectedTransform.position - cam.ScreenToWorldPoint(mousePointer);  
                    
                    isDrag = true;  //está clicado
                }
            }
        }
        else if (Input.GetMouseButtonUp(0) && isDrag)
        {
            isDrag = false; //NO está clicado

            //Item Deseleccionado
            selectedObject.DeselectedItem();

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else if (isDrag)
        {
            selectedObject.GetComponent<Collider>().enabled = false;
            
            //Item Seleccionado
            selectedObject.SelectedItem();
            
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;

            mousePointer = Input.mousePosition;
            mousePointer.z = screenPos.z;

            Vector3 currentPos = cam.ScreenToWorldPoint(mousePointer) + mOffset; //donde queremos que esté el objeto
            selectedObject.GetComponent<Collider>().enabled = true;

            selectedTransform.position = currentPos;   //desplazamos el objeto

            //Intento de bloquear el eje Y con una altura minima
            if (selectedTransform.position.y < .33f)
                selectedTransform.position += hitInfo.point; ;
        }
    }
}