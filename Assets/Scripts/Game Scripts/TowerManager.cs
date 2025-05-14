using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameManager))]
public class TowerManager : MonoBehaviour{
    // Holds a reference to all the towers in the game
    public List<Tower> towers;

    void Start(){
        towers = new List<Tower>();
        StartCoroutine(TowerEnemyCheck());
    }

    private IEnumerator TowerEnemyCheck(){
        while(true){
            foreach (Tower tower in towers){
                tower.GetClosestEnemy();
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
