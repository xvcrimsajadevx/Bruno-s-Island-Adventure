using System;
using UnityEngine;
using RPG.Utility;


namespace RPG.Character
{
    public class EnemyController : MonoBehaviour
    {
        [NonSerialized] public float distanceFromPlayer;
        [NonSerialized] public Vector3 originalPosition;
        [NonSerialized] public Movement movementCmp;
        [NonSerialized] public GameObject player;

        public float chaseRange = 2.5f;
        public float attackRange = 0.75f;

        private AIBaseState currentState;
        public AIReturnState returnState = new AIReturnState();
        public AIChaseState chaseState = new AIChaseState();
        public AIAttackState attackState = new AIAttackState();

        private void Awake()
        {
            currentState = returnState;

            player = GameObject.FindWithTag(Constants.PLAYER_TAG);
            movementCmp = GetComponent<Movement>();

            originalPosition = transform.position;
        }

        private void Start()
        {
            currentState.EnterState(this);
        }

        private void Update()
        {
            CalculateDistanceFromPlayer();

            currentState.UpdateState(this);
        }

        public void SwitchStates(AIBaseState newState)
        {
            currentState = newState;
            currentState.EnterState(this);
        }

        private void CalculateDistanceFromPlayer()
        {
            if (player == null) return;

            Vector3 enemyPosition = transform.position;
            Vector3 playerPosition = player.transform.position;

            distanceFromPlayer = Vector3.Distance(enemyPosition, playerPosition);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseRange);
        }
    }
}

