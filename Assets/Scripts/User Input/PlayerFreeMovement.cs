using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Barely.AI.Movement;
using Barely.Helper;
using Barely.Mono;

namespace Barely.UserInput
{
    /// <summary>
    /// Player movement.
    /// </summary>
    /// <remarks>Made by Celezt.</remarks>
    [AddComponentMenu("Barely/User Input/PlayerFreeMovement")]
    [RequireComponent(typeof(NavMovement))]
    public class PlayerFreeMovement : BarelyMono
    {
        NavMovement _movement;

        private int _mousebutton = 0;

        private void Awake()
        {
            _movement = GetComponent<NavMovement>();
        }

        protected void Start()
        {
            StartCoroutine(UpdateCoroutine());
        }

        private IEnumerator UpdateCoroutine()
        {
            bool _executeCoroutine = false;

            void MouseAction()
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit, float.PositiveInfinity))
                    _movement.RayCast = hit;
            }

            IEnumerator Hold()
            {
                _executeCoroutine = true;
                MouseAction();
                yield return new WaitForSeconds(_movement.CalculationDelay);
                _executeCoroutine = false;
            }

            while (true)
            {
                if (PauseManager.pauseState == PauseManager.PauseState.Playing)
                {
                    
                    if (Input.GetMouseButtonDown(_mousebutton))
                    {
                        MouseAction();
                        yield return new WaitForSeconds(0.2f);
                    }
                    else if (Input.GetMouseButton(_mousebutton))
                        if (!_executeCoroutine)
                            StartCoroutine(Hold());
                }

                yield return new WaitForFixedUpdate();
            }
        }
    }
}