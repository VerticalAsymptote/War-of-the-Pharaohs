using UnityEngine;

[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour{
    private float speed;
    private float damage;
    private Vector3 direction;
    public Projectile(float speed, float damage, Vector3 dir){
        this.speed = speed;
        this.damage = damage;
        direction = dir;
    }

    void Update(){
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other){
        if (other.CompareTag("Enemy")){
            other.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(GetComponent<Transform>().gameObject);
        }
    }
}
