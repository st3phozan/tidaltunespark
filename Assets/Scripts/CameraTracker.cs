using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracker : MonoBehaviour
{
	public GameObject Player, trackerArrow;
	public float cameraOffset = 10, BCAngle = 5, ACAngle = 7f;
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
		Debug.Log(timeElapsed);
		transform.position = new Vector3(Player.transform.position.x + cameraOffset, transform.position.y, transform.position.z);
		if (above)
		{
			timeElapsed += Time.deltaTime * speed;
			// Lerp between startPoint and endPoint positions
			cam.orthographicSize = Mathf.Lerp(BCAngle, ACAngle, timeElapsed);
			// Ensure the lerp value is clamped between 0 and 1
			timeElapsed = Mathf.Clamp01(timeElapsed);
			trackerArrow.transform.position = new Vector3(Player.transform.position.x, trackerArrow.transform.position.y, trackerArrow.transform.position.z);
			
		}
		else if (!above)
		{
			timeElapsed += Time.deltaTime * speed;
			// Lerp between startPoint and endPoint positions
			cam.orthographicSize = Mathf.Lerp(ACAngle, BCAngle, timeElapsed);
			// Ensure the lerp value is clamped between 0 and 1
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