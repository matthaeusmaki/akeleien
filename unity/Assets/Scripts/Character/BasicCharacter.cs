using UnityEngine;
using System.Collections;

public class BasicCharacter : MonoBehaviour {

    /// <summary>
    /// Gibt an ob der Charakter gerade angreift. 
    /// Wird verwendet damit ein Charakter nicht mehrfach angreift.
    /// </summary>
    [HideInInspector]
    public bool isAttacking = false;

    /// <summary>   Die Sound-Quelle für Schritt-Geräusche  </summary>
    public AudioSource audioSourceFootsteps;

    /// <summary>   eine Sound-Quelle für alle anderen Sounds des Characters außer die Schritt-Geräushce    </summary>
    public AudioSource audioSourceMouth;	
    
    /// <summary>   Aduio Clip of the player attacking  </summary>
    public AudioClip audioClip_attack;
	
	/// <summary>   Audio Clips für die verschiedenen Sounds wenn der Charakter Schaden erleidet    </summary>				
    public AudioClip[] audioClips_getHits;		

    /// <summary>   Audio-clip für das Sterben des Charakters   </summary>
    public AudioClip audioClip_die;
	
	/// <summary>   Audio-clips für die verschiedenen Schrittgeräusche  </summary>				
    public AudioClip[] audioClips_footsteps;
    // -------------------------------------------------------------------------
    /// <summary>
    /// 
    /// </summary>    
    protected HealthManagement healthManagement;


    

    /// <summary> Referenz auf die Agent für die automatische Navigation </summary>
    protected NavMeshAgent nav;

    /// <summary> Eine Referenz auf das Rigidbody-Objekt welches für Physikspielereien benötigt wird </summary>
    protected Rigidbody rigidbody;	
	
	/// <summary> Reference to the animator component </summary>		
    protected Animator anim;

    /// <summary>
    /// Start ist called imidiatly called after awake, and before the first update,
    /// but only if the Script-Component ist enabled.
    /// </summary>
    void Start()
    {
        anim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
        healthManagement = GetComponent<HealthManagement>();
    }

}
