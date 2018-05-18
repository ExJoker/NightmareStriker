using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

	public bool isDead;
	int hp = 20;

	void UnderFire ()
	{
		float x = Random.Range (-2f, -5f);
		GetComponent<Rigidbody> ().AddExplosionForce (10f,
			transform.position - new Vector3 (x, 2.0f, 0),
			20f);
		GetComponent<Rigidbody> ().velocity =
			transform.TransformDirection (new Vector3 (x, 2.0f, 0));
		hp -= Random.Range (10, 20);
		if (hp <= 0) {
			isDead = true;
			Destroy (gameObject, 0.5f);
			return;
		}
	}
}
