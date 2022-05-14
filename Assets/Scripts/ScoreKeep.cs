using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeep : MonoBehaviour
{
    public int countBuild;
    public int score;
    public int lives;
    // Start is called before the first frame update
    void Start()
    {
        countBuild = 0;
        score = 0;
        lives = 5;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
      /*  if (other.gameObject.CompareTag("RobotLegs") && countBuild == 0)
        {
         //   Destroy(other.gameObject);
            countBuild++;
        }
        else if (other.gameObject.CompareTag("RobotTorso") && countBuild == 1)
        {
         //   Destroy(other.gameObject);
            countBuild++;
        }
        else if (other.gameObject.CompareTag("RobotHead") && countBuild == 2)
        {
          //  Destroy(other.gameObject);
            countBuild++;
        }
        if (countBuild == 3)
        {
        //    Destroy(other.gameObject);
            score++;
        }
        else
        {
         //   Destroy(other.gameObject);
            countBuild = 0;
            lives--;
        }*/
    }
}
