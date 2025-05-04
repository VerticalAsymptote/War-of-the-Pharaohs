using UnityEngine;

public struct Tile{
    // Position of the cell relative to its parent
    public Vector2Int position;
    // Tile state of the cell
    public TileState tileState;
    // If not null, then holds the tower data beloning to the tile
    public Tower tower;

    // Basic contructor for a tile
    public Tile(Vector2Int pos){
        position = pos;
        tileState = TileState.Empty;
        tower = null;
    }
    
    public Tile(Vector2Int pos, TileState state){
        position = pos;
        tileState = state;
        tower = null;
    }
}

// Holds the current tile's state, which would determine the behavior of the cell
public enum TileState{
    Empty, Spawn, Destination, Path, Walls, Occupied
}