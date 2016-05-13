using UnityEngine;
using System.Collections;

public class DamageCollider : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		
		if (other.CompareTag ("Enemy")) {
			HealthManagement health = other.GetComponent<HealthManagement>();

		}

	}
}
