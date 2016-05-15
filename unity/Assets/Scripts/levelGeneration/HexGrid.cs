using UnityEngine;
using System.Collections;

public class HexGrid : MonoBehaviour {

    private float size;
    public float Size {
        get {
            return size;
        }
        set {
            size = value;
            tileHeight = size * 2;
            tileWidth = Mathf.Sqrt(3) / 2 * tileHeight;
        }
    }
    public int rows = 10;
    public int columns = 10;
    public float offset = 0.0f;
    public Color color = Color.red;
    
    private float tileHeight;
    private float tileWidth;

    void OnDrawGizmos() {
        Gizmos.color = color;
        Vector3 pos = this.transform.position;

        float gridWidth = columns * tileWidth;

        for (int col = 0; col < columns; ++col) {
            float posX = pos.x + col * tileWidth;
            for (int row = 0; row < rows; ++row) {
                float posZ = pos.z + row * (3 * tileHeight/4);

                Vector3 center = new Vector3(posX + (row % 2 == 0 ? tileWidth / 2 : 0), posZ, offset);
                drawHex(center);
            }
        }
    }

    private void drawHex(Vector3 center) {
        for(int i = 0; i <= 6; ++i) {
            Gizmos.DrawLine(hexCorner(center, i), hexCorner(center, i == 6 ? 0 : i + 1));
        }
    }

    private Vector3 hexCorner(Vector3 center, int i) {
        float angleDeg = 60 * i + 30;
        float angleRad = Mathf.PI / 180 * angleDeg;
        return new Vector3(center.x + size * Mathf.Cos(angleRad), 0.0f, center.y + size * Mathf.Sin(angleRad));
    }
}