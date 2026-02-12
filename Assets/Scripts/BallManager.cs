using System;
using Unity.VisualScripting;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    public GameObject ballPrefab;
    public GameObject spawnerObject;
    public GameObject paddleLeft;
    public GameObject paddleRight;
    public GoalManager goalManager;
    public AudioClip paddleHitSound;

    private int nameIndex = 0;
    private int lastLeftScore = 0;
    private int lastRightScore = 0;

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
        Renderer renderer = ballPrefab.GetComponent<Renderer>();
        renderer.sharedMaterial.color = UnityEngine.Random.ColorHSV(0f, 1f, 0.5f, 1f, 0.5f, 1f);
        nameIndex++;
        Transform myTransform = GetComponent<Transform>();
        GameObject spawnedBall = Instantiate(ballPrefab, myTransform.position, Quaternion.identity);
        spawnedBall.name = "Ball" + nameIndex;

        // Add Ball script component for collision handling
        // Add/get Ball script component
        Ball ball = spawnedBall.GetComponent<Ball>();
        if (ball == null)
            ball = spawnedBall.AddComponent<Ball>();

        // Ensure AudioSource exists on the ball
        AudioSource src = spawnedBall.GetComponent<AudioSource>();
        if (src == null)
            src = spawnedBall.AddComponent<AudioSource>();

        // OPTIONAL: keep it 2D so it doesn't pan with position
        src.spatialBlend = 0f;

        // Assign the clip to the ball (make paddleHitSound a field on BallManager)
        ball.paddleHitSound = paddleHitSound;

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
        lastLeftScore = goalManager.getLeftScore();
        lastRightScore = goalManager.getRightScore();
        float zvel = UnityEngine.Random.Range(-10f, 10f);
        Vector3 initialForce = new Vector3(xvel, 0f, zvel);
        if (Math.Abs(lastLeftScore - lastRightScore) > 2)
        {
            ball.transform.localScale = new Vector3(3f, 3f, 3f);
            initialForce *= 1.75f;
        }

        ballMovement.linearVelocity = initialForce;
    }
}
