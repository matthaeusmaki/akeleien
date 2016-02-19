using UnityEngine;
using System.Collections;

public class MainCharacterHashIds : MonoBehaviour {
	
	/// <summary> HashId des Paramters welcher für die Geschwindigkeit (float) des Spielers verantwortlich ist </summary>
	public int speed;

	/// <summary> HashId des Parameters für Attackieren (bool) </summary>
	public int attack;

	/// <summary> HashId des Parameters für GettingHit (Trigger) </summary>
	public int gettingHit;

	/// <summary> HashId des Paramters für Stereben (Trigger) </summary>
	public int die;


	// Use this for initialization
	void Start () {
		/*
		Animator anim = GetComponent<Animator> ();
		speed		=	anim.StringToHash ("Speed");
		attack		=	anim.StringToHash ("Attack");
		gettingHit	=	anim.StringToHash ("GettingHit");
		die 		=	anim.StringToHash ("die");
		*/
	}

}
