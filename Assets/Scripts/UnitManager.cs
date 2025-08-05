using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public List<UnitBase> unitList = new List<UnitBase>();

    public static UnitManager Instance;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;


    private void Awake()
    {
        Instance = this;
    }

    int enemyPosY = 3;
    private int EnemyId = 0;
    private int PlayerId = 0;
    public void GenerateEnemy()
    { 

        GameObject enemyObject = Instantiate(enemyPrefab, new Vector3(3, enemyPosY, 0), Quaternion.identity);
        enemyPosY -= 3;
        UnitBase enemyUnit = enemyObject.GetComponent<UnitBase>();
        unitList.Add(enemyUnit);

        enemyUnit.name = "몬스따/UnitId:" + EnemyId++;
    }

    public void GeneratePlayer()
    {
        GameObject playerObject = Instantiate(playerPrefab, new Vector3(-3, 0, 0), Quaternion.identity);
        UnitBase playerUnit = playerObject.GetComponent<UnitBase>();
        unitList.Add(playerUnit);

        playerUnit.name = "용사/UnitId:" + PlayerId++;
    }
}

public abstract class UnitBase : MonoBehaviour
{
    protected List<UnitBase> unitList => UnitManager.Instance.unitList;
    protected List<UnitBase> turnOrder => Turnmanager.Instance.turnOrder;

    public MovManager movManager;

    public int speed;
    public int atk;
    public int hp;
    public int exp;

    public bool isMovable = false;

    public abstract IEnumerator TakeTurn();

    public abstract void Attack(UnitBase unit);
    public abstract void Die();


    
}

