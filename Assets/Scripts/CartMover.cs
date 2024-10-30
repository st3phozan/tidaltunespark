using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartMover : MonoBehaviour
{
	public bool landedOn = false, collided = false;
	public GameObject stopPos; // Start is called before the first frame update
	Rigidbody2D rb;
	public Vector2 direction = Vector2.right; 
	public float speed = 1f;
	void Start()
	{
	rb = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{
		if (landedOn && !collided)
		{
			rb.velocity = direction*speed;
		}

		if (transform.position.x == (stopPos.transform.position.x - 30))
		{
			collided = true;
			rb.velocity = Vector2.zero;
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			landedOn = true;
		}
	}
}