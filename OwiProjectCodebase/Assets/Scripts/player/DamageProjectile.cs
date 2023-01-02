using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageProjectile : DamageDealer
{
    [Header("Destroy")]
    public bool autoDestroy = false;
    public float autoDestroyTime = 5;
    public GameObject explosionPrefab;

    [Header("Projectile")]
    public float speed = 10;


    private void Start()
    {
        if (autoDestroy)
        {
            Destroy(gameObject, autoDestroyTime);
        }
    }

    private void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    protected override void OnTriggerEnter(Collider col)
    {
        base.OnTriggerEnter(col);

        Destroy(gameObject);

        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, transform.rotation);
        }

    }
}
