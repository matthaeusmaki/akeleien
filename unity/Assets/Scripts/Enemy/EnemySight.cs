using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour {

	/// <summary> Gradzahl des Blickwinkels dass der Gegner sehen kann	</summary>
	public float fieldOfViewAngle = 110f;
	/// <summary> Angabe ob der Spieler in Sichtweite ist </summary>
	public bool isPlayerInSight = false;
	/// <summary> Angabe wo der Spieler beim letzten Frame noch gesehen wurde </summary>
	public Vector3 lastPlayerSighting;
	/// <summary> Die letzte Position des Spielers die dieser Gegner selbst gesehen hat	</summary>
	public float lastPlayerSightingTime;

	//----------------------------------------------------------------------------------------------

	/// <summary> Eine Referenz auf die NavMesAgent-Komponente </summary>
	private NavMeshAgent nav;
	/// <summary> Eine Referenz auf den SphereCollider, welcher verwendet wird um den Blickwinkel des Gegner zu realiseren</summary>
	private SphereCollider col;
	/// <summary> Eine Referenz auf den Animator </summary>
	private Animator anim;
	/// <summary> Eine Referenz auf den Spieler </summary>
	private GameObject player;
	/// <summary> Eine Referenz auf den Animator des Spielers </summary>
	private Animator playerAnim;

	//==============================================================================================

	/// <summary>
	/// Event:
	/// 	Awake is called first (Even before Start), Even if the Script-Component is not enabled.
	/// 	Is best used for: References between scripts and initialisation
	/// 
	/// Funktion:
	/// 	Wird verwendet um die Referenzen zu erzeugen
	/// </summary>
	void Awake() {
		nav			=	GetComponent<NavMeshAgent> ();
		col			=	GetComponent<SphereCollider> ();
		anim 		=	GetComponent<Animator> ();
		player		=	GameObject.FindGameObjectWithTag ("Player");
		playerAnim	=	player.GetComponent<Animator> ();

		if (player == null) 
			Debug.Log("Player-Referenz nicht gefunden !");
		if (anim == null)
			Debug.Log ("Animator-Referenz nicht gefunden !");
		if ( nav == null) 
			Debug.Log("NavMeshAgent-Referenz nicht gefunden !");
		if (playerAnim == null)
			Debug.Log ("PlayerAnimator-Referenz nicht gefunden !");
		if (col == null)
			Debug.Log ("SphereCollider-Referenz nicht gefunden");
	}

	/// <summary>
	/// Start ist called imidiatly called after awake, and before the first update,
	/// but only if the Script-Component ist enabled.
	/// </summary>
	void Start () {
			
	}
	
	/// <summary>
	/// - Called Every Frame
	/// - Used for regular updates such as:
	///   - Moving Non-Physics objects
	///   - simple Timers
	///   - Receiving Inputs
	/// - Update Intervals times vary
	/// </summary>
	void Update () {
		
		//	nur wenn Spieler am Leben ist
		if (playerAnim.GetBool ("Alive")) {
			if (isPlayerInSight)
				Debug.Log ("Player is in Sight");
			//	Setze den Flag ob Spieler in Sichtweite ist (wird von Event 'OnTriggerStay' ermittelt)
			//anim.SetBool ("PlayerInSight", isPlayerInSight);
		} else {
			//anim.SetBool ("PlayerInSight", false);
		}

	}


	/// <summary>
	///	<EVENT>:
	/// 	OnTriggerStay is called once per frame for every Collider other that is touching the trigger.
	/// 
	/// <FUNKTION>:
	/// 	Überprüft ob der Spieler in Sichtweite ist
	/// </summary>
	/// <param name="other">Name of the collider.</param>
	void OnTriggerStay(Collider other) {
		
		//	Handelt es sich um den Spieler der den Trigger ausgelöst hat?
		if (other.gameObject == player) {
			
			//	Standardmäßig setzen dass Spieler nicht in Sichtweite ist
			isPlayerInSight = false;

			//	Erzeuge einen Vector vom Gegner zum Spieler und ermittle den Winkel dazwischen startend bei forward
			Vector3 direction	=	other.transform.position - transform.position;
			float angle =	Vector3.Angle (direction, transform.forward);

			//	Ist der Winkel noch im Sichtfeld? (FieldOfView muss halbiert werden da von forward [=Mitte] ausgegeangen wird)
			if (angle < fieldOfViewAngle * 0.5f) {

				//	Besteht eine dirkete Sicht zum Spieler?
				RaycastHit hit;
				if (Physics.Raycast (transform.position + transform.up, //+up damit der raycast nicht am Boden entlang läuft
					    direction.normalized, out hit, col.radius)) {

					if (hit.collider.gameObject == player) {
						isPlayerInSight = true;
						lastPlayerSightingTime = Time.fixedTime;
						lastPlayerSighting = player.transform.position;
					}
				}
			
			}

		}

	}

	/// <summary>
	/// OnTriggerExit is called when the Collider other has stopped touching the trigger.
	/// </summary>
	/// <param name="other">Other.</param>
	void OnTriggerExit (Collider other)
	{
		// If the player leaves the trigger zone...
		if (other.gameObject == player) {
			// ... the player is not in sight.
			isPlayerInSight = false;
		}
	}

	/// <summary>
	/// OnTriggerEnter is called when the Collider other enters the trigger.
	/// </summary>
	/// <param name="other">Other.</param>
	void OnTriggerEnter(Collider other) 
	{
		
	}


}
