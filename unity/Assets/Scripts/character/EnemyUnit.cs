using UnityEngine;
using System.Collections;

public class EnemyUnit : CharacterUnit {

	public int maxSpeed;
	public int minSpeed;
	public int aggroRange;
	
	protected override void Start() {
		base.Start ();
		Debug.Log ("start enemyUnit");
		this.SetTargetPoint(new Vector3(8.6f, 1.0f, -8.4f));
	}
	
	void Update() {

	}
}
