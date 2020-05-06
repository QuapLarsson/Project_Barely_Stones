using Barely.AI.Movement;
using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

namespace Barely.AI.Animation
{
    public partial class HumanoidAnimator
    {
        protected IEnumerator Init_Enter()
        {
            yield return new WaitUntil(() => GetComponent<NavMovement>().isActiveAndEnabled);

            FSM.ChangeState(States.Idle);
        }
    }
}
