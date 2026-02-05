using System;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    public GameObject ballPrefab;
    public GameObject spawnerObject;
    public GameObject paddleLeft;
    public GameObject paddleRight;
    public GoalManager goalManager;
    private int nameIndex = 0;
    private int lastLeftScore = 0;
    private int lastRightScore = 0;

    int getIndex()
    {
        return nameIndex;
    }

    void Start()
    {
        spawnBall();
    }

    private float lastSpawnTime = -1f;

    void Update()
    {
        //if ball has been destroyed, spawn a new one after 3 seconds
        if (GameObject.Find("Ball" + nameIndex) == null)
        {
            if (lastSpawnTime == -1f)
            {
                lastSpawnTime = Time.time;
            }
            if (Time.time - lastSpawnTime < 3f)
            {
                //wait
            }
            else
            {
                spawnBall();
                lastSpawnTime = -1f;
            }
        }
    }

    void spawnBall()
    {
        nameIndex++;
        Transform myTransform = GetComponent<Transform>();
        GameObject spawnedBall = Instantiate(ballPrefab, myTransform.position, Quaternion.identity);
        spawnedBall.name = "Ball" + nameIndex;

        // Add Ball script component for collision handling
        spawnedBall.AddComponent<Ball>();

        Rigidbody ballMovement = spawnedBall.GetComponent<Rigidbody>();
        float xvel = UnityEngine.Random.Range(-40f, 40f);
        if (xvel > -10f && xvel < 10f)
        {
            xvel = xvel < 0 ? -10f : 10f;
        }
        if (lastLeftScore < goalManager.getLeftScore())
        {
            xvel = Math.Abs(xvel); //serve to right
        }
        else if (lastRightScore < goalManager.getRightScore())
        {
            xvel = -Math.Abs(xvel); //serve to left
        }
        float zvel = UnityEngine.Random.Range(-10f, 10f);
        Vector3 initialForce = new Vector3(xvel, 0f, zvel);
        ballMovement.linearVelocity = initialForce;
        lastLeftScore = goalManager.getLeftScore();
        lastRightScore = goalManager.getRightScore();
    }
}
