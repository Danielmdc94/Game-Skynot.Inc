using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class CharacterController2D : MonoBehaviour
{
    // Move player in 2D space
    public float maxSpeed = 3.4f;
	public AnimationCurve acceleration;
	public float accelTime = 1f;
	[Space]
	public LayerMask groundMask;
    public float jumpHeight = 6.5f;

	Player player;
	Rigidbody2D r2d;
    CapsuleCollider2D mainCollider;
    Transform t;
	SpriteRenderer rend;

	private  float colliderRadius;
    private bool isGrounded = false;

    // Use this for initialization
    private void Awake()
    {
        t = transform;
        r2d = GetComponent<Rigidbody2D>();
        mainCollider = GetComponent<CapsuleCollider2D>();
<<<<<<< HEAD
		rend = GetComponent<SpriteRenderer>();
		player = GetComponent<Player>();
		
		colliderRadius = mainCollider.size.x * 0.4f * Mathf.Abs(transform.localScale.x);
=======

        r2d.freezeRotation = true;
        r2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        facingRight = t.localScale.x > 0;
>>>>>>> 134a34513add06aa9cd1a1a6f171b67dd0a94cba
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD
=======
        // Movement controls

        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && (isGrounded || Mathf.Abs(r2d.velocity.x) > 0.01f))
        {
            moveDirection = Input.GetKey(KeyCode.A) ? -1 : 1;
        }
        else
        {
            if (isGrounded || r2d.velocity.magnitude < 0.01f)
            {
                moveDirection = 0;
            }
        }

        // Change facing direction
        if (moveDirection != 0)
        {
            if (moveDirection > 0 && !facingRight)
            {
                facingRight = true;
                t.localScale = new Vector3(Mathf.Abs(t.localScale.x), t.localScale.y, transform.localScale.z);
            }
            if (moveDirection < 0 && facingRight)
            {
                facingRight = false;
                t.localScale = new Vector3(-Mathf.Abs(t.localScale.x), t.localScale.y, t.localScale.z);
            }
        }

        // Jumping
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            r2d.velocity = new Vector2(r2d.velocity.x, jumpHeight);
        }
>>>>>>> 134a34513add06aa9cd1a1a6f171b67dd0a94cba
    }

    void FixedUpdate()
    {
		Vector2 velocity = r2d.velocity;
		isGrounded = CheckGround();
		
        // Calculate movement velocity
		velocity.x = player.moveDir.x * maxSpeed;

		// Jumping
		if (player.queJump && isGrounded)
			velocity.y = Mathf.Sqrt(-2f * jumpHeight * Physics2D.gravity.y * r2d.gravityScale);

		// Apply forces
		r2d.velocity = velocity;

		// Update Graphic
		if (!rend.flipX && r2d.velocity.x < 0f)
			rend.flipX = true;
		else if (rend.flipX && r2d.velocity.x > 0f)
			rend.flipX = false;
    }

	private bool CheckGround()
	{
		Bounds colliderBounds = mainCollider.bounds;
		Vector3 groundCheckPos = colliderBounds.min + new Vector3(colliderBounds.size.x * 0.5f, colliderRadius * 0.9f, 0);

		// Simple debug
		//        Debug.DrawLine(groundCheckPos, groundCheckPos - new Vector3(0, colliderRadius, 0), isGrounded ? Color.green : Color.red);
		//        Debug.DrawLine(groundCheckPos, groundCheckPos - new Vector3(colliderRadius, 0, 0), isGrounded ? Color.green : Color.red);
		
		// Check if player is grounded
		Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckPos, colliderRadius, groundMask);
		foreach (Collider2D c in colliders)
			if (c != mainCollider)
				return (true);
		return (false);
	}

private void OnDrawGizmosSelected()
{
	if (mainCollider == null)
		mainCollider = GetComponent<CapsuleCollider2D>();
	Gizmos.color = Color.yellow;
	Gizmos.DrawWireCube(new Vector3(mainCollider.bounds.center.x, mainCollider.bounds.min.y + jumpHeight * .5f, 0f), new Vector3(.4f, jumpHeight, 0f));
}

}
