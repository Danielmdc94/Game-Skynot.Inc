using UnityEngine;

public class RobotPart : MonoBehaviour
{
	private GameManager gameManager;
	private float rightDestroy = 18;
	private float leftDestroy = -27;
	public enum RobotPartType
	
	{
		Head, Torso, Arm
	};

	[HideInInspector]
	public RobotPartType partType;
	private int colorIndex;

	void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
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
			gameManager.UpdateScore(1);
            Destroy(gameObject);
        }
		if (transform.position.x > rightDestroy)
        {
			gameManager.UpdateScore(1);
            Destroy(gameObject);
        }
    }
}
