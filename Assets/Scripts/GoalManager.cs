using System;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    public GameObject leftGoal;
    public GameObject rightGoal;
    int leftScore = 0;
    int rightScore = 0;

    public int getLeftScore()
    {
        return leftScore;
    }

    public int getRightScore()
    {
        return rightScore;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() { }

    // Update is called once per frame
    void Update() { }

    public void AddScore(GameObject goal)
    {
        if (goal == leftGoal)
        {
            rightScore += 1;
            Debug.Log("Right Player Scored! Score: " + leftScore + "-" + rightScore);
        }
        else if (goal == rightGoal)
        {
            leftScore += 1;
            Debug.Log("Left Player Scored! Score: " + leftScore + "-" + rightScore);
        }

        WhoWon();
    }

    void WhoWon()
    {
        if (leftScore == 11)
        {
            Debug.Log("Game Over, Left Player Wins!");
            leftScore = 0;
            rightScore = 0;
        }
        else if (rightScore == 11)
        {
            Debug.Log("Game Over, Right Player Wins!");
            leftScore = 0;
            rightScore = 0;
        }
    }
}
