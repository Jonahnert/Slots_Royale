using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class Unit : MonoBehaviour {

    public enum TeamIndex { Team_1, Team_2 }
    public TeamIndex teamIndex;
    public enum ColorIndex {Blue, Green, Red}
    public ColorIndex colorIndex;
    public int damage = 1;
    public Transform targetGoal;
    public GameObject attackTarget;
    private NavMeshAgent agent;
    public float maxCooldown = 1;
    public float currentCooldown = 0;
    public float attackDistance = 0.5f;
    private bool shouldCheckTargetsInRadius = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void AttackTarget(GameObject target, int attackDamage)
    {
        Health targetHealth = target.GetComponent<Health>();
        targetHealth.ReceiveDamage(damage);
    }

    // Update is called once per frame
    void Update()
    {

        //This part of the script controls the targeting of the enemy.  As it stands, whenever the enemy encounters a minion it will attack it.  Otherwise the enemy will chase
        //objective targets.  Logic may need to be added later if the enemy needs to chase objective targets even when it encounters an enemy.

        //If you encounter an enemy, start attacking it
        if (attackTarget)
        {
            if (Vector3.Distance(transform.position, attackTarget.transform.position) <= attackDistance)
            {
                agent.isStopped = true;

                if(currentCooldown <= 0)
                {
                    Unit attackUnit = attackTarget.GetComponent<Unit>();
                    switch (this.colorIndex)
                    {
                        case ColorIndex.Blue:
                            switch (attackUnit.colorIndex)
                            {
                                case ColorIndex.Blue:
                                    AttackTarget(attackTarget, damage);
                                    currentCooldown = maxCooldown;
                                    break;
                                case ColorIndex.Green:
                                    AttackTarget(attackTarget, Mathf.CeilToInt(damage * 0.1f));
                                    currentCooldown = maxCooldown;
                                    break;
                                case ColorIndex.Red:
                                    AttackTarget(attackTarget, damage * 2);
                                    currentCooldown = maxCooldown;
                                    break;
                            }
                            break;
                        case ColorIndex.Green:
                            switch (attackUnit.colorIndex)
                            {
                                case ColorIndex.Blue:
                                    AttackTarget(attackTarget, damage * 2);
                                    currentCooldown = maxCooldown;
                                    break;
                                case ColorIndex.Green:
                                    AttackTarget(attackTarget, damage);
                                    currentCooldown = maxCooldown;
                                    break;
                                case ColorIndex.Red:
                                    AttackTarget(attackTarget, Mathf.CeilToInt(damage * 0.1f));
                                    currentCooldown = maxCooldown;
                                    break;
                            }
                            break;
                        case ColorIndex.Red:
                            switch (attackUnit.colorIndex)
                            {
                                case ColorIndex.Blue:
                                    AttackTarget(attackTarget, Mathf.CeilToInt(damage * 0.1f));
                                    currentCooldown = maxCooldown;
                                    break;
                                case ColorIndex.Green:
                                    AttackTarget(attackTarget, damage * 2);
                                    currentCooldown = maxCooldown;
                                    break;
                                case ColorIndex.Red:
                                    AttackTarget(attackTarget, damage);
                                    currentCooldown = maxCooldown;
                                    break;
                            }
                            break;
                    }
                }
                else
                {
                    currentCooldown -= Time.deltaTime;
                }
            }

            else
            {
                agent.isStopped = false;
                agent.destination = attackTarget.transform.position;
                if (!shouldCheckTargetsInRadius)
                {
                    shouldCheckTargetsInRadius = true;
                }
            }
        }

        //Otherwise run towards the objectives
        else if(targetGoal)
        {
            if(shouldCheckTargetsInRadius)
            {
                AggroManager aggro = GetComponentInChildren<AggroManager>();
                aggro.CheckAggro();
                shouldCheckTargetsInRadius = false;
            }
            else
            {
                agent.isStopped = false;
                agent.destination = targetGoal.transform.position;
            }           
        }
    }
}
