using System.Collections;
using UnityEngine;
using Barely.Helper;

namespace Barely.AI.Animation
{
    public partial class HumanoidAnimator
    {
        protected IEnumerator Idle_Enter()
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
