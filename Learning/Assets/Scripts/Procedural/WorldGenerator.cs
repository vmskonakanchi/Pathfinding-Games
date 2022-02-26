using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    [SerializeField] private GameObject cubeprefab;
    [SerializeField, Range(0, 500)] private int worldSizeX;
    [SerializeField, Range(0, 500)] private int worldSizeY;
    [SerializeField, Range(0, 10)] private int offset;
    [SerializeField, Range(0, 30)] private float noiseHeight;
    [SerializeField, Range(0, 50)] private float levelOfDetail;
    [SerializeField, Range(0, 256)] private float mapOffsetX;
    [SerializeField, Range(0, 256)] private float mapOffsetY;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        GenerateWorld();
    }


    private void GenerateWorld()
    {
        if (cubeprefab == null) return;
        for (int x = 0; x < worldSizeX; x++)
        {
            for (int y = 0; y < worldSizeY; y++)
            {
                Vector3 centre = new Vector3(x,
                    GenerateNoise(x, y, levelOfDetail) * noiseHeight,
                    y) * offset;
                Gizmos.DrawCube(centre, Vector3.one);
            }
        }
    }


    private float GenerateNoise(int x, int y, float detailScale)
    {
        float xNoise = (x + mapOffsetX) / detailScale;
        float yNoise = (y + mapOffsetY) / detailScale;

        return Mathf.PerlinNoise(xNoise, yNoise);
    }
}
