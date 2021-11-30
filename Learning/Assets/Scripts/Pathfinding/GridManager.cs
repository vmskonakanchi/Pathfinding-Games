using UnityEngine;
using System.Collections.Generic;
using Pathfinding;
using System;

public class GridManager : MonoBehaviour
{
    Node[,] grid;

    [SerializeField] Vector2 gridSize;
    [SerializeField] LayerMask unWalkableMask;
    [SerializeField] float nodeRadius;
    [SerializeField] bool displayPath;

    float nodeDiameter;
    int gridSizeX, gridSizeY;
    public List<Node> pathfindingNodes;
    private void Start()
    {
        CreateGrid();
    }
    private void OnDrawGizmos()
    {
        ShowGrid();
    }
    private void ShowGrid()
    {
        gridSizeX = Mathf.RoundToInt(gridSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridSize.y / nodeDiameter);
        nodeDiameter = nodeRadius * 2f;
        Gizmos.DrawWireCube(transform.position, new Vector3(gridSize.x, 0f, gridSize.y));
        if (grid != null)
        {
            foreach (var node in grid)
            {
                if (pathfindingNodes != null)
                {
                    if (pathfindingNodes.Contains(node))
                    {
                        if (displayPath)
                        {
                            Gizmos.color = Color.white;
                            Gizmos.DrawCube(node.coordinates, Vector3.one * (nodeDiameter - 0.1f));
                        }
                        else
                        {
                            Gizmos.color = Color.black;
                            Gizmos.DrawCube(node.coordinates, Vector3.one * (nodeDiameter - 0.1f));
                        }
                    }
                    else
                    {
                        Gizmos.color = node.isWalkable ? Color.white : Color.red;
                    }
                }


            }
        }
    }
    private void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomleft = transform.position - (Vector3.right * gridSize.x / 2) - (Vector3.forward * gridSize.y / 2);
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomleft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unWalkableMask));
                grid[x, y] = new Node(worldPoint, walkable, x, y);
            }
        }
    }

    public Node GetNodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridSize.x / 2) / gridSize.x;
        float percentY = (worldPosition.z + gridSize.y / 2) / gridSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);
        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return grid[x, y];
    }

    public List<Node> GetNeighBourNodes(Node node)
    {
        List<Node> neighbours = new List<Node>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                {
                    continue;
                }
                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeX)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }
        return neighbours;
    }

    public int maxHeapSize
    {
        get => gridSizeX * gridSizeY;
    }
}