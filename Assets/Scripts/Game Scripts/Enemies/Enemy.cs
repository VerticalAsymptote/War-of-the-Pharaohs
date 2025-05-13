using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform))]
public abstract class Enemy : MonoBehaviour{
    // How much health the enemy has and how fast it moves
    public float health, speed;
    // What kind of damage can the enemy take
    public DamageType type;
    // Holds the path that the AI will take to get to the exit.
    public List<Cell> pathToExit;

    void Awake(){
        StartCoroutine("MoveAIAlongPath");
        Debug.Log("Ran");
    }

    public void TakeDamage(float damage){
        health -= damage;
        if (health <= 0)
            Destroy(transform.gameObject);
    }

    private IEnumerable MoveAIAlongPath(){
        foreach (Cell node in pathToExit){
            Vector3 position = new Vector3(node.x, 0f, node.y);
            transform.Translate(position * speed * Time.deltaTime);
            yield return new WaitUntil(() => transform.position == position);
        }
    }
}