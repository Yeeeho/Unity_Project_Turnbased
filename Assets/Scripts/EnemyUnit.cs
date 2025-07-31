using System.Collections;
using UnityEngine;

public class EnemyUnit : UnitBase
{
    private void Awake()
    {
        hp = 50;
        atk = 5;
        speed = 5;
    }
    public override IEnumerator TakeTurn()
    {
        Debug.Log($"{this.name}�� ���̴�. {this.name}�� ü��:{this.hp}/ 2�� �� ���� ���� �����Ŵ�.");
        this.Attack(UnitManager.Instance.unitList[0]);
        yield return new WaitForSeconds(2f);
        Debug.Log($"{this.name}�� �ൿ�� ����.");
    }

    public override void Attack(UnitBase unit)
    {
        Debug.Log($"{this.name}�� {unit.name}�� �����ߴ�.");
        unit.hp -= this.atk;
    }
}
