using UnityEngine;
using System.Collections;

public class FackelLicht : MonoBehaviour {
	
	public float m_IntensitiyMin = 3;
	public float m_IntensitiyMax = 4;
	public float m_RangeMin = 6;
	public float m_RangeMax = 7;
	public float m_biggestStep = 0.01f;
	public float m_jumpMax = 0.05f;

	private Vector3 m_originalPosition;

	private Light light;

	// Use this for initialization
	void Start () {
		light = GetComponent<Light> ();
		m_originalPosition = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		setRandomLightRange ();
		setRandomLihgtIntensitiy ();
		setRandomPosition ();
	}

	private void setRandomPosition() {		
		float newYPosition = Random.Range (m_originalPosition.y - m_jumpMax, m_originalPosition.y + m_jumpMax);
		Vector3 newPosition = new Vector3(m_originalPosition.x, newYPosition, m_originalPosition.z);
		transform.position = Vector3.MoveTowards (transform.position, newPosition, 1);
	}

	private void setRandomLightRange() {
		float newRange;
		int minusRange = Random.Range(0,10);
		if (minusRange < 5) {			
			newRange = Random.Range (light.intensity - m_biggestStep, light.intensity); 
			if (newRange < m_RangeMin) 
				newRange = m_RangeMin;			
		} else {			
			newRange = Random.Range (light.range, light.range + m_biggestStep); 
			if (newRange > m_RangeMax) 
				newRange = m_RangeMax;			
		}
		light.range = newRange;
	}

	private void setRandomLihgtIntensitiy() {
		int minusIntensity = Random.Range (0, 10);

		float newIntensitiy;

		if (minusIntensity < 5) {
			newIntensitiy = Random.Range (light.intensity - m_biggestStep, light.intensity);
			if (newIntensitiy < m_IntensitiyMin)
				newIntensitiy = m_IntensitiyMin;
		} else {
			newIntensitiy = Random.Range (light.intensity, light.intensity + m_biggestStep);
			if (newIntensitiy < m_IntensitiyMax)
				newIntensitiy = m_IntensitiyMax;
		}

		light.intensity = newIntensitiy;
	}
}
