using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
	Rigidbody2D body;
	public float jumpSpeed = 1;
	public GameObject arrow, camera, launchBar;
	public bool launching = false, flopping = false, fixOrient = false, inAir = false, swimming = false, tutorialMode = true, isCol = false;
	public float startArc, arcStrength, launchStrength = 15, flopStart = 1, moveSpeed = 2, health = 0, maxFlopTime = 10, flopFactor = 1;
	public Quaternion startDir, idleDir;
	public AudioClip[] audioClips;
	public Animator launchBarAnim, fishAnim;
	public Transform startPos;
	public Tutorial tut;
	// public Sprite[] tempFishSprites;
	public AudioSource audioSource;
	public AudioSource sfxLoop, sfxLoopSwim;
	private SpriteRenderer fishRenderer;
	public Sprite normal, flop;
	public float xInput;
	public float orientTime = 0f;
	// Start is called before the first frame update
	void Start()
	{
		body = GetComponent<Rigidbody2D>();
		idleDir = transform.rotation;
		fishRenderer = GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update()
	{
		//left and right movement
		xInput = Input.GetAxis("Horizontal");
		transform.position = new Vector3(transform.position.x + xInput / 50, transform.position.y, transform.position.z);
		//health bar information
		if (swimming && !sfxLoopSwim.isPlaying)
		{
			//Debug.Log("swimming");
			sfxLoopSwim.clip = audioClips[3];
			sfxLoopSwim.Play();
		}
		else if (!swimming)
		{
			sfxLoopSwim.Stop();
		}

		if (flopping)

		{
			fishAnim.SetBool("flopping", true);

			float flopTime = Time.time - flopStart;
			health = (maxFlopTime - flopTime) / maxFlopTime;
			flopFactor = health;
		//Debug.Log(maxFlopTime - flopTime + " " + health);
		}

		if (flopping == false)
		{
			fishAnim.SetBool("flopping", false);
			if (health < 1)
			{
				health = 1;
			}
		}

		if (health <= 0)
		{
			SceneManager.LoadScene("Level1");
		}

		//launching fish
		if (isCol){
		if (Input.GetButtonDown("Jump"))
		{
			launchStrength = 15;
			arrow.SetActive(true);
			launchBar.SetActive(true);
			//Debug.Log("jump");
			startArc = Time.time;
			launching = true;
			PlaySpecificClip(1);
			launchBarAnim.SetBool("launching", true);
		}

		//Debug.Log(transform.forward);
		if (Input.GetButtonUp("Jump"))
		{
			fishRenderer.sprite = normal;
			PlaySpecificClip(4);
			launchBarAnim.SetBool("launching", true);
			flopFactor = 1;
			arcStrength = (Time.time - startArc) * flopFactor;
			transform.rotation = Quaternion.Euler(0, 0, launchStrength + 50);
			float launcherClamp = Mathf.Clamp (arcStrength * launchStrength, 0, 50);
			body.AddForce(transform.right * launcherClamp, ForceMode2D.Impulse);
			inAir = true;
			//Debug.Log(arcStrength);
			launching = false;
			arrow.transform.rotation = Quaternion.Euler(0, 0, -90);
			arrow.SetActive(false);
			launchBar.SetActive(false);
			audioSource.Stop();
			sfxLoopSwim.Stop();
			sfxLoop.Stop();
		}
		}

		if (launching)
		{
			// Debug.Log(Time.time - startArc);
			arrow.SetActive(true);
			float rotationArc = Mathf.Clamp(((Time.time - startArc) * 50), 0, 90);
			arrow.transform.rotation = Quaternion.Euler(0, 0, rotationArc);
		}

tutorialMode = !tut.tutorialDone;
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
		//Debug.LogWarning("Clip index out of range.");
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		isCol = true;
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
		else if (col.gameObject.tag == "dry" && flopping == false)
		{
			if (!launching){
			fishRenderer.sprite = flop; 
			}
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
		isCol = true;
		if (col.gameObject.tag == "tank")
		{
			//gameRestarted = true;
			startDir = transform.rotation;
			orientTime = 0;
			fixOrient = true;
			body.drag = 2;
			flopping = false;
			if (inAir)
			{
				PlaySpecificClip(0);
			}

			inAir = false;
			if (xInput != 0)
			{
				swimming = true;
			}
		}

		if (col.gameObject.tag == "locked")
		{
			//gameRestarted = true;
			flopping = false;
			col.gameObject.GetComponent<FishTank>().FreeFish();
			PlaySpecificClip(5);
		}

		if (col.gameObject.tag == "tutorial")
		{
			//gameRestarted = true;
			tut.TutChange();
			Destroy(col.gameObject);
		}
	}

	void OnTriggerStay2D(Collider2D col)
	{
		
		/*if (col.gameObject.tag == "tank")
		{
			if (xInput != 0)
			{
				swimming = true;
			}

			if (xInput == 0)
			{
				swimming = false;
			}
		}*/
	}

	void OnTriggerExit2D(Collider2D col)
	{
		inAir = false;
		body.drag = 0;
	}
}