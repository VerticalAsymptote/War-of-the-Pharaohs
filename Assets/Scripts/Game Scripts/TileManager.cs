using UnityEngine;

[RequireComponent(typeof(GameManager))]
public class TileManager : MonoBehaviour{
    [SerializeField, Tooltip("Prefab of the tiles in the game")]
    public GameObject tilePrefab;

    [SerializeField, Tooltip("Reference to the parent containing the game board")]
    public Transform board;

    // Holds all the tiles for the game
    [HideInInspector]
    public Tile[] tiles;

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
                tiles[PositionToIndex(x, y)] = new Tile(new Vector2Int(x, y));
            }
        }
    }

    // Following methods convert easily from position information to index of the tiles array
    private int PositionToIndex(Vector2Int position) => position.y * gameManager.size + position.x;
    private int PositionToIndex(Vector3 position) => (int)position.y * gameManager.size + (int)position.x;
    private int PositionToIndex(int x, int y) => y * gameManager.size + x;
}
