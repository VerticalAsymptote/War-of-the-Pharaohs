// Basic Ground Attack Tower
public class BasicTower : Tower{
    void OnEnable(){
        attackDmg = 1;
        attackSpeed = 1;
        attackRange = 5;
        type = DamageType.Ground;
    }
}