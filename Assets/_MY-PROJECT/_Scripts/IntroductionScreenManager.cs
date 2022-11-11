using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroductionScreenManager : MonoBehaviour
{
    [Header("Logo Image Animation")]
    [SerializeField]
    GameObject swordFish_Logo;
    [SerializeField]
    float logoMoveTo;
    [SerializeField]
    float logoTimer;

    [Header("Logo Name Animation")]
    [SerializeField]
    CanvasGroup nameLogo;
    [SerializeField]
    float alphaIntensity = 1f;
    [SerializeField]
    float fadeInTime, fadeOutTime;

    // Start is called before the first frame update
    void Start()
    {
        LeanTween.moveLocalX(swordFish_Logo, logoMoveTo, 10f).setOnComplete(LogoNameFadeIn);
    }

    // Fade In del Logo Nombre
    public void LogoNameFadeIn()
    {
        LeanTween.alphaCanvas(nameLogo, alphaIntensity, fadeInTime).setOnComplete(LogoNameFadeOut);
    }
    //Fade Out del Logo Name
    public void LogoNameFadeOut()
    {
        alphaIntensity = 0f;
        LeanTween.alphaCanvas(nameLogo, alphaIntensity, fadeOutTime);
    }
}
