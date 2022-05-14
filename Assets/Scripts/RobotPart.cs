using UnityEngine;

public class RobotPart : MonoBehaviour
{
	public enum RobotPartType
	{
		Head, Torso, Arm
	};

	[HideInInspector]
	public RobotPartType partType;
	private int colorIndex;

	private void OnEnable()
	{
		Initialize();
	}
	
	public void Initialize()
	{
		
	}
}
