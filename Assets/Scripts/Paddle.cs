using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;

public class Paddle : MonoBehaviour
{
    public GameObject paddleLeft;
    public GameObject paddleRight;
    public float paddleSpeed = 1f;
    public float forceStrength = 10f;
    public float angle = 50f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        //Left Paddle
        Rigidbody rbodyLeft = paddleLeft.GetComponent<Rigidbody>();
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
        Rigidbody rbodyRight = paddleRight.GetComponent<Rigidbody>();
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
        //Rays
        Vector3 up = Vector3.up;
        Quaternion rotation = Quaternion.Euler(60f, 0f, 0f);
        Vector3 rotatedVector = rotation * up;
        Quaternion otherRotation = Quaternion.Euler(-60f, 0f, 0f);
        Vector3 otherRotatedVector = otherRotation * up;
        Quaternion someOtherRotation = Quaternion.Euler(angle, 0f, 0f);
        Vector3 someOtherRotatedVector = someOtherRotation * up;
        Debug.DrawRay(paddleLeft.transform.position, rotatedVector * 5, Color.red);
        Debug.DrawRay(paddleLeft.transform.position, otherRotatedVector * 5, Color.blue);
        Debug.DrawRay(paddleLeft.transform.position, someOtherRotatedVector * 5, Color.green);
    }
}
