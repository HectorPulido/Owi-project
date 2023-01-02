using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageContact : DamageDealer
{
    [Header("Cool Down")]
    public float coolDown = 1;
    private bool isCoolingDown = false;

    private IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(coolDown);
        isCoolingDown = false;
    }

    protected override void OnTriggerEnter(Collider col)
    {
        if (isCoolingDown)
        {
            return;
        }

        base.OnTriggerEnter(col);
        
        isCoolingDown = true;
        PlayerController.singleton.StartCoroutine(CoolDown());
    }
}
