
using UnityEngine;
namespace Pathfinding
{
    public class Node : IHeapItem<Node>
    {
        public Vector3 coordinates;
        public bool isWalkable;
        public int g_cost;
        public int h_cost;
        public int gridX;
        public int gridY;
        public Node parent;
        int heapIndex;
        public int f_cost => g_cost + h_cost;


        public Node(Vector3 _coordinates, bool _isWalkable, int _gridX, int _gridY)
        {
            coordinates = _coordinates;
            isWalkable = _isWalkable;
            gridX = _gridX;
            gridY = _gridY;
        }

        public int CompareTo(Node othernodetoCompare)
        {
            int compare = f_cost.CompareTo(othernodetoCompare.f_cost);
            if (compare == 0)
            {
                compare = h_cost.CompareTo(othernodetoCompare.h_cost);
            }
            return -compare;
        }
        public int HeapIndex { get => heapIndex; set => heapIndex = value; }
    }
}