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

        Debug.Log($"{this.name}�� ���̴�.");

        yield return StartCoroutine(this.Attack());

        Debug.Log("����Ű�� ���� ĳ���͸� �� ĭ �̵�.");

        yield return StartCoroutine(MovManager.Instance.Move(this));

        Debug.Log($"{this.name}�� ���� ����.");
    }

    public override IEnumerator Attack()
    {

        Debug.Log($"{this.name}�� ���� ����. ���콺�� ���� ����� ����.");

        yield return StartCoroutine(inputManager.SelectCollider());

        UnitBase unit = inputManager.curCollider.GetComponent<UnitBase>();

        yield return StartCoroutine(CheckFriendlyAttack(unit));

        Debug.Log($"{this.name}�� {unit.name}�� �����ߴ�.");

        unit.hp -= this.atk;
        Debug.Log($"{unit.name}�� ���� ü��:{unit.hp}");

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
                Debug.Log("���� ���� ���� �����ðڽ��ϱ�? ����:Y / ���:N");
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