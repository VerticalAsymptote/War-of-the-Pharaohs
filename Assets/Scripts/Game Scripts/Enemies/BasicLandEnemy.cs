public class BasicLandEnemy : Enemy{
    void OnEnable(){
        health = 100;
        speed = 0.5f;
        type = DamageType.Ground;
    }
}
