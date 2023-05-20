using UnityEngine;

namespace RPG.Character
{
    public class AIPatrolState : AIBaseState
    {
        public override void EnterState(EnemyController enemy)
        {
            enemy.patrolCmp.ResetTimers();
        }

        public override void UpdateState(EnemyController enemy)
        {
            if (enemy.distanceFromPlayer < enemy.chaseRange)
            {
                enemy.SwitchStates(enemy.chaseState);
                return;
            }

            enemy.patrolCmp.CalculateNextPosition();

            Vector3 currentPosition = enemy.transform.position;
            Vector3 newPosition = enemy.patrolCmp.GetNextPosition();
            Vector3 offset = newPosition - currentPosition;

            enemy.movementCmp.MoveAgentByOffset(offset);
        }
    }
}

