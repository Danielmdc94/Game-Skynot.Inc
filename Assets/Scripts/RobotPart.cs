using System.Collections.Generic;
using UnityEngine;

public class RobotPart : MonoBehaviour
{
	private static GameManager gm;

	private SpriteRenderer rend;

	public enum RobotPartType
	{
		Head, Torso, Legs
	};

	public static RobotPartType[] rpTypes = {
		RobotPartType.Head,
		RobotPartType.Torso,
		RobotPartType.Legs
	};
	
	public static Color[] colors = {
		Color.red,
		Color.white,
		Color.blue,
		Color.green
	};

	[HideInInspector]
	public RobotPartType partType = RobotPartType.Head;
	private int colorIndex;

	void Awake()
	{
		if (gm == null)
			gm = GameManager.instance;
		rend = GetComponent<SpriteRenderer>();
	}
	
	public void Initialize()
	{
		partType = RandomType();
		rend.sprite = gm.GetImage(partType);
		// set color index and color
	}

	void FixedUpdate()
    {
		if (!gm.gameArea.Contains(transform.position))
			SpawnManager.RecallToPool(this);
    }
	
	private void OnTriggerEnter2D(Collider2D  other)
	{
		RobotDeposit deposit = other.GetComponent<RobotDeposit>();
		if (deposit == null)
			return ;
		deposit.Evaluate(this);
	}

	public static RobotPartType RandomType()
	{
		int index = Random.Range(0, rpTypes.Length);
		return (rpTypes[index]);
	}
}
