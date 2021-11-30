using System.Diagnostics;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Astar : MonoBehaviour
{
    GridManager grid;
    [SerializeField] Transform seeker, target;
    private void Start()
    {
        grid = GetComponent<GridManager>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FindPath(seeker.position, target.position);
        }
    }
    void FindPath(Vector3 startPos, Vector3 endPos)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        Node startnode = grid.GetNodeFromWorldPoint(startPos);
        Node targetNode = grid.GetNodeFromWorldPoint(endPos);

        Heap<Node> OpenSet = new Heap<Node>(grid.maxHeapSize);

        HashSet<Node> ClosedSet = new HashSet<Node>();

        OpenSet.Add(startnode);
        while (OpenSet.Count > 0)
        {
            Node currentnode = OpenSet.RemoveFirstItem();

            #region If you want to see the performance difference b/w LIST and HEAP then open this region uncomment below line and change currentnode to first element in OpenSet

            //for (int i = 1; i < OpenSet.Count; i++)
            //{
            //    if (OpenSet[i].f_cost < currentnode.f_cost || OpenSet[i].f_cost == currentnode.f_cost && OpenSet[i].h_cost < currentnode.h_cost)
            //    {
            //        currentnode = OpenSet[i];
            //    }
            //}
            //OpenSet.Remove(currentnode);

            #endregion

            ClosedSet.Add(currentnode);
            if (currentnode == targetNode)
            {
                stopwatch.Stop();
                print("The Time elapsed is " + stopwatch.ElapsedMilliseconds);
                RetracePath(startnode, targetNode);
                return;
            }
            foreach (var neighbour in grid.GetNeighBourNodes(currentnode))
            {
                if (!neighbour.isWalkable || ClosedSet.Contains(neighbour))
                {
                    continue;
                }

                int newMovementCosttoneighbour = currentnode.g_cost + GetDistance(currentnode, neighbour);

                if (newMovementCosttoneighbour < neighbour.g_cost || !OpenSet.Contains(neighbour))
                {
                    neighbour.g_cost = newMovementCosttoneighbour;
                    neighbour.h_cost = GetDistance(neighbour, targetNode);
                    neighbour.parent = currentnode;
                    if (!OpenSet.Contains(neighbour))
                    {
                        OpenSet.Add(neighbour);
                    }
                }
            }
        }
    }

    void RetracePath(Node startnode, Node endnode)
    {
        List<Node> path = new List<Node>();
        Node currentnode = endnode;
        while (currentnode != startnode)
        {
            path.Add(currentnode);
            currentnode = currentnode.parent;
        }
        path.Reverse();
        grid.pathfindingNodes = path;
    }

    int GetDistance(Node a, Node b)
    {
        int distX = Mathf.Abs(a.gridX - b.gridX);
        int distY = Mathf.Abs(a.gridY - b.gridY);
        if (distX > distY)
        {
            return (14 * distY) + (10 * (distX - distY));
        }
        else
        {
            return (14 * distX) + (10 * (distY - distX));
        }
    }
}
