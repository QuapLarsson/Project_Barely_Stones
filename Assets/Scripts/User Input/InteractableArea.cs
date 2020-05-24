using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Barely.AI.Movement;
using Barely.Helper;
using Barely.Mono;
using UnityEngine.Events;
using System.Xml.Schema;
using UnityEngine.Serialization;
using System.Threading;

namespace Barely.UserInput
{
    /// <summary>
    /// Execute script if clicked on and inside of an certain distance.
    /// </summary>
    /// <remarks>Made by Celezt.</remarks>
    [AddComponentMenu("Barely/User Input/InteractableArea")]
    public class InteractableArea : BarelyEvent
    {
        [Tooltip("NavDistance from trigger.")]
        public float MinimumDistance = 2;
        [Tooltip("How often it looks for distance.")]
        public float CalculateFrequency = 1;

        private void OnValidate()
        {
            MinimumDistance = Mathf.Max(MinimumDistance, 0);
            CalculateFrequency = Mathf.Max(CalculateFrequency, 0);
        }

        public void OnCalled()
        {
            StopAllCoroutines(); // Stop all existing coroutines
            StartCoroutine(OnDestination());
        }

        private IEnumerator OnDestination()
        {
            NavMovement[] movements = (NavMovement[])FindObjectsOfType(typeof(NavMovement));
            WaitForSeconds wait = new WaitForSeconds(CalculateFrequency);

            while (true)
            {
                bool noTarget = true;

                for (int i = 0; i < movements.Length; i++) // Loop through all NavMovement objects
                    if (movements[i].TargetObject == this || movements[i].TargetObject == GetComponentInChildren(typeof(Collider)).gameObject)
                    {
                        noTarget = false;

                        if (movements[i].CurrentPathLength < MinimumDistance)
                        {
                            _onInteract.Invoke();
                            yield break;
                        }
                    }

                if (noTarget)
                    yield break;

                yield return wait;
            }
        }
    }
}