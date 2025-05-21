using UnityEngine;

// Holds fields for all towers
public abstract class Tower : MonoBehaviour{
    // Tile that the tower is on
    public Tile tile;
    // Holds the attack damage, attack speed, and attack range of the tower
    public float attackDmg, attackSpeed, attackRange;
    // Determines what type of enemy the tower can attack
    public DamageType type;
    // Which enemy is being targetted right now
    public Enemy targetEnemy;


    // Gets the closest enemy to the tower that is within its attack range
    public void GetClosestEnemy(){
        Vector3 position = new Vector3(tile.tileObject.transform.position.x, 0f, tile.tileObject.transform.position.y);
        Collider[] enemies = Physics.OverlapSphere(position, attackRange, ~LayerMask.NameToLayer("Enemy"));
        Collider closestEnemy = null;
        float minDistance = float.MaxValue;
        foreach (Collider enemy in enemies){
            float distance = Vector3.Distance(enemy.transform.position, position);
            if (enemy.CompareTag("Enemy") & distance < minDistance){
                closestEnemy = enemy;
                minDistance = distance;
            }
        }
        if (targetEnemy == null){
            targetEnemy = closestEnemy.GetComponent<Enemy>();
            Debug.Log(targetEnemy);
        }
    }

    // Attacks the enemy provided in the parameter
    private void AttackEnemy(){
    
    }
}

public enum DamageType{
    Ground, Air
}