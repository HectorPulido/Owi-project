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


    protected virtual void OnTriggerEnter(Collider col)
    {
        switch (attackTo)
        {
            case AttackTo.Enemy:
                if (!col.CompareTag("Enemy"))
                    return;
                break;
            case AttackTo.Player:
                if (!col.CompareTag("Player"))
                    return;
                break;
            default:
                break;
        }

        HealthAndMana healthAndMana = col.GetComponent<HealthAndMana>();
        if (healthAndMana == null)
        {
            return;
        }

        healthAndMana.TakeDamage(damage);

    }


}
