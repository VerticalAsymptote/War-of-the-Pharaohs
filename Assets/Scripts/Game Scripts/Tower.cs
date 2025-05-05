using UnityEngine;

// Holds fields for all towers
public abstract class Tower{
    // Asset of the tower to be placed in world
    public GameObject towerPrefab;
    // Holds the attack damage, attack speed, and attack range of the tower
    public float attackDmg, attackSpeed, attackRange;
    // Determines what type of enemy the tower can attack
    public DamageType type;
    // Which enemy is being targetted right now
    public GameObject targetEnemy;

    void Start(){
        
    }

    void Update(){
        GetClosestEnemy();
    }

    // Opens the shop for the tower
    public void OpenShopGUI(){

    }

    // Gets the closest enemy to the tower that is within its attack range
    private GameObject GetClosestEnemy(){
        return null;
    }

    // Attacks the enemy provided in the parameter
    private void AttackEnemy(GameObject enemy){

    }
}

// Basic Ground Attack Tower
public class BasicTower : Tower{
    public BasicTower(){
        towerPrefab = (GameObject)Resources.Load("Assets/Prefabs/Towers/Basic Tower Prefab.prefab");
        attackDmg = 1;
        attackSpeed = 1;
        attackRange = 5;
        type = DamageType.Ground;
    }
}

public enum DamageType{
    Ground, Air
}