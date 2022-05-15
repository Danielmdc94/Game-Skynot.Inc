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
	public TextMeshProUGUI scoreText;
	public GameObject bgBlack;
	public Button startButton;
	public bool isGameActive;
	private int score;
	public static int topCount = 0;
	public static int midCount = 0;
	public static int bottomCount = 0;

	void Start()
	{

	}


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
	
	public void hideBGBlack()
	{
		bgBlack.gameObject.SetActive(false);
		isGameActive = true;
	}
}
