using UnityEngine;

[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Projectile : MonoBehaviour{
    private float speed;
    private float damage;
    private Vector3 direction;
    private new Rigidbody rigidbody;
    private new Collider collider;
    
    public void FireProjectile(float speed, float damage, Vector3 dir){
        this.speed = speed;
        this.damage = damage;
        direction = dir;
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();

        rigidbody.useGravity = false;
        rigidbody.isKinematic = false;
        collider.isTrigger = true;
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
