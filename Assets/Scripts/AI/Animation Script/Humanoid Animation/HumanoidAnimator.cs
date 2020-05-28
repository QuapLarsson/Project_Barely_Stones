using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Barely.Mono;
using Barely.Helper;
using MonsterLove.StateMachine;
using Barely.AI.Movement;
//using Boo.Lang;
using UnityEngine.Assertions.Must;
using UnityEngine.Rendering.HighDefinition;

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
        protected Fighter _fighter;

        protected bool _newState;

        [HideInInspector] public StateMachine<States> FSM;

        [Tooltip("Weight on the lower part of the avatar")]
        public float LowerPartWeight = 1;

        private void OnValidate()
        {
            LowerPartWeight = Mathf.Max(LowerPartWeight, 0);
            if (_animator != null && _animator.HasLayer(HumanoidHash.Layer.LowerLayerHash))
                _animator.SetLayerWeight(HumanoidHash.Layer.LowerLayerHash, LowerPartWeight);
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _movement = GetComponent<NavMovement>();
            _fighter = GetComponent<Fighter>();

            if (_animator.HasLayer(HumanoidHash.Layer.LowerLayerHash))
                _animator.SetLayerWeight(HumanoidHash.Layer.LowerLayerHash, LowerPartWeight);

            StartCoroutine(IfFullAnimation());

            FSM = StateMachine<States>.Initialize(this, States.Init);
        }

        /// <summary>
        /// Look if the animation is full animation based on the state tag.
        /// </summary>
        /// <returns></returns>
        protected IEnumerator IfFullAnimation()
        {
            bool cache = false;

            while (true)
            {
                bool transition = _animator.IsInTransition(HumanoidHash.Layer.BaseLayerHash);

                if (transition)
                    cache = true;
                else if (cache && !transition)
                {
                    _newState = true;
                    cache = false;

                    if (_animator.HasParameter(HumanoidHash.Parameter.LowerPartHash))
                    {
                        if (_animator.GetCurrentAnimatorStateInfo(HumanoidHash.Layer.BaseLayerHash).IsTag("Full"))
                            _animator.SetBool(HumanoidHash.Parameter.LowerPartHash, false);
                        else
                            _animator.SetBool(HumanoidHash.Parameter.LowerPartHash, true);
                    }
                }
                else
                    _newState = false;

                yield return new WaitForFixedUpdate();
            }
        }
    }
}
