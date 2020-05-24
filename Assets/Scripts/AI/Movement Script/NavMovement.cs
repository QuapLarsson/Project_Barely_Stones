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
        protected GameObject _targetObject;
        protected bool _pause;

        #region Public properties
        public StateMachine<States> FSM { get; set; }
        /// <summary>
        /// Pause NavMesh.
        /// </summary>
        public bool Pause
        {
            get => _pause;
            set => SetPause(value);
        }
        /// <summary>
        /// Path's target.
        /// </summary>
        public Vector3 Target
        {
            get =>  _target;
            set => GetDestination(value);
        }
        /// <summary>
        /// Target object.
        /// </summary>
        public GameObject TargetObject
        {
            get => _targetObject;
            set => GetTargetObject(value);
        }
        /// <summary>
        /// Target's raycast. Set target object and target destination.
        /// </summary>
        public RaycastHit RayCast
        {
            set => GetRayCast(value);
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
        /// Get new path length (does not affect NavMovement).
        /// </summary>
        public float CurrentPathLength { get => _agent.path.GetPathLength(); }
        /// <summary>
        /// Update path length.
        /// </summary>
        public float UpdatePathLength { get => LastPathLength = _agent.path.GetPathLength(); }
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
        public bool GetDestination(Vector3 target)
        {
            bool pathFound = _agent.CalculatePath(target, _path, NavMesh.AllAreas);
            _target = target;
            LastPathLength = _agent.path.GetPathLength();

            return pathFound;
        }
        /// <summary>
        /// Get target game object.
        /// </summary>
        /// <param name="hit">Raycast hit.</param>
        /// <returns>If not null.</returns>
        public bool GetTargetObject(RaycastHit hit) => GetTargetObject(hit.transform.gameObject);
        /// <summary>
        /// Get target game object.
        /// </summary>
        /// <param name="hit">Game object.</param>
        /// <returns>If not null.</returns>
        public bool GetTargetObject(GameObject gameObject) => _targetObject = gameObject;
        /// <summary>
        /// Get raycast and set target object and target destination.
        /// </summary>
        /// <param name="hit">Raycast.</param>
        /// <returns>Return if both point and game object is not null.</returns>
        public bool GetRayCast(RaycastHit hit)
            => GetTargetObject(hit) && ((hit.transform.gameObject.layer == LayerMask.NameToLayer("Walkable")) ? GetDestination(hit.point) : GetDestination(hit.transform.position));
        /// <summary>
        /// Reset the current path.
        /// </summary>
        public void ResetPath()
        {
            _agent.ResetPath();
            _targetObject = null;
        }
        /// <summary>
        /// Pause NavMesh.
        /// </summary>
        /// <param name="state">Pause state.</param>
        public void SetPause(bool state) => _pause = _agent.isStopped = state;
        #endregion
    }
}
