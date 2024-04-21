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
    
    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }
    
    public void SetGCost(int gCost)
    { 
        this.gCost = gCost;
    }
    
    public void SetHCost(int hCost)
    { 
        this.hCost = hCost;
    }
    
    public void CalculateFCost()
    { 
        fCost = hCost + gCost;
    }

    public void ResetCameFromPathNode()
    {
        cameFromPathNode = null;
    }

    public void SetCameFromPathNode(PathNode pathNode)
    {
        cameFromPathNode = pathNode;
    }
    
    public PathNode GetCameFromPathNode()
    {
        return cameFromPathNode;
    }
}
