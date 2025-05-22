using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(EnemyManager))]
[RequireComponent(typeof(TileManager))]
[RequireComponent(typeof(TowerManager))]
[RequireComponent(typeof(PlayerManager))]
public class GameManager : MonoBehaviour{

    [SerializeField, Tooltip("The size of the gameboard")] 
    public int size;
    
    // Manages the enemies for the game
    public EnemyManager enemyManager;
    
    // Manages the tiles for the map
    public TileManager tileManager;

    // Manages the towers in the game
    public TowerManager towerManager;

    // Manages player information
    public PlayerManager playerManager;

    void Start(){
        // Start and End positions to generate the random routes from start to end
        Vector2Int startPos = new Vector2Int(0, 0), endPos = new Vector2Int(9, 9); 

        // Enables the managers for the game
        tileManager = GetComponent<TileManager>();
        enemyManager = GetComponent<EnemyManager>();
        towerManager = GetComponent<TowerManager>();
        playerManager = GetComponent<PlayerManager>();

        // Initialize the tiles into the world
        tileManager.InitializeTiles(startPos, endPos);

        // Allows the enemy to move to the end square
        tileManager.pathNodes.AddLast(new Cell(endPos.x, endPos.y));

        // Adjust Camera Location to better see the map
        AdjustCameraLocation();

        //DEBUG PURPOSES
        StartCoroutine(PlayLevel());
    }

    void Update(){
    }
    
    private IEnumerator PlayLevel(){
        enemyManager.SpawnEnemy(new Vector3(0f, 0f, 0f), tileManager.pathNodes);
        yield return new WaitForSeconds(2);
        enemyManager.SpawnEnemy(new Vector3(0f, 0f, 0f), tileManager.pathNodes);
        yield return new WaitForSeconds(2);
        enemyManager.SpawnEnemy(new Vector3(0f, 0f, 0f), tileManager.pathNodes);
        yield return new WaitForSeconds(2);
        enemyManager.SpawnEnemy(new Vector3(0f, 0f, 0f), tileManager.pathNodes);        
        yield return new WaitForSeconds(2);
        enemyManager.SpawnEnemy(new Vector3(0f, 0f, 0f), tileManager.pathNodes);        
        yield return new WaitForSeconds(2);
        enemyManager.SpawnEnemy(new Vector3(0f, 0f, 0f), tileManager.pathNodes);        
        yield return new WaitForSeconds(2);
        enemyManager.SpawnEnemy(new Vector3(0f, 0f, 0f), tileManager.pathNodes);        
        yield return new WaitForSeconds(2);
        enemyManager.SpawnEnemy(new Vector3(0f, 0f, 0f), tileManager.pathNodes);        
        yield return new WaitForSeconds(2);
        enemyManager.SpawnEnemy(new Vector3(0f, 0f, 0f), tileManager.pathNodes);        
        yield return new WaitForSeconds(2);
        enemyManager.SpawnEnemy(new Vector3(0f, 0f, 0f), tileManager.pathNodes);        
        yield return new WaitForSeconds(2);
        enemyManager.SpawnEnemy(new Vector3(0f, 0f, 0f), tileManager.pathNodes);
    }

    // Centers the Camera relative to the size of the board
    private void AdjustCameraLocation(){
        // TODO: Find formula to find best y value for camera position
        Vector3 cameraPosition = new Vector3(size / 2.0f, size, size / 2.0f);
        Camera.main.transform.position = cameraPosition;
    }
}
