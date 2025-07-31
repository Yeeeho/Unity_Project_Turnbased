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
        Debug.Log($"{this.name}의 턴이다. {this.name}의 체력:{this.hp} 스페이스를 눌러 공격");
        while(!Input.GetKeyDown(KeyCode.Space))
        {
            yield return null;
        }
        this.Attack(UnitManager.Instance.unitList[1]);
        Debug.Log($"{this.name}의 턴이 끝남.");
    }

    public override void Attack(UnitBase unit)
    {
        Debug.Log($"{this.name}이 {unit.name}을 공격했다.");
        unit.hp -= this.atk;
    }
}