using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody rb;
    private float velocityMultiplier = 1.25f;
    private float lastPaddleCollisionTime = -1f;
    private float lastWallCollisionTime = -1f;
    private float collisionCooldown = 0.2f;
    private float wallBounceCooldown = 0.1f;
    private float wallBounceDistance = 1.5f;
    private float minSpeed = 20f;
    public float pitchExponent = 2f;
    float minPitch = 0.5f;
    float maxPitch = 3f;
    private float minBounceAngle = 30f;
    public AudioClip paddleHitSound;
    AudioSource audioSource;
    private float currentBallSpeed = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.mass = 0.1f;
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        var sources = GetComponents<AudioSource>();
        if (sources.Length == 0)
            audioSource = gameObject.AddComponent<AudioSource>();
        else
            audioSource = sources[0];

        for (int i = 1; i < sources.Length; i++)
            Destroy(sources[i]);

        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            if (Time.time - lastPaddleCollisionTime >= collisionCooldown)
            {
                HandlePaddleCollision(collision);

                if (audioSource != null && paddleHitSound != null)
                {
                    float speed = currentBallSpeed;

                    float minSpeedForPitch = 20f;
                    float maxSpeedForPitch = 80f;

                    float t = Mathf.InverseLerp(minSpeedForPitch, maxSpeedForPitch, speed);
                    audioSource.pitch = Mathf.Lerp(
                        minPitch,
                        maxPitch,
                        (float)Math.Pow(t, pitchExponent)
                    );
                    // Debug.Log($"Speed={speed:F1} Pitch={audioSource.pitch:F2}");

                    audioSource.PlayOneShot(paddleHitSound);
                }

                lastPaddleCollisionTime = Time.time;
            }
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            if (Time.time - lastWallCollisionTime >= wallBounceCooldown)
            {
                BounceOffWall(collision.contacts[0].normal);
                lastWallCollisionTime = Time.time;
            }
        }
    }

    void FixedUpdate()
    {
        // Check if ball is stuck (low velocity for too long)
        if (rb.linearVelocity.magnitude < 5f)
        {
            Vector3 currentVel = rb.linearVelocity;
            if (currentVel.magnitude > 0)
            {
                rb.linearVelocity = currentVel.normalized * minSpeed;
            }
        }

        // Raycast in the direction of movement to detect walls ahead
        RaycastHit hit;
        Vector3 ballPos = transform.position;
        Vector3 velocity = rb.linearVelocity;

        if (velocity.magnitude > 0.1f && Time.time - lastWallCollisionTime >= wallBounceCooldown)
        {
            if (Physics.Raycast(ballPos, velocity.normalized, out hit, wallBounceDistance))
            {
                if (hit.collider.CompareTag("Wall"))
                {
                    BounceOffWall(hit.normal);
                    lastWallCollisionTime = Time.time;
                }
            }
        }
    }

    private void HandlePaddleCollision(Collision collision)
    {
        Collider paddleCollider = collision.gameObject.GetComponent<Collider>();
        float paddleHeight = paddleCollider.bounds.size.z;
        float paddleCenter = collision.gameObject.transform.position.z;

        float hitPos = collision.contacts[0].point.z;
        float hitPosition = (hitPos - paddleCenter) / (paddleHeight / 2f);
        hitPosition = Mathf.Clamp(hitPosition, -1f, 1f);

        // Use collision normal to determine which side the ball hit from
        Vector3 contactNormal = collision.contacts[0].normal;

        bool hitLeftPaddle = contactNormal.x > 0;

        float baseX = Mathf.Max(20f, Mathf.Abs(rb.linearVelocity.x));
        float newXVelocity = hitLeftPaddle ? baseX : -baseX;

        float newZVelocity = hitPosition * 30f;
        Vector3 newVelocity = new Vector3(newXVelocity, 0f, newZVelocity);

        // Ensure 30 degree minimum bounce angle
        Vector3 paddleNormal = hitLeftPaddle ? Vector3.right : Vector3.left;
        float dotProduct = Vector3.Dot(newVelocity.normalized, paddleNormal);
        float angleFromPaddle = Mathf.Acos(Mathf.Clamp01(Mathf.Abs(dotProduct))) * Mathf.Rad2Deg;

        if (angleFromPaddle < minBounceAngle)
        {
            float minZVelocity =
                Mathf.Abs(newXVelocity) * Mathf.Tan((90f - minBounceAngle) * Mathf.Deg2Rad);
            newZVelocity = Mathf.Sign(hitPosition) * minZVelocity;
            newVelocity = new Vector3(newXVelocity, 0f, newZVelocity);
        }

        float currentSpeed = rb.linearVelocity.magnitude;
        float newSpeed = Mathf.Max(currentSpeed * velocityMultiplier, minSpeed); //Increase speed
        currentBallSpeed = Mathf.Max(currentBallSpeed * velocityMultiplier, minSpeed);
        rb.linearVelocity = newVelocity.normalized * currentBallSpeed;
    }

    private void BounceOffWall(Vector3 normal)
    {
        Vector3 currentVelocity = rb.linearVelocity;
        Vector3 reflectedVelocity = Vector3.Reflect(currentVelocity, normal);
        float currentSpeed = currentVelocity.magnitude;
        float newSpeed = Mathf.Max(currentSpeed, minSpeed);
        rb.linearVelocity = reflectedVelocity.normalized * newSpeed;
    }
}
