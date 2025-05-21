using UnityEngine;

public class PlayerManager : MonoBehaviour{
    // How much health the base has remaining
    public int baseHealth;
    
    public void TakeDamage(int damage){
        baseHealth -= damage;
        if (baseHealth <= 0){
            Debug.Log("Player Loses");
        }
    }
}
