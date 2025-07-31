using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public List<UnitBase> unitList = new List<UnitBase>();
    public static UnitManager Instance;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    private int UnitId = 0;
    private void Awake()
    {
        Instance = this;
    }

    public void GenerateEnemy()
    {
        GameObject enemyObject = Instantiate(enemyPrefab, new Vector3(3, 0, 0), Quaternion.identity);
        UnitBase enemyUnit = enemyObject.GetComponent<UnitBase>();
        UnitManager.Instance.unitList.Add(enemyUnit);

        enemyUnit.name = "몬스따/UnitId:" + UnitId++;
    }

    public void GeneratePlayer()
    {
        GameObject playerObject = Instantiate(playerPrefab, new Vector3(-3, 0, 0), Quaternion.identity);
        UnitBase playerUnit = playerObject.GetComponent<UnitBase>();
        UnitManager.Instance.unitList.Add(playerUnit);

        playerUnit.name = "용사/UnitId:" + UnitId++;
        UnitId++ ;
    }
}

public abstract class UnitBase : MonoBehaviour
{
    public int speed;
    public int atk;
    public int hp;
    public abstract IEnumerator TakeTurn();

    public abstract void Attack(UnitBase unit);
}

