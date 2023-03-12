using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealthAndMana : MonoBehaviour
{

    public string playerName;
    public int maxHealth = 100;

    public int health = 100;

    public int maxMana = 100;

    public int mana = 100;

    public bool alive = true;

    public bool autoHeal = false;

    public int autoHealAmount = 10;

    public float autoHealTime = 1f;

    public float healthPercentage
    {
        get
        {
            return (float)health / (float)maxHealth;
        }
    }
    public float manaPercentage
    {
        get
        {
            return (float)mana / (float)maxMana;
        }
    }

    public System.Action<float> healthCallback;
    public System.Action<float> manaCallback;
    public System.Action<bool> aliveCallback;


    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(autoHealTime);
            if (!autoHeal || !alive)
                continue;
            Heal(autoHealAmount);
        }
    }

    public void Heal(int heal)
    {
        if (health == maxHealth)
            return;

        if (!alive)
            return;

        if (health < 0)
            return;

        health += heal;

        if (health > maxHealth)
            health = maxHealth;

        if (healthCallback != null)
            healthCallback(healthPercentage);

        ShowOnomatopeia($"+{heal}", true);
    }

    public void TakeDamage(int damage)
    {
        if (!alive)
            return;

        health -= damage;

        if (health <= 0)
        {
            health = 0;
            alive = false;
            aliveCallback(alive);
        }

        if (healthCallback != null)
            healthCallback(healthPercentage);

        ShowOnomatopeia($"-{damage}", false);
    }

    public void ShowOnomatopeia(string number, bool positive)
    {
        var color = positive ? "green" : "red";
        Onomatopoeia.singleton.MoveText(transform);
        Onomatopoeia.singleton.SetText($"<color={color}>{number}</color>");
    }

    [ContextMenu("TestHeal")]
    public void TestHeal()
    {
        Heal(10);
    }

    [ContextMenu("TestGetDamage")]
    public void TestGetDamage()
    {
        TakeDamage(10);
    }
}
