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
    private bool isAlive = true;

    // Allows the agent to take damage
    public void TakeDamage(float damage){
        health -= damage;
        if (health <= 0){
            StopCoroutine(MoveAIAlongPath());
            isAlive = false;
            Destroy(transform.gameObject);
        }
    }

    // A coroutine that makes the AI follow the path given to it
    public IEnumerator MoveAIAlongPath(){
        foreach (Cell node in pathToExit){
            if (!isAlive)
                break;
            Vector3 startPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z) - new Vector3(0.5f, 0f, 0.5f);
            Vector3 targetPosition = new Vector3(node.x, transform.position.y, node.y);
            float time = 0;
            while (time < 1){
                if (!isAlive)
                    break;
                transform.position = Vector3.Lerp(startPosition, targetPosition, time) + new Vector3(0.5f, 0f, 0.5f);
                time += speed * Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
