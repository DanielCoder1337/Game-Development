using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAttribute : MonoBehaviour
{
	public GameObject explosionEffect;
	public float radius;
	public float explosionForce;
	bool hasExploded = false;

	private void Update()
	{
		if (!hasExploded)
		{
			explosion(gameObject.transform);
			hasExploded = true;
			Invoke("DestroyGameObject", 1f);
		}
	}

	void DestroyGameObject()
	{
		Destroy(gameObject);
	}

	void explosion(Transform origin)
	{
		var a = Instantiate(explosionEffect, origin.position, origin.rotation);
		a.transform.parent = gameObject.transform;
		Collider[] colliders = Physics.OverlapSphere(origin.position, radius);

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
