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
    public float pointSize = 1.0f;
    public Color color = Color.red;
    
    private float tileHeight;
    private float tileWidth;
    public float TileWidth {
        get {
            return tileWidth;
        }
    }

    public Vector3[,] points;

    void OnDrawGizmos() {
        Gizmos.color = color;
        Vector3 pos = this.transform.position;

        points = new Vector3[columns, rows];

        for (int col = 0; col < columns; ++col) {
            float posX = pos.x + col * tileWidth;
            for (int row = 0; row < rows; ++row) {
                float posZ = pos.z + row * (3 * tileHeight/4);

                Vector3 center = new Vector3(posX + (row % 2 == 0 ? tileWidth / 2 : 0), posZ, offset);
                drawHex(center);
                points[col, row] = center;
            }
        }
    }

    private void drawHex(Vector3 center) {
        for(int i = 0; i <= 6; ++i) {
            Gizmos.DrawLine(hexCorner(center, i), hexCorner(center, i == 6 ? 0 : i + 1));
            drawPoint(center);
        }
    }

    private Vector3 hexCorner(Vector3 center, int i) {
        float angleDeg = 60 * i + 30;
        float angleRad = Mathf.PI / 180 * angleDeg;
        return new Vector3(center.x + size * Mathf.Cos(angleRad), center.z, center.y + size * Mathf.Sin(angleRad));
    }

    private void drawPoint(Vector3 center) {
        Gizmos.DrawLine(new Vector3(center.x - pointSize / 2, center.z, center.y), new Vector3(center.x + pointSize / 2, center.z, center.y));
        Gizmos.DrawLine(new Vector3(center.x, center.z, center.y - pointSize / 2), new Vector3(center.x, center.z, center.y + pointSize / 2));
    }

    public Vector3 FindNextPoint(Vector3 mousePos) {
        Debug.Log("find nearest hex to " + mousePos);
        Debug.DrawLine(new Vector3(mousePos.x - pointSize / 2, mousePos.z, mousePos.y), new Vector3(mousePos.x + pointSize / 2, mousePos.z, mousePos.y));
        Debug.DrawLine(new Vector3(mousePos.x, mousePos.z, mousePos.y - pointSize / 2), new Vector3(mousePos.x, mousePos.z, mousePos.y + pointSize / 2));

        float distance = tileWidth;
        Vector3 nearestPoint = new Vector3(0f,0f,0f);
        for (int i = 0; i < points.GetLength(0); ++i) {
            for (int j = 0; j < points.GetLength(1); ++j) {
                //Debug.Log(j);
                float d = Vector3.Distance(points[i, j], mousePos);
                if (d < distance) {
                    distance = d;
                    nearestPoint = points[i, j];
                    Debug.Log("x: " + i + ", y: " + j);
                }
            }
        }
        
        return nearestPoint;
    }
}