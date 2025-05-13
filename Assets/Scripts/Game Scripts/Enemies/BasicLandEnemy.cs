public class BasicLandEnemy : Enemy{
    void OnEnable(){
        health = 100;
        speed = 1.0f;
        type = DamageType.Ground;
    }
}
