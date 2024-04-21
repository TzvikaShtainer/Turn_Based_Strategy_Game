using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    private GridPosition gridPosition;

    private int gCost; //walking cost from the start node
    private int hCost; //heuristic cost to reach end node
    private int fCost; //G + H

    private PathNode cameFromPathNode;
    
    public PathNode(GridPosition gridPosition)
    {
        this.gridPosition = gridPosition;
    }
    
    public override string ToString()
    {
        return gridPosition.ToString();
    }

    public int GetGCost()
    {
        return gCost;
    }
    
    public int GetHCost()
    {
        return hCost;
    }
    
    public int GetFCost()
    {
        return fCost;
    }
    
}
