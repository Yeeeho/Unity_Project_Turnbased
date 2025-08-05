using System.Collections;
using UnityEngine;

public class EnemyUnit : UnitBase
{
    
    public CamFollowTarget cam;
    private void Awake()
    {
        hp = 50;
        atk = 5;
        speed = 5;
        exp = 1;
    }
    public override IEnumerator TakeTurn()
    {
        cam.SetTarget(this.transform);
        Debug.Log($"{this.name}�� ���̴�. {this.name}�� ü��:{this.hp}/ 2�� �� ���� ���� �����Ŵ�.");
        this.Attack(UnitManager.Instance.unitList[0]);
        yield return new WaitForSeconds(2f);
        Debug.Log($"{this.name}�� �ൿ�� ����.");
    }

    public override void Attack(UnitBase unit)
    {
        Debug.Log($"{this.name}�� {unit.name}�� �����ߴ�.");
        unit.hp -= this.atk;
        if (unit.hp <= 0) 
        {
            unit.Die();
        }
    }

    public override void Die()
    {
        Debug.Log($"{this.name}�� �׾���. ����ġ{this.exp}�� ȹ���ߴ�.");
        unitList.Remove(this);
        turnOrder.Remove(this);
        Debug.Log(unitList.Count);
        Debug.Log(turnOrder.Count);
        Destroy(this.gameObject);
    }
}
