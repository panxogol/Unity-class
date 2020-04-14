using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using UnityEngine;

public class sc_move : MonoBehaviour
{
    public float forceValue;
    public float jumpValue;
    private new Rigidbody rigidbody;
    private new AudioSource jumpSound;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        jumpSound = GetComponent<AudioSource>();
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
    }

    void FixedUpdate()
    {
        rigidbody.AddForce(new Vector3(Input.GetAxis("Horizontal"),
                                       0,
                                       Input.GetAxis("Vertical")) * forceValue);
    }
}
