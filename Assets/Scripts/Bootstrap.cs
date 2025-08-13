using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    UnitManager um => UnitManager.Instance;
    Turnmanager tm => Turnmanager.Instance;

    void Start()
    {
        um.GeneratePlayer();
        um.GenerateEnemy();
    }

        
}
