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
	public Slider m_Slider;

	/// <summary> The Healhtpoints the character starts with </summary>
	public float m_StartingHealth	=	100f;

	/// <summary> The Color the fo the Healthbar with full HealthPoints </summary>
	public Color m_FullHealthColor 	=	Color.green;

	/// <summary> The Color the fo the Healthbar with no more HealthPoints </summary>
	public Color m_ZeroHealthColor	=	Color.red;

	/// <summary> The image component of the Slieder </summary>
	public Image m_FillImage;

	/// <summary> The time of last damage. </summary>
	[HideInInspector]
	public System.DateTime timeOfLastCombatAction;


	private Camera mainCamera;


	/// <summary> The current Amount of Healthpoints</summary>
	private float m_CurrentHealth;

	[HideInInspector]
	/// <summary> Indicator if the Character is alive </summary>
	public bool alive =	true;

	public void Awake() {		
		mainCamera = Camera.main;
	}


	public void Update() {
		drawHealthbar ();
	}

	//	===================================================================================

	public void OnEnable() {
		m_CurrentHealth	=	m_StartingHealth;
		alive =	true;

		drawHealthbar ();
	}

	//	===================================================================================


	/// <summary> Takes damage. </summary>
	/// <param name="amountOfDamage">Amount of damage.</param>
	public void takeDamage (int amountOfDamage) {
		m_CurrentHealth -= amountOfDamage;
		if (m_CurrentHealth <= 0) {
			alive = false;
		}

		drawHealthbar ();
		timeOfLastCombatAction	=	System.DateTime.Now;

	}

	/// <summary>
	/// Update the Healthbar. (Setting value, and setting the Color of the bar)
	/// </summary>
	private void drawHealthbar() {

		Debug.Log ("...draw Healthbar...");
		
		//	Set the sliedrs valure
		if (!m_Slider) 
			return;		

		m_Slider.value = m_CurrentHealth;

		if (m_FillImage)
			m_FillImage.color = Color.Lerp (m_ZeroHealthColor, m_FullHealthColor, m_CurrentHealth / m_StartingHealth);

		m_Slider.transform.rotation = mainCamera.transform.rotation;
			
	}

}
