using UnityEngine;

[RequireComponent(typeof(GameManager))]
public class TileManager : MonoBehaviour{

    // Holds all the tiles for the game
    [HideInInspector]
    public Tile[] tiles;

    [SerializeField, Tooltip("Prefab of the tiles in the game")]
    private GameObject tilePrefab;

    [SerializeField, Tooltip("Reference to the parent containing the game board")]
    private Transform board;

    // Handles the game logic
    private GameManager gameManager;

    void Start(){
        gameManager = GetComponent<GameManager>();
    }

    // Initializes the tiles and places them in the world
    public void InitializeTiles(){
        tiles = new Tile[gameManager.size * gameManager.size];
        for (int y = 0; y < gameManager.size; y++){
            for (int x = 0; x < gameManager.size; x++){
                Instantiate(tilePrefab, new Vector3(x, 0f, y), Quaternion.identity, board);
                tiles[PositionToIndex(x, y, gameManager.size)] = new Tile(new Vector2Int(x, y));
            }
        }
    }

    // Given a tile, places a tower on the tile
    public void PlaceTower(ref Tile tile, Tower tower){
        if (tile.tower != null)
            return;
        Instantiate(tower.towerPrefab);
        tile.tower = tower;
    }

    // Given a tile, removes the tower on that tile
    public void RemoveTower(ref Tile tile){

    }

    // Following methods convert easily from position information to index of the tiles array
    public static int PositionToIndex(Vector2Int position, int size) => position.y * size + position.x;
    public static int PositionToIndex(Vector3 position, int size) => (int)position.y * size + (int)position.x;
    public static int PositionToIndex(int x, int y, int size) => y * size + x;
}
