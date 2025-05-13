using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour{
    [SerializeField]
    private GameObject basicLandEnemy;
    
    public void CreateEnemy(Vector3 start, List<Cell> path){
        GameObject obj = Instantiate(basicLandEnemy, start, Quaternion.identity);
        obj.AddComponent(typeof(BasicLandEnemy));
        Enemy script = obj.GetComponent<Enemy>();
        script.pathToExit = path;
    }
}
