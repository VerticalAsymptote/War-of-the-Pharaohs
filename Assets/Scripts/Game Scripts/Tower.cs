using UnityEngine;

// Holds fields for all towers
public abstract class Tower{
    // Tile that the tower is on
    public Tile tile;
    // Asset of the tower to be placed in world
    public GameObject towerPrefab;
    // Holds the attack damage, attack speed, and attack range of the tower
    public float attackDmg, attackSpeed, attackRange;
    // Determines what type of enemy the tower can attack
    public DamageType type;
    // Which enemy is being targetted right now
    public Enemy targetEnemy;


    // Gets the closest enemy to the tower that is within its attack range
    public void GetClosestEnemy(){
        Vector3 position = new Vector3(tile.position.x, 0f, tile.position.y);
        Collider[] enemies = Physics.OverlapSphere(position, attackRange, LayerMask.NameToLayer("Enemy"));
        Collider closestEnemy = null;
        float minDistance = float.MaxValue;
        foreach (Collider enemy in enemies){
            float distance = Vector3.Distance(enemy.transform.position, position);
            if (distance < minDistance){
                closestEnemy = enemy;
                minDistance = distance;
            }
        }
        targetEnemy = closestEnemy.GetComponent<Enemy>();
    }

    // Attacks the enemy provided in the parameter
    private void AttackEnemy(GameObject enemy){
    
    }
}

// Basic Ground Attack Tower
public class BasicTower : Tower{
    public BasicTower(Tile tile){
        this.tile = tile;
        towerPrefab = Resources.Load<GameObject>("Towers/Basic Tower Prefab");
        attackDmg = 1;
        attackSpeed = 1;
        attackRange = 5;
        type = DamageType.Ground;
    }
}

public enum DamageType{
    Ground, Air
}