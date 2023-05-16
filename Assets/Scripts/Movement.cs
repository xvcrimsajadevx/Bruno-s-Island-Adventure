using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using System;
using UnityEngine.UIElements;

namespace RPG.Character
{
    public class Movement : MonoBehaviour
    {
        private NavMeshAgent agent;
        private Vector3 movementVector;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            MovePlayer();
            RotatePlayer();
        }

        private void MovePlayer()
        {
            Vector3 offset = movementVector * Time.deltaTime * agent.speed;
            agent.Move(offset);
        }

        private void RotatePlayer()
        {
            if (movementVector == Vector3.zero) return;

            Quaternion startRotation = transform.rotation;
            Quaternion endRotation = Quaternion.LookRotation(movementVector);

            transform.rotation = Quaternion.Lerp(
                startRotation,
                endRotation,
                Time.deltaTime * agent.angularSpeed
            );
        }

        public void HandleMove(InputAction.CallbackContext context)
        {
            Vector2 input = context.ReadValue<Vector2>();
            movementVector = new Vector3(input.x, 0, input.y);
        }
    }
}


