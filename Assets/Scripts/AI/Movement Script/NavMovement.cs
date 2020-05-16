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
    /// <summary>
    /// Navmesh movement.
    /// </summary>
    /// <remarks>Made by Celezt.</remarks>
    [AddComponentMenu("Barely/AI/NavMovement")]
    [RequireComponent(typeof(NavMeshAgent))]
    public partial class NavMovement : BarelyMono
    {
        protected NavMeshAgent _agent;
        protected NavMeshPath _path;

        protected float _speed;
        protected Vector3 _target;

        #region Exposed To Inspector
        [HideInInspector] public StateMachine<States> FSM;
        /// <summary>
        /// Path's target.
        /// </summary>
        public Vector3 Target
        {
            get =>  _target;
            set => CalculateDestination(value);
        }
        /// <summary>
        /// Length of the current destination.
        /// </summary>
        public float PathLength { get; private set; }

        [Header("NavMesh")]

        [Tooltip("Calculate the time between each path calculation in seconds.")]
        public float CalculationDelay = 0.2f;
        [Tooltip("Distance from destination before stopping.")]
        public float Threshold = 0.01f;

        [Header("Movement")]

        [Tooltip("Distance threshold for when to run.")]
        public float DistanceToRun = 3;
        [Tooltip("How fast the run-speed is compared to the standard walk-speed.")]
        public float RunMultiplier = 3f;
        #endregion
        #region Unity Methods
        protected virtual void OnValidate()
        {
            CalculationDelay = Mathf.Max(CalculationDelay, 0);
            Threshold = Mathf.Max(Threshold, 0);
            DistanceToRun = Mathf.Max(DistanceToRun, 0);
            RunMultiplier = Mathf.Max(RunMultiplier, 0);
        }

        protected virtual void Awake()
        {
            _path = new NavMeshPath();
            _agent = GetComponent<NavMeshAgent>();

            _speed = _agent.speed;

            FSM = StateMachine<States>.Initialize(this, States.Idle);
        }

        #endregion
        #region Support Methods
        /// <summary>
        /// Calculate new destination.
        /// </summary>
        /// <param name="target">New position.</param>
        /// <returns>If the path was found.</returns>
        public bool CalculateDestination(Vector3 target)
        {
            bool pathFound = _agent.CalculatePath(target, _path, NavMesh.AllAreas);
            _target = target;
            PathLength = _agent.path.GetPathLength();
            return pathFound;
        }
        #endregion
    }
}
