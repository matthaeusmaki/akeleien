using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(HexGrid))]
public class HexGridEditor : Editor {

    private const char ADD = 'a';
    private const char REMOVE = 'd';

    HexGrid grid;

    public void OnEnable() {
        grid = (HexGrid)target;
        SceneView.onSceneGUIDelegate += GridUpdate;
    }

    void GridUpdate(SceneView sceneView) {
        Event e = Event.current;
        Ray r = Camera.current.ScreenPointToRay(new Vector3(e.mousePosition.x, -e.mousePosition.y + Camera.current.pixelHeight));
        Vector3 mousePos = r.origin;

        if (e.isKey) {
            if (e.character == ADD) {
                Debug.Log("Add Tile");
                //GameObject obj;
                //Object prefab = PrefabUtility.GetPrefabParent(Selection.activeObject);
                //if (prefab) {
                //    obj = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
                //    obj.transform.position = new Vector3(mousePos.x, 0.0f, mousePos.z);

                //    obj.transform.localScale = new Vector3(grid.tileWidth * (obj.transform.localScale.x / grid.tileWidth), obj.transform.localScale.y, obj.transform.localScale.z);
                //}
            } else if (e.character == REMOVE) {
                Debug.Log("Remove Tile");
            }
        }
    }

    public override void OnInspectorGUI() {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Size");
        grid.Size = Mathf.Max(0.001f, EditorGUILayout.FloatField(grid.Size, GUILayout.Width(150)));
        GUILayout.EndHorizontal();
        
        GUILayout.BeginHorizontal();
        GUILayout.Label("Offset");
        grid.offset= Mathf.Max(0, EditorGUILayout.FloatField(grid.offset, GUILayout.Width(150)));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Rows");
        grid.rows = Mathf.Max(1, EditorGUILayout.IntField(grid.rows, GUILayout.Width(150)));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Columns");
        grid.columns = Mathf.Max(1, EditorGUILayout.IntField(grid.columns, GUILayout.Width(150)));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Grid Color");
        grid.color = EditorGUILayout.ColorField(grid.color, GUILayout.Width(150));
        GUILayout.EndHorizontal();

        SceneView.RepaintAll();
    }
}
