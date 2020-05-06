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
        protected IEnumerator Walk_Enter()
        {
            _agent.speed = _speed;

            while (true)
            {
                if (_agent.isOnDestination(Threshold))
                {
                    FSM.ChangeState(States.Idle);
                    yield break;
                }
                else if (PathLength >= DistanceToRun)
                {
                    FSM.ChangeState(States.Run);
                    yield break;
                }

                yield return new WaitForFixedUpdate();
            }
        }
    }
}
