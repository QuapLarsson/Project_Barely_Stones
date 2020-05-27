using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Barely.Mono;
using Barely.Helper;
using MonsterLove.StateMachine;
using System.Linq;

namespace Barely.AI.Animation
{
    public partial class HumanoidAnimator
    {
        protected IEnumerator Run_Enter()
        {
            _animator.SetEnum(HumanoidHash.Parameter.MoveStateHash, _movement.FSM.State);

            while (true)
            {
                if (_movement.FSM.State == Movement.States.Walk)
                {
                    FSM.ChangeState(States.Walk);
                    yield break;
                }

                yield return new WaitForFixedUpdate();
            }
        }
    }
}
