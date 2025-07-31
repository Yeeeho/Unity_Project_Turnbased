using System.Collections;
using UnityEngine;

public class PlayerUnit : UnitBase
{
    private void Awake()
    {
        hp = 100;
        atk = 10;
        speed = 10;
    }
    public override IEnumerator TakeTurn()
    {
        Debug.Log($"{this.name}�� ���̴�. {this.name}�� ü��:{this.hp} �����̽��� ���� ����");
        while(!Input.GetKeyDown(KeyCode.Space))
        {
            yield return null;
        }
        this.Attack(UnitManager.Instance.unitList[1]);
        Debug.Log($"{this.name}�� ���� ����.");
    }

    public override void Attack(UnitBase unit)
    {
        Debug.Log($"{this.name}�� {unit.name}�� �����ߴ�.");
        unit.hp -= this.atk;
    }
}