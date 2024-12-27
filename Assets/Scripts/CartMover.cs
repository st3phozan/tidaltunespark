using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartMover : MonoBehaviour
{
	public bool landedOn = false, collided = false;
	public GameObject stopPos; 
	Rigidbody2D rb;
	public Vector2 direction = Vector2.right; 

	private GameObject player, parentPlayer;
	public float speed = 1f;
	void Start()
	{
	rb = GetComponent<Rigidbody2D>();
	parentPlayer = player.transform.parent.gameObject;
	}

	// Update is called once per frame
	void Update()
	{
		//player lands on cart
		if (landedOn && !collided)
		{
			rb.velocity = direction*speed;
			//player.transform.parent = transform;
			player.transform.position = new Vector3(transform.position.x, player.transform.position.y, player.transform.position.z);
			
		}

		//cart stops at object
		if ((stopPos.transform.position.x - transform.position.x ) <= 10)
		{
			collided = true;
			//player.transform.parent = parentPlayer.transform;
			player = null;
			//rb.velocity = Vector2.zero;
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			landedOn = true;
			player = col.gameObject;
		}
	}
}