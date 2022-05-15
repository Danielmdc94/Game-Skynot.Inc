using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotPart : MonoBehaviour
{
	private GameManager gameManager;
	private float rightDestroy = 18;
	private float leftDestroy = -27;
	public static int topCount = 0;
	public static int midCount = 0;
	public static int bottomCount = 0;
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
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("GoalTop"))
		{
			if (topCount == 0 && CompareTag("RobotHead"))
			{
				topCount++;
			}
			else if (topCount == 1 && CompareTag("RobotTorso"))
			{
				topCount++;
			}
			else if (topCount == 2 && CompareTag("RobotLegs"))
			{
				topCount++;
			}
			else if (topCount == 3)
			{
				gameManager.UpdateScore(1);
				topCount = 0;
			}
			else 
			{
				topCount = 0;
			}
			
		}
		if (other.gameObject.CompareTag("GoalMid"))
		{
			if (midCount == 0 && CompareTag("RobotHead"))
			{
				topCount++;
			}
			else if (midCount == 1 && CompareTag("RobotTorso"))
			{
				topCount++;
			}
			else if (midCount == 2 && CompareTag("RobotLegs"))
			{
				midCount++;
			}
			else if (midCount == 3)
			{
				gameManager.UpdateScore(1);
				midCount = 0;
			}
			else 
			{
				midCount = 0;
			}
		}
		if (other.gameObject.CompareTag("GoalBottom"))
		{
			if (bottomCount == 0 && CompareTag("RobotLegs"))
			{
				bottomCount++;
			}
			else if (bottomCount == 1 && CompareTag("RobotTorso"))
			{
				topCount++;
			}
			else if (bottomCount == 2 && CompareTag("RobotHead"))
			{
				bottomCount++;
			}
			else if (topCount == 3)
			{
				gameManager.UpdateScore(1);
				bottomCount = 0;
			}
			else 
			{
				bottomCount = 0;
			}
		}
	}
}
