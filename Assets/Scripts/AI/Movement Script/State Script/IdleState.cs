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
        protected IEnumerator Idle_Enter()
        {
            _agent.speed = _speed;

            while (true)
            {
                if (_agent.hasPath)
                {
                    FSM.ChangeState(States.Walk);
                    yield break;
                }

                yield return new WaitForFixedUpdate();
            }
        }
    }
}
