using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	
	public GameObject player;
	
	private Vector3 playerPos;
	private Vector3 cameraPos;

	private int minFov	=	10;
	private int maxFov	=	100;
	private int sensivity = 10;
	
	void Start () {
		this.cameraPos = this.transform.position;
		this.playerPos = this.player.transform.position;
	}
	
	void Update () {
		if (Input.GetAxis ("Mouse ScrollWheel") < 0) {
			float fov = Camera.main.fieldOfView;
			fov = Mathf.Clamp(fov + sensivity, minFov, maxFov);
			Camera.main.fieldOfView = fov;
		} else if (Input.GetAxis ("Mouse ScrollWheel") > 0) {
			float fov = Camera.main.fieldOfView;
			fov = Mathf.Clamp(fov - sensivity, minFov, maxFov);
			Camera.main.fieldOfView = fov;
		}
	}
}
