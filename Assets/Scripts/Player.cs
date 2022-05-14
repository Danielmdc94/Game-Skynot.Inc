using UnityEngine;

public class Player : MonoBehaviour
{
	[HideInInspector]
	public Vector2 rawMoveDir;

	private void Update()
	{
		rawMoveDir.x = Input.GetAxisRaw("Horizontal");
		rawMoveDir.y = Input.GetAxisRaw("Vertical");
	}
}
