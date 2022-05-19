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

	private static RobotPartType[] rpTypes = {
		RobotPartType.Head,
		RobotPartType.Torso,
		RobotPartType.Legs
	};
	
	[HideInInspector] public RobotPartType partType = RobotPartType.Head;
	[HideInInspector] public int colorIndex;

	public void Awaken()
	{
		gm = GameManager.instance;
		rend = GetComponent<SpriteRenderer>();
	}
	
	public void Initialize()
	{
		partType = RandomType();
		rend.sprite = gm.GetImage(partType);
		rend.color = RandomColor(ref colorIndex);
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

	public static Color RandomColor(ref int index)
	{
		index = GameManager.RndColorIndex();
		return (GameManager.GetColor(index));
	}
	public static Color GetColor(int index)
	{
		return (GameManager.GetColor(index));
	}
	
	public static RobotPartType RandomType()
	{
		int index = Random.Range(0, rpTypes.Length);
		return (rpTypes[index]);
	}
}
