using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameManager))]
public class TowerManager : MonoBehaviour{

    // Holds a reference to all the towers in the game
    [HideInInspector]
    public List<Tower> towers;

    [SerializeField]
    private GameObject basicTowerPrefab;

    void Start(){
        towers = new List<Tower>();
        StartCoroutine(TowerEnemyCheck());
    }
    
    public void PlaceTower(ref Tile tile){
        if (tile.tower != null)
            return;
        GameObject tower = Instantiate(basicTowerPrefab);
        tower.transform.parent = tile.tileObject.transform;
        tower.transform.localPosition = Vector3.zero;
        tower.AddComponent(typeof(BasicTower));
        tile.tower = tower.GetComponent<Tower>();
        tile.tower.tile = tile;
        towers.Add(tile.tower);
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
