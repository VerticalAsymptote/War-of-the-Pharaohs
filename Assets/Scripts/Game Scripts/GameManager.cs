using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Transform))]
public class GameManager : MonoBehaviour{

    [Tooltip("The size of the gameboard")] 
    public int size;

    [SerializeField, Tooltip("Prefab of the tiles")] 
    public GameObject tilePrefab;

    [SerializeField, Tooltip("Reference to the parent containing the game board")]
    public Transform board;
    
    // Holds all the tiles in the game
    [HideInInspector]
    public Tile[] tiles;
    // Holds the currently selected tile
    private GameObject selectedTile;

    void Start(){
        InitializeTiles();
        AdjustCameraLocation();
    }

    void Update(){
        // Handles getting the gameobject clicked on by mouse, and the visualizatio of selected tile
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
        }
    }

    // Initializes the tiles into the world, relative to the game board parent
    private void InitializeTiles(){
        tiles = new Tile[size * size];
        for (int y = 0; y < size; y++){
            for (int x = 0; x < size; x++){
                Instantiate(tilePrefab, new Vector3(x, 0f, y), Quaternion.identity, board);
                tiles[PositionToIndex(x, y)] = new Tile(new Vector2Int(x, y));
            }
        }
    }

    // Centers the Camera relative to the size of the board
    private void AdjustCameraLocation(){
        // TODO: Find formula to find best y value for camera position
        Vector3 cameraPosition = new Vector3(size / 2.0f, size, size / 2.0f);
        Camera.main.transform.position = cameraPosition;
    }

    // Shoots a ray from the camera corresponding to the screen position and returns the gameobject hit
    private GameObject GetClickedGameObject(){
        Vector3 screenPos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000.0f, ~LayerMask.NameToLayer("Tile")))
            return hit.collider.gameObject;
        return null;
    }

    // Both methods convert easily from position information to index of the tiles array
    private int PositionToIndex(Vector2Int position) => position.y * size + position.x;
    private int PositionToIndex(Vector3 position) => (int)position.y * size + (int)position.x;
    private int PositionToIndex(int x, int y) => y * size + x;
}
