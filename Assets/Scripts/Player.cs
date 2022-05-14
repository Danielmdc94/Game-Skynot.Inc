using UnityEngine;

public class Player : MonoBehaviour
{
	[HideInInspector] public Vector2 moveDir;
	[HideInInspector] public bool queJump;

	private void Update()
	{
		moveDir.x = Input.GetAxis("Horizontal");
		moveDir.y = Input.GetAxis("Vertical");
		queJump = Input.GetButtonDown("Jump");
	}
}
