using JetBrains.Annotations;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turnmanager : MonoBehaviour
{
    public List<UnitBase> turnOrder = new List<UnitBase>();
    
    private int currentIndex = 0;

    private void Start()
    {
        Debug.Log("게임을 시작했다.");

        UnitManager.Instance.GeneratePlayer();
        UnitManager.Instance.GenerateEnemy();
        UnitManager.Instance.GenerateEnemy();

        turnOrder = UnitManager.Instance.unitList
            .Where(u => u.hp > 0)
            .OrderByDescending(u => u.speed)
            .ToList();

        if (turnOrder.Count == 0)
        {
            Debug.Log("턴 큐가 비어부렀다.");
            return;
        }

        StartCoroutine(TurnLoop());

    }

    IEnumerator TurnLoop()
    {
        Debug.Log($"턴 루프를 시작했다. 시작 인덱스{currentIndex}");
        for (int i = 0; i < 10; i++)
        {
            UnitBase currentUnit = turnOrder[currentIndex];
            yield return StartCoroutine(currentUnit.TakeTurn());

            yield return new WaitForSeconds(2f);
            currentIndex = (currentIndex + 1) % turnOrder.Count;
        }
    }

}
