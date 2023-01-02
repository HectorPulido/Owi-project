using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody rb;

    public float followingMultiplier = 0.9f;
    public float verticalSpeed;
    public float horizontalSpeed;

    public bool following = false;
    public Transform pivot;
    public Bumping bumping;

    private Vector3 speed;

    private float initialYRotation;


    private void Awake()
    {
        Dependecies();
        initialYRotation = transform.eulerAngles.y;
    }

    private void Dependecies()
    {
        if (bumping == null)
        {
            bumping = GetComponentInChildren<Bumping>();
        }
    }

    public void Move(Vector2 direction)
    {
        Flip(direction.x);
        direction.Normalize();
        speed = new Vector3(
            direction.x * horizontalSpeed * (following ? followingMultiplier : 1),
            0,
            direction.y * verticalSpeed * (following ? followingMultiplier : 1)
        );
    }

    private void Flip(float directionX)
    {
        if (directionX == 0)
        {
            return;
        }

        var rotation = transform.rotation.eulerAngles;
        rotation.y = directionX > 0 ? initialYRotation : initialYRotation + 180;
        transform.eulerAngles = rotation;

    }

    private void FixedUpdate()
    {
        var yVel = rb.velocity.y;
        var newVel = new Vector3(speed.x, yVel, speed.z);
        rb.velocity = newVel;
    }
}