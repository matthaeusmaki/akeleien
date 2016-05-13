using UnityEngine;
using System.Collections;

public class SimpleEnemyKi : MonoBehaviour {

	/// <summary>Die Höhe des Schadens die der Gegner verursacht</summary>
	public float damage = 15.0f;

	/// <summary>Die Geschwindigkeit des Gegners, wenn er angreift</summary>
	public float runSpeed = 4.0f;

	/// <summary>Die Geschwindigkeit des Gegners, während er patroulliert</summary>
	public float patroleSpeed = 2.0f;

	/// <summary>Die Geschwindigkeit des Gegners, wenn er den Rückzug wegen Inaktivität antrtit</summary>
	public float retreadSpeed = 5.0f;

	/// <summary> Zeit in Sekunden die der Gegner idelt bevor er wieder patrolliert</summary>
	public float timeOfIdle = 5.0f;

	/// <summary>Die aggro range</summary>
	public float aggroRange = 10.0f;

	/// <summary> Die Waffenreichweite des Gegnerz </summary>
	public float weaponRange = 2.0f;

	/// <summary>The max time of inactivity on combat.</summary>
	public float maxTimeOfInactivityOnCombat = 10.0f;

	/// <summary> Die Zeit die der Gegner beim Erreichen eines Wegpunktes wartet bevor er weiter patrolliert </summary>
	public float patrolWaitTime = 10.0f;

	/// <summary> Die Wegpunkte die der Charakter abläuft </summary>
	public Transform[] waypoints;

	[HideInInspector]
	public bool isAttacking = false;

	[HideInInspector]
	public bool isChasing = false;

	//------------------------------------------------------------------------------------------------------------------------

	/// <summary> Zeit die der Gegner am erreichte Wegpunkt schon wartet </summary>
	private float patrolTime = 0f;

	/// <summary> Referenz auf die Agent für die automatische Navigation </summary>
	private NavMeshAgent nav;

	/// <summary>The Main-Character</summary>
	private GameObject player;

	// <summary> Referenz auf den zugehörigen Animator. Mit diesem Objekt werden die Parameter angesteuert welche wiederum die Animationen antriggern </summary>
	private AnimationManagement animManagement;

	/// <summary>Referenz auf das Script 'EnemySight' welches sich darum kümmert ob der Spieler gesehen wird.</summary>
	private EnemySight enemySight;

	/// <summary> The health management. </summary>
	private HealthManagement healthManagement;

	/// <summary>The index of the waypoint. </summary>
	private int waypointIndex = 0;

	private float timeSinceLastActionHappend = 0.0f;

	//  ======================================================================================================================

	/// <summary>
	///	Event:
	/// 	Awake is called first (Even before Start), Even if the Script-Component is not enabled.
	/// 	Is best used for: References between scripts and initialisation. 
	/// 
	/// Beschreibung:
	/// 	Wird verwendet um die referenzen auf andere Objekte zu erlangen		
	/// 
	/// </summary>
	void Awake() {		
		nav 		= 	GetComponent<NavMeshAgent> ();
		player		=	GameObject.FindGameObjectWithTag ("Player");

		enemySight	=	GetComponent<EnemySight> ();
		healthManagement	=	GetComponent<HealthManagement> ();
		animManagement	=	GetComponent<AnimationManagement> ();

		if (healthManagement == null)
			Debug.Log ("HealthManagement nicht gefunden !");

		if (animManagement == null)
			Debug.Log ("AnimManagement nicht gefunden !");
			

		if (player == null) 
			Debug.Log ("Player-Referenz nicht gefunden !");		

		if ( nav == null) 
			Debug.Log("NavMeshAgent-Referenz nicht gefunden !");

		if (enemySight == null)
			Debug.Log ("EnemySihgtScript-Referenz nicht gefunden");

	}

	/// <summary>
	/// Event:
	/// 	Start ist called imidiatly called after awake, and before the first update,
	/// 	but only if the Script-Component ist enabled.
	/// </summary>
	void Start () {


	}

	// Update is called once per frame
	void Update () {
		//		Debug.DrawLine (this.transform.position, player.transform.position);

		//	Is the character alive? 
		if (healthManagement.alive) {

			bool isPlayerInSight = enemySight.isPlayerInSight;		//	is the player in Sight?
			bool isPlayerInWeaponrange = isPlayerInWeaponRange ();	//	Is the player in Weapon-Range?

			//Debug.Log ("(isEnemyAlive? " + isEnemyAlive +") (isPlayerAlive? " + isPlayerAlive + ") (isPlayerInSight? " + isPlayerInSight + ") (isPlayerInWeaponRange? " + isPlayerInWeaponrange + ") (Distance=" + Vector3.Distance(this.transform.position, player.transform.position) + ")");

			//	verifies if the character is/should chase
			setMode (isPlayerInSight);

			//	Wenn der Spieler am Leben und in Waffen-Reichweite ist
			if (healthManagement.alive
			    && isPlayerInWeaponrange) {

				//	Timer wieder auf 0 setzen
				timeSinceLastActionHappend = 0;

				//	Schlage / Attackiere Spieler
				Debug.Log ("Schlage Spieler ! [" + Time.fixedTime + "]");
				animManagement.attack (player, damage);

			} 

			//	Wenn der Spieler am Leben ist und in Sichtweite
			else if (isChasing) {
				animManagement.run (player.transform.position, runSpeed);
			} 

			//	Default --> Patrollieren
			else {

				//	Patrolliere weiter
				//Debug.Log ("patrolliere ! [" + Time.fixedTime + "]");
				patrolling ();
			}

		} else {
			animManagement.die ();
		}

	}

	/// <summary>
	/// Verifies if the character should chase the player.
	/// </summary>
	/// <param name="isPlayerInSight">If set to <c>true</c> is player in sight.</param>
	/// <param name="isPlayerInWeaponrange">If set to <c>true</c> is player in weaponrange.</param>
	private void setMode(bool isPlayerInSight) {



		//	Wenn (Spieler am Leben) und (noch nicht im Chase-Modus) und (Spieler in Sichtweite ist)
		if (
			healthManagement.alive
			&&	isPlayerInSight) {
			isChasing = true;
		}

		//	Wenn im Chase-Modues
		if (isChasing) {

			//	Is the last combat-Action hast past to long
			if (timeSinceLastActionHappend > maxTimeOfInactivityOnCombat)
				isChasing = false;
		}
	}

	/// <summary>
	/// Überprüft ob er der Spieler sich in Waffenreichweite befindet
	/// </summary>
	/// <returns><c>true</c>, if player in weapon range was ised, <c>false</c> otherwise.</returns>
	private bool isPlayerInWeaponRange() {
		float distance 	= Vector3.Distance (this.transform.position, player.transform.position);
		return  distance <= weaponRange;
	}

	/// <summary>
	/// Patrolliert weiter.
	/// </summary>
	private void patrolling() {

		Debug.Log ("Start patrolling...");

		//	If no Waypoints set --> abort
		if (waypoints.Length == 0)
			return;

		//	Is the target reached?
		if (nav.remainingDistance <= nav.stoppingDistance) {

			//	Warte eine gewisse Zeit am erreichten Wegpunkt
			patrolTime += Time.deltaTime;
			if (patrolTime >= patrolWaitTime) {

				//	gehen Nächsten Wegpunkt ab
				if (waypointIndex == waypoints.Length - 1) {
					waypointIndex = 0;
				} else {
					waypointIndex++;
				}

				//	Setze Timer zurück
				patrolTime = 0;
				animManagement.walk (waypoints [waypointIndex].position, patroleSpeed);

			} else {
				animManagement.idle ();
			}

		} else {
			animManagement.walk (waypoints [waypointIndex].position, patroleSpeed);
		}

	}
}
