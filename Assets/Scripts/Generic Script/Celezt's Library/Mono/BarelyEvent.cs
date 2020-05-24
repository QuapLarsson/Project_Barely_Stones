using System;
using UnityEngine;
using Barely.Mono;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Barely.Mono
{
    /// <summary>
    /// Support class for easy events.
    /// </summary>
    /// <remarks>Made by Celezt.</remarks>
    public abstract class BarelyEvent : BarelyMono
    {
        [Serializable] public class ObjectInteractionEvent : UnityEvent { }
        [FormerlySerializedAs("OnInteract")]
        [SerializeField] protected ObjectInteractionEvent _onInteract = new ObjectInteractionEvent();
        public ObjectInteractionEvent OnInteract
        {
            get => _onInteract;
            set => _onInteract = value;
        }
    }
}
