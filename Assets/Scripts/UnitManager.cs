using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnitManager : MonoBehaviour
{
    public List<UnitBase> unitList = new List<UnitBase>();
    public List<UnitBase> enemyList = new List<UnitBase>();
    public List<UnitBase> playerList = new List<UnitBase>();

    public static UnitManager Instance;

    public GameObject playerPrefab; 
    public GameObject enemyPrefab;


    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    int enemyPosY = 3;
    private int EnemyId = 0;
    private int PlayerId = 0;
    public void GenerateEnemy()
    {
        Debug.Log("적을 생성했다.");

        var activeScene = SceneManager.GetActiveScene();
        Debug.Log($"Active scene:{activeScene.name}");

        GameObject enemyObject = Instantiate(enemyPrefab, new Vector3(3, enemyPosY, 0), Quaternion.identity);
        enemyPosY -= 3;

        SceneManager.MoveGameObjectToScene(enemyObject, activeScene);

        UnitBase enemyUnit = enemyObject.GetComponent<UnitBase>();
        unitList.Add(enemyUnit);
        enemyList.Add(enemyUnit);

        enemyUnit.name = "몬스따/UnitId:" + EnemyId++;
    }

    public void GeneratePlayer()
    {
        Debug.Log("플레이어를 생성했다.");

        var activeScene = SceneManager.GetActiveScene();
        Debug.Log($"Active scene:{activeScene.name}");

        GameObject playerObject = Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        SceneManager.MoveGameObjectToScene(playerObject, activeScene);

        UnitBase playerUnit = playerObject.GetComponent<UnitBase>();
        unitList.Add(playerUnit);
        playerList.Add(playerUnit);

        playerUnit.name = "용사/UnitId:" + PlayerId++;
    }
}

public abstract class UnitBase : MonoBehaviour
{
    protected List<UnitBase> unitList => UnitManager.Instance.unitList;
    protected List<UnitBase> enemyList => UnitManager.Instance.enemyList;
    protected List<UnitBase> playerList => UnitManager.Instance.playerList;
    protected List<UnitBase> turnOrder => Turnmanager.Instance.turnOrder;
    protected InputManager inputManager => InputManager.Instance;

    public CamFollowTarget cam => CamFollowTarget.Instance;

    public MovManager movManager;

    public int speed;
    public int atk;
    public int hp;
    public int exp;
    public abstract IEnumerator TakeTurn();
    public abstract IEnumerator Attack();
    public abstract void Die();


    
}

