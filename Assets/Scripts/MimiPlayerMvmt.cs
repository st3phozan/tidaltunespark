using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimiPlayerMvmt : MonoBehaviour
{
    public bool isCamo = false, isNearObject = true;
    public bool vase, plant, crack;
    public Sprite[] mimiPhases;

    public GameObject CharacterSwap, reflObject;

    public Animator mimiAnim;
    public SpriteRenderer mimiImg, objRenderer;
    public GameObject mimiAnimObj, mimiTransObj;
    public float xInput, speed = 1;

    public float camoTimer, camoStart, health, maxTime = 10;
    public float refillTimer, refillStart, maxTimeRefill = 5;
    // Start is called before the first frame update
    void Start()
    {
        health = 1;
    }

    // Update is called once per frame
    void Update()
    {
        mimiAnim.SetBool("vase", vase);
        mimiAnim.SetBool("plant", plant);
        xInput = Input.GetAxis("Horizontal");
        mimiAnim.SetBool("camoBool", isCamo);
        mimiAnim.SetBool("vase", vase);
        mimiAnim.SetBool("plant", plant);
        mimiAnim.SetBool("crack", crack);
        transform.position += new Vector3(xInput, 0, 0) * (speed * Time.deltaTime);
        mimiAnim.SetFloat("xMovement", xInput);
        CharacterSwap.transform.position = transform.position;
        if (Input.GetButtonDown("Jump"))
        {
            /*lr.enabled = true;
            launchStrength = 15;
            arrow.SetActive(true);
            launchBar.SetActive(true);
            //Debug.Log("jump");
            startArc = Time.time;
            launching = true;
            PlaySpecificClip(1);
            launchBarAnim.SetBool("launching", true);*/
            Debug.Log("jump");
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isNearObject && health >= 1)
            {
                isCamo = !isCamo;
                if (isCamo)
                {
                    
                    camoStart = Time.time;
                    mimiAnim.SetTrigger("isCamo");
                    Transformation();

                }
            }
        }
        if (isCamo)
        {
            camoTimer = Time.time - camoStart;
            health = 1 - camoTimer / maxTime;

        }
        else if (!isCamo)
        {

            //mimiImg.sprite = mimiPhases[0];
            mimiAnimObj.SetActive(true);
            mimiImg.maskInteraction = SpriteMaskInteraction.None;
            //reflObject.GetComponent<Renderer> ().enabled = true;
            // mimiTransObj.SetActive(false);

            if (health < 1)
            {
                health = (Time.time - refillStart) / maxTimeRefill;
                Debug.Log("refresh... health:" + health);
            }
        }
        if (health <= 0)
        {
            isCamo = false;
            refillStart = Time.time;
        }
        if (isCamo)
        {

            



        }


    }
    public void Transformation()
    {
        //reflObject.GetComponent<Renderer> ().enabled = false;
        if (crack == true)
        {
            //mimiImg.sprite = mimiPhases[1];
            mimiImg.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            
            //mimiAnimObj.SetActive(false);
            //mimiAnim.SetBool("vase", true);
            //mimiTransObj.SetActive(true);
        }
        else if (plant == true)
        {
            //mimiImg.sprite = mimiPhases[1];
            Debug.Log("inrange");
            //mimiAnimObj.SetActive(false);
            //mimiAnim.SetBool("plant", true);
            //mimiTransObj.SetActive(true);
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log(' ');
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        
        isNearObject = true;
        Debug.Log("print");
        if (col.gameObject.tag == "vase"){
        vase = true;
        reflObject = col.gameObject;
        
        
        }
        else if (col.gameObject.tag == "plant"){
        plant = true;
        reflObject = col.gameObject;
        }
        else if (col.gameObject.tag == "crack"){
        crack = true;
        
        reflObject = col.gameObject;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        isNearObject = false;
        isCamo = false;
        refillStart = Time.time;
        vase = false;
        plant = false;
        crack = false;


    }
}