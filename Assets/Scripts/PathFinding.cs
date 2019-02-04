    using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding
{
    private static List<Node> orderNodeList = new List<Node>();
    public static Vector2[] SearchPath(Vector2 currentPosition, Vector2 targetPosition)
    {
        targetPosition = Camera.main.ScreenToWorldPoint(targetPosition);
        Cordinat targetCordinat = GridController.m.GetCordinatFromWorldPosition(targetPosition);
        if (!GridController.m.GetGridIsEmpty(targetCordinat))
            return null;
            
        targetPosition = GridController.m.GetGridPositionFromCordinat(targetCordinat);
        GridController.m.ShowTargetIcon(targetPosition);
        orderNodeList.Clear();
        Cordinat cord = GridController.m.GetCordinatFromWorldPosition(currentPosition);
        Node currentNode = new Node(0, Vector2.Distance(currentPosition, targetPosition), currentPosition, cord, null);

        while (true)
        {
            if (currentNode.position == targetPosition)
                break;
            currentNode.isVisited = true;
            currentNode.gridCordinat.GetNeighbors().ForEach(c =>
            {
                Vector2 neighborsNodePosition = GridController.m.GetGridPositionFromCordinat(c);
                Node neighborsNode = new Node(currentNode.needMoveValue + (Vector2.Distance(currentNode.position, neighborsNodePosition) / 2f)
                                , Vector2.Distance(neighborsNodePosition, targetPosition), neighborsNodePosition, c, currentNode);
                AddAndOrder(neighborsNode);
            });

            currentNode = null;

            for (int i = 0; i < orderNodeList.Count; i++)
            {
                if (!orderNodeList[i].isVisited)
                {
                    currentNode = orderNodeList[i];
                    break;
                }
            }
            if (currentNode == null)
                break;

        }

        List<Node> reverseNodes = new List<Node>();

        while (true)
        {
            reverseNodes.Add(currentNode);

            if (currentNode.parent == null)
                break;
            currentNode = currentNode.parent;
        }
        reverseNodes.Reverse();

        Vector2[] finalPosition = new Vector2[reverseNodes.Count];
        for (int i = 0; i < finalPosition.Length; i++)
        {
            finalPosition[i] = reverseNodes[i].position;
        }

        return finalPosition;
    }

    public static void AddAndOrder(Node node)
    {
        if (node == null)
            return;

        if (orderNodeList.Contains(node))
            return;

        int currentIndis = orderNodeList.Count;
        orderNodeList.Add(node);

        while (true)
        {
            if (currentIndis == 0)
                break;

            if (orderNodeList[currentIndis].totalValue <= orderNodeList[currentIndis - 1].totalValue || orderNodeList[currentIndis - 1].isVisited)
            {
                Node tempNode = orderNodeList[currentIndis];
                orderNodeList[currentIndis] = orderNodeList[currentIndis - 1];
                orderNodeList[currentIndis - 1] = tempNode;
                currentIndis--;
            }
            else
            {
                break;
            }

        }
    }

}
public class Node : IEquatable<Node>
{
    public float needMoveValue;
    public float heurosticValue;
    public float totalValue;
    public Vector2 position;
    public Cordinat gridCordinat;
    public bool isVisited;
    public Node parent;

    public Node(float needMoveValue, float heurosticValue, Vector2 position, Cordinat gridCordinat, Node parent)
    {
        this.needMoveValue = needMoveValue;
        this.heurosticValue = heurosticValue;
        this.totalValue = needMoveValue + heurosticValue;
        this.position = position;
        this.gridCordinat = gridCordinat;
        this.parent = parent;
    }


    public bool Equals(Node other)
    {
        if (gridCordinat.x == other.gridCordinat.x && gridCordinat.y == other.gridCordinat.y)
        {
            return true;
        }
        else
            return false;
    }
}
