using JetBrains.Annotations;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class Turnmanager : MonoBehaviour
{
    public List<UnitBase> turnOrder = new List<UnitBase>();
    public static Turnmanager Instance;
    public UnitManager um => UnitManager.Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }

        Instance = this;
    }

    private int currentIndex = 0;

    //----------Main----------
    private void Start()
    {
        Debug.Log("게임을 시작했다.");

        um.GeneratePlayer();
        um.GenerateEnemy();

        turnOrder = um.unitList
            .Where(u => u.hp > 0)
            .OrderByDescending(u => u.speed)
            .ToList();

        if (um.unitList.Count == 0)
        {
            Debug.Log("유닛 리스트가 비어부렀다.");
            return;
        }
        if (turnOrder.Count == 0)
        {
            Debug.Log("턴 큐가 비어부렀다.");
            return;
        }

        StartCoroutine(TurnLoop());

    }

    public IEnumerator TurnLoop()
    {
        
        Debug.Log($"턴 루프를 시작했다. 시작 인덱스{currentIndex}");

        bool stopLoop = false;

        while (!stopLoop)
        {
            UnitBase currentUnit = turnOrder[currentIndex];
            yield return StartCoroutine(currentUnit.TakeTurn());
            IsEnemyAllDead();
            yield return new WaitForSeconds(1f);
            currentIndex = (currentIndex + 1) % turnOrder.Count;
        }
    }
    public void IsEnemyAllDead()
    {
        if (um.enemyList.Count == 0)
        {
            Debug.Log("적이 모두 죽었다.");
            
            SceneController.Instance.NextStage();
        }
    }
}
