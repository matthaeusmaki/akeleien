using UnityEngine;
using System.Collections;

public class SimpleEnemyKi : BasicCharacter {

	/// <summary>Die Höhe des Schadens die der Gegner verursacht</summary>
	public float damage = 15.0f;

	/// <summary>Die Geschwindigkeit des Gegners, wenn er angreift</summary>
	public float runSpeed = 4.0f;

	/// <summary>Die Geschwindigkeit des Gegners, während er patroulliert</summary>
	public float patroleSpeed = 1.5f;

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

    /// <summary> Das Prefab das als Projektil bei einem Magie-Angriff erzeugt wird </summary>
    public GameObject projectilePrefab;

    /// <summary> Die Position wo das Projektil erzeugt wird </summary>
    public GameObject projectileSpawn;
	

	//------------------------------------------------------------------------------------------------------------------------

	/// <summary> Zeit die der Gegner am erreichte Wegpunkt schon wartet </summary>
	private float patrolTime = 0f;
    
	/// <summary>The Main-Character</summary>
	private GameObject player;

	// <summary> Referenz auf den zugehörigen Animator. Mit diesem Objekt werden die Parameter angesteuert welche wiederum die Animationen antriggern </summary>
	private AnimationManagement animManagement;

	/// <summary>Referenz auf das Script 'EnemySight' welches sich darum kümmert ob der Spieler gesehen wird.</summary>
	private EnemySight enemySight;

	/// <summary>The index of the waypoint. </summary>
	private int waypointIndex = 0;

	private float timeSinceLastActionHappend = 0.0f;

    private readonly short STATE_Idle = 0;
    private readonly short STATE_Patrolling = 1;
    private readonly short STATE_Charge = 2;
    private readonly short STATE_Attack = 3;

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
    void Awake() { }

	/// <summary>
	/// Event:
	/// 	Start ist called imidiatly called after awake, and before the first update,
	/// 	but only if the Script-Component ist enabled.
	/// </summary>
	void Start () {

        player = GameObject.FindGameObjectWithTag("Player");
        enemySight = GetComponent<EnemySight>();
        animManagement = GetComponent<AnimationManagement>();

        if (player == null)
            Debug.Log("Player-Referenz nicht gefunden !");
        
        if (enemySight == null)
            Debug.Log("EnemySihgtScript-Referenz nicht gefunden");

        if (animManagement == null)
            Debug.Log("AnimManagement nicht gefunden !");

        if (nav == null)
        {
            nav = GetComponent<NavMeshAgent>();
            if (nav == null) { 
                Debug.Log("NavMeshAgent-Referenz nicht gefunden !");
            }
        }

        if (healthManagement == null)
        {
            healthManagement = GetComponent<HealthManagement>();
            if (healthManagement == null)
            {
                Debug.Log("HealthManagement nicht gefunden !");
            }
        }
            
	}

	// Update is called once per frame
	void Update () {

		//	Is the character alive? 
		if (healthManagement.alive) 
        {
            if (healthManagement.currentHealth <= 0)
            {
                Debug.LogError("Alive obwohl keine Lebenspunkte mehr");
            }

            //  Ermittle State-Machine
            short state = getState();

            //  Attackiere 
            if (state == STATE_Attack)
            {
                isAttacking = true;
                timeSinceLastActionHappend = 0f;
                animManagement.attack(player, 5);
            }

            //  charge
            if (state == STATE_Charge)
            {
                animManagement.run(player.transform.position, runSpeed);
            }

            //  Patrolliere
            if (state == STATE_Patrolling)
            {
                patrolling();
            }


		} else {
			animManagement.die ();
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
    /// Bestimmt den Status für den charakter.
    /// Mögliche Stati sind:
    ///     -   Idle
    ///     -   Patrollieren
    ///     -   Charging (Zum Ziel hinlaufen)
    ///     -   Attacking (= Zuschlagen)
    /// </summary>
    private short getState() {

        bool isPlayerInSight = enemySight.isPlayerInSight;		//	is the player in Sight?
        bool isPlayerInWeaponrange = isPlayerInWeaponRange();	//	Is the player in Weapon-Range?
                
        //Debug.Log("[getState] (PlayerInWeaponRange? " + isPlayerInWeaponrange + ") (isAttacking? " + isAttacking + ") Time=" + Time.deltaTime );
        
        //	Wenn der Spieler am Leben und in Waffen-Reichweite ist --> attackiere
        if (isPlayerInWeaponrange   
            &&  !isAttacking) 
        {
            Debug.Log("Return attack. " + Time.deltaTime);
            return STATE_Attack;
        }

        //	Wenn der Spieler am Leben ist und in Sichtweite --> charge
        else if (healthManagement.alive 
            &&  isPlayerInSight 
            &&  timeSinceLastActionHappend < maxTimeOfInactivityOnCombat)
        {
            return STATE_Charge;
        }

        //	Default --> Patrollieren
        else
        {
            return STATE_Patrolling;
        }
        
    }

	/// <summary>
	/// Patrolliert weiter.
	/// </summary>
	private void patrolling() {

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

    public void attackMagic()
    {
        
        //  erzeuge Gameobject aus Prefab
        GameObject goProjectile = Instantiate(projectilePrefab.gameObject, projectileSpawn.transform.position, projectileSpawn.transform.rotation) as GameObject;
        
        //  Schieße Projektil auf aktuelle Position des Spielers
        Vector3 vel = (player.transform.position - projectileSpawn.transform.position).normalized * 7;
        vel.y = 0;
        goProjectile.GetComponent<Rigidbody>().velocity = vel;
    }

    
}
