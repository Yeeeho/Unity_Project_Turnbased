using NUnit.Framework;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerUnit : UnitBase
{
    public CamFollowTarget cam;
    private void Awake()
    {
        hp = 100;
        atk = 25;
        speed = 10; 
    }
    public override IEnumerator TakeTurn()
    {
        cam.SetTarget(this.transform);
        Debug.Log($"{this.name}�� ���̴�. {this.name}�� ü��:{this.hp}");
        yield return StartCoroutine(this.Attack());
        Debug.Log("����Ű�� ���� ĳ���͸� �� ĭ �̵�.");
        yield return StartCoroutine(MovManager.Instance.Move(this));
        Debug.Log($"{this.name}�� ���� ����.");
    }

    public override IEnumerator Attack()
    {
        Debug.Log($"{this.name}�� ���� ����. ���콺�� ���� ����� ����.");
        yield return StartCoroutine(inputManager.MouseSelect());
        UnitBase unit = inputManager.selectedUnit;
        Debug.Log($"{this.name}�� {unit.name}�� �����ߴ�.");
        unit.hp -= this.atk;
        Debug.Log($"���� ���� ü��:{unit.hp}");
        if (unit.hp <= 0)
        {
            unit.Die();
        }
    }

    public override void Die()
    {
        Debug.Log("����� �׾���..");
        GameOver();
    }
    public void GameOver()
    {
        Debug.Log($"������ ������.");
    }

    public void CamFollowTarget()
    {
        
    }
}