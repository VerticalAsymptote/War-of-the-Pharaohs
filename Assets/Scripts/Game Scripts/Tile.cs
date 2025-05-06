using UnityEngine;

public struct Tile{
    // Position of the cell relative to its parent
    public Vector2Int position;
    // Tile state of the cell
    public TileState tileState;
    // If not null, then holds the tower data beloning to the tile
    public Tower tower;
    // Holds the reference to the tile game object
    public GameObject tileObject;

    // Basic contructor for a tile
    public Tile(Vector2Int pos, GameObject obj){
        position = pos;
        tileState = TileState.Empty;
        tower = null;
        tileObject = obj;
    }
    
    public Tile(Vector2Int pos, TileState state, GameObject obj){
        position = pos;
        tileState = state;
        tower = null;
        tileObject = obj;
    }
}

// Holds the current tile's state, which would determine the behavior of the cell
public enum TileState{
    Empty, Spawn, Destination, Path, Walls, Occupied
}