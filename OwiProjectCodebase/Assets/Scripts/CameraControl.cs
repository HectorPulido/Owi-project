using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public bool lockCursor;
    [Range(0.1f, 5)]
    public float mousePitchSensitivity = 2;
    [Range(0.1f, 5)]
    public float mouseYawSensitivity = 2;
    public Vector2 pitchMinMax = new Vector2(-30, 60);
    public float rotationSmoothTime = 0.2f;
    public Transform target;
    public float zoomTime = 0.5f;
    public float targetDistance;
    public Transform camTransform;
    public LayerMask linecastMask;

    private Vector3 rotationSmoothVelocity;
    private Vector3 currentRotation;
    private float distFromTarget = 30;
    private float yaw;
    private float pitch;
    private Vector3 vel;

    private void Start()
    {
        Cursor.visible = false;
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (camTransform == null)
        {
            camTransform = Camera.main.transform;
        }
        distFromTarget = Mathf.SmoothStep(distFromTarget, targetDistance, zoomTime);
        currentRotation = camTransform.eulerAngles;

        yaw = currentRotation.y;
        pitch = currentRotation.x;
    }

    [ContextMenu("Preheat Movement")]
    private void Move()
    {
        //Rotate Camera
        yaw += InputManager.HorizontalMouse * mouseYawSensitivity;

        pitch += InputManager.VerticalMouse * mousePitchSensitivity;
        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

        currentRotation = Vector3.SmoothDamp(currentRotation,
                                             new Vector3(pitch, yaw),
                                             ref rotationSmoothVelocity,
                                             rotationSmoothTime);
        transform.eulerAngles = currentRotation;

        // Move camera
        transform.position = target.position - transform.forward * distFromTarget;
        distFromTarget = Mathf.SmoothStep(distFromTarget, targetDistance, zoomTime);

        camTransform.rotation = transform.rotation;

        // Check for obstacles
        (bool isObstacle, Vector3 obstaclePosition) = CheckObstacles();
        if (isObstacle)
        {
            camTransform.position = obstaclePosition;
            return;
        }
        camTransform.position = transform.position;

    }

    private (bool, Vector3) CheckObstacles()
    {
        RaycastHit hit;
        // Add mask
        if (Physics.Linecast(
                target.position,
                transform.position,
                out hit,
                linecastMask,
                QueryTriggerInteraction.Ignore
            ))
        {
            if (hit.transform != target)
            {
                return (true, hit.point + hit.normal * 0.01f);
            }
        }
        return (false, Vector3.zero);
    }

    private void LateUpdate()
    {
        Move();
    }
}