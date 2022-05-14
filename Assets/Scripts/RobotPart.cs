using UnityEngine;

public class RobotPart : MonoBehaviour
{
	private float rightDestroy = 18;
	private float leftDestroy = -27;
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

	void Update()
    {
        if (transform.position.x < leftDestroy)
        {
            Destroy(gameObject);
        }
		if (transform.position.x > rightDestroy)
        {
            Destroy(gameObject);
        }
    }
}
