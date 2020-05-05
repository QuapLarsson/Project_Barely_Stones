using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

namespace Barely.Helper
{
    /// <summary>
    /// Helping methods to use alongside other method.
    /// </summary>
    /// <remarks>Made by Celezt.</remarks>
    public partial class MethodHelper
    {
        #region Class Access
        /// <summary>
        /// Action related help methods.
        /// </summary>
        public HelperAction Action = new HelperAction();
        /// <summary>
        ///Math related help methods.
        /// </summary>
        public HelperMath Math = new HelperMath();
        #endregion

        /// <summary>
        /// Nested extension. For action related methods.
        /// </summary>
        public partial class HelperAction
        {
            private Dictionary<int, bool> _infoDict = new Dictionary<int, bool>();

            /// <summary>
            /// Time delay for an action.
            /// </summary>
            /// <param name="id">Unique id.</param>
            /// <param name="time">Seconds.</param>
            /// <param name="task">Action.</param>
            public IEnumerator Delay(int id, float time, Action task)
            {
                var coroutine = OnCalled(id);

                if (coroutine)
                    yield break;

                coroutine = true;
                yield return new WaitForSeconds(time);
                task();
                coroutine = false;
            }

            /// <summary>
            /// Create new instance if the id does not already exist.
            /// </summary>
            private bool OnCalled(int id)
            {
                if (!_infoDict.ContainsKey(id))
                    _infoDict.Add(id, false);
                return _infoDict[id];
            }
        }

        /// <summary>
        /// Nested extension. For Math related methods.
        /// </summary>
        public partial class HelperMath
        {
            /// <summary>
            /// Snap to a 2D grid (x, z).
            /// </summary>
            /// <param name="x">Raw x position.</param>
            /// <param name="z">Raw z position.</param>
            /// <param name="length">Length of each tile.</param>
            /// <param name="offset">Offset.</param>
            /// <returns>New position.</returns>
            public Vector3 Snap2D(int x, int z, float length, float offset = 0) => Snap2D(new Vector3(x, 0, z), length, offset);
            /// <summary>
            /// Snap to a 2D grid (x, z).
            /// </summary>
            /// <param name="pos">Raw Position.</param>
            /// <param name="length">Length of each tile.</param>
            /// <param name="offset">Offset.</param>
            /// <returns>New position.</returns>
            public Vector3 Snap2D(Vector3 pos, float length, float offset = 0)
            {
                pos.x = Mathf.FloorToInt(pos.x / length) * length + offset;
                pos.z = Mathf.FloorToInt(pos.z / length) * length + offset;
                return pos;
            }
            /// <summary>
            /// Snap to value.
            /// </summary>
            /// <param name="value">Raw value.</param>
            /// <param name="length">Length of each segment.</param>
            /// <param name="offset">Offset.</param>
            /// <returns>New value.</returns>
            public float SnapValue(float value, float length, float offset = 0)
            {
                value = Mathf.FloorToInt(value / length) * length + offset;
                return value;
            }
        }
    }
}
