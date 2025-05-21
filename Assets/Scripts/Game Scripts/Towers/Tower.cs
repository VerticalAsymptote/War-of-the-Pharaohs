using System.Collections;
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
    public Transform targetEnemy;
    // Speed of the projectile
    protected float projectileSpeed;
    // Reference to the prefab of the projectile
    protected GameObject projectilePrefab;

    // Gets the closest enemy to the tower that is within its attack range
    public void GetClosestEnemy(){
        Vector3 position = new Vector3(tile.tileObject.transform.position.x, 0f, tile.tileObject.transform.position.z);
        Collider[] enemies = Physics.OverlapSphere(position, attackRange, 1 << 6);
        Collider closestEnemy = null;
        float minDistance = float.MaxValue;
        foreach (Collider enemy in enemies){
            float distance = Vector3.Distance(enemy.transform.position, position);
            if (enemy.CompareTag("Enemy") & distance < minDistance){
                closestEnemy = enemy;
                minDistance = distance;
            }
        }
        if (closestEnemy != null){
            targetEnemy = closestEnemy.transform;
        }
        else targetEnemy = null;
    }

    // Attacks the enemy provided in the parameter
    public IEnumerator AttackEnemy(){
        while (true){
            if (targetEnemy != null){
                Vector3 dir = targetEnemy.transform.position - (tile.tileObject.transform.position + new Vector3(0f, 0.5f, 0f));
                dir.y = 0;
                dir.Normalize();
                GameObject bullet = Instantiate(projectilePrefab);
                Vector3 position = tile.tileObject.transform.position;
                position.y = targetEnemy.transform.position.y;
                bullet.transform.position = position;
                bullet.AddComponent<Projectile>();
                Projectile script = bullet.GetComponent<Projectile>();
                script.FireProjectile(projectileSpeed, attackDmg, dir);
            }
            yield return new WaitForSeconds(1 / attackSpeed);
        }
    }
}

public enum DamageType{
    Ground, Air
}