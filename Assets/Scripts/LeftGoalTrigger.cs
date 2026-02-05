using UnityEngine;

public class LeftGoalTrigger : MonoBehaviour
{
    public GoalManager goalManager;
    public GameObject leftGoal;

    void OnTriggerEnter(Collider ball)
    {
        if (!ball.CompareTag("Ball"))
            return;
        goalManager.AddScore(leftGoal);
        Destroy(ball.gameObject);
    }
}
