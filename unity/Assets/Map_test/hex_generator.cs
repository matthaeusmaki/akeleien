using UnityEngine;
using System.Collections;

public class hex_generator : MonoBehaviour {

    public GameObject tile;
    public int width = 10;
    public int height = 10;
    public int hexRadius = 2;

	// Use this for initialization
	void Start () {
        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++){
                float xPos = hexRadius * x + (y%2 == 0 ? 0 : hexRadius/2);
                float yPos = 0.865f * hexRadius * y;
                Instantiate(tile, new Vector3(xPos, 0, yPos), Quaternion.identity);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
