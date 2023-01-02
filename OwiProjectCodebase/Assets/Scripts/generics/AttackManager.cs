using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct MovementData
{
    public string name;
    public UnityEvent onAttack;

    public float coolDown;

    [HideInInspector]
    public float coolDownTimer;
}

public class AttackManager : MonoBehaviour
{
    public GameObject[] projectile;
    public MovementData[] movement;
    public int currentMovement = 0;
    public Transform projectileSpawnPoint;

    private void Update()
    {
        if (movement.Length == 0)
            return;

        if (movement[currentMovement].coolDownTimer < 0)
            return;

        movement[currentMovement].coolDownTimer -= Time.deltaTime;
    }

    public void InstantiateProjectile(int projectileIndex)
    {
        if (projectileIndex < 0 || projectileIndex >= projectile.Length)
        {
            Debug.LogError("Projectile index out of range");
            return;
        }
        if (projectileSpawnPoint == null)
        {
            Debug.LogError("Projectile spawn point not set");
            return;
        }
        GameObject.Instantiate(
            projectile[projectileIndex],
            projectileSpawnPoint.position,
            transform.rotation
        );
    }

    [ContextMenu("Attack")]
    public void Attack()
    {
        if (movement.Length == 0)
            return;

        if (movement[currentMovement].coolDownTimer > 0)
            return;

        movement[currentMovement].onAttack.Invoke();
        movement[currentMovement].coolDownTimer = movement[currentMovement].coolDown;
    }

    public void NextMovement()
    {
        if (currentMovement + 1 >= movement.Length)
        {
            currentMovement = 0;
            return;
        }
        currentMovement++;
    }

    public void PreviousMovement()
    {
        if (currentMovement - 1 < 0)
        {
            currentMovement = movement.Length - 1;
            return;
        }
        currentMovement--;
    }
}
