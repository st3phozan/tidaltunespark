using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
	public Rigidbody2D body;
	public GameObject CharacterSwap;
	public int collisions = 0;
	public float jumpSpeed = 1;
	public GameObject arrow, camera, launchBar;
	public TrajectoryTracker traj;
	public float speed = 1;
	public bool launching = false, flopping = false, fixOrient = false, inAir = false, swimming = false, tutorialMode = true, isCol = false, chosen = false;
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

	private LineRenderer lr;
	public float orientTime = 0f;
	public string LevelName;
	// Start is called before the first frame update
	void Start()
	{
		body = GetComponent<Rigidbody2D>();
		idleDir = transform.rotation;
		fishRenderer = GetComponent<SpriteRenderer>();
		lr = GetComponent<LineRenderer>();

	}

	// Update is called once per frame
	void Update()
	{
		if (chosen){
		//Debug.Log(collisions);
		//left and right movement
		CharacterSwap.transform.position = transform.position;
		 xInput = Input.GetAxis("Horizontal");
        transform.position += new Vector3(xInput, 0, 0) * (speed * Time.deltaTime);
		
    
		launchBar.transform.position = new Vector3(transform.position.x, transform.position.y - 1.2f, transform.position.z);
		arrow.transform.position = new Vector3(transform.position.x + 1.4f, transform.position.y - .2f, transform.position.z);

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
			SceneManager.LoadScene(LevelName);
			ScoreVarPersistent.RestartCt += 1;
		}

		//launching fish
		if (collisions > 0)
		{
			if (Input.GetButtonDown("Jump"))
			{
				lr.enabled = true;
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
				lr.enabled = false;
				fishRenderer.sprite = normal;
				PlaySpecificClip(4);
				launchBarAnim.SetBool("launching", true);
				flopFactor = 1;
				arcStrength = (Time.time - startArc) * flopFactor;
				
				float launcherClamp = Mathf.Clamp(arcStrength * launchStrength, 0, 20);
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
				traj.trajOn = false;
			}
		}
		}


		if (launching)
		{
			// Debug.Log(Time.time - startArc);
			
			arrow.SetActive(true);
			float rotationArc = Mathf.Clamp(((Time.time - startArc) * 50), 0, 90);
			arrow.transform.rotation = Quaternion.Euler(0, 0, rotationArc);
			
			float trajClamp = Mathf.Clamp((Time.time - startArc) * launchStrength, 0, 20);
			transform.rotation = Quaternion.Euler(0, 0, launchStrength + 50);
			Vector2 trajVelocity = transform.right*trajClamp;
			Vector2[] trajectory = Plot(body, (Vector2)transform.position, trajVelocity, 1000);
			lr.positionCount = trajectory.Length;
			Vector3[] positions = new Vector3[trajectory.Length];
			for (int i = 0; i < trajectory.Length; i++){
				positions[i] = trajectory[i];
			}
			lr.SetPositions(positions);
		}

		//tutorialMode = !tut.tutorialDone;
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
	public Vector2[] Plot(Rigidbody2D rigidbody, Vector2 pos, Vector2 velocity, int steps){

        Vector2[] results = new Vector2[steps];
        float timestep = Time.fixedDeltaTime / Physics2D.velocityIterations;
        Vector2 gravityAccel = Physics2D.gravity * rigidbody.gravityScale *timestep * timestep;
        float drag = 1f - timestep *rigidbody.drag;
        Vector2 moveStep = velocity * timestep;
        for (int i = 0; i < steps; i++) {
            moveStep += gravityAccel;
            moveStep *= drag;
            pos += moveStep;
            results[i] = pos;
        }
        return results;;
    }
	public void chooseCharacter(){
		flopStart = Time.time;
	}
	void OnCollisionEnter2D(Collision2D col)
	{
		if (chosen){
		collisions++;
		isCol = true;
		//Debug.Log("isCol is on" + col.gameObject.tag);
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
			if (!launching)
			{
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
	}


	void OnTriggerEnter2D(Collider2D col)
	{
		//collisions++;
		isCol = true;
		//Debug.Log("isCol is on" + col.gameObject.tag);
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
			Debug.Log("FREE DEM FISHSES");
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
		//collisions--;
		isCol = false;

		//Debug.Log("isCol is off" + isCol);
		inAir = false;
		body.drag = 0;
	}
	void OnCollisionExit2D(Collision2D col)
	{
		collisions--;
		isCol = false;

		//Debug.Log("isCol is off" + isCol);
		inAir = false;
		body.drag = 0;
	}
}