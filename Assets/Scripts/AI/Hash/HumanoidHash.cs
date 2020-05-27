using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Barely.AI
{
    /// <summary>
    /// Contains all hash values from humanoid Animator.
    /// </summary>
    public class HumanoidHash
    {
        public class Parameter
        {

            public static readonly int MoveStateHash = Animator.StringToHash("MoveState");
            public static readonly int TaskStateHash = Animator.StringToHash("TaskState");
            public static readonly int LowerPartHash = Animator.StringToHash("LowerPart");
        }
        public class Layer
        {
            public static readonly int BaseLayerHash = 0;
            public static readonly int LowerLayerHash = 1;
        }
    }
}
