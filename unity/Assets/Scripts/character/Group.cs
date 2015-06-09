using UnityEngine;
using System.Collections.Generic;

public class Group : MonoBehaviour {

	public List<CharacterUnit> units = new List<CharacterUnit>();
	public GameObject path;

	public int patrolSpeed;

	private Vector3 patrolPoint;
	private UnitStateEnum groupState = UnitStateEnum.IDLE;

	void Start () {
	
	}

	void Update () {
	
	}
}
