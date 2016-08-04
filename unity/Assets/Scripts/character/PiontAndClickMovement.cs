using UnityEngine;
using System.Collections;
using System;
using UnityEngine.EventSystems;

public class PiontAndClickMovement : MonoBehaviour {
	    
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
            try
            {
                //  Nur wenn Maus nicht über UI Element ist
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    return;
                }

				//  Ermittle Ziel
				Vector3 ziel = findTargetPoint();
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
		if (Physics.Raycast(ray, out hit, 1000.0f, (1 << 8)))
		{
			return hit.point;
		}
		throw new Exception("Boden nicht geunfen !");
    }
}
