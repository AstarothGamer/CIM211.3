using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class COntroller2 : MonoBehaviour
{
    [SerializeField] private float wheelBase = 0.6f; 

    [SerializeField] private float maxWheelSpeed = 2.0f;

    [SerializeField] private float wheelAcceleration = 4.0f;

    [SerializeField] private float wheelDrag = 1.0f;


    [SerializeField] private string leftWheelAxis = "Vertical";  

    [SerializeField] private string rightWheelAxis = "Vertical2"; 

    // [SerializeField] private float wheelRadius = 0.3f;

    private Rigidbody rb;

    private float leftWheelSpeed;
    private float rightWheelSpeed;

    private float leftInput;
    private float rightInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    private void Update()
    {
        leftInput = Input.GetAxisRaw(leftWheelAxis);   
        rightInput = Input.GetAxisRaw(rightWheelAxis); 
    }

    private void FixedUpdate()
    {
        float dt = Time.fixedDeltaTime;

        float targetLeftSpeed = leftInput * maxWheelSpeed;
        float targetRightSpeed = rightInput * maxWheelSpeed;

        leftWheelSpeed = Mathf.MoveTowards(leftWheelSpeed, targetLeftSpeed, wheelAcceleration * dt);
        rightWheelSpeed = Mathf.MoveTowards(rightWheelSpeed, targetRightSpeed, wheelAcceleration * dt);

        if (Mathf.Approximately(leftInput, 0f))
        {
            leftWheelSpeed = Mathf.MoveTowards(leftWheelSpeed, 0f, wheelDrag * dt);
        }
        if (Mathf.Approximately(rightInput, 0f))
        {
            rightWheelSpeed = Mathf.MoveTowards(rightWheelSpeed, 0f, wheelDrag * dt);
        }

        float v = (rightWheelSpeed + leftWheelSpeed) * 0.5f;
        float omega = (rightWheelSpeed - leftWheelSpeed) / wheelBase; 

        Vector3 localDelta = new Vector3(0f, 0f, v * dt);

        float deltaAngleDeg = omega * Mathf.Rad2Deg * dt;
        Quaternion deltaRot = Quaternion.Euler(0f, deltaAngleDeg, 0f);

        Quaternion newRotation = rb.rotation * deltaRot;
        Vector3 newPosition = rb.position + newRotation * localDelta;

        rb.MoveRotation(newRotation);
        rb.MovePosition(newPosition);
    }
}
