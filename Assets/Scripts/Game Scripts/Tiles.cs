using UnityEngine;

public struct Tiles{
    // Position of the cell relative to its parent
    public Vector2Int position;
    // Tile state of the cell
    public TileState tileState;
    public Tower tower;
}

// Holds the current tile's state, which would determine the behavior of the cell
public enum TileState{
    Empty, Spawn, Destination, Path, Walls, Occupied
}