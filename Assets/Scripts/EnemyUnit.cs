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
        Debug.Log($"{this.name}의 턴이다. {this.name}의 체력:{this.hp}/ 2초 후 적의 턴이 끝날거다.");
        this.Attack(UnitManager.Instance.unitList[0]);
        yield return new WaitForSeconds(2f);
        Debug.Log($"{this.name}의 행동이 끝남.");
    }

    public override void Attack(UnitBase unit)
    {
        Debug.Log($"{this.name}이 {unit.name}을 공격했다.");
        unit.hp -= this.atk;
        if (unit.hp <= 0) 
        {
            unit.Die();
        }
    }

    public override void Die()
    {
        Debug.Log($"{this.name}은 죽었다. 경험치{this.exp}를 획득했다.");
        unitList.Remove(this);
        turnOrder.Remove(this);
        Debug.Log(unitList.Count);
        Debug.Log(turnOrder.Count);
        Destroy(this.gameObject);
    }
}
