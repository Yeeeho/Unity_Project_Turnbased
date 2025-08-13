using NUnit.Framework;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

public class PlayerUnit : UnitBase
{
    private void Awake()
    {
        hp = 100;
        atk = 50;
        speed = 10;
    }
    public override IEnumerator TakeTurn()
    {
        if (!this) yield break;

        cam.SetTarget(this.transform);

        Debug.Log($"{this.name}의 턴이다.");

        yield return StartCoroutine(this.Attack());

        Debug.Log("방향키를 눌러 캐릭터를 한 칸 이동.");

        yield return StartCoroutine(MovManager.Instance.Move(this));

        Debug.Log($"{this.name}의 턴이 끝남.");
    }

    public override IEnumerator Attack()
    {

        Debug.Log($"{this.name}의 공격 차례. 마우스로 공격 대상을 선택.");

        yield return StartCoroutine(inputManager.SelectCollider());

        UnitBase unit = inputManager.curCollider.GetComponent<UnitBase>();

        yield return StartCoroutine(CheckFriendlyAttack(unit));

        Debug.Log($"{this.name}이 {unit.name}을 공격했다.");

        unit.hp -= this.atk;
        Debug.Log($"{unit.name}의 남은 체력:{unit.hp}");

        if (unit.hp <= 0)
        {
            unit.Die();
        }
    }

    public IEnumerator CheckFriendlyAttack(UnitBase unit)
    {
        bool input = false;
        foreach (UnitBase playerUnit in playerList)
        {
            if (playerUnit == unit)
            {
                Debug.Log("정말 같은 편을 때리시겠습니까? 동의:Y / 취소:N");
                while (!input)
                {
                    if (Input.GetKeyDown(KeyCode.Y))
                    {
                        input = true; break;
                    }
                    else if (Input.GetKeyDown(KeyCode.N))
                    {
                        input = true;
                        yield return StartCoroutine(this.Attack());
                        break;
                    }
                    yield return null;
                }
            }
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