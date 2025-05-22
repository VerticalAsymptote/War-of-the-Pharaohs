using UnityEngine;

[RequireComponent(typeof(Transform))]
public class BaseTile : MonoBehaviour{
    
    [SerializeField]
    public GameObject gameManager;

    private PlayerManager playerManager;

    void Awake(){
        gameManager = GameObject.FindWithTag("GameController");
        playerManager = gameManager.GetComponent<GameManager>().playerManager;
        Debug.Assert(gameManager.GetComponent<GameManager>() != null);
        Debug.Assert(playerManager != null);
    }

    void OnTriggerStay(Collider other){
        if (other.CompareTag("Enemy")){
            Enemy script = other.GetComponent<BasicLandEnemy>();
            if(script.isAtEnd){
                Debug.Assert(script != null);
                Debug.Assert(playerManager != null);
                playerManager.TakeDamage(script.health);
                Destroy(other.gameObject);
            }
        }
    }
}
