using UnityEngine;

[RequireComponent(typeof(Transform))]
public abstract class Enemy : MonoBehaviour{
    // How much health the enemy has and how fast it moves
    public float health, speed;
    // What kind of damage can the enemy take
    public DamageType type;

    public void TakeDamage(float damage){
        health -= damage;
        if (health <= 0)
            Destroy(transform.gameObject);
    }
}