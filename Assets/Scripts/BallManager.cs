using JetBrains.Annotations;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    public GameObject ballPrefab;
    public GameObject spawnerObject;
    private int nameIndex = 0;

    void Start()
    {
        spawnBall();
    }

    void Update()
    {
        //if ball has been destroyed, spawn a new one after 3 seconds
        if (GameObject.Find("Ball" + nameIndex) == null)
        {
            float lastSpawnTime = Time.time;
            while (Time.time - lastSpawnTime < 3f)
            {
                //wait
            }
            spawnBall();
        }
    }

    void spawnBall()
    {
        nameIndex++;
        Transform myTransform = GetComponent<Transform>();
        GameObject spawnedBall = Instantiate(ballPrefab, myTransform.position, Quaternion.identity);
        spawnedBall.name = "Ball" + nameIndex;
        Rigidbody ballMovement = spawnedBall.GetComponent<Rigidbody>();
        float xvel = Random.Range(-40f, 40f);
        if (xvel > -10f && xvel < 10f)
        {
            xvel = xvel < 0 ? -10f : 10f;
        }
        float zvel = Random.Range(-10f, 10f);
        Vector3 initialForce = new Vector3(xvel, 0f, zvel);
        ballMovement.linearVelocity = initialForce;
    }
}
