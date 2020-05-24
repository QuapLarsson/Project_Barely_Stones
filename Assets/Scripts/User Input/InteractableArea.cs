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
    /// Player movement.
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
        }

        public void OnCalled()
        {
            StopAllCoroutines(); // Stop all existing coroutines
            StartCoroutine(OnDestination());
        }

        private IEnumerator OnDestination()
        {
            NavMovement[] movement = (NavMovement[])FindObjectsOfType(typeof(NavMovement));
            WaitForSeconds wait = new WaitForSeconds(CalculateFrequency);

            while (true)
            {
                Debug.Log("Calculate");
                bool noTarget = true;
                for (int i = 0; i < movement.Length; i++) // Look through all objects in the scene that contains NavMovement
                    if (Vector3.Distance(movement[i].Target, transform.position) < MinimumDistance)
                    {
                        noTarget = false;

                        if (movement[i].CurrentPathLength < MinimumDistance)
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