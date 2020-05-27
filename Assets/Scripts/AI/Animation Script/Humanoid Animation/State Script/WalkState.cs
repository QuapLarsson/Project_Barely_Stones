﻿using System.Collections;
using UnityEngine;
using Barely.Helper;

namespace Barely.AI.Animation
{
    public partial class HumanoidAnimator
    {
        protected IEnumerator Walk_Enter()
        {
            _animator.SetEnum(HumanoidHash.Parameter.MoveStateHash, _movement.FSM.State);

            while (true)
            {
                if (_movement.FSM.State == Movement.States.Idle)
                {
                    FSM.ChangeState(States.Idle);
                    yield break;
                }
                else if (_movement.FSM.State == Movement.States.Run)
                {
                    FSM.ChangeState(States.Run);
                    yield break;
                }

                yield return new WaitForFixedUpdate();
            }
        }
    }
}
