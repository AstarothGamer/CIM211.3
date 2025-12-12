using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class COntroller2 : MonoBehaviour
{
    [SerializeField] private TutorialManager tutorial;
    [SerializeField] private float wheelBase = 0.6f; 

    [SerializeField] private float maxWheelSpeed = 2.0f;

    [SerializeField] private float wheelAcceleration = 4.0f;

    [SerializeField] private float wheelDrag = 1.0f;


    [SerializeField] private string leftWheelAxis = "Vertical";  

    [SerializeField] private string rightWheelAxis = "Vertical2"; 

    // [SerializeField] private float wheelRadius = 0.3f;

    [SerializeField] private AnimationCurve wheelCurve;
    [SerializeField] private float wheelCurveSpeed;

    private Rigidbody rb;

    private float leftWheelSpeed;
    private float rightWheelSpeed;

    private float leftInput;
    private float rightInput;

    private float leftAnimationProgress;
    private float rightAnimationProgress;

    public bool getUpFun = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        // rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    private void Update()
    {
        leftInput = Input.GetAxisRaw(leftWheelAxis);   
        rightInput = Input.GetAxisRaw(rightWheelAxis);
        
        GettingUp();
    }

    private void FixedUpdate()
    {
        Movement();
        
    }

    private void Movement()
    {
        float dt = Time.fixedDeltaTime;

        if(rightInput != 0 && rightAnimationProgress == 0)
            leftAnimationProgress = 0;
        
        if (leftInput != 0)
        {
            if (leftAnimationProgress == 0) rightAnimationProgress = 0;
            leftAnimationProgress += Time.fixedDeltaTime;
        }
        else leftAnimationProgress = 0f;

        if (rightInput != 0)
        {
            if (rightInput == 0) leftAnimationProgress = 0;
            rightAnimationProgress += Time.fixedDeltaTime;
        }
        else rightAnimationProgress = 0f;
        
        if (leftAnimationProgress >= wheelCurveSpeed || rightAnimationProgress >= wheelCurveSpeed)
        {
            leftAnimationProgress = 0;
            rightAnimationProgress = 0;
        }
        
        
        float targetLeftSpeed = leftInput * maxWheelSpeed * wheelCurve.Evaluate(leftAnimationProgress / wheelCurveSpeed);
        float targetRightSpeed = rightInput * maxWheelSpeed * wheelCurve.Evaluate(rightAnimationProgress / wheelCurveSpeed);
        
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

    public void Rotate()
    {
        rb.angularVelocity = Vector3.zero;
    }

    private void GettingUp()
    {
        float angleToUp = Vector3.Angle(transform.up, Vector3.up);

        if(angleToUp > 60)
        {
            getUpFun = true;
            tutorial.getUpTutorial.SetActive(true);

            if(Input.GetKeyDown(KeyCode.Space))
            {
                transform.position += Vector3.up;
                Invoke(nameof(Rotate), time: 0.01f);
                transform.rotation = Quaternion.identity;
            }
        }
        else
        {
            tutorial.getUpTutorial.SetActive(false);
        }
    }
}
