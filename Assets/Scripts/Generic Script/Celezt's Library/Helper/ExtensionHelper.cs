using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Barely.Helper
{
    /// <summary>
    /// Static helper functions to use alongside other functions.
    /// </summary>
    /// <remarks>Made by Celezt.</remarks>
    public static partial class ExtensionHelper
    {
        #region Mathematical Extension
        /// <summary>
        /// Limit min value.
        /// </summary>
        /// <param name="value">Value to limit.</param>
        /// <param name="minLimit">Limit threshold.</param>
        /// <returns>New value.</returns>
        public static float MinLimit(this ref float value, float minLimit) => Mathf.Max(value, minLimit);
        /// <summary>
        /// Limit min value.
        /// </summary>
        /// <param name="value">Value to limit.</param>
        /// <returns>New value.</returns>
        public static float MinLimit(this ref float value) => Mathf.Max(value, 0);
        /// <summary>
        /// Limit max value.
        /// </summary>
        /// <param name="value">Value to limit.</param>
        /// <param name="maxLimit">Limit threshold.</param>
        /// <returns>New value.</returns>
        public static float MaxLimit(this ref float value, float maxLimit) => Mathf.Min(value, maxLimit);
        /// <summary>
        /// Limit max value.
        /// </summary>
        /// <param name="value">Value to limit.</param>
        /// <returns>New value.</returns>
        public static float MaxLimit(this ref float value) => Mathf.Min(value, 0);
        /// <summary>
        /// Snap to a 2D grid (x, z).
        /// </summary>
        /// <param name="pos">Raw Position.</param>
        /// <param name="length">Length of each tile.</param>
        /// <param name="offset">Offset.</param>
        /// <returns>New position.</returns>
        public static Vector3 Snap2DRef(this ref Vector3 pos, float length, float offset = 0.5f) => Snap2D(pos, length, offset);
        /// <summary>
        /// Snap to a 2D grid (x, z).
        /// </summary>
        /// <param name="pos">Raw Position.</param>
        /// <param name="length">Length of each tile.</param>
        /// <param name="offset">Offset.</param>
        /// <returns>New position.</returns>
        public static Vector3 Snap2D(this Vector3 pos, float length, float offset = 0.5f)
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
        public static float SnapValueRef(this ref float value, float length, float offset = 0) => SnapValue(value, length, offset);
        /// <summary>
        /// Snap to value.
        /// </summary>
        /// <param name="value">Raw value.</param>
        /// <param name="length">Length of each segment.</param>
        /// <param name="offset">Offset.</param>
        /// <returns>New value.</returns>
        public static float SnapValue(this float value, float length, float offset = 0)
        {
            value = Mathf.FloorToInt(value / length) * length + offset;
            return value;
        }
        #endregion
        #region Transform Extensions
        /// <summary>
        /// Reset transformation on this object.
        /// </summary>
        /// <param name="transform">Transformation.</param>
        public static void Reset(this Transform transform)
        {
            transform.position = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }
        #endregion
        #region NavMesh Extensions
        /// <summary>
        /// Calculate a path to a specified point and set the agent to that path.
        /// </summary>
        /// <param name="agent">Agent.</param>
        /// <param name="targetPosition">The final position of the path requested.</param>
        /// <param name="path">The resulting path.</param>
        /// <param name="areaMask">Area mask.</param>
        /// <returns>True if a path is found.</returns>
        public static bool CalculatePath(this NavMeshAgent agent, Vector3 targetPosition, NavMeshPath path, int areaMask)
        {
            path.ClearCorners();
            NavMesh.CalculatePath(agent.transform.position, targetPosition, areaMask, path);
            return agent.SetPath(path);
        }
        /// <summary>
        /// Calculate the paths Length in unit.
        /// </summary>
        /// <param name="path">Path.</param>
        /// <returns>Length in decimals.</returns>
        public static float GetPathLength(this NavMeshPath path)
        {
            float length = 0f;

            if ((path.status != NavMeshPathStatus.PathInvalid) && (path.corners.Length > 1))
                for (int i = 1; i < path.corners.Length; ++i)
                    length += Vector3.Distance(path.corners[i - 1], path.corners[i]);

            return length;
        }
        /// <summary>
        /// Get a point on the path.
        /// </summary>
        /// <see cref="https://forum.unity.com/threads/get-a-point-partway-along-a-navmeshagents-path.458353/"/>
        /// <param name="path">Path.</param>
        /// <param name="distanceToTravel">Distance from the agent along the path.</param>
        /// <returns>Position.</returns>
        public static Vector3 GetPointAlongPath(this NavMeshPath path, float distanceToTravel)
        {
            if (distanceToTravel < 0)
                return path.corners[0];

            //Loop Through Each Corner in Path
            for (int i = 0; i < path.corners.Length - 1; i++)
            {
                //If the distance between the next to points is less than the distance you have left to travel
                if (distanceToTravel <= Vector3.Distance(path.corners[i], path.corners[i + 1]))
                {
                    //Calculate the point that is the correct distance between the two points and return it
                    Vector3 directionToTravel = path.corners[i + 1] - path.corners[i];
                    directionToTravel.Normalize();
                    return (path.corners[i] + (directionToTravel * distanceToTravel));
                }
                else
                {
                    //otherwise subtract the distance between those 2 points from the distance left to travel
                    distanceToTravel -= Vector3.Distance(path.corners[i], path.corners[i + 1]);
                }
            }

            //if the distance to travel is greater than the distance of the path, return the final point
            return path.corners[path.corners.Length - 1];
        }
        /// <summary>
        /// Get all points along a path.
        /// </summary>
        /// <param name="path">Path.</param>
        /// <param name="spacing">Spacing between the points.</param>
        /// <returns>Array of points.</returns>
        public static Vector3[] GetAllPointsAlongPath(this NavMeshPath path, float spacing)
        {
            List<Vector3> points = new List<Vector3>();
            float lenght = spacing;
            float pathlength = GetPathLength(path);

            while (lenght < pathlength)
            {
                points.Add(GetPointAlongPath(path, lenght));
                lenght += spacing;
            }

            return points.ToArray();
        }

        /// <summary>
        /// If the agent is inside the threshold of destination or not.
        /// </summary>
        /// <param name="agent">Agent.</param>
        /// <param name="threshold">´Threshold radius around destination.</param>
        /// <returns>If in destination.</returns>
        public static bool isOnDestination(this NavMeshAgent agent, float threshold) => Vector3.SqrMagnitude(agent.transform.position - agent.destination) < threshold ? true : false;
        #endregion
        #region Animator
        /// <summary>
        /// Sets the value of the given enum parameter.
        /// </summary>
        /// <typeparam name="T">Enumerable.</typeparam>
        /// <param name="animator">Animator.</param>
        /// <param name="id">The parameter ID.</param>
        /// <param name="enumValue">The new parameter value.</param>
        /// <returns></returns>
        public static bool SetEnum<T>(this Animator animator, int id, T enumValue) where T: IComparable, IFormattable, IConvertible
        {
            if (typeof(T).IsEnum)
            {
                int enumInt = (int)Convert.ChangeType(enumValue, typeof(int));
                animator.SetInteger(id, enumInt);
                return true;
            }
            return false;
        }
        #endregion
    }
}
