using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public float rotationSpeed = 200f; // Pelaajan kääntönopeus
    public float jumpForce = 400f;

    Rigidbody rb;

    Animator anim;
    Vector3 playerInput;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Pelaajan kääntäminen
        Vector3 rotation = transform.up * Input.GetAxis("Horizontal");
        transform.Rotate(rotation * rotationSpeed * Time.deltaTime);
        playerInput = transform.forward * Input.GetAxis("Vertical") * speed;

        playerInput.y = rb.velocity.y;

        // Onko pelaajan kiihtyvyys y -0.05 ja 0.05 välillä (eli suurinpiirtein 0)
        if (rb.velocity.y > -0.05f && rb.velocity.y < 0.05f)
        {
            // jos pelaaja painaa välilyöntiä
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // lisätään hahmolle voimaa YLÖSPÄIN kerrottuna hypyn voima arvolla
                rb.AddForce(Vector3.up * jumpForce);
            }
        }

        anim.SetFloat("velocity", rb.velocity.magnitude);

    }

    private void FixedUpdate()
    {
        rb.velocity = playerInput;
    }
}
