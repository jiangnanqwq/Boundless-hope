
using System.Collections.Generic;
using UnityEngine;

public class MapManagement : MonoBehaviour
{
    public static List<GameObject> Sheeps;
    public Quad tree;
    private void Start()
    {
        //tree = new(new(0, 10), new(10, 0));
        //tree.Insert(new Node(new Point(1, 2), 45));
        //print(tree.Search(new(1, 2)));
        //print(GameObject.FindGameObjectWithTag("Resource").name);

        Sheeps = new(GameObject.FindGameObjectsWithTag("Sheep"));
    }
}
public struct Point
{
    public float x;
    public float y;
    public Point(float _x, float _y)
    {
        x = _x;
        y = _y;
    }

};
public class Node
{
    public Point pos;
    public int data;
    public Node(Point _pos, int _data)
    {
        pos = _pos;
        data = _data;
    }
    public override string ToString()
    {
        return "current grid data is: " + data;
    }
};
public class Quad
{
    public Point topLeft;
    public Point botRight;

    public Node n;

    // 子结点
    public Quad topLeftTree;
    public Quad topRightTree;
    public Quad botLeftTree;
    public Quad botRightTree;

    public Quad()
    {
        topLeft = new Point(0, 0);
        botRight = new Point(0, 0);
        n = null;
        topLeftTree = null;
        topRightTree = null;
        botLeftTree = null;
        botRightTree = null;
    }
    public Quad(Point topL, Point botR)
    {
        n = null;
        topLeftTree = null;
        topRightTree = null;
        botLeftTree = null;
        botRightTree = null;
        topLeft = topL;
        botRight = botR;
    }
    //插入
    public void Insert(Node node)
    {
        if (node == null)
            return;

        if (!InBoundary(node.pos))
            return;

        if (Mathf.Abs(topLeft.x - botRight.x) <= 1 &&
            Mathf.Abs(topLeft.y - botRight.y) <= 1)
        {
            //覆盖或者跳过
            n ??= node;
            return;
        }

        if ((topLeft.x + botRight.x) / 2 >= node.pos.x)
        {
            // Indicates botLeftTree
            if ((topLeft.y + botRight.y) / 2 >= node.pos.y)
            {
                botLeftTree ??= new Quad(
                        new Point(topLeft.x, (topLeft.y + botRight.y) / 2),
                        new Point((topLeft.x + botRight.x) / 2, botRight.y)
                        );
                botLeftTree.Insert(node);
            }

            // Indicates topLeftTree
            else
            {
                topLeftTree ??= new Quad(
                    new Point(topLeft.x, topLeft.y),
                    new Point((topLeft.x + botRight.x) / 2, (topLeft.y + botRight.y) / 2)
                    );
                topLeftTree.Insert(node);
            }
        }
        else
        {
            // Indicates botRightTree
            if ((topLeft.y + botRight.y) / 2 >= node.pos.y)
            {
                botRightTree ??= new Quad(
                        new Point((topLeft.x + botRight.x) / 2, (topLeft.y + botRight.y) / 2),
                        new Point(botRight.x, botRight.y)
                        );
                botRightTree.Insert(node);
            }

            // Indicates topRightTree
            else
            {
                topRightTree ??= new Quad(
                        new Point((topLeft.x + botRight.x) / 2, topLeft.y),
                        new Point(botRight.x, (topLeft.y + botRight.y) / 2)
                        );
                topRightTree.Insert(node);
            }
        }
    }
    //搜索
    public Node Search(Point p)
    {
        if (!InBoundary(p))
            return null;

        if (n != null)
            return n;

        if ((topLeft.x + botRight.x) / 2 >= p.x)
        {
            // Indicates botLeftTree
            if ((topLeft.y + botRight.y) / 2 >= p.y)
            {
                if (botLeftTree == null)
                    return null;
                return botLeftTree.Search(p);
            }

            // Indicates topLeftTree
            else
            {
                if (topLeftTree == null)
                    return null;
                return topLeftTree.Search(p);
            }
        }
        else
        {
            // Indicates botRightTree
            if ((topLeft.y + botRight.y) / 2 >= p.y)
            {
                if (topRightTree == null)
                    return null;
                return topRightTree.Search(p);
            }

            // Indicates topRightTree
            else
            {
                if (botRightTree == null)
                    return null;
                return botRightTree.Search(p);
            }
        }
    }
    //碰撞
    public bool InBoundary(Point p)
    {
        return (p.x >= topLeft.x &&
            p.x <= botRight.x &&
            p.y >= botRight.y &&
            p.y <= topLeft.y);
    }
};