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
	[Space]
	public AudioClip sfx_jump;

	Player player;
	Rigidbody2D r2d;
    CapsuleCollider2D mainCollider;
    Transform t;
	SpriteRenderer rend;
	AudioSource speaker;

	private  float colliderRadius;
    private bool isGrounded = false;

	private float accelRatio = 0f;
	private float accelDampV;
	
    // Use this for initialization
    private void Awake()
    {
        t = transform;
        r2d = GetComponent<Rigidbody2D>();
        mainCollider = GetComponent<CapsuleCollider2D>();
		rend = GetComponent<SpriteRenderer>();
		player = GetComponent<Player>();
		speaker = GetComponent<AudioSource>();
		
		colliderRadius = mainCollider.size.x * 0.4f * Mathf.Abs(transform.localScale.x);
    }

    void FixedUpdate()
    {
		Vector2 velocity = r2d.velocity;
		isGrounded = CheckGround();
		
        // Calculate movement velocity
		accelRatio = Mathf.SmoothDamp(accelRatio, Mathf.Abs(player.moveDir.x),  ref accelDampV, accelTime);
		velocity.x = player.moveDir.x * maxSpeed * acceleration.Evaluate(accelRatio);

		// Jumping
		if (player.queJump && isGrounded)
		{
			velocity.y = Mathf.Sqrt(-2f * jumpHeight * Physics2D.gravity.y * r2d.gravityScale);
			speaker.PlayOneShot(sfx_jump);
		}

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
