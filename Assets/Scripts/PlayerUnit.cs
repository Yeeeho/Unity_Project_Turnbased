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
        Debug.Log($"{this.name}의 턴이다. {this.name}의 체력:{this.hp}");
        yield return StartCoroutine(this.Attack());
        Debug.Log("방향키를 눌러 캐릭터를 한 칸 이동.");
        yield return StartCoroutine(MovManager.Instance.Move(this));
        Debug.Log($"{this.name}의 턴이 끝남.");
    }

    public override IEnumerator Attack()
    {
        Debug.Log($"{this.name}의 공격 차례. 마우스로 공격 대상을 선택.");
        yield return StartCoroutine(inputManager.MouseSelect());
        UnitBase unit = inputManager.selectedUnit;
        Debug.Log($"{this.name}이 {unit.name}을 공격했다.");
        unit.hp -= this.atk;
        Debug.Log($"적의 남은 체력:{unit.hp}");
        if (unit.hp <= 0)
        {
            unit.Die();
        }
    }

    public override void Die()
    {
        Debug.Log("당신은 죽었다..");
        GameOver();
    }
    public void GameOver()
    {
        Debug.Log($"게임이 끝났다.");
    }

    public void CamFollowTarget()
    {
        
    }
}