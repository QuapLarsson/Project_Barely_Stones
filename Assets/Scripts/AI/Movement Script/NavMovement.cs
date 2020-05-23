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

        #region Public properties
        public StateMachine<States> FSM { get; set; }
        /// <summary>
        /// Path's target.
        /// </summary>
        public Vector3 Target
        {
            get =>  _target;
            set => CalculateDestination(value);
        }
        /// <summary>
        /// Look if agent is on destination.
        /// </summary>
        public bool IsOnDestination { get => !_agent.hasPath; }
        /// <summary>
        /// Length of the current destination.
        /// </summary>
        public float LastPathLength { get; private set; }
        /// <summary>
        /// Get new path length (does not effect NavMovement).
        /// </summary>
        public float CurrentPathLength { get => _agent.path.GetPathLength(); }
        /// <summary>
        /// Update path length.
        /// </summary>
        public float UpdatePathLength { get => LastPathLength = _agent.path.GetPathLength(); }
        /// <summary>
        /// Reset the current path.
        /// </summary>
        public void ResetPath() => _agent.ResetPath();
        #endregion
        #region Exposed To Inspector
        [Header("NavMesh")]

        [Tooltip("Calculate the time between each path calculation in seconds.")]
        public float CalculationDelay = 0.2f;

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
            DistanceToRun = Mathf.Max(DistanceToRun, 0);
            RunMultiplier = Mathf.Max(RunMultiplier, 0);
        }

        //private void Update()
        //{
        //    Debug.Log(_agent.hasPath + " " + FSM.State);
        //}

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
            LastPathLength = _agent.path.GetPathLength();
            return pathFound;
        }
        #endregion
    }
}
