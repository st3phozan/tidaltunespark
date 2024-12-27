using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracker : MonoBehaviour
{
	public GameObject Player, trackerArrow;
	public float cameraOffset = 10, BCAngle = 5, ACAngle = 7f;

	private Vector3 placement;
	public bool above = false;
	private float timeElapsed = 0f, speed = 1f;
	public Camera cam;
	// Start is called before the first frame update
	void Start()
	{
			}

	// Update is called once per frame
	void Update()
	{
		//if player is above camera view, have the camera zoom out
		transform.position = new Vector3(Player.transform.position.x + cameraOffset, transform.position.y, transform.position.z);
		if (above)
		{
			timeElapsed += Time.deltaTime * speed;
			
			cam.orthographicSize = Mathf.Lerp(BCAngle, ACAngle, timeElapsed);
	
			timeElapsed = Mathf.Clamp01(timeElapsed);
			trackerArrow.transform.position = new Vector3(Player.transform.position.x, trackerArrow.transform.position.y, trackerArrow.transform.position.z);
			
		}
		// once player lands back close, zoom back in to screen
		else if (!above)
		{
			timeElapsed += Time.deltaTime * speed;

			cam.orthographicSize = Mathf.Lerp(ACAngle, BCAngle, timeElapsed);

			timeElapsed = Mathf.Clamp01(timeElapsed);
			
		}
	}

	public void AboveTranslator()
	{
	timeElapsed = 0f;
	above = true;
	trackerArrow.SetActive(true);
	}
	public void BelowTranslator()
	{
	timeElapsed = 0f;
	above = false;
	trackerArrow.SetActive(false);
	}

	//changes focus on player once character is swapped
	public void CharacterChange(GameObject newPlayer){
		Player = newPlayer;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			AboveTranslator();
		}
	}
	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			BelowTranslator();
		}
	}
}