using UnityEngine;

public class Player : MonoBehaviour
{
	[HideInInspector]
	public Vector2 moveDir;
	public bool queJump = false;

	private const float jumpBufferTime = .2f;
	private float jumpBuffer = 0f;
	
	private void Update()
	{
		moveDir.x = Input.GetAxis("Horizontal");
		moveDir.y = Input.GetAxis("Vertical");

		jumpBuffer -= Time.deltaTime;
		if (Input.GetButtonDown("Jump"))
		{
			queJump = true;
			jumpBuffer = jumpBufferTime;
		}
		else if (jumpBuffer <= 0f)
			queJump = false;
	}
}
