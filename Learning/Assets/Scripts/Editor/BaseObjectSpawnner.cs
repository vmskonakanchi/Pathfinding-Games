using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class BaseObjectSpawnner : EditorWindow
{
    private string objectName = "";
    private int objectId = 1;
    private GameObject objectToSpawn;
    private float objectScale = 0.5f;
    private float spawnRadius = 5f;
    private List<GameObject> spawnList = new List<GameObject>();

    [MenuItem("Tools/Basic Object Spawnner")]
    public static void ShowWindow()
    {
        GetWindow(typeof(BaseObjectSpawnner));
    }

    private void OnGUI()
    {
        GUILayout.Label("Spawn New Object", EditorStyles.boldLabel);

        objectName = EditorGUILayout.TextField("Object Name", objectName);
        objectId = EditorGUILayout.IntField("Object id ", objectId);
        objectScale = EditorGUILayout.Slider("Spawn Radius", objectScale, 0.5f, 3f);
        objectToSpawn = EditorGUILayout.ObjectField("Object To Spawn", objectToSpawn, typeof(GameObject), true) as GameObject;

        if (GUILayout.Button("Spawn Obejct"))
        {
            SpawnObject();
        }
        if (GUILayout.Button("Remove All Objects"))
        {
            RemoveAllObjects();
        }
    }

    private void RemoveAllObjects()
    {
        if (spawnList.Count == 0)
        {
            Debug.LogError("There Are no objects to destroy");
        }
        if (spawnList.Count > 0)
        {
            foreach (GameObject obj in spawnList)
            {
                DestroyImmediate(obj);
            }
        }
    }

    private void SpawnObject()
    {
        //spawn objects
        if (objectToSpawn == null)
        {
            Debug.LogError("Error: There is no Object to spawn Please Select object to spawn");
            return;
        }
        if (objectName == string.Empty)
        {
            Debug.LogError("Error : The Object name cannot be empty");
            return;
        }

        GameObject gameObject = Instantiate(objectToSpawn);
        gameObject.name = objectName + objectId;
        spawnList.Add(gameObject);
        objectId++;
    }
}
