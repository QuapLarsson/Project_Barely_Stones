using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Barely.Mono;
using Barely.Helper;
using MonsterLove.StateMachine;
using Barely.AI.Movement;

namespace Barely.AI.Animation
{
    /// <summary>
    /// Animator for humanoid avatars.
    /// </summary>
    /// <remarks>Made by Celezt.</remarks>
    [AddComponentMenu("Barely/AI/HumanoidAnimator")]
    [RequireComponent(typeof(Animator), typeof(NavMovement))]
    public partial class HumanoidAnimator : BarelyMono
    {
        protected Animator _animator;
        protected NavMovement _movement;

        [HideInInspector] public StateMachine<States> FSM;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _movement = GetComponent<NavMovement>();

            FSM = StateMachine<States>.Initialize(this, States.Init);
        }
    }
}
