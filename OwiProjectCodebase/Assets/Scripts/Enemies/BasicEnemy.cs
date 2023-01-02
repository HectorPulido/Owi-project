using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicEnemy : MonoBehaviour
{
    public enum States { patrol, attack, waiting }

    public States state = States.patrol;
    public float attackFrequency;
    public float attackRange;
    public float attackPower;
    public float waitingTime;
    public float patrolRange;

    public Image healthBar;

    public Bumping bumping;
    public Movement movement;
    public HealthAndMana stats;

    public GameObject explotionPrefab;

    private Transform target;
    private Vector3 patrolTarget;
    private Vector3 direction;

    IEnumerator Start()
    {
        if (!bumping)
            bumping = GetComponent<Bumping>();

        if (!movement)
            movement = GetComponent<Movement>();

        if (!stats)
            stats = GetComponent<HealthAndMana>();

        stats.healthCallback = (float amount) => healthBar.fillAmount = amount;
        stats.aliveCallback = (bool alive) => {
            if(!alive) {
                Instantiate(explotionPrefab, transform.position, Quaternion.identity); 
                Destroy(gameObject);
            }
        };

        patrolTarget = transform.position;

        while (true)
        {
            yield return null;
            ManageState();
        }
    }

    float CalculateDistance(Vector3 target)
    {
        return Vector3.Distance(transform.position, target);
    }

    Vector3 GetPatrolTarget()
    {
        var target = Random.insideUnitCircle * patrolRange;
        return new Vector3(target.x, 0, target.y) + transform.position;
    }

    Vector3 GetDirection(Vector3 target)
    {
        var nonNormDir = target - transform.position;
        return nonNormDir.normalized;
    }

    void StatusPatrol()
    {
        if (CalculateDistance(patrolTarget) < attackRange)
        {
            StartCoroutine(WaitingState());
            return;
        }
        direction = GetDirection(patrolTarget);

        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up * 0.25f, direction, out hit, attackRange))
        {
            StartCoroutine(WaitingState());
            return;
        }

        var player = PlayerController.singleton.currentPlayerGameObject.transform;
        var playerPos = player.position;
        var disToPlayer = CalculateDistance(playerPos);
        if (patrolRange > disToPlayer)
        {
            state = States.attack;
            target = player;
            return;
        }
    }

    void StatusAttack()
    {
        if (target == null)
        {
            state = States.patrol;
            return;
        }

        var distanceToTarget = CalculateDistance(target.position);
        if (distanceToTarget > attackRange)
        {
            direction = GetDirection(target.position);
        }

        if (distanceToTarget > patrolRange + 1)
        {
            state = States.patrol;
            target = null;
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up * 0.25f, direction, out hit, attackRange * (1 - 0.25f)))
        {
            direction = Vector3.zero;
        }
    }

    IEnumerator WaitingState()
    {
        direction = Vector3.zero;
        patrolTarget = GetPatrolTarget();
        state = States.waiting;
        yield return new WaitForSeconds(waitingTime);
        state = States.patrol;
    }

    void ManageState()
    {
        if (state == States.waiting)
        {
            return;
        }


        if (state == States.patrol)
        {
            StatusPatrol();
            return;
        }

        if (state == States.attack)
        {
            StatusAttack();
            return;
        }
    }

    void Update()
    {
        var isMoving = direction.sqrMagnitude > 0;
        bumping.moving = isMoving;
        movement.Move(new Vector2(direction.x, direction.z));
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, patrolRange);

        Gizmos.color = Color.red;

        if (state == States.patrol)
        {
            Gizmos.DrawWireSphere(patrolTarget, 0.25f);
        }
        else
        {
            if (target != null)
                Gizmos.DrawWireSphere(target.position, 0.25f);
        }
    }
}
