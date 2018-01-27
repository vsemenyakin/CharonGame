using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

	private Rigidbody2D rb;
	private Animator an;

	public float movementSpeed = 10f;
	public float jumpForce = 400f;
	public float maxVelocityX = 4f;

	public AudioClip soundsEffect;

	private bool Grounded;

	private Vector3 upLadder, downLadder, ladderPos;
	private bool isLadder;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		an = GetComponent<Animator>();
	}

	void Update(){
		float v = Input.GetAxis ("Vertical");
		if (isLadder) {
			LadderMode (v);
		}
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		var force = new Vector2(0f, 0f);

		float moveHorizontal = Input.GetAxis("Horizontal");

		var absVelocityX = Mathf.Abs(rb.velocity.x);
		var absVelocityY = Mathf.Abs(rb.velocity.y);

		if (absVelocityY == 0)
		{
			Grounded = true;
		}
		else
		{
			Grounded = false;
		}

		if (absVelocityX < maxVelocityX)
		{
			force.x = (movementSpeed * moveHorizontal);
		}

		if (Grounded == true && Input.GetButton("Jump"))
		{
			if (soundsEffect)
			{
				AudioSource.PlayClipAtPoint(soundsEffect, transform.position);
			}
			Grounded = false;
			force.y = jumpForce;
			an.SetInteger("AnimState", 2);
		}
		rb.AddForce(force);

		if (moveHorizontal > 0)
		{
			transform.localScale = new Vector3(1, 1, 1);
			if (Grounded)
			{
				an.SetInteger("AnimState", 1);
			}
		}
		else if (moveHorizontal < 0)
		{
			transform.localScale = new Vector3(-1, 1, 1);
			if (Grounded)
			{
				an.SetInteger("AnimState", 1);
			}
		}
		else
		{
			if (Grounded)
			{
				an.SetInteger("AnimState", 0);
			}
		}
	}
	void OnTriggerStay2D(Collider2D collider){
		/*
		if (collider.tag == "LadderCollider"&& !isLadder) {
			Ladder ladder = collider.GetComponent<Ladder> ();
			upLadder = ladder.up.position;
			downLadder = ladder.down.position;
			ladderPos = collider.transform.position;
			isLadder = true;


		}
*/
	}

	void LadderMode(float vertical){
		if (transform.position.y < upLadder.y && vertical>0){
			rb.isKinematic = true;

		}else if (transform.position.y> downLadder.y && vertical <0){

		}
	}
}
