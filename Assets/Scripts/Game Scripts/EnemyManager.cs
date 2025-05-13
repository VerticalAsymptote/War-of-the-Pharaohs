using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour{
    [SerializeField]
    private GameObject basicLandEnemy;

    public void CreateEnemy(Vector3 start, List<Cell> path){
        GameObject enemyObj = Instantiate(basicLandEnemy, start, Quaternion.identity);
        enemyObj.AddComponent(typeof(BasicLandEnemy));
        Enemy script = enemyObj.GetComponent<Enemy>();
        script.pathToExit = path;
        enemyObj.transform.position += new Vector3(0.5f, 0.5f, 0.5f);

        StartCoroutine(script.MoveAIAlongPath());
    }
}
