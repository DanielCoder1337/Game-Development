using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tankExplosion : MonoBehaviour
{

    public GameObject explosionEffect;
    public float radius = 5f;
    public float explosionForce = 700f;

    public virtual void explosion(Transform origin)
    {
        Instantiate(explosionEffect, origin);
        Collider[] colliders = Physics.OverlapSphere(origin.position, 2);

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, origin.position, radius);
            }
        }
    }
}
