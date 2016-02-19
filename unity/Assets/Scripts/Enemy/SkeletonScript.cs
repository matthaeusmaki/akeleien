using UnityEngine;
using System.Collections;

public class SkeletonScript : MonoBehaviour {


	private Animation m_animation;

	//	Eigenschaften 
	public float m_Reichweite = 3.0f;


	//	Angriffsverhalten 
	public float m_Schaden = 10;
	public float m_maxZeitOhneInteraktionBeiAngriff;
	private GameObject m_aktuellesAngriffsZiel;
	private float m_ZeitOhneInteraktion;

	//	AnimationsClips
	public AnimationClip[] idleAnimationClips;
	public AnimationClip[] gethitAnimationClips;
	public AnimationClip walkAnimationClip;
	public AnimationClip deathAnimationClip;
	public AnimationClip caststeppAnimationClip;
	public AnimationClip attackNormalAnimationClip;
	public AnimationClip attackHeavyAnimationClip;
	public AnimationClip attackQuicklAnimationClip;


	// Use this for initialization
	void Start () {
		//anim = GetComponent<Animator> ();
		m_animation = GetComponent<Animation> ();
	}
	
	// Update is called once per frame
	void Update () {
		//anim.Play ("Idle");
		m_animation.Play ("Idle");
	}


	/// <summary>
	/// Attack the specified target.
	/// </summary>
	/// <param name="target">Target.</param>
	public bool attack(Transform target) {
		
		//	Falls das Zile in Reichwiete ist --> angreifen
		/*
		if (istZielInReichweite (target)) {
				
		} else { //	falls das Ziel nicht in Reichweite ist, läuft dieser Charakter dort hin.
			
		}*/

		return false;
	}

	public void patrolliere() {

	}
		
}
