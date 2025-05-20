// Basic Ground Attack Tower
public class BasicTower : Tower{
    public BasicTower(Tile tile){
        this.tile = tile;
        attackDmg = 1;
        attackSpeed = 1;
        attackRange = 10;
        type = DamageType.Ground;
    }
}