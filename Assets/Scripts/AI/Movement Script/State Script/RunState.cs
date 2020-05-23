using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Barely.Mono;
using Barely.Helper;
using MonsterLove.StateMachine;

namespace Barely.AI.Movement
{
    public partial class NavMovement
    {
        protected IEnumerator Run_Enter()
        {
            _agent.speed = _speed * RunMultiplier;

            while (true)
            {
                if (LastPathLength < DistanceToRun || !_agent.hasPath)
                {
                    FSM.ChangeState(States.Walk);
                    yield break;
                }

                yield return new WaitForFixedUpdate();
            }
        }
    }
}
