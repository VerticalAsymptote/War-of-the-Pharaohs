using System.Collections.Generic;
using UnityEngine;


// TODO: Fix Path Generator (using debugger)
public static class PathGenerator{
    public static List<Tile> GeneratePath(Tile start, Tile end, ref Tile[] tiles, int size){
        List<Tile> path = new List<Tile>(1000);
        Vector2Int currentPosition = start.position;
        while (currentPosition != end.position){
            path.Add(tiles[TileManager.PositionToIndex(currentPosition, size)]);
            Vector2Int directionVector = Vector2Int.zero;
            int newTileIndex = -1;
            while (newTileIndex > (size * size) && newTileIndex < 0){
                int direction = Random.Range(0, 4);
                switch(direction){
                    case 0:
                        directionVector = new Vector2Int(0, 1);
                        break;
                    case 1:
                        directionVector = new Vector2Int(1, 0);
                        break;
                    case 2:
                        directionVector = new Vector2Int(0, -1);
                        break;
                    case 3:
                        directionVector = new Vector2Int(-1, 0);
                        break;
                    default: throw new System.Exception("How is this possible");
                }
                newTileIndex = TileManager.PositionToIndex(currentPosition + directionVector, size);
            }
            currentPosition += directionVector;
        }
        return path;
    }
}