using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameManager))]
public class TileManager : MonoBehaviour{

    // Holds all the tiles for the game
    [HideInInspector]
    public Tile[] tiles;

    [HideInInspector]
    public LinkedList<Cell> pathNodes;

    [SerializeField, Tooltip("Reference to the parent containing the game board")]
    private Transform board;

    [SerializeField, Tooltip("Prefab of the spawn tiles in the game")]
    private GameObject spawnPrefab;

    [SerializeField, Tooltip("Prefab of the destination tiles in the game")]
    private GameObject destPrefab;

    [SerializeField, Tooltip("Prefab of the placeable tiles in the game")]
    private GameObject tilePrefab;

    [SerializeField, Tooltip("Prefab of the path tiles in the game")]
    private GameObject pathPrefab;

    // Handles the game logic
    private GameManager gameManager;
    
    // Reference to the currently selected tile
    private GameObject selectedTile;

    void Start(){
        // Populates the reference to gameManager
        gameManager = GetComponent<GameManager>();
    
        // Initializes tiles array
        tiles = new Tile[gameManager.size * gameManager.size];
    }

    void Update(){
        // Handles getting the gameobject clicked on by mouse, and the visualization of selected tile
        if (Input.GetKeyDown(KeyCode.Mouse0)){
            GameObject tile = GetClickedGameObject();
            if (tile != null && selectedTile == null){
                selectedTile = tile;
                tile.transform.position += new Vector3(0f, 0.5f, 0f);
            } else if (selectedTile != null && selectedTile == tile){
                selectedTile = null;
                tile.transform.position -= new Vector3(0f, 0.5f, 0f);
            }
        }
        // Alternate method of deselecting tile
        if (selectedTile != null && Input.GetKeyDown(KeyCode.Escape)){
            selectedTile.transform.position -= new Vector3(0f, 0.5f, 0f);
            selectedTile = null;
        } else if (selectedTile != null && Input.GetKeyDown(KeyCode.B)){
            Tile tile = tiles[PositionToIndex(selectedTile.transform.localPosition, gameManager.size)];
            gameManager.towerManager.PlaceTower(ref tile);
            selectedTile.transform.position -= new Vector3(0f, 0.5f, 0f);
            selectedTile = null;
        }
    }

    // Initializes the tiles and places them in the world
    public void InitializeTiles(Vector2Int start, Vector2Int end){
        // Generates a random path from start to end, then instantiates path, spawn, and destination prefabs
        pathNodes = PathGenerator.GeneratePath(start, end, gameManager.size);
        foreach (Cell node in pathNodes)
        {
            GameObject obj = Instantiate(pathPrefab, new Vector3(node.x, 0f, node.y), Quaternion.identity, board);
            tiles[PositionToIndex(node.x, node.y, gameManager.size)] = new Tile(new Vector2Int(node.x, node.y), TileState.Path, obj);
        }
        GameObject temp = Instantiate(spawnPrefab, new Vector3(start.x, 0f, start.y), Quaternion.identity, board);
        tiles[PositionToIndex(start, gameManager.size)] = new Tile(start, TileState.Spawn, temp);
        temp = Instantiate(destPrefab, new Vector3(end.x, 0f, end.y), Quaternion.identity, board);
        tiles[PositionToIndex(end, gameManager.size)] = new Tile(end, TileState.Destination, temp);
        temp.AddComponent<BaseTile>();

        // Places tiles in the remaining spaces
        for (int y = 0; y < gameManager.size; y++)
        {
            for (int x = 0; x < gameManager.size; x++)
            {
                if (tiles[PositionToIndex(x, y, gameManager.size)].tileState == TileState.Empty)
                {
                    GameObject obj = Instantiate(tilePrefab, new Vector3(x, 0f, y), Quaternion.identity, board);
                    tiles[PositionToIndex(x, y, gameManager.size)] = new Tile(new Vector2Int(x, y), obj);
                }
            }
        }

        // Adjusts after tile instantiations to allow for camera to be centered
        board.transform.position += new Vector3(0.5f, 0f, 0.5f);
    }

    // Given a tile, removes the tower on that tile
    public void RemoveTower(ref Tile tile){

    }


//===========================================Helper Methods===========================================//

    // Shoots a ray from the camera corresponding to the screen position and returns the gameobject hit
    private GameObject GetClickedGameObject(){
        Vector3 screenPos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000.0f, ~LayerMask.NameToLayer("Tile")))
            return hit.collider.gameObject;
        return null;
    }

    // Following methods convert easily from position information to index of the tiles array
    public static int PositionToIndex(Vector2Int position, int size) => position.y * size + position.x;

    public static int PositionToIndex(Vector3 position, int size) => (int)position.z * size + (int)position.x;

    public static int PositionToIndex(int x, int y, int size) => y * size + x;
}
