using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public enum AttackTo
    {
        Enemy,
        Player,
        All
    }

    [Header("Base damage")]
    public int damage = 10;
    public AttackTo attackTo = AttackTo.All;

    public float coolDownTime = 0.5f;

    private bool canAttack = true;
    private MonoBehaviour parent;

    private void Start()
    {
        parent = Camera.main.GetComponent<MonoBehaviour>();
    }

    private IEnumerator CoolDown()
    {
        canAttack = false;
        yield return new WaitForSeconds(coolDownTime);
        canAttack = true;
    }


    protected virtual void OnTriggerEnter(Collider col)
    {
        if (!canAttack)
            return;

        if (attackTo == AttackTo.Enemy && !col.CompareTag("Enemy")) 
            return;
        if (attackTo == AttackTo.Player && !col.CompareTag("Player")) 
            return;

        HealthAndMana healthAndMana = col.GetComponent<HealthAndMana>();
        if (healthAndMana == null)
        {
            return;
        }

        healthAndMana.TakeDamage(damage);

        parent.StartCoroutine(CoolDown());

    }


}
