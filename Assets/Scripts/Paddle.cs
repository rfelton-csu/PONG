using UnityEngine;
using UnityEngine.InputSystem;

public class Paddle : MonoBehaviour
{
    public GameObject paddleLeft;
    public GameObject paddleRight;
    public float paddleSpeed = 1f;
    public float forceStrength = 10f;
    public float angle = 50f;

    private Rigidbody rbodyLeft;
    private Rigidbody rbodyRight;

    void Start()
    {
        rbodyLeft = paddleLeft.GetComponent<Rigidbody>();
        rbodyRight = paddleRight.GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Left Paddle
        if (Keyboard.current.wKey.isPressed)
        {
            Vector3 force = new Vector3(0, 0, forceStrength);
            rbodyLeft.AddForce(force);
        }
        if (Keyboard.current.sKey.isPressed)
        {
            Vector3 force = new Vector3(0, 0, -forceStrength);
            rbodyLeft.AddForce(force);
        }
        //Right Paddle
        if (Keyboard.current.upArrowKey.isPressed)
        {
            Vector3 force = new Vector3(0, 0, forceStrength);
            rbodyRight.AddForce(force);
        }
        if (Keyboard.current.downArrowKey.isPressed)
        {
            Vector3 force = new Vector3(0, 0, -forceStrength);
            rbodyRight.AddForce(force);
        }
    }
}
