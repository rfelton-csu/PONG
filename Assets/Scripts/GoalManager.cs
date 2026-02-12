using System;
using TMPro;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    public GameObject leftGoal;
    public GameObject rightGoal;
    int leftScore = 0;
    int rightScore = 0;
    public GameObject paddleLeft;
    public GameObject paddleRight;
    public TextMeshProUGUI leftScoreText;
    public TextMeshProUGUI rightScoreText;

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
            rightScoreText.text = "Right: " + rightScore;
            rightScoreText.color = UnityEngine.Random.ColorHSV(0f, 1f, 0.5f, 1f, 0.5f, 1f);
            // Debug.Log("Right Player Scored! Score: " + leftScore + "-" + rightScore);
            Vector3 currentScale = paddleLeft.transform.localScale;
            paddleLeft.transform.localScale = new Vector3(1f, 1f, currentScale.z * 1.05f);
            rightScoreText.fontSize = (int)UnityEngine.Random.Range(30f, 40f);
        }
        else if (goal == rightGoal)
        {
            leftScore += 1;
            // Debug.Log("Left Player Scored! Score: " + leftScore + "-" + rightScore);
            leftScoreText.text = "Left: " + leftScore;
            leftScoreText.color = UnityEngine.Random.ColorHSV(0f, 1f, 0.5f, 1f, 0.5f, 1f);
            Vector3 currentScale = paddleRight.transform.localScale;
            paddleRight.transform.localScale = new Vector3(1f, 1f, currentScale.z * 1.05f);
            leftScoreText.fontSize = (int)UnityEngine.Random.Range(30f, 40f);
        }

        WhoWon();
    }

    void WhoWon()
    {
        if (leftScore == 11 || rightScore == 11)
        {
            leftScore = 0;
            rightScore = 0;
            paddleLeft.transform.localScale = new Vector3(1, 1, 5);
            paddleRight.transform.localScale = new Vector3(1, 1, 5);
            leftScoreText.text = "Left: " + leftScore;
            rightScoreText.text = "Right: " + rightScore;
            leftScoreText.color = UnityEngine.Color.black;
            rightScoreText.color = UnityEngine.Color.black;
            leftScoreText.fontSize = 36f;
            rightScoreText.fontSize = 36f;
        }
    }
}
