using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Main character.
/// </summary>
public class MainCharacter : MonoBehaviour {

	public AudioSource 	audioSourceFootsteps;		//	Die Sound-Quelle für Schritt-Geräusche
	public AudioSource 	audioSourceMouth;			//	eine Sound-Quelle für alle anderen Sounds des Characters außer die Schritt-Geräushce
	public AudioClip 	attackClip;					//	Aduio Clip of the player attacking
	public AudioClip[] 	gethitClips;				//	Audio Clips für die verschiedenen Sounds wenn der Charakter Schaden erleidet
	public AudioClip 	dieClip;					//	Audio-clip für das Sterben des Charakters
	public AudioClip[] 	footstepClips;				//	Audio-clips für die verschiedenen Schrittgeräusche
	public float		health;						//	Current Healthpoints of the player
	public float 		attackRange = 1;			//	Die Reichweite der Waffe
	public float 		forceToPushStone = 100f;

	private PiontAndClickMovement movementScript;	//	Eine Referenz auf das Script das sich um das Movement kümmert
	private Rigidbody 	m_rigidbody;				//	Eine Referenz auf das Rigidbody-Objekt welches für Physikspielereien benötigt wird
	private Animator 	anim;						//	Reference to the animator component
	private NavMeshAgent agent;						//	Eine Referenz auf den Agent für die Pfadfindung notwendig ist




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
		movementScript = GetComponent<PiontAndClickMovement> ();
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

		SteuerungsManagement ();

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
		if (agent.remainingDistance > 0.5f) { // ein abfrage auf exakt 0 kann sporadisch dazu führen dass der Charakter um den Zielpunkt "herumeiert"
			anim.SetFloat ("Speed", agent.speed);
		} else {
			anim.SetFloat ("Speed", 0f);
		}

	}


	/// <summary>
	/// Controles what the characters does
	/// </summary>
	private void SteuerungsManagement () {

		if (anim.GetBool ("Alive")) {
			
			//	Angriffs ggf triggern
			bool attack = Input.GetKey(KeyCode.Space);
			if (attack && !anim.GetCurrentAnimatorStateInfo (0).IsName ("Attack")) {				
				audioSourceMouth.clip = attackClip;
				audioSourceMouth.Play ();
			}
			anim.SetBool ("Attack", attack);

			if (attack) {
				angreifen ();
			}
		}

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

	}

	/// <summary>
	/// Routine wenn der Charakter Schaden erleidet.
	///  - Abzug des Schadens von den aktuellen Lebenspunkten
	///  - Kontrolle ob noch Lebenspunkte übrig sind. Wenn nicht Sterben-Sequenz einleiten
	///  - Audio-Cip für das Schaden erleiden abspielen
	/// </summary>
	/// <param name="dmg">Die Höhe des Schadens die der Charakter erleidet</param>
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

	/// <summary>
	/// Leitet das Sterben des Charakters ein:
	///  - triggert die Sterben-Animation an
	///  - Spielt die Sterben-Sound ab
	///  - deaktivierte die anderen Scripte (Movement...)
	/// </summary>
	private void sterben() {
		
		if (anim.GetBool("Alive")) {	//	nur wenn Charakter überhaupt noch am Leben ist
			
			anim.SetBool ("Alive", false);	//	Charakter als Tot kennzeichnen
			anim.SetTrigger ("Sterben");	//	Sterbe-Animation antriggern

			//	Sterbe-Sound abspielen
			audioSourceMouth.clip = dieClip;
			audioSourceMouth.Play ();

			//	Movement-Steuerung deaktivieren
			movementScript.enabled = false;
		}
	}

	private void angreifen() {
		//if (Physics.Raycast(ray, out hit, 1000.0f, (1 << 8)))
		Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange, (1<<9) );

		for (int i = 0; i < colliders.Length; i++) {
			Debug.Log ("Treffer: " + colliders [i].name);

			for (int c = 0; c < colliders [i].transform.childCount; c++) {
				Transform child = colliders [i].transform.GetChild (c);

				Collider childCollider = child.GetComponent<Collider>();
				childCollider.enabled = true;

				Rigidbody rigidbody = child.GetComponent<Rigidbody> ();
				rigidbody.AddForce (-this.transform.forward * forceToPushStone);
				rigidbody.useGravity = true;

				Destroy (child.gameObject, 3);
			}



		}
	}
		


}
