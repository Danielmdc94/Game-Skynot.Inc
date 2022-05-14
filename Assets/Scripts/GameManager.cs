using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	public static Player[] players;

	private void Awake()
	{
		// Singleton
		if (instance)
		{
			Destroy(gameObject);
			return ;
		}
		instance = this;

		players = FindObjectsOfType<Player>();
	}
}
