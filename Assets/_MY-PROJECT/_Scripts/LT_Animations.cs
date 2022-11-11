using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LT_Animations : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject diana;

    [Header("Tiempos")]
    [SerializeField]
    float timeScaleUp;
    [SerializeField]
    float timeScaleDown;
    [SerializeField]
    float timeColorChange;

    [Header("Escalados")]
    [SerializeField]
    Vector3 scaleUp;
    [SerializeField]
    Vector3 scaleDown;

    [Header("Materiales")]
    [SerializeField]
    Color defColor;
    [SerializeField]
    Color hitColor;

    [Header("Comprobador")]
    [SerializeField]
    bool clicked = false;

    private void Start()
    {
        diana = this.gameObject;
    }

    public void HittedDiana()
    {
        if (!clicked)
        {
            clicked = true;
            ScaledUp();
        }
        else if (clicked)
        {
            clicked = false;
            ScaleDown();
        }
    }
    public void ScaledUp()
    {
        LeanTween.scale(diana, scaleUp, timeScaleUp).setEaseOutBounce();
        diana.LeanColor(hitColor, timeColorChange).setEaseInOutQuart();
    }
    public void ScaleDown()
    { 
        LeanTween.scale(diana, scaleDown, timeScaleDown).setEaseInBounce();
        diana.LeanColor(defColor, timeColorChange).setEaseInOutQuart();
    }
}
