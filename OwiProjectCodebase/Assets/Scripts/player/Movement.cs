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
    private bool flip = false;
    private float followingSpeed = 1;


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

    public void Follow(Transform player, float partnerMinDistance, bool followPlayer){
        Vector3 direction = player.transform.position - transform.position;
        var distance = direction.magnitude;

        bumping.moving = true;
        following = true;

        if (distance <= partnerMinDistance || !followPlayer)
        {
            direction = Vector3.zero;
            bumping.moving = false;
        }

        direction.y = direction.z;
        direction.z = 0;

        Move(direction, true);
    }

    public void Move(Vector2 direction, bool minion = false)
    {
        if (direction.x != 0)
            flip = direction.x < 0;
    
        followingSpeed = following ? followingMultiplier : 1;

        direction.Normalize();

        transform.eulerAngles = new Vector3(
            transform.eulerAngles.x,
            (flip ? 180f: 0) + Camera.main.transform.eulerAngles.y,
            transform.eulerAngles.z
        );

        if (minion)
        {
            speed = new Vector3(
                direction.x * horizontalSpeed * followingSpeed,
                0,
                direction.y * verticalSpeed * followingSpeed
            );
            return;
        }

        speed = transform.right * direction.x * horizontalSpeed
            + transform.forward * direction.y * verticalSpeed ;
        speed *= followingSpeed * (flip ? -1 : 1);
    }

    private void FixedUpdate()
    {
        var yVel = rb.velocity.y;
        var newVel = new Vector3(speed.x, yVel, speed.z);
        rb.velocity = newVel;
    }
}