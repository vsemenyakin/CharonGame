using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Unit {

	[SerializeField]
	private int lives = 5;
	[SerializeField]
	private float speed = 3.0F;
	[SerializeField]
	private float jumpForce = 15.0F;

	private bool isGrounded = false;



	private CharState State
	{
		get{
			return (CharState)animator.GetInteger ("State");
		}
		set{
			animator.SetInteger ("State", (int)value);
		}
	}

	new private Rigidbody2D rigidbody;
	private Animator animator;
	private SpriteRenderer sprite;

	private void Awake()
	{
		rigidbody = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
		sprite = GetComponentInChildren<SpriteRenderer> ();


	}

	private void FixedUpdate()
	{
		CheckGround ();
	}

	private void Update()
	{
		if(isGrounded) State = CharState.Idle;


		if (Input.GetButton ("Horizontal")) {
			if(isGrounded) State = CharState.Run;
			Run ();
		}
			
		if (isGrounded && Input.GetButtonDown ("Jump"))
			Jump ();
	}

	private void Run()
	{
		Vector3 direction = transform.right * Input.GetAxis ("Horizontal");//в зависимости от нажатия на "A" или "D" будет возвращать "1" или "-1"

		transform.position = Vector3.MoveTowards (transform.position, transform.position + direction, speed * Time.deltaTime);

		sprite.flipX = direction.x < 0.0F;


	}

	private void Jump()
	{
		rigidbody.AddForce (transform.up * jumpForce, ForceMode2D.Impulse);

	}



	public override void ReceiveDamage(){
		lives--;
		Debug.Log (lives);
		rigidbody.velocity = Vector3.zero;
		rigidbody.AddForce (transform.up * 8.0F, ForceMode2D.Impulse);
	}

	private void CheckGround()
	{

		Vector3 playerPosition = transform.position;
		playerPosition.y -= 1.2F;
		Collider2D[] colliders = Physics2D.OverlapCircleAll (playerPosition, 0.1F);

		isGrounded = colliders.Length > 1;
		Debug.Log (State);
		

		if(!isGrounded) State = CharState.Jump;
	}

	private void OnTriggerEnter2D(Collider2D collider){
		/*Unit unit = collider.gameObject.GetComponent<Unit> ();
		if (unit)
			ReceiveDamage ();   */
	}

}

public enum CharState
{
	Idle,
	Run,
	Jump,
}