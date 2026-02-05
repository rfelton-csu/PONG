using UnityEngine;

public class RightGoalTrigger : MonoBehaviour
{
    public GoalManager goalManager;
    public GameObject rightGoal;

    void OnTriggerEnter(Collider ball)
    {
        if (!ball.CompareTag("Ball"))
            return;
        goalManager.AddScore(rightGoal);
        Destroy(ball.gameObject);
    }
}
