using UnityEngine;
using System.Collections;

/// <summary>
/// <Kurzbeschreibung>
/// 	Ist die KI des Gegners. Bestimmt was dieser tut.
/// <Beschreibung>:
/// 	Einem Gegner sind folgende Status bekannt (Niedrigste Nummer = höchste Priorität)
/// 		[0]	Zuschlagen
/// 		[1] Angreifen / Jagen
/// 		[2] patrollieren
/// 		[3] idle
/// 	Ist der Spieler in Reichweite für einen Schlag, wird dieser ausgeführt.
/// 	Ist der Spieler nicht in Reichweite, dafür aber in Sichtweite, wird dieser angegriffen
/// 	Ist der Spieler weder in Reichweiter noch nicht Sichtweite patrolliert der Gegner
/// 	Ist der Gegner an einem Wegpunkt seiner Patrollie angekommen idelt er kurz bevor er weiter patrolliert.
/// 
/// 	Zu [1] "Angreifen"
/// 		Ein Gegner hat eine Kegelförmige Sicht nach vorne (=Sichtfeld). Nur wenn der Spieler sich im 
/// 		Sichtfeld des Gegner befindet wird dieser auch angegriffen.
/// <Wichtig>
/// 	Es wird auf das Script "EnemySight" zugegriffen
/// </summary>
public class EnemyDefaultScript : MonoBehaviour {
	
	/// <summary>Die Lebenspunkte des Gegners</summary>
	public float healthPoints = 100.0f;

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

	//------------------------------------------------------------------------------------------------------------------------

	/// <summary> Zeit die der Gegner am erreichte Wegpunkt schon wartet </summary>
	private float patrolTime = 0f;

	/// <summary> Referenz auf die Agent für die automatische Navigation </summary>
	private NavMeshAgent nav;

	/// <summary>The Main-Character</summary>
	private GameObject player;

	/// <summary> Referenz auf den zugehörigen Animator. Mit diesem Objekt werden die Parameter angesteuert welche wiederum die Animationen antriggern </summary>
	private Animator anim;

	/// <summary>Referenz auf das Script 'EnemySight' welches sich darum kümmert ob der Spieler gesehen wird.</summary>
	private EnemySight enemySight;

	/// <summary>Referenz auf den Animator des Spielers</summary>
	private Animator playerAnim;

	/// <summary>The index of the waypoint. </summary>
	private int waypointIndex = 0;

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
		anim		=	GetComponent<Animator> ();
		player		=	GameObject.FindGameObjectWithTag ("Player");
		playerAnim	=	player.GetComponent<Animator> ();
		enemySight	=	GetComponent<EnemySight> ();

		if (player == null) Debug.Log("Player-Referenz nicht gefunden !");
		if (anim == null)
			Debug.Log ("Animator-Referenz nicht gefunden !");
		if ( nav == null) Debug.Log("NavMeshAgent-Referenz nicht gefunden !");
		if (playerAnim == null)
			Debug.Log ("PlayerAnimator-Referenz nicht gefunden !");
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
		Debug.DrawLine (this.transform.position, player.transform.position);

		bool isEnemyAlive = anim.GetBool ("Alive");
		bool isPlayerAlive = playerAnim.GetBool ("Alive");
		bool isPlayerInSight = enemySight.isPlayerInSight;
		bool isPlayerInWeaponrange = isPlayerInWeaponRange ();

		anim.SetBool ("PlayerIsInSight", isPlayerInSight);
		anim.SetBool ("PlayerIsInWeaponRange", isPlayerInWeaponrange);

		Debug.Log ("(isEnemyAlive? " + isEnemyAlive +") (isPlayerAlive? " + isPlayerAlive + ") (isPlayerInSight? " + isPlayerInSight + ") (isPlayerInWeaponRange? " + isPlayerInWeaponrange + ") (Distance=" + Vector3.Distance(this.transform.position, player.transform.position) + ")");


		//	Wenn der Spieler am Leben und in Waffen-Reichweite ist
		if (isEnemyAlive 
			&& isPlayerAlive
			&& isPlayerInWeaponrange) {

			Debug.Log ("Schlage Spieler ! [" + Time.fixedTime + "]");
			//	Schlage zu
			hitPlayer();
		}	

		//	Oder wenn der Spieler am Leben und in Sichtweite ist
		else if (isEnemyAlive 
			&& isPlayerAlive
			&& isPlayerInSight) {
			Debug.Log ("greife Spieler an ! [" + Time.fixedTime + "]");
			//	attackieren 
			chasing();

		}

		//	andernfalls...
		else {
			//	Patrolliere weiter
			Debug.Log ("patrolliere ! [" + Time.fixedTime + "]");
			patrolling();

		}

//		anim.SetFloat ("Speed", nav.speed);

	}

	/// <summary>
	/// Schlägt den Spieler
	/// </summary>
	public void hitPlayer () {
		Debug.Log ("HitPlayer ... ");
		anim.SetBool ("Attack", true);
		playerAnim.SetTrigger("GettingHit");
	}

	/// <summary>
	/// Attackiert den Spieler >> Läuft zu ihm hin und versucht in Waffenreichweite zu kommen
	/// </summary>
	public void chasing() {

		Debug.Log ("Chasing ...");


		//	Setze Ziel
		nav.destination = player.transform.position;

		//	Setzte Geschwindigkeit
		nav.speed = runSpeed;

		//@todo: Timer für inaktivität im Kamps integrieren
	}

	/// <summary>
	/// Der Gegner bekommt Schaden. 
	/// </summary>
	/// <param name="amountOfDamage">Amount of damage.</param>
	public void gettingHit(float amountOfDamage) {
		healthPoints -= amountOfDamage;
		if (healthPoints <= 0f) {
			anim.SetBool ("Alive", false);
		}
	}

	/// <summary> 
	/// Triggert das Sterben des Gegners an. 
	/// </summary>
	private void die() {

		//	Animation via Animator antriggern
		anim.SetTrigger("Sterben");

		//	Merke dass Charakter tot ist
		anim.SetBool ("Alive", false);

	}

	/// <summary>
	/// Überprüft ob er der Spieler sich in Waffenreichweite befindet
	/// </summary>
	/// <returns><c>true</c>, if player in weapon range was ised, <c>false</c> otherwise.</returns>
	private bool isPlayerInWeaponRange() {
		float distance = Vector3.Distance (this.transform.position, player.transform.position);
		return  distance <= weaponRange;
	}

	/// <summary>
	/// Patrolliert weiter.
	/// </summary>
	private void patrolling() {
		
		Debug.Log ("Start patrolling...");
		if (waypoints.Length == 0)
			return;

		//	Setze die Geschwindigkeit des Gegners
		nav.speed = patroleSpeed;

		//	wurde das Ziel erreicht?
		if (nav.remainingDistance <= nav.stoppingDistance) {

			anim.SetFloat ("Speed", 0.0f);

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
				anim.SetFloat ("Speed", patroleSpeed);
				nav.destination = waypoints[waypointIndex].position;
			} else {
				//warten	
			}

		}

	}

	private void retread() {

	}
}
