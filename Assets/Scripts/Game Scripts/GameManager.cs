using UnityEngine;

[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(EnemyManager))]
[RequireComponent(typeof(TileManager))]
[RequireComponent(typeof(TowerManager))]
public class GameManager : MonoBehaviour{

    [SerializeField, Tooltip("The size of the gameboard")] 
    public int size;
    
    // Manages the enemies for the game
    private EnemyManager enemyManager;
    
    // Manages the tiles for the map
    private TileManager tileManager;

    // Manages the towers in the game
    private TowerManager towerManager;
    
    // Reference to the currently selected tile
    private GameObject selectedTile;

    void Start(){
        // Start and End positions to generate the random routes from start to end
        Vector2Int startPos = new Vector2Int(0, 0), endPos = new Vector2Int(9, 9); 

        // Enables the managers for the game
        tileManager = GetComponent<TileManager>();
        enemyManager = GetComponent<EnemyManager>();
        towerManager = GetComponent<TowerManager>();

        // Initialize the tiles into the world
        tileManager.InitializeTiles(startPos, endPos, size);

        // Adjust Camera Location to better see the map
        AdjustCameraLocation();

        //DEBUG PURPOSES
        enemyManager.CreateEnemy(Vector3.zero, tileManager.pathNodes);
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
            Tile tile = tileManager.tiles[TileManager.PositionToIndex(selectedTile.transform.localPosition, size)];
            CreateTower(ref tile);
            selectedTile.transform.position -= new Vector3(0f, 0.5f, 0f);
            selectedTile = null;
        }
    }

    private void CreateTower(ref Tile tile){
        tileManager.PlaceTower(ref tile, new BasicTower(tile));
        towerManager.towers.Add(tile.tower);
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
}
