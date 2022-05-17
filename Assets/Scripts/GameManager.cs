using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	public static Player[] players;
	public Bounds gameArea;

	public TextMeshProUGUI scoreText;
	public GameObject bgBlack;
	public Button startButton;
	public bool isGameActive;

	public Sprite imgRobotHead;
	public Sprite imgRobotTorso;
	public Sprite imgRobotLegs;
	public Sprite imgEmpty;

	private int score = 0;
	private int lives = 4;
	
	public static int topCount = 0;
	public static int midCount = 0;
	public static int bottomCount = 0;

	private void Awake()
	{
		// Singleton
		if (instance)
		{
			Destroy(gameObject);
			return ;
		}
		instance = this;
		//DontDestroyOnLoad(gameObject);
		
		players = FindObjectsOfType<Player>();
	}

	void Update()
	{
		if (isGameActive == false && (SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("TitleScreen")))
		{
			if (Input.GetKeyUp("space"))
			{
				hideBGBlack();
			}
		}
	}
		
	public void StartGame()
	{
		SceneManager.LoadScene("FactoryScene");
	}
	
	public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = score +" TermiNOTors built";
    }

	public void UpdateLives(int scoreToAdd)
    {
        lives += scoreToAdd;
		Debug.Log("Lives Remaining :" + lives.ToString());
		if (lives <= 0)
		{
			Debug.Log("Game Over!");
		}
    }	

	public void hideBGBlack()
	{
		bgBlack.gameObject.SetActive(false);
		isGameActive = true;
	}

	public Sprite GetImage(RobotPart.RobotPartType? rptype)
	{
		switch (rptype)
		{
			case RobotPart.RobotPartType.Head: return (imgRobotHead);
			case RobotPart.RobotPartType.Torso: return (imgRobotTorso);
			case RobotPart.RobotPartType.Legs: return (imgRobotLegs);
			default : return (imgEmpty);
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(gameArea.center, gameArea.size);
	}
}
