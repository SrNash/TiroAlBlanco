using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedObject : MonoBehaviour
{
    [Header("GameObject")]
    [SerializeField]
    GameObject go;

    [Header("Controlador")]
    public bool isSelected = false;

    [Header("Tamaños")]
    [SerializeField]
    Vector3 scaleUp;
    [SerializeField]
    Vector3 scaleDown;
    [Header("Colores")]
    [SerializeField]
    Color defColor;
    [SerializeField]
    Color selectedColor;

    // Start is called before the first frame update
    void Start()
    {
        isSelected = false;
        go = this.gameObject;
        scaleUp = go.transform.localScale * 1.5f;
        scaleDown = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        SelectedAnimation();
    }

    void SelectedAnimation()
    {
        if (isSelected)
        {
            go.LeanColor(selectedColor, .05f);
            LeanTween.scale(go, scaleUp, .125f);
        }
        else
        {
            //go.LeanColor(defColor, .05f);
            LeanTween.scale(go, scaleDown, .125f);
        }
    }
}
