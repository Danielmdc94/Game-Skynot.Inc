using UnityEngine;

public class Player : MonoBehaviour
{
	[HideInInspector] public Vector2 moveDir;
	[HideInInspector] public bool queJump = false;
	[HideInInspector] public bool queInteract = false;

	private static bool initP1 = false;
	private bool isP1;

	private const float jumpBufferTime = .2f;
	private float jumpBuffer = 0f;

	private const float actionBufferTime = .05f;
	private float actionBuffer;

	private void Awake()
	{
		isP1 = !initP1;
		if (!initP1)
			initP1 = true;
	}

	private void Update()
	{
		jumpBuffer -= Time.deltaTime;
		actionBuffer -= Time.deltaTime;

		if (isP1)
			GetInputPlayer1();
		else GetInputPlayer2();
	}

	private void GetInputPlayer1()
	{
		KeyCode keyR = KeyCode.D, keyL = KeyCode.A;
		KeyCode keyUp = KeyCode.W, keyDown = KeyCode.S;
		GetMoveTargets(keyR, keyL, keyUp, keyDown);

		KeyCode[] interactKeys = {KeyCode.E, KeyCode.Q, KeyCode.LeftShift};
		GetAction(interactKeys);

		KeyCode[] jumpKeys = {keyUp, KeyCode.Space};
		GetJumpState(jumpKeys);
	}

	private void GetInputPlayer2()
	{
		KeyCode keyR = KeyCode.RightArrow, keyL = KeyCode.LeftArrow;
		KeyCode keyUp = KeyCode.UpArrow, keyDown = KeyCode.DownArrow;
		GetMoveTargets(keyR, keyL, keyUp, keyDown);

		KeyCode[] interactKeys = {KeyCode.RightShift, KeyCode.RightControl, KeyCode.RightApple};
		GetAction(interactKeys);

		KeyCode[] jumpKeys = {keyUp};
		GetJumpState(jumpKeys);
	}

	private void GetMoveTargets(KeyCode right, KeyCode left, KeyCode up, KeyCode down)
	{
		moveDir.x = (Input.GetKey(right) ? 1f : 0f) + (Input.GetKey(left) ? -1f : 0f);
		moveDir.y = (Input.GetKey(up) ? 1f : 0f) + (Input.GetKey(down) ? -1f : 0f);
	}
	
	private void GetJumpState(KeyCode[] keys)
	{
		foreach (KeyCode k in keys)
			if (Input.GetKeyDown(k))
			{
				queJump = true;
				jumpBuffer = jumpBufferTime;
			}
			else if (jumpBuffer <= 0f)
				queJump = false;
	}

	private void GetAction(KeyCode[] keys)
	{
		foreach (KeyCode k in keys)
			if (Input.GetKeyDown(k))
			{
				queInteract = true;
				actionBuffer = actionBufferTime;
			}
			else if (actionBuffer <= 0f)
				queInteract = false;
	}
}
