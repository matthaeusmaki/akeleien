using UnityEngine;
using System.Collections;
using UnityEngine.UI;


/// <summary>
/// Health management.
/// 	-	Contains the current HealthPoints
/// 	-	calculates the addition or subtraction of HealthPoints (Demage / Potions / ...)
/// 	-	Shows the Healthbar above the character
/// </summary>
public class HealthManagement : MonoBehaviour {


	/// <summary> The GUI-Element to show the HealthPoints </summary>
	public Slider slider;

    /// <summary> If the slider rotates to the camera </summary>
    public bool rotateSliderToCamera = true;

	/// <summary> The Healhtpoints the character starts with </summary>
	public float startingHealth	=	100f;

	/// <summary> The Color the fo the Healthbar with full HealthPoints </summary>
	public Color fullHealthColor 	=	Color.green;

	/// <summary> The Color the fo the Healthbar with no more HealthPoints </summary>
	public Color zeroHealthColor	=	Color.red;

	/// <summary> The image component of the Slieder </summary>
	public Image fillImage;

	/// <summary> The time of last damage. </summary>
	[HideInInspector]
	public System.DateTime timeOfLastCombatAction;


	private Camera mainCamera;


	/// <summary> The current Amount of Healthpoints</summary>
	public float currentHealth;

	[HideInInspector]
	/// <summary> Indicator if the Character is alive </summary>
	public bool alive =	true;

	private Animator anim;

	public void Awake() {		
		mainCamera = Camera.main;
		anim = GetComponent<Animator> ();
	}


	public void Update() {
		drawHealthbar ();
	}

	//	===================================================================================

	public void OnEnable() {
		currentHealth	=	startingHealth;
		alive =	true;

		drawHealthbar ();
	}

	//	===================================================================================


	/// <summary> Takes damage. </summary>
	/// <param name="amountOfDamage">Amount of damage.</param>
	public void takeDamage (int amountOfDamage) {
        
		currentHealth -= amountOfDamage;
		if (currentHealth <= 0) {
			alive = false;
		}

		drawHealthbar ();
		timeOfLastCombatAction	=	System.DateTime.Now;

	}

	/// <summary>
	/// Update the Healthbar. (Setting value, and setting the Color of the bar)
    /// Also rotates the healthbar to the camera
	/// </summary>
	private void drawHealthbar() {

		if (alive) 
        {		
			//	Set the sliedrs valure
            if (!slider)
            {
                return;
            }

			slider.value = currentHealth;

            if (fillImage)
            {
                fillImage.color = Color.Lerp(zeroHealthColor, fullHealthColor, currentHealth / startingHealth);
            }				

            if (rotateSliderToCamera)
            {
                slider.transform.rotation = mainCamera.transform.rotation;
            }
		} else {
            if (slider != null)
            {
                if (slider.gameObject != null)
                {
                    slider.gameObject.SetActive(false);
                }
            }
		}
			
	}

}
