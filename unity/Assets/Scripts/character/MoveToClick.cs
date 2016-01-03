using UnityEngine;
using System.Collections;
using System;

public class MoveToClick : MonoBehaviour {

    private int speed = 50;
    private NavMeshAgent agent;
    


	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
	    
        //  Bei Mausklick
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("Mausklick registriert");
            //  Ermittle Ziel
            Vector3 ziel = findTargetPoint();
            try
            {
                Debug.Log("Bewege Spieler zu (X=" + ziel.x + ") (z=" + ziel.z);
                agent.SetDestination(ziel);
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
            
        }
        
	}

    private Vector3 findTargetPoint()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.name.Equals("ground"))
            {
                return hit.point;
            }
        }
        throw new Exception("Boden nicht geunfen !");
    }
}
