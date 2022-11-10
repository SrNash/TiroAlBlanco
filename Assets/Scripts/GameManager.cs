using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Material hitMaterial, defMaterial;
    public AudioClip shotSound, metalHitSound, emptySound, reloadSound;
    private AudioSource gunAudioSource;

    [SerializeField]
    int maxAmmo, ammo;
    [SerializeField]
    TextMeshProUGUI bulletsText;

    [SerializeField]
    int score;
    [SerializeField]
    TextMeshProUGUI scoreText;

    [SerializeField]
    float timerLevel;
    [SerializeField]
    TextMeshProUGUI timerText;

    [Header("FireRate")]
    [SerializeField]
    float fireRate;
    [SerializeField]
    float nextFire;
    [SerializeField]
    float cooldownSeconds;
    [SerializeField]
    float cooldown;

    void Start()
    {
        maxAmmo = 12;
    }
    void Awake()
    {
        scoreText.text = "SCORE: " + score.ToString();

        gunAudioSource = GetComponent<AudioSource>();
        ammo = maxAmmo;
    }
    // Update is called once per frame
    void Update()
    {
        if (ammo <= 0)
            ammo = 0;

        //Control del disparo, DPS o fire rate

        if (((Input.touchCount >= 1 && Input.GetTouch(0).phase == TouchPhase.Ended) || (Input.GetMouseButtonUp(0))) && ammo != 0)
        {
            Shoot();
            
            /*
            gunAudioSource.PlayOneShot(shotSound);
            Vector3 pos = Input.mousePosition;
            if (Application.platform == RuntimePlatform.Android)
            { 
                pos = Input.GetTouch(0).position;
            }

            Ray rayo = Camera.main.ScreenPointToRay(pos);
            RaycastHit hitInfo;
            if (Physics.Raycast(rayo, out hitInfo)) 
            {
                if (hitInfo.collider.tag.Equals("Lata"))
                {
                    gunAudioSource.PlayOneShot(metalHitSound);
                    Rigidbody rigidbodyLata = hitInfo.collider.GetComponent<Rigidbody>();
                    rigidbodyLata.useGravity = true;
                    rigidbodyLata.AddForce(rayo.direction * 50f, ForceMode.VelocityChange);
                    hitInfo.collider.GetComponent<MeshRenderer>().material = hitMaterial;
                    score += 10;
                }
                else
                { score -= 5; }


            }
            else
            { score -= 5; }*/
        }
        if (ammo == 0)
        {
            cooldown += Time.deltaTime;
            if (cooldown >= cooldownSeconds)
            {
                gunAudioSource.PlayOneShot(reloadSound);
                cooldown = 0;
                ammo += maxAmmo;
                //StartCoroutine(ReloadCoroutine());
            }

            if (((Input.touchCount >= 1 && Input.GetTouch(0).phase == TouchPhase.Ended) || (Input.GetMouseButtonUp(0))))
                gunAudioSource.PlayOneShot(emptySound,1.5f);
        }

        

        if (score <= 0) //Cantidad mín. de puntuación siempre será 0
            score = 0;

        bulletsText.text = ammo.ToString() + " / " + maxAmmo.ToString();
        scoreText.text = "SCORE: " + score.ToString();

        TimerTextShow();
    }

    void Shoot()
    {
        ammo--;

        gunAudioSource.PlayOneShot(shotSound);
        Vector3 pos = Input.mousePosition;
        if (Application.platform == RuntimePlatform.Android)
        {
            pos = Input.GetTouch(0).position;
        }

        Ray rayo = Camera.main.ScreenPointToRay(pos);
        RaycastHit hitInfo;
        if (Physics.Raycast(rayo, out hitInfo))
        {
            if (hitInfo.collider.tag.Equals("Lata"))
            {
                gunAudioSource.PlayOneShot(metalHitSound);
                Rigidbody rigidbodyLata = hitInfo.collider.GetComponent<Rigidbody>();
                rigidbodyLata.useGravity = true;
                rigidbodyLata.AddForce(rayo.direction * 50f, ForceMode.VelocityChange);
                hitInfo.collider.GetComponent<MeshRenderer>().material = hitMaterial;
                score += 10;
            }
            else if (hitInfo.collider.tag.Equals("Diana"))
            {
                gunAudioSource.PlayOneShot(metalHitSound);
                Transform scaleDiana = hitInfo.collider.GetComponent<Transform>();
                Checker checkerDiana = hitInfo.collider.GetComponent<Checker>();
                //Agrandamos
                if(!checkerDiana.hitted)
                {
                    scaleDiana.localScale = new Vector3(scaleDiana.localScale.x * 1.125f, scaleDiana.localScale.y * 1.125f, scaleDiana.localScale.z);
                    hitInfo.collider.GetComponent<MeshRenderer>().material = hitMaterial;
                    checkerDiana.hitted = true;
                }else if(checkerDiana.hitted)
                {
                    scaleDiana.localScale = new Vector3(scaleDiana.localScale.x / 1.125f, scaleDiana.localScale.y / 1.125f, scaleDiana.localScale.z);
                    hitInfo.collider.GetComponent<MeshRenderer>().material = defMaterial;
                    checkerDiana.hitted = false;
                }
                score += 10;
            }
            else
            { score -= 5; }


        }
        else
        { score -= 5; }
    }
    void TimerTextShow()
    {
        timerLevel += Time.deltaTime;

        //Dividiremos dicho tiempo en minutos y segundos
        int seconds = (int)(timerLevel % 60);    //Realizamos una operación para comprobar si hemos llegado a contar 60 segundos
        int minutes = (int)(timerLevel / 60) % 60;   //Realizamos una operación para conocer la cantidad de minutos que llebamos

        if (seconds <= 9f)
        {
            timerText.text = "0" + minutes.ToString() + ":0" + seconds.ToString();
        }
        else if (seconds >= 10f && seconds <= 59.9f)
        {
            timerText.text = "0" + minutes.ToString() + ":" + seconds.ToString();
        }
    }

    //A LO GUARRISIMO
    IEnumerator ReloadCoroutine()
    {
        //gunAudioSource.PlayOneShot(reloadSound);
        ammo += maxAmmo;

        yield return new WaitForSeconds(cooldownSeconds + 1.5f);
    }
}
