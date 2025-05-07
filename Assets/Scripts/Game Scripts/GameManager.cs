using UnityEngine;

[RequireComponent(typeof(TileManager))]
[RequireComponent(typeof(Transform))]
public class GameManager : MonoBehaviour{

    [Tooltip("The size of the gameboard")] 
    public int size;

    // Manages the tiles for the map
    private TileManager tileManager;
    
    // Reference to the currently selected tile
    private GameObject selectedTile;

    void Start(){
        tileManager = GetComponent<TileManager>();
        tileManager.InitializeTiles();
        AdjustCameraLocation();
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
            tileManager.PlaceTower(ref tile, new BasicTower(tile));
            selectedTile.transform.position -= new Vector3(0f, 0.5f, 0f);
            selectedTile = null;
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
}
