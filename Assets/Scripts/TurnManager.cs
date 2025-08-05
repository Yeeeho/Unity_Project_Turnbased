using JetBrains.Annotations;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turnmanager : MonoBehaviour
{
    public List<UnitBase> turnOrder = new List<UnitBase>();
    public static Turnmanager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private int currentIndex = 0;

    private void Start()
    {
        Debug.Log("������ �����ߴ�.");

        UnitManager.Instance.GeneratePlayer();
        UnitManager.Instance.GenerateEnemy();
        UnitManager.Instance.GenerateEnemy();

        turnOrder = UnitManager.Instance.unitList
            .Where(u => u.hp > 0)
            .OrderByDescending(u => u.speed)
            .ToList();

        if (UnitManager.Instance.unitList.Count == 0)
        {
            Debug.Log("���� ����Ʈ�� ���η���.");
            return;
        }
        if (turnOrder.Count == 0)
        {
            Debug.Log("�� ť�� ���η���.");
            return;
        }

        StartCoroutine(TurnLoop());

    }

    IEnumerator TurnLoop()
    {
        
        Debug.Log($"�� ������ �����ߴ�. ���� �ε���{currentIndex}");
        while (true)
        {
            UnitBase currentUnit = turnOrder[currentIndex];
            yield return StartCoroutine(currentUnit.TakeTurn());
            yield return new WaitForSeconds(2f);
            currentIndex = (currentIndex + 1) % turnOrder.Count;
        }
    }

}
