using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D body;
    public float jumpSpeed = 1;

    public GameObject arrow, camera;
    public bool launching = false, flopping = false;
    public float startArc, arcStrength, launchStrength = 15, flopStart = 1, moveSpeed =2;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        camera.transform.position = new Vector3(transform.position.x + 10, camera.transform.position.y, camera.transform.position.z);
        Debug.Log(body.velocity.y);
        float xInput = Input.GetAxis("Horizontal");
        transform.position = new Vector3(transform.position.x + xInput/50, transform.position.y,  transform.position.z);
        /*if (flopping){
        Debug.Log(launchStrength / (Time.time - flopStart));
        }*/
        if (Input.GetButtonDown("Jump"))
        {
            flopping = false;
            launchStrength = 15;
            Debug.Log("jump");
            startArc = Time.time;
            launching = true;

        }

        //Debug.Log(transform.forward);
        if (Input.GetButtonUp("Jump"))
        {


            arcStrength = Time.time - startArc;
            transform.rotation = Quaternion.Euler(0, 0, arcStrength * 50);
            body.AddForce(transform.right * arcStrength * launchStrength, ForceMode2D.Impulse);
            Debug.Log(arcStrength);

            launching = false;


        }
        if (launching)
        {
            Debug.Log(Time.time - startArc);
            arrow.SetActive(true);
            arrow.transform.rotation = Quaternion.Euler(0, 0, ((Time.time - startArc) * 50) - 90);
        }
        else
        {
            if (body.velocity.y < 0)
            {
                transform.right = new Vector3(0, -1, 0) - transform.position;

            }
            arrow.transform.rotation = Quaternion.Euler(0, 0, -90);
            arrow.SetActive(false);
        }

    }





    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("LAND IN WATER");
        if (col.gameObject.tag == "water")
        {
            //gameRestarted = true;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            body.velocity = new Vector3(0, 0, 0);
        }
        else if (col.gameObject.tag == "dry")
        {
            //flopping = true;
            //gameRestarted = true;
            //flopStart =Time.time; 
            transform.rotation = Quaternion.Euler(0, 0, 0);
            body.velocity = new Vector3(0, 0, 0);
        }
    }
}
