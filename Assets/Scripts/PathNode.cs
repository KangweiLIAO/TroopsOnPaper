using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathNode
{
    private Tilemap map;
    private int x, y;           // The coordinate of this node
    private int[,] grid;        // The array that stores the whole map matrix

    public PathNode(Tilemap map, int x, int y)
    {
        this.x = x;
        this.y = y;
        grid = new int[x,y];
        this.map = map;
    }
}
