using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] robotParts;
    private float startDelay = 1.0f;
    private float spawnTime = 1.5f;
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
        Vector2 spawnPos = new Vector2(0, 0);
        Instantiate(robotParts[randomIndex], spawnPos, robotParts[randomIndex].gameObject.transform.rotation);
    }

}
