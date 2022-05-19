using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	public static SpawnManager instance;

    public RobotPart prefab;
    public Vector2[] spawnPos;

	// Controls the difficulty over time, factoring how much time has passed
	public float spawnTimeScale = 10f;
	public AnimationCurve spawnIntervalDerivation;
	public AnimationCurve jokerChanceOverTime;

	private bool gameRunning = false;
	public const float maxDifficultyTime = 180f;
	private float gameTime = 0f;

	private const float startDelay = 3f;
    private float spawnTime;

	public const uint maxPoolSize = 150;
	private static Queue<RobotPart> goPool = new Queue<RobotPart>();
	private static uint poolCount = 0;

	public float poissonRadius;
	public LayerMask poissonMask;

	void Awake()
	{
		if (instance == null)
			instance = this;
		else Destroy(gameObject);
	}
	
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Begin", startDelay);
    }

	private void Begin()
	{
		gameRunning = true;
		gameTime = 0f;
		spawnTime = 0f;
	}
	
    // Update is called once per frame
    void Update()
    {
		if (!gameRunning)
			return ;
		gameTime += Time.deltaTime;
		spawnTime -= Time.deltaTime;
		if (spawnTime <= 0f)
			SpawnRobotPart();
    }
	
    void SpawnRobotPart()
    {
		spawnTime = spawnIntervalDerivation.Evaluate(gameTime / maxDifficultyTime) * spawnTimeScale;

		Vector2 pos = spawnPos[Random.Range(0, spawnPos.Length)];
		if (Physics2D.OverlapCircle(pos, poissonRadius, poissonMask) != null)
			return ;
		RobotPart go = goPool.Count == 0 ? null : goPool.Dequeue();
		if (go == null)
		{
			if (poolCount >= maxPoolSize)
				return ;
			poolCount++;
			go = Instantiate<RobotPart>(prefab, pos, Quaternion.identity, transform);
			go.Awaken();
		}
		go.gameObject.SetActive(true);
		go.transform.position = pos;
		go.Initialize();
    }

	public static void RecallToPool(RobotPart target)
	{
		target.gameObject.SetActive(false);
		goPool.Enqueue(target);
	}

	// Debug visualization
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.green;
		foreach (Vector2 pos in spawnPos)
			Gizmos.DrawWireSphere(pos, poissonRadius);
	}
}
