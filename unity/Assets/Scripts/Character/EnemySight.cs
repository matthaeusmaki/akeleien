using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour {

	/// <summary> Gradzahl des Blickwinkels dass der Gegner sehen kann	</summary>
	public float fieldOfViewAngle = 110f;
	/// <summary> Angabe ob der Spieler in Sichtweite ist </summary>
	public bool isPlayerInSight = false;
    public float viewDistance = 10f;

    [HideInInspector]
	/// <summary> Angabe wo der Spieler beim letzten Frame noch gesehen wurde </summary>
	public Vector3 lastPlayerSighting;

    [HideInInspector]
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
		player		=	GameObject.FindGameObjectWithTag ("Player");

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
        calculateIfPlayerIsInSight();
		if (isPlayerInSight)
			Debug.Log ("Player is in Sight");
		

	}

    private void calculateIfPlayerIsInSight()
    {
        //  Distanz zu Spieler geringer als Sichtweite?
        float distance = Vector3.Distance(player.transform.position, this.transform.position);
        if (distance < viewDistance)
        {

            //  Ist Spieler auch im Blickfeld?
            Vector3 direction	=	player.transform.position - transform.position;
			float angle =	Vector3.Angle (direction, transform.forward);

			//	Ist der Winkel noch im Sichtfeld? (FieldOfView muss halbiert werden da von forward [=Mitte] ausgegeangen wird)
            if (angle < fieldOfViewAngle * 0.5f)
            {
                //	Besteht eine dirkete Sicht zum Spieler?
                RaycastHit hit;
                if (Physics.Raycast(transform.position + transform.up, //+up damit der raycast nicht am Boden entlang läuft
                        direction.normalized, out hit, viewDistance))
                {

                    if (hit.collider.gameObject == player)
                    {
                        isPlayerInSight = true;
                        lastPlayerSightingTime = Time.fixedTime;
                        lastPlayerSighting = player.transform.position;
                        return;
                    }
                }
            }
        }
        isPlayerInSight = false;
    }
    

}
