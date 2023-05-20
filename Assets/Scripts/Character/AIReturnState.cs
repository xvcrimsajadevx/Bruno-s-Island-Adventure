using UnityEngine;

namespace RPG.Character
{
    public class AIReturnState : AIBaseState
    {
        Vector3 targetPosition;

        public override void EnterState(EnemyController enemy)
        {
            if (enemy.patrolCmp != null)
            {
                targetPosition = enemy.patrolCmp.GetNextPosition();

                enemy.movementCmp.MoveAgentByDestination(targetPosition);
            }
            else
            {
                enemy.movementCmp.MoveAgentByDestination(enemy.originalPosition);
            }
        }

        public override void UpdateState(EnemyController enemy)
        {
            if (enemy.distanceFromPlayer < enemy.chaseRange)
            {
                enemy.SwitchStates(enemy.chaseState);
                return;
            }

            if (enemy.movementCmp.ReachedDestination())
            {
                if (enemy.patrolCmp != null)
                {
                    enemy.SwitchStates(enemy.patrolState);
                    return;
                }
            }
        }
    }
}