using UnityEditor;
using UnityEngine;

public class LevelGenerator : EditorWindow
{
    private int gridSize = 10;
    private GameObject tilePrefab;
    private Transform gridParent;
    private int sectionSpacing = 10;
    private GameObject[,] gridTiles;
    

    [MenuItem("Tools/Level Generator")]
    public static void ShowWindow()
    {
        GetWindow<LevelGenerator>("Level Generator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Grid Settings", EditorStyles.boldLabel);
        gridSize = EditorGUILayout.IntField("Grid Size", gridSize);
        GUILayout.Space(sectionSpacing);

        GUILayout.Label("Title Prefab", EditorStyles.boldLabel);
        tilePrefab = (GameObject)EditorGUILayout.ObjectField("Title Prefab", tilePrefab, typeof(GameObject), false);

        GUILayout.Label("Grid Parent", EditorStyles.boldLabel);
        gridParent = (Transform)EditorGUILayout.ObjectField("Grid Parent", gridParent, typeof(Transform), true);
        GUILayout.Space(sectionSpacing);

        if (GUILayout.Button("Generate Grid"))
        {
            GenerateGrid();
        }

        if(GUILayout.Button("Delete Previous Grid"))
        {
            ClearGrid();
        }
    }

    /// <summary>
    /// Upon clicking on "Generate Grid" it will make a Grid base on gridSize.
    /// </summary>
    private void GenerateGrid()
    {
        if(tilePrefab == null)
        {
            Debug.Log("Tile Prefab is not assigned");
            return;
        }

        gridTiles = new GameObject[gridSize, gridSize];
        for(int x = 0; x < gridSize; x++)
        {
            for(int z = 0; z < gridSize; z++)
            {
                Vector3 position = new Vector3(x, 0, z);
                gridTiles[x, z] = (GameObject)PrefabUtility.InstantiatePrefab(tilePrefab, gridParent);
                gridTiles[x, z].transform.position = position;
            }
        }
    }

    /// <summary>
    /// Upon clicking on "Delete Previous Grid" it will delete the grid
    /// </summary>
    private void ClearGrid()
    {
         for (int x = 0; x < gridSize; x++)
         {
             for (int z = 0; z < gridSize; z++)
             {
                 DestroyImmediate(gridTiles[x, z]);
             }
         }
    }

    //TODO: Make a Function that randomly generate an array that 
}
