using UnityEngine;
using System.Collections;

/// <summary>
/// Manages the Animation --> Sets the AnimationControlloer Variables
/// </summary>
public class AnimationManagement : MonoBehaviour {

	private string PARAM_walk 		=	"walk";
	private string PARAM_run		=	"run";
	private string PARAM_idle		=	"idle";
	private string PARAM_attack		=	"attack";
	private string PARAM_die		=	"die";
	private string PARAM_takeDamage	=	"takeDamage";

	private HealthManagement 	healthManagement;
	private NavMeshAgent 		nav;
	private Animator 			animator;


	// Use this for initialization
	void Start () {
		healthManagement	=	GetComponent<HealthManagement> ();
		nav 				=	GetComponent<NavMeshAgent> ();
		animator 			=	GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// Attacks the given Object.
	/// Takes the Health-Management of the target, and gives it the damage
	/// </summary>
	/// <param name="target">The target to attack</param>
	/// <param name="damage">The amount of Damage the target gets</param>
	public void attack(GameObject target, float damage) {

		//	Controle the Aniamtor
		animator.SetTrigger(PARAM_attack);

        //  Calculate the damage
        MainCharacter mc = target.GetComponent<MainCharacter>();
        mc.gettingHit(damage);

	}

	/// <summary>
	/// Triggers the death of the character
	/// </summary>
	public void die() {
		
		animator.SetBool (PARAM_die, true);        
        nav.enabled = false;
		//@todo character disablen

	}

	/// <summary>
	/// Triggers the character to take damage.
	/// If the character has no more Healthpoints left, it will trigger the "die"-Method
	/// </summary>
	/// <param name="damage">The amount of Damage, the character takes</param>
	public void takeDamage(int damage) {
		
		//	Controle Animator
		animator.SetTrigger (PARAM_takeDamage);

		//	Calculate Damage
		healthManagement.takeDamage (damage);

		//	may trigger death
		if (!healthManagement.alive)
			die ();
	}

	/// <summary>
	/// Make the character walk to the given Position with the given speed. (Animation=walk)
	/// </summary>
	/// <param name="targetPosition">Target position.</param>
	/// <param name="speed">Speed.</param>
	public void walk(Vector3 targetPosition, float speed) {
		move (false, true, false, targetPosition, speed);
	}

	/// <summary>
	/// Make the character run to the given Position with the given speed. (Animation=run)
	/// </summary>
	/// <param name="targetPosition">Target position.</param>
	/// <param name="speed">Speed.</param>
	public void run(Vector3 targetPosition, float speed) {
		move (true, false, false, targetPosition, speed);
	}

	/// <summary>
	/// Make the character idle --> stand on his position and idles...
	/// </summary>
	public void idle() {
		Debug.Log ("idling...");
		move (false, false, true, this.transform.position, 0.0f);
	}

	/// <summary>
	/// Overall Method for moving the character
	/// </summary>
	/// <param name="run">Should Character Run?</param>
	/// <param name="walk">Or Should the Character just walk?</param>
	/// <param name="idle">Or should the Character idle?</param>
	/// <param name="position">The Position where to go. Null is allowed</param>
	/// <param name="speed">The Speed the characters moves</param>
	private void move(bool run, bool walk, bool idle, Vector3 position, float speed) {
		
		//	Set the Animator-Paramters
		animator.SetBool (PARAM_run, run);
		animator.SetBool (PARAM_walk, walk);
		animator.SetBool (PARAM_idle, idle);

		//	Set the NavMeshAgent-Parameters
		nav.destination = position;
		nav.speed = speed;
	}

}
