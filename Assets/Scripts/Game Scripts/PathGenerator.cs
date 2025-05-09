using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class PathGenerator{
    // Arraylist holding all the cells yet to be searched
    private static Dictionary<(int, int), Cell> openList;
    // Queue holding the frontier cells to be searched
    private static Queue<Cell> searchQueue;
    // Holds references to the start and end node
    private static Cell startNode, endNode;
    // Size of the grid
    private static int size;
    private static int[] cellCosts;

    // Returns the path for the enemies to travel to
    public static List<Cell> GeneratePath(Vector2Int start, Vector2Int end, int side){
        searchQueue = new Queue<Cell>();
        size = side;
        openList = new Dictionary<(int, int), Cell>(size * size);
        PopulateDictionary();
        openList.TryGetValue((start.x, start.y), out startNode);
        openList.TryGetValue((end.x, end.y), out endNode);
        startNode.distanceToStart = 0;
        searchQueue.Enqueue(startNode);

        Debug.Assert(startNode != null);
        Debug.Assert(endNode != null);

        Cell currentNode = null;
        while (currentNode != endNode){
            if (searchQueue.TryDequeue(out currentNode)){
                GetFrontierNodes(currentNode);
                searchQueue = new Queue<Cell>(searchQueue.OrderBy(z => z.totalCost));
            } else throw new System.Exception("Error with generating path");
        }

        List<Cell> path = new List<Cell>();
        currentNode = endNode;
        while (currentNode != startNode){
            path.Add(currentNode);
            currentNode = currentNode.previousCell;
        }
        path.Reverse();
        return path;
    }

    // Fills the hashset with cells
    private static void PopulateDictionary(){
        for (int y = 0; y < size; y++){
            for (int x = 0; x < size; x++){
                openList.Add((x, y), new Cell(x, y));
            }
        }    
    }

    private static void GetFrontierNodes(Cell cell){
        Cell neighbor;
        if (openList.TryGetValue((cell.x, cell.y + 1), out neighbor)){
            neighbor.previousCell = cell;
            neighbor.CalculateExactDistance(startNode);
            neighbor.CalculateManhattanDistance(endNode);
            searchQueue.Enqueue(neighbor);
            openList.Remove((neighbor.x, neighbor.y));
        }
        if (openList.TryGetValue((cell.x + 1, cell.y), out neighbor)){
            neighbor.previousCell = cell;
            neighbor.CalculateExactDistance(startNode);
            neighbor.CalculateManhattanDistance(endNode);
            searchQueue.Enqueue(neighbor);
            openList.Remove((neighbor.x, neighbor.y));
        }
        if (openList.TryGetValue((cell.x, cell.y - 1), out neighbor)){
            neighbor.previousCell = cell;
            neighbor.CalculateExactDistance(startNode);
            neighbor.CalculateManhattanDistance(endNode);
            searchQueue.Enqueue(neighbor);
            openList.Remove((neighbor.x, neighbor.y));
        }
        if (openList.TryGetValue((cell.x - 1, cell.y), out neighbor)){
            neighbor.previousCell = cell;
            neighbor.CalculateExactDistance(startNode);
            neighbor.CalculateManhattanDistance(endNode);
            searchQueue.Enqueue(neighbor);
            openList.Remove((neighbor.x, neighbor.y));
        }
    }


    //public static List<Tile> GeneratePath(Tile start, Tile end, ref Tile[] tiles, int size){
    //    List<Tile> path = new List<Tile>(1000);
    //    Vector2Int currentPosition = start.position;
    //    while (currentPosition != end.position){
    //        path.Add(tiles[TileManager.PositionToIndex(currentPosition, size)]);
    //        Vector2Int directionVector = Vector2Int.zero;
    //        int newTileIndex = -1;
    //        while (newTileIndex > (size * size) && newTileIndex < 0){
    //            int direction = Random.Range(0, 4);
    //            switch(direction){
    //                case 0:
    //                    directionVector = new Vector2Int(0, 1);
    //                    break;
    //                case 1:
    //                    directionVector = new Vector2Int(1, 0);
    //                    break;
    //                case 2:
    //                    directionVector = new Vector2Int(0, -1);
    //                    break;
    //                case 3:
    //                    directionVector = new Vector2Int(-1, 0);
    //                    break;
    //                default: throw new System.Exception("How is this possible");
    //            }
    //            newTileIndex = TileManager.PositionToIndex(currentPosition + directionVector, size);
    //        }
    //        currentPosition += directionVector;
    //    }
    //    return path;
    //}
}

public class Cell{
    // Stores the location of the cell in the board
    public int x, y;

    // Holds the cell that led up to the current cell
    public Cell previousCell;

    // Total cost for a cell
    public float totalCost => distanceToStart + distanceToEnd;

    // Holds the distance to start node and end node to calculate the cost
    public float distanceToStart, distanceToEnd;

    public Cell(int x, int y){
        this.x = x;
        this.y = y;
    }

    public Cell(Vector2Int position){
        x = position.x;
        y = position.y;
    }

    // Method to calculate the exact distance from one cell to another
    public void CalculateExactDistance(Cell cell){
        distanceToStart = Vector2Int.Distance(new Vector2Int(x, y), new Vector2Int(cell.x, cell.y));
    }
    
    // Method to calculate the manhattanDistance from one cell to another
    public void CalculateManhattanDistance(Cell cell){
        distanceToEnd = Mathf.Abs(cell.x - x) + Mathf.Abs(cell.y - y);
    }
}