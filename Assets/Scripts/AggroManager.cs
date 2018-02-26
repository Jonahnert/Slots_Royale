using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroManager : MonoBehaviour {

    SphereCollider sCollider;
    Unit unit;

    private void Awake()
    {
        sCollider = GetComponent<SphereCollider>();
        unit = transform.root.GetComponent<Unit>();
    }

    public void CheckAggro()
    {
        Collider[] enemyColliders = Physics.OverlapSphere(transform.position, sCollider.radius);
        foreach(Collider other in enemyColliders)
        {
            if(other.transform.root.CompareTag("Enemy"))
            {
                Unit enemyUnit = other.transform.root.GetComponent<Unit>();
                switch (enemyUnit.teamIndex)
                {
                    case Unit.TeamIndex.Team_1:

                        if (unit && unit.teamIndex == Unit.TeamIndex.Team_2)
                        {
                            unit.attackTarget = other.transform.root.gameObject;
                            return;
                        }
                        break;
                    case Unit.TeamIndex.Team_2:

                        if (unit && unit.teamIndex == Unit.TeamIndex.Team_1)
                        {
                            unit.attackTarget = other.transform.root.gameObject;
                            return;
                        }
                        break;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!unit.attackTarget)
        {
            if (other.CompareTag("Enemy"))
            {
                Unit enemyUnit = other.transform.root.GetComponent<Unit>();
                switch (enemyUnit.teamIndex)
                {
                    case Unit.TeamIndex.Team_1:

                        if (unit && unit.teamIndex == Unit.TeamIndex.Team_2)
                        {
                            unit.attackTarget = other.transform.root.gameObject;
                        }
                        break;
                    case Unit.TeamIndex.Team_2:

                        if (unit && unit.teamIndex == Unit.TeamIndex.Team_1)
                        {
                            unit.attackTarget = other.transform.root.gameObject;
                        }
                        break;
                }
            }
        }
    }
}
