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

    public void TakeDamage(float damage){
        health -= damage;
        if (health <= 0)
            Destroy(transform.gameObject);
    }

    public IEnumerator MoveAIAlongPath(){
        foreach (Cell node in pathToExit){
            Vector3 startPosition = new Vector3(transform.position.x - 0.5f, 0f, transform.position.z - 0.5f);
            Vector3 currentPosition = startPosition;
            Vector3 targetPosition = new Vector3(node.x, 0f, node.y);
            float distanceToTravel = Vector3.Distance(startPosition, targetPosition);
            float time = 0;
            while (time < 1){
                float currentDistance = Vector3.Distance(currentPosition, targetPosition);
                time = (distanceToTravel - currentDistance) / distanceToTravel;
                transform.Translate(Vector3.Lerp(startPosition, targetPosition, time));
                currentPosition = new Vector3(transform.position.x - 0.5f, 0f, transform.position.z - 0.5f);
                currentDistance += speed * Time.deltaTime;
                yield return new WaitForEndOfFrame();

                Debug.Log(time);
            }
            
            Debug.Log("New Node");

            // Difference between the two positions to get the direction, multipled by speed and divided by tile to cross a tile
            //Vector3 dir = targetPosition - transformPosition;
            //dir.Normalize();
            //dir *= speed / 2.0f;
            //while (transform.position.x - 0.5f < targetPosition.x && transform.position.z - 0.5f < targetPosition.z){
            //    transform.Translate(dir * Time.deltaTime);
            //    yield return new WaitForEndOfFrame();
            //}


            //Vector3 dir = new Vector3(node.x - (transform.position.x - 0.5f), 0f, node.y - (transform.position.z - 0.5f));
            //dir.Normalize();
            //while (transform.position.x - 0.5f < targetPosition.x || transform.position.z - 0.5f < targetPosition.z){
            //    transform.Translate(dir * speed * Time.deltaTime);
            //    yield return new WaitForSeconds(0.001f);
            //}
            //transform.position = targetPosition += new Vector3(0.5f, 0f, 0.5f);
        }
    }
}