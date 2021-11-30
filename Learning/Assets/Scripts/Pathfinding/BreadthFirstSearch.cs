//using System.Collections.Generic;
//using UnityEngine;
//using Pathfinding;

//public class BreadthFirstSearch : MonoBehaviour
//{
//    private Transform target;
//    private Vector3Int[] directions = { Vector3Int.right, Vector3Int.left, Vector3Int.forward, Vector3Int.back };

//    private Dictionary<Vector3Int, Node> actualNodes;

//    private Dictionary<Vector3Int, Node> reached = new Dictionary<Vector3Int, Node>();

//    private Queue<Node> frontier = new Queue<Node>();

//    private Node currentSearchNode;
//    private Node startNode;
//    private Node endNode;

//    private Vector3Int startPos;
//    private Vector3Int endPos;

//    private void Start()
//    {
//        actualNodes = FindObjectOfType<GridManager>().nodes;
//        target = GameObject.FindGameObjectWithTag("Target").transform;

//        startPos = new Vector3Int(0, 0, 0);
//        endPos = new Vector3Int((int)target.position.x, 0, (int)target.position.z);

//        currentSearchNode = new Node(startPos, true, false, false);
//        startNode = new Node(startPos, true, false, false);
//        endNode = new Node(endPos, true, false, false);

//        BFS();
//        BuildPath();
//    }
//    private void ExploreNeighBours()
//    {
//        List<Node> neighbours = new List<Node>();
//        foreach (var dir in directions)
//        {
//            Vector3Int _neighbourPos = currentSearchNode.coordinates + dir;
//            if (actualNodes.ContainsKey(_neighbourPos))
//            {
//                neighbours.Add(actualNodes[_neighbourPos]);
//            }
//        }
//        foreach (var neighbour in neighbours)
//        {
//            if (!reached.ContainsKey(neighbour.coordinates) && neighbour.isWalkable)
//            {
//                neighbour.connectedTo = currentSearchNode;
//                reached.Add(neighbour.coordinates, neighbour);
//                frontier.Enqueue(neighbour);
//            }
//        }
//    }

//    private void BFS()
//    {
//        bool isRunning = true;
//        frontier.Enqueue(startNode);
//        reached.Add(startPos, startNode);
//        while (frontier.Count > 0 && isRunning)
//        {
//            currentSearchNode = frontier.Dequeue();
//            currentSearchNode.isExplored = true;
//            ExploreNeighBours();
//            if (currentSearchNode.coordinates == endNode.coordinates)
//            {
//                isRunning = false;
//            }
//        }
//    }

//    private List<Node> BuildPath()
//    {
//        List<Node> path = new List<Node>();
//        Node currentNode = endNode;
//        path.Add(currentNode);
//        currentNode.isPath = true;

//        while (currentNode.connectedTo != null)
//        {
//            currentNode = currentNode.connectedTo;
//            path.Add(currentNode);
//            currentNode.isPath = true;
//        }
//        path.Reverse();
//        return path;
//    }
//}
