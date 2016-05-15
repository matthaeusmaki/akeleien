using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(HexGrid))]
public class HexGridEditor : Editor {

    private const char ADD = 'a';
    private const char REMOVE = 'd';

    private int selectedPrefab = 0;

    private List<GameObject> prefabs = new List<GameObject>();

    private string prefabPath = "Assets/Scripts/levelGeneration";

    HexGrid grid;

    public void OnEnable() {
        grid = (HexGrid)target;
        SceneView.onSceneGUIDelegate += GridUpdate;
        updatePrefabslist();
    }

    void GridUpdate(SceneView sceneView) {
        Event e = Event.current;
        Ray r = Camera.current.ScreenPointToRay(new Vector3(e.mousePosition.x, -e.mousePosition.y + Camera.current.pixelHeight));
        Vector3 mousePos = r.origin;

        if (e.isKey) {
            if (e.character == ADD) {
                Debug.Log("Add Tile");
                Vector3 prefabPos = grid.FindNextPoint(new Vector3(mousePos.x, 0.0f, mousePos.z));
                GameObject obj;
                Object prefab = prefabs[selectedPrefab]; //PrefabUtility.GetPrefabParent(Selection.activeObject);
                if (prefab) {
                    obj = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
                    obj.transform.position = new Vector3(prefabPos.x, 0.0f, prefabPos.z); // new Vector3(mousePos.x, 0.0f, mousePos.z);

                    obj.transform.localScale = new Vector3(grid.TileWidth * (obj.transform.localScale.x / grid.TileWidth), obj.transform.localScale.y, obj.transform.localScale.z);
                }
            } else if (e.character == REMOVE) {
                Debug.Log("Remove Tile");
            }
        }
    }

    private void updatePrefabslist() {
        prefabs.Clear();
        string[] search_results = System.IO.Directory.GetFiles(prefabPath, "*.prefab", System.IO.SearchOption.AllDirectories);
        for (int i = 0; i < search_results.Length; i++) {
            Object prefab = null;
            prefab = AssetDatabase.LoadAssetAtPath(search_results[i], typeof(GameObject));
            prefabs.Add(prefab as GameObject);
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
        GUILayout.Label("Point Size");
        grid.pointSize = Mathf.Max(1, EditorGUILayout.FloatField(grid.pointSize, GUILayout.Width(150)));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Grid Color");
        grid.color = EditorGUILayout.ColorField(grid.color, GUILayout.Width(150));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Path to Tiles");
        prefabPath = GUILayout.TextField(prefabPath);
        if (GUILayout.Button("Update")) {
            updatePrefabslist();
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUIContent[] content = new GUIContent[prefabs.Count];
        for (int i = 0; i < prefabs.Count; ++i) {
            content[i] = new GUIContent(prefabs[i].name);
        }
        selectedPrefab = GUILayout.SelectionGrid(selectedPrefab, content, 1);
        GUILayout.EndHorizontal();

        SceneView.RepaintAll();
    }
}
