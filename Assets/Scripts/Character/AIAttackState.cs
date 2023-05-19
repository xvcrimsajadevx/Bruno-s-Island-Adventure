using UnityEngine;

namespace RPG.Character
{
    public class AIAttackState : AIBaseState
    {
        public override void EnterState(EnemyController enemy)
        {
            enemy.movementCmp.StopMovingAgent();
        }

        public override void UpdateState(EnemyController enemy)
        {
            if (enemy.distanceFromPlayer > enemy.attackRange)
            {
                enemy.SwitchStates(enemy.chaseState);
                return;
            }

            Debug.Log("Player is being attacked!");
        }
    }
}


