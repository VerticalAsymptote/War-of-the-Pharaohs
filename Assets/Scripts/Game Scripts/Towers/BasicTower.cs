using UnityEngine;

// Basic Ground Attack Tower
public class BasicTower : Tower{
    void OnEnable(){
        attackDmg = 5;
        attackSpeed = 1;
        attackRange = 2;
        type = DamageType.Ground;
        projectileSpeed = 5;
        projectilePrefab = (GameObject)Resources.Load("Towers/Projectile Prefab");
    }
}