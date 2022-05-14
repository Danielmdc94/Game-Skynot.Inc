using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] robotParts;
    public Vector2[] spawnPos;
    private float startDelay = 1.0f;
    private float spawnTime = 3f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRobotParts", startDelay, spawnTime);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnRobotParts()
    {
        int randomIndex = Random.Range(0, robotParts.Length);
        int randomPos = Random.Range(0, 3);
        Instantiate(robotParts[randomIndex], spawnPos[randomPos], robotParts[randomIndex].gameObject.transform.rotation);
    }

}
