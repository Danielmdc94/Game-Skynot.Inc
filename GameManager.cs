using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	public Player[] players;

	private void Awake()
	{
		// Singleton
		if (instance)
			return (Destroy(gameObject));
		instance = this;
	}
}
