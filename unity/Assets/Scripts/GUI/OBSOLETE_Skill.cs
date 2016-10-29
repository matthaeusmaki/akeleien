using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class OSBOLETE_Skill : MonoBehaviour {

    /// <summary>
    /// Das Bild/Icon das den Skill representiert
    /// </summary>
    public Image icon;
  
    /// <summary>
    /// Die Zeit in Millisekunden die der Skill benötigt um wieder Einsatzbereit zu sein
    /// </summary>
    public float cooldown;
   
    /// <summary>
    /// Die Zeit die schon vom Cooldown verstrichen ist
    /// </summary>
    [HideInInspector]
    public float currentCooldown;

    /// <summary>
    /// Die Angabe ob der Skill Einsatzbereit ist (Kein Aktiver Cooldown)
    /// </summary>
    [HideInInspector]
    public bool isReady;

    /// <summary>
    /// Der Key der gedrückt werden muss um die Fähigkeit zu aktivieren
    /// </summary>
    public KeyCode key;

    /// <summary>
    /// Name des Triggers im Animator der bei Aktivierung des Skills angetriggert wird
    /// </summary>
    public string animatorTrigger;

    /// <summary>
    /// Der Animator des Hauptcharakters
    /// </summary>
    public Animator anim;

    /// <summary>
    /// AnimationManagement des Hauptcharacters, der die Aniamtion triggert
    /// </summary>
    public AnimationManagement animManangement;


    void Start()
    {        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        anim = player.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (isReady)
        {
            if (Input.GetKeyDown(key))
            {   
                //  Set the Skill to be not ready
                isReady = false;

                //  Trigger the skill
                anim.SetTrigger(animatorTrigger);
            }
        }
        else
        {
            if (currentCooldown >= cooldown)
            {
                currentCooldown = 0;
                isReady = true;
            }
        }
    }

    void Update()
    {
        if (!isReady)
        {
            if (currentCooldown < cooldown)
            {
                currentCooldown += Time.deltaTime;
                icon.fillAmount = currentCooldown / cooldown;
            }
        }
    }

    
}
