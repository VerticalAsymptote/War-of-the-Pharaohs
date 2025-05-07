public class BasicLandEnemy : Enemy{
    public BasicLandEnemy(){
        health = 100;
        speed = 0.5f;
        type = DamageType.Ground;
    }
}
