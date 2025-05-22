using UnityEngine;

[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Projectile : MonoBehaviour{
    private float speed;
    private int damage;
    private Vector3 direction;
    private float lifetime;
    private new Rigidbody rigidbody;
    private new Collider collider;
    
    public void FireProjectile(float speed, int damage, Vector3 dir){
        this.speed = speed;
        this.damage = damage;
        direction = dir;
        lifetime = 0;
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();

        rigidbody.useGravity = false;
        rigidbody.isKinematic = false;
        collider.isTrigger = true;
    }

    void Update(){
        transform.Translate(direction * speed * Time.deltaTime);
        if (lifetime > 5.0f)
            Destroy(GetComponent<Transform>().gameObject);
        lifetime += Time.deltaTime;
    }

    void OnTriggerEnter(Collider other){        
        if (other.CompareTag("Enemy")){
            other.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(GetComponent<Transform>().gameObject);
        }
    }
}
