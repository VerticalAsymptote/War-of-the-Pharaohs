using UnityEngine;

[RequireComponent(typeof(Transform))]
public class GameManager : MonoBehaviour{

    [Tooltip("The size of the gameboard")] 
    public int size;

    [SerializeField, Tooltip("Prefab of the tiles")] 
    public GameObject tiles;

    [SerializeField, Tooltip("Reference to the parent containing the game board")]
    public Transform board;

    void Start(){
        InitializeTiles();
        AdjustCameraLocation();
    }

    // Initializes the tiles into the world, relative to the game board parent
    private void InitializeTiles(){
        for (int y = 0; y < size; y++){
            for (int x = 0; x < size; x++){
                Instantiate(tiles, new Vector3(x, 0f, y), Quaternion.identity, board);
            }
        }
    }

    // Centers the Camera relative to the size of the board
    private void AdjustCameraLocation(){
        // TODO: Find formula to find best y value for camera position
        Vector3 cameraPosition = new Vector3(size / 2.0f, 10f, size / 2.0f);
        Camera.main.transform.position = cameraPosition;
    }
}
