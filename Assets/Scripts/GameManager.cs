using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Material hitMaterial;
    public AudioClip shotSound, metalHitSound, reloadSound;
    private AudioSource gunAudioSource;

    [SerializeField]
    int maxBullets, currentBullets;
    [SerializeField]
    TextMeshProUGUI bulletsText;

    [SerializeField]
    int score;
    [SerializeField]
    TextMeshProUGUI scoreText;


    void Start()
    {
        maxBullets = 12;
    }
    void Awake()
    {
        scoreText.text = "SCORE: " + score.ToString();

        gunAudioSource = GetComponent<AudioSource>();
        currentBullets = maxBullets;
    }
    // Update is called once per frame
    void Update()
    {
        
        if (currentBullets <= 0)
            currentBullets = 0;
        
        if(((Input.touchCount >= 1 && Input.GetTouch(0).phase == TouchPhase.Ended) || (Input.GetMouseButtonUp(0))) && currentBullets != 0)
        {
            currentBullets -= 1;
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
            { score -= 5; }
        }
        else if(currentBullets <= 0)
        {
            currentBullets = 0;
            gunAudioSource.PlayOneShot(reloadSound);
            currentBullets = maxBullets;
        }

        if (score <= 0) //Cantidad mín. de puntuación siempre será 0
            score = 0;
        
        bulletsText.text = currentBullets.ToString() + " / " + maxBullets.ToString();
        scoreText.text = "SCORE: " + score.ToString();
    }
}
