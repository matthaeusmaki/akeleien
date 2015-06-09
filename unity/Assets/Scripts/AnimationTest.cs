using UnityEngine;
using System.Collections;

public class AnimationTest : MonoBehaviour {
	
	private const short STATUS_idle 	= 	0;
	private const short STATUS_walk 	= 	1;
	private const short STATUS_run 	= 	2;
	private const short STATUS_die 	= 	3;
	private const short STATUS_punch 	= 	4;
	
	private string ANIMATION_idle	=	"idle";
	private string ANIMATION_walk	=	"walk";
	private string ANIMATION_run	=	"run";
	private string ANIMATION_die	=	"die1";
	private string ANIMATION_punch	=	"punch1";
	
	private short status = 0;
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		checkInputs ();
		animate ();
	}
	
	/// <summary>
	/// Checks the inputs.
	/// </summary>
	void checkInputs() {
		
		if (Input.GetKeyDown (KeyCode.Keypad1)) {
			status = STATUS_walk;
		}	
		
		if (Input.GetKeyDown (KeyCode.Keypad2)) {
			status = STATUS_run;
		}	
		
		if (Input.GetKeyDown (KeyCode.Keypad3)) {
			status = STATUS_die;
		}	
		
		if (Input.GetKeyDown (KeyCode.Keypad4)) {
			status = STATUS_punch;
		}	
		
		if (Input.GetKeyDown (KeyCode.Keypad0)) {
			status = STATUS_idle;
		}
		
	}
	
	/// <summary>
	/// Animate this instance.
	/// </summary>
	void animate() {
		switch (status)
		{
		case STATUS_idle:
			animation.Play( ANIMATION_idle );
			break;
		case STATUS_walk:
			animation.Play( ANIMATION_walk );
			break;
		case STATUS_run:
			animation.Play( ANIMATION_run );
			break;
		case STATUS_die:
			animation.Play( ANIMATION_die );
			break;
		case STATUS_punch:
			animation.Play( ANIMATION_punch );
			break;
		default:
			animation.Play( ANIMATION_idle );
			break;
		}
	}
}
