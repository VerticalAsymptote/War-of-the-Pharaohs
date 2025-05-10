using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameManager))]
public class TileManager : MonoBehaviour{

    // Holds all the tiles for the game
    [HideInInspector]
    public Tile[] tiles;

    [SerializeField, Tooltip("Prefab of the spawn tiles in the game")]
    private GameObject spawnPrefab;

    [SerializeField, Tooltip("Prefab of the destination tiles in the game")]
    private GameObject destPrefab;

    [SerializeField, Tooltip("Prefab of the placeable tiles in the game")]
    private GameObject tilePrefab;

    [SerializeField, Tooltip("Prefab of the path tiles in the game")]
    private GameObject pathPrefab;

    [SerializeField, Tooltip("Reference to the parent containing the game board")]
    private Transform board;

    // Handles the game logic
    private GameManager gameManager;

    void Start(){
        gameManager = GetComponent<GameManager>();
    }

    // Initializes the tiles and places them in the world
    public void InitializeTiles(Vector2Int start, Vector2Int end){
        // Populates the reference to gameManager
        gameManager = GetComponent<GameManager>();

        // Places the tiles in the world
        tiles = new Tile[gameManager.size * gameManager.size];
        
        // Generates a random path from start to end, then instantiates path prefabs
        List<Cell> pathNodes = PathGenerator.GeneratePath(start, end, gameManager.size);
        foreach (Cell node in pathNodes){
            GameObject obj = Instantiate(pathPrefab, new Vector3(node.x, 0f, node.y), Quaternion.identity, board);
            tiles[PositionToIndex(node.x, node.y, gameManager.size)] = new Tile(new Vector2Int(node.x, node.y), TileState.Path, obj);
        }
        GameObject temp = Instantiate(spawnPrefab, new Vector3(start.x, 0f, start.y), Quaternion.identity, board);
        tiles[PositionToIndex(start, gameManager.size)] = new Tile(start, TileState.Spawn, temp);
        temp = Instantiate(destPrefab, new Vector3(end.x, 0f, end.y), Quaternion.identity, board);
        tiles[PositionToIndex(end, gameManager.size)] = new Tile(end, TileState.Destination, temp);

        // Places tiles in the remaining spaces
        for (int y = 0; y < gameManager.size; y++){
            for (int x = 0; x < gameManager.size; x++){
                if (tiles[PositionToIndex(x, y, gameManager.size)].tileState == TileState.Empty){
                    GameObject obj = Instantiate(tilePrefab, new Vector3(x, 0f, y), Quaternion.identity, board);
                    tiles[PositionToIndex(x, y, gameManager.size)] = new Tile(new Vector2Int(x, y), obj);
                }
            }
        }

        // Adjusts after tile instantiations to allow for camera to be centered
        board.transform.position += new Vector3(0.5f, 0f, 0.5f);
    }

    // Given a tile, places a tower on the tile
    public void PlaceTower(ref Tile tile, Tower tower){
        if (tile.tower != null)
            return;
        GameObject obj = Instantiate(tower.towerPrefab);
        obj.transform.parent = tile.tileObject.transform;
        obj.transform.localPosition = Vector3.zero;
        tile.tower = tower;
    }

    // Given a tile, removes the tower on that tile
    public void RemoveTower(ref Tile tile){

    }

    // Following methods convert easily from position information to index of the tiles array
    public static int PositionToIndex(Vector2Int position, int size) => position.y * size + position.x;
    public static int PositionToIndex(Vector3 position, int size) => (int)position.z * size + (int)position.x;
    public static int PositionToIndex(int x, int y, int size) => y * size + x;
}
