using UnityEngine;
using System.Collections;
using System;

public class MainCharacter : MonoBehaviour {

	public AudioSource audioSourceFootsteps;
	public AudioSource audioSourceMouth;
	public AudioClip attackClip;		//	Aduio Clip of the player attacking
	public AudioClip[] gethitClips;		//	Audio Clip of the player getting hit
	public AudioClip dieClip;
	public AudioClip[] footstepClips;

	public float health;				//	Current Healthpoints of the player

	private Rigidbody m_rigidbody;
	private Animator anim;				//	Reference to the animator component
	private NavMeshAgent agent;
	private NavMeshHit navHitPosition;
	private Vector3 cursorPosition;
	private GameObject targetObject;

	/// <summary>
	/// Awake is called first (Even before Start), Even if the Script-Component is not enabled.
	/// Is best used for: References between scripts and initialisation
	/// </summary>
	private void Awake() {		


	}

	/// <summary>
	/// Start ist called imidiatly called after awake, and before the first update,
	/// but only if the Script-Component ist enabled.
	/// </summary>
	void Start () {
		anim = GetComponent<Animator>();
		m_rigidbody = GetComponent<Rigidbody> ();
		agent = GetComponent<NavMeshAgent> ();
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

		//	Bestimme die Bewegung des Charakters
		MovementManagement ();

		//	Bestimmme die Animation die abgespielt werden soll
		AnimationManagement ();

		//	Bestimme den Sound der abgespielt werden soll
		AudioManagement ();

		if (Input.GetKeyDown ("1")) {
			sterben ();
		}
	}

	/// <summary>
	/// - Called Every Physics Step
	/// - Fixed Update intervals are consistent
	/// - Useed for regular updates such as "Adjusting Physics (Rigidbody) objects"
	/// </summary>
	void FixedUpdate() {		

	}

	private void AnimationManagement () {

		//	Bestimme Geschwindigkeit
		if (agent.remainingDistance > 0.5f) { // ein abfrage auf 0 kann sporadisch dazu führen dass der Charakter um den Zielpunkt "herumeiert"
			anim.SetFloat ("Speed", agent.speed);
		} else {
			anim.SetFloat ("Speed", 0f);
		}



	}


	/// <summary>
	/// Controles what the characters does
	/// </summary>
	/// <param name="horizotnal">Horizotnal.</param>
	/// <param name="vertical">Vertical.</param>
	/// <param name="attack">If set to <c>true</c> attack.</param>
	private void MovementManagement () {

		if (anim.GetBool ("Alive")) {
			//	Setze Zielpunkt zu Mausklick
			if (Input.GetKeyDown (KeyCode.Mouse0)) {
				Debug.Log ("Mausklick registriert");
				agent.ResetPath ();

				//  Ermittle Ziel
				Vector3 ziel = findTargetPoint ();
				try {
					Debug.Log ("Bewege Spieler zu (X=" + ziel.x + ") (z=" + ziel.z);
					agent.SetDestination (ziel);
				} catch (Exception e) {
					Debug.Log (e);
				}

			}

			//	Angriffs ggf triggern
			bool attack = Input.GetKey(KeyCode.Space);
			if (attack && !anim.GetCurrentAnimatorStateInfo (0).IsName ("Attack")) {				
				audioSourceMouth.clip = attackClip;
				audioSourceMouth.Play ();
			}
			anim.SetBool ("Attack", attack);
		}

	}

	private Vector3 findTargetPoint()
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hit, 1000.0f, (1 << 8)))
		{
			return hit.point;
		}
		throw new Exception("Boden nicht geunfen !");
	}

	/// <summary>
	/// Plays the sounds of the character
	///  - footsteps
	///  - attack
	///  - gethit
	///  - die
	/// </summary>
	void AudioManagement() {
		
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Run")) {
			if (!audioSourceFootsteps.isPlaying) {
				audioSourceFootsteps.clip = footstepClips[UnityEngine.Random.Range(0,footstepClips.Length -1)];
				audioSourceFootsteps.Play ();
			} 
		}

		//	Besser beim abgreifen der Inputs mit einbauen und als trigger verwenden
		/*
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Attack")) {
			if (!audioSourceMouth.isPlaying) {
				audioSourceMouth.clip = attackClip;
				audioSourceMouth.Play ();
			}
		}
		*/

	}


	public void gettingHit(float dmg) {
		//	Taking dmg 
		health -= dmg;

		//	Play animation 
		if (health <= 0f) { //	is Player dead?
			sterben ();
		} else {	// getting hit
			anim.SetTrigger("GettingHit");

			audioSourceMouth.clip = gethitClips[UnityEngine.Random.Range(0,gethitClips.Length -1)];
			audioSourceMouth.Play ();
		}

	}

	public void sterben() {
		if (anim.GetBool("Alive")) {
			anim.SetBool ("Alive", false);
			anim.SetTrigger ("Sterben");

			audioSourceMouth.clip = dieClip;
			audioSourceMouth.Play ();
		}
	}


}
