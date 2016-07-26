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

        //  Rotiere Kamera hinter den Spieler
        rotateCamera();
                
	}

    /// <summary>
    /// Rotiert die Kamere immer hitnen den Spieler
    /// </summary>
    private void rotateCamera()
    {
        transform.LookAt(player.transform);

        //var lookDir = player.position - transform.position;
        //lookDir.y = 0; // keep only the horizontal direction
        //transform.rotation = Quaternion.LookRotation(lookDir);
    }

    /// <summary>
    /// [Experimentiell]
    /// Mit Eingaben über das Scrollrad der Maus kann die Kamera hoch oder runter gefahren werden
    /// </summary>
    private void input()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            float fov = Camera.main.fieldOfView;
            fov = Mathf.Clamp(fov + sensivity, minFov, maxFov);
            Camera.main.fieldOfView = fov;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            float fov = Camera.main.fieldOfView;
            fov = Mathf.Clamp(fov - sensivity, minFov, maxFov);
            Camera.main.fieldOfView = fov;
        }
    }
}
