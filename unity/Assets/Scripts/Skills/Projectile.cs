using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public float maxLiveTime = 3;
    public GameObject explosionPrefab;
    

	// Use this for initialization
	void Start () {
        Destroy(gameObject, maxLiveTime);
	}
	
	// Update is called once per frame
    /*
	void Update () {
        
	}
     */

    void OnCollisionEnter(Collision collision)
    {
        //  gehe alle Collisionspunkte durch
        Debug.Log("Collision mit: " + collision.gameObject.name);
        Debug.Log("Collisoin.tag: " + collision.gameObject.tag);

        if (collision.gameObject.tag.Equals("Player")
            || collision.gameObject.tag.Equals("Wall"))
        {
            explode();
        }

    }

    /// <summary>
    /// Startet eine Explosion bei der momemntan Standpunkt des Projektils.
    /// Löscht dafür aber das Projektil.
    /// Schaut ob der Spieler im Explodusionsradius ist --> wenn ja wird schaden hinzugefügt
    /// </summary>
    private void explode()
    {
        //  Start the Explosion
        if (explosionPrefab)
            Instantiate(explosionPrefab.gameObject, transform.position, transform.rotation);

        //  Lösche das Projektil
        Destroy(this);

        //  Calculate damage to player
        dealDamage();
    }

    private void dealDamage()
    {
        //  Berechne Distanz von Feuerball zu Spieler


        //  Füge beim Spieler Schaden hinzu
    }
}
