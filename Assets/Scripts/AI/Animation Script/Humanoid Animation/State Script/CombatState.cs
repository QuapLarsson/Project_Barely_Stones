using System.Collections;
using UnityEngine;
using Barely.Helper;

namespace Barely.AI.Animation
{
    public partial class HumanoidAnimator
    {
        protected IEnumerator Combat_Enter()
        {
            _animator.SetEnum(HumanoidHash.Parameter.TaskStateHash, 1);

            while (true)
            {
                if (_animator.GetCurrentAnimatorStateInfo(HumanoidHash.Layer.BaseLayerHash).IsTag("Attack"))
                {
                    _animator.SetInteger(HumanoidHash.Parameter.TaskStateHash, 0);
                    FSM.ChangeState(States.Walk);

                    yield break;
                }

                yield return new WaitForFixedUpdate();
            }
        }
    }
}
