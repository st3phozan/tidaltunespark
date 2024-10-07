using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D body;
    public float jumpSpeed = 1;

    public GameObject arrow, camera;
    public bool launching = false, flopping = false, fixOrient = false, inAir = false, swimming = false;
    public float startArc, arcStrength, cameraOffset = 10, launchStrength = 15, flopStart = 1, moveSpeed = 2, health = 0, maxFlopTime = 5;
    public Quaternion startDir, idleDir;
    public AudioClip[] audioClips;

   // public Sprite[] tempFishSprites;
    public AudioSource audioSource;
    public AudioSource sfxLoop, sfxLoopSwim;
    public float xInput;
    public float orientTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        idleDir = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        camera.transform.position = new Vector3(transform.position.x + cameraOffset, camera.transform.position.y, camera.transform.position.z);

        //left and right movement
        xInput = Input.GetAxis("Horizontal");
        transform.position = new Vector3(transform.position.x + xInput / 50, transform.position.y, transform.position.z);
        

        //health bar information
        if (swimming && !sfxLoopSwim.isPlaying){
            Debug.Log("swimming");
            sfxLoopSwim.clip = audioClips[3];
            sfxLoopSwim.Play();
        }
        else if(!swimming){
            sfxLoopSwim.Stop();
        }
        
        if (flopping)
        {
            float flopTime = Time.time - flopStart;
            health = (maxFlopTime - flopTime) / maxFlopTime;
            //launchStrength -= (flopTime);
            Debug.Log(maxFlopTime - flopTime + " " + health);
        }
        if (flopping == false)
        {
            if (health < 1)
            {
                health = 1;
            }
        }

        //launching fish
        if (Input.GetButtonDown("Jump"))
        {

            launchStrength = 15;
            //Debug.Log("jump");
            startArc = Time.time;
            launching = true;
            PlaySpecificClip(1);

        }

        //Debug.Log(transform.forward);
        if (Input.GetButtonUp("Jump"))
        {
            PlaySpecificClip(4);
            flopping = false;
            arcStrength = Time.time - startArc;
            transform.rotation = Quaternion.Euler(0, 0, launchStrength+50);
            body.AddForce(transform.right * arcStrength * launchStrength, ForceMode2D.Impulse);
            inAir = true;
            //Debug.Log(arcStrength);
            launching = false;
            arrow.transform.rotation = Quaternion.Euler(0, 0, -90);
            arrow.SetActive(false);
            audioSource.Stop();
            sfxLoopSwim.Stop();
            sfxLoop.Stop();


        }
        if (launching)
        {
            // Debug.Log(Time.time - startArc);
            arrow.SetActive(true);
            arrow.transform.rotation = Quaternion.Euler(0, 0, ((Time.time - startArc) * 50));
        }

        
        /*if(!fixOrient)
        {
            if (body.velocity.y < 0)
            {
                transform.right = new Vector3(0, -1, 0) - transform.position;

            }
            
        }

        if (fixOrient)
        {
            orientTime += Time.deltaTime;
            float t = orientTime / 2;

            transform.rotation = Quaternion.Lerp(startDir, idleDir, t);

            if (t >= 1f)
            {
                fixOrient = false;  // Stop lerping when complete
            }

        }*/

    }



    void PlaySpecificClip(int index)
{
    if (index >= 0 && index < audioClips.Length)
    {
        audioSource.clip = audioClips[index];
        audioSource.Play();
    }
    else
    {
        Debug.LogWarning("Clip index out of range.");
    }
}

    void OnCollisionEnter2D(Collision2D col)
    {
        //Debug.Log("LAND IN WATER");
        if (col.gameObject.tag == "water")
        {
            //gameRestarted = true;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            body.velocity = new Vector3(0, 0, 0);
        }
        /*if (col.gameObject.tag == "tank")
        {
            //gameRestarted = true;
            body.drag = 3;
        }*/
        else if (col.gameObject.tag == "dry")
        {
            flopping = true;
            //gameRestarted = true;
            flopStart = Time.time;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            body.velocity = new Vector3(0, 0, 0);
            sfxLoop.clip = audioClips[2];
            sfxLoop.Play();
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "tank")
        {
            //gameRestarted = true;
            startDir = transform.rotation;
            orientTime = 0;
            fixOrient = true;
            body.drag = 2;
            if (inAir){
                PlaySpecificClip(0);
            }
            inAir = false;
            if (xInput != 0 ){
            swimming = true;
        }

        }
        if (col.gameObject.tag == "locked")
        {
            //gameRestarted = true;
            col.gameObject.GetComponent<FishTank>().FreeFish();
        }

    }
     void OnTriggerStay2D(Collider2D col)
    {

        if (col.gameObject.tag == "tank")
        {
            
            if (xInput != 0 ){
            swimming = true;
        }
        if (xInput == 0 ){
            swimming = false;
        }
            


        }

    }
    void OnTriggerExit2D(Collider2D col)
    {
        body.drag = 0;
        
    }
}
