using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ActionBar : MonoBehaviour {

    /// <summary>
    /// Sammlung der Verfügbaren Skills
    /// </summary>
    public Skill[] skills;    

    [HideInInspector]
    public bool busy = false;

    private Skill activeSkill = null;

    private AnimationManagement animManagement;
    private Animator anim;

	// Use this for initialization
	void Start () {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        animManagement = player.GetComponent<AnimationManagement>();
        anim = player.GetComponent<Animator>();

        //  Init all skills
        foreach (Skill skill in skills)
        {
            if (skill.icon != null)
            {
                skill.icon.fillAmount = 1;
            }
        }
	}

    void FixedUpdate()
    {

    }
	
	// Update is called once per frame
	void Update () 
    {
        if (busy)
        {
            if (activeSkill != null)
            {
                //  Ist der Cooldown noch nicht fertig?
                if (activeSkill.currentCooldown < activeSkill.cooldown)
                {   // ja - Cooldown ist noch nicht fertig
                    //  Zähler Cooldown
                    Debug.Log( "Skill '" + activeSkill.animatorTrigger + "' ist nicht bereit (" + activeSkill.currentCooldown + "/" + activeSkill.cooldown + ")");
                    activeSkill.currentCooldown += Time.deltaTime;
                    activeSkill.icon.fillAmount = activeSkill.currentCooldown / activeSkill.cooldown;
                }
                else // nein - Cooldown ist verstrichen
                {
                    activeSkill.currentCooldown = 0;
                    Debug.Log("Setze Aktiven Skill '" + activeSkill.animatorTrigger + "' zurück");
                    busy = false;
                    activeSkill = null;
                }
            }
            //  Absicherung für Fehlerzustand
            else
            {
                Debug.LogWarning("Absichersung für Fehlerzustand in [ActionBar.Update]");
                busy = false;
                activeSkill = null;
            }
        }

        //  Skills sind bereit
        else
        {            
            foreach (Skill skill in skills)
            {
                //if (skill.isReady)
                //{
                    if (Input.GetKeyDown(skill.key)) 
                    {
                        Debug.Log("Trigger skill '" + skill.animatorTrigger + "'");
                        //  Setze activen Skil sowie beschäftgit-Flag
                        activeSkill = skill;
                        busy = true;

                        //  Führe Aktion durch
                        if (animManagement != null) 
                        { 
                            animManagement.triggerSkill(skill.animatorTrigger);
                        }
                        else
                        {
                            anim.SetTrigger(skill.animatorTrigger);
                        }
                    }
                //}
            }
        }
	}
}

/// <summary>
/// Jeder Skill/Fähigkeit
/// </summary>
[System.Serializable]
public class Skill
{
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
    public float currentCooldown = 0;

    /// <summary>
    /// Die Angabe ob der Skill Einsatzbereit ist (Kein Aktiver Cooldown)
    /// </summary>
    [HideInInspector]
    public bool isReady = true;

    /// <summary>
    /// Der Key der gedrückt werden muss um die Fähigkeit zu aktivieren
    /// </summary>
    public KeyCode key;

    /// <summary>
    /// Name des Triggers im Animator der bei Aktivierung des Skills angetriggert wird
    /// </summary>
    public string animatorTrigger;
}