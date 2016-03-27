using UnityEngine;
using System.Collections;

public class HexGrid : MonoBehaviour {


    public float tileWidth = 10.0f;
    public int rows = 10;
    public int columns = 10;

    public Color color = Color.red;

	void Start () {
	}
	
	void Update () {
	}

    void OnDrawGizmos() {
        Gizmos.color = color;
        Vector3 pos = this.transform.position;

        float gridWidth = columns * tileWidth;

        for (int row = 0; row <= rows; row++) {
            float posZ = pos.z + row * tileWidth;

            // x-Richtung
            Gizmos.DrawLine(new Vector3(pos.x + (rows % 2 == 0 && row == rows ? tileWidth/2 : 0), 0.0f, posZ),
                        new Vector3(pos.x + gridWidth + (row == 0 || (rows %2 != 0 && row == rows) ? 0 : tileWidth / 2), 0.0f, posZ));

            float offset = row % 2 == 0 ? 0 : tileWidth / 2;
            if (row < rows) {
                // y-Richtung (bzw. z)
                for (int col = 0; col <= columns; col++) {
                    float px = pos.x+ tileWidth*col + offset;
                    Gizmos.DrawLine(new Vector3(px, 0.0f, posZ),
                                    new Vector3(px, 0.0f, posZ + tileWidth));
                }
            }
        }
        
    }
}