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

    [SerializeField, Tooltip("A prefab to visualize the tower radius")]
    private GameObject towerRadiusPrefab;

    // The gameobject of the tower's radius
    private GameObject towerRadius;

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
        StartCoroutine(tile.tower.AttackEnemy());
        tile.tower.tile = tile;
        VisualizeTowerRange(tower);
        towers.Add(tile.tower);
    }
    
    public void VisualizeTowerRange(GameObject tower){
        Tower script = tower.GetComponent<Tower>();
        float attackRange = script.attackRange * 2;
        GameObject visual = Instantiate(towerRadiusPrefab);
        visual.transform.position = tower.transform.position - new Vector3(0f, 0.5f, 0f);
        visual.transform.localScale = new Vector3(attackRange, 0.1f, attackRange);
        towerRadius = visual;
    }

    private IEnumerator TowerEnemyCheck(){
        while(true){
            foreach (Tower tower in towers){
                tower.GetClosestEnemy();
            }
            yield return new WaitForSeconds(0.2f);
        }
    }
}
