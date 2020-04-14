//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Collections.Specialized;
//using System.Threading;
using System;
using UnityEngine;

public class sc_move : MonoBehaviour
{
    public float forceValue;
    public float jumpValue;
    public float zJumpLimit;
    public float yJumpMin;
    int points = 0;
    int deaths = 0;
    int wins = 0;
    int totalCoins;
    private new Rigidbody rigidbody;
    private AudioSource jumpSound;
    private AudioSource coinSound;
    private AudioSource deathSound;
    private AudioSource winingSound;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        AudioSource[] sphereSounds = GetComponents<AudioSource>();
        jumpSound = sphereSounds[0];
        coinSound = sphereSounds[1];
        deathSound = sphereSounds[2];
        winingSound = sphereSounds[3];

        //contar monedas iniciales}
        GameObject[] coinsTotal = GameObject.FindGameObjectsWithTag("coin");
        totalCoins = coinsTotal.Length;
    }

    //Texto de Puntaje
    void OnGUI()
    {
        GUI.Label(new Rect(5, 5, 200, 20), "Points: " + points.ToString());
        GUI.Label(new Rect(5, 25, 200, 20), "Deaths: " + deaths.ToString());
        GUI.Label(new Rect(5, 45, 200, 20), "Winnings: " + wins.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 
            //0, 
            //Input.GetAxis("Vertical") * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && Mathf.Abs(rigidbody.velocity.y) < 0.01f)
        {
            rigidbody.AddForce(Vector3.up * jumpValue, ForceMode.Impulse);
            jumpSound.Play();
        }
        
        //Para dispositivos moviles
        if(Input.touchCount == 1)
        {
            if (Input.touches[0].phase == TouchPhase.Began && Mathf.Abs(rigidbody.velocity.y) < yJumpMin)
            {
                rigidbody.AddForce(Vector3.up * jumpValue, ForceMode.Impulse);
                jumpSound.Play();
            }
        }

        //Utilizando acelerometro en Z
        if (Input.acceleration.z > zJumpLimit && Mathf.Abs(rigidbody.velocity.y) < yJumpMin)
        {
            rigidbody.AddForce(Vector3.up * jumpValue, ForceMode.Impulse);
            jumpSound.Play();
        }
    }

    void FixedUpdate()
    {
        rigidbody.AddForce(new Vector3(Input.GetAxis("Horizontal"),
                                       0,
                                       Input.GetAxis("Vertical")) * forceValue);

/*#if UNITY_ANDROID
        rigidbody.AddForce(new Vector3(Input.acceleration.x,
                                       0,
                                       Input.acceleration.y) * forceValue);
#endif */
    }

    void OnCollisionEnter(Collision collision)
    {
       if(collision.gameObject.tag == "coin")
        {
            points++;
            coinSound.Play();
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "enemy")
        {
            deaths++;
            deathSound.Play();
            rigidbody.transform.position = new Vector3(0, 2, 0);
        }

        if (collision.gameObject.tag == "finish" && points == totalCoins)
        {
            wins++;
            winingSound.Play();
            rigidbody.transform.position = new Vector3(0, 6, 0);
        }
    }
}
