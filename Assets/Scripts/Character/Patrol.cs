using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.AI;

namespace RPG.Character
{
    public class Patrol : MonoBehaviour
    {
        [SerializeField] private GameObject splineGameObject;
        [SerializeField] private float walkDuration = 3f;
        [SerializeField] private float pauseDuration = 2f;

        private SplineContainer splineCmp;
        private NavMeshAgent agentCmp;

        private float splinePosition = 0f;
        private float splineLength = 0f;
        private float lengthWalked = 0f;

        private float walkTime = 0f;
        private float pauseTime = 0f;

        private bool isWalking = true;

        private void Awake()
        {
            if (splineCmp == null)
            {
                Debug.LogWarning($"{name} does not have a spline");
            }

            agentCmp = GetComponent<NavMeshAgent>();
            splineCmp = splineGameObject.GetComponent<SplineContainer>();
            splineLength = splineCmp.CalculateLength();
        }

        public Vector3 GetNextPosition()
        {
            return splineCmp.EvaluatePosition(splinePosition);
        }

        public void CalculateNextPosition()
        {
            walkTime += Time.deltaTime;

            if (walkTime > walkDuration)
            {
                isWalking = false;
            }

            if (!isWalking)
            {
                pauseTime += Time.deltaTime;

                if (pauseTime < pauseDuration)
                {
                    return;
                }

                ResetTimers();
            }

            lengthWalked += Time.deltaTime * agentCmp.speed;

            if (lengthWalked > splineLength)
            {
                lengthWalked = 0f;
            }

            splinePosition = Mathf.Clamp01(lengthWalked / splineLength);
        }

        public void ResetTimers()
        {
            walkTime = 0f;
            pauseTime = 0f;

            isWalking = true;
        }
    }
}
