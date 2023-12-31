﻿using MalbersAnimations.Controller.Reactions;
using MalbersAnimations.Events;
using MalbersAnimations.Scriptables;
using UnityEngine;
using UnityEngine.Events;

namespace MalbersAnimations
{
    [UnityEngine.DisallowMultipleComponent]
    /// <summary> Damager Receiver</summary>
    public class MDamageable : MonoBehaviour, IMDamage
    {
        [Tooltip("Animal Reaction to apply when the damage is done")]
        public MReaction reaction;

        [Tooltip("Stats component to apply the Damage")]
        public Stats stats;

        [Tooltip("Multiplier for the Stat modifier Value")]
        public FloatReference multiplier = new FloatReference(1);

        [Hide("ShowRoot", true,false)]
        public MDamageable Root;
        [HideInInspector] public bool ShowRoot;
        public damagerEvents events;

        public Vector3 HitDirection { get; set; }
        public Transform Damager { get; set; }
        public Transform Damagee => transform;

        public DamageData LastDamage;

        public virtual void ReceiveDamage(Vector3 Direction, Transform Damager, StatModifier modifier, bool isCritical, bool react, bool damageeMult)
        {
            Stat st = stats.Stat_Get(modifier.ID);

            if (st!= null && !st.IsInmune)
            {
                SetDamageable(Direction, Damager);
                Root?.SetDamageable(Direction, Damager);                                 //Send the Direction and Damager to the Root 

                if (isCritical)
                {
                    events.OnCriticalDamage.Invoke();
                    Root?.events.OnCriticalDamage.Invoke();
                }

                if (!damageeMult) modifier.Value *= multiplier;                                           //Apply to the Stat modifier a new Modification

                events.OnReceivingDamage.Invoke(modifier.Value);
                Root?.events.OnReceivingDamage.Invoke(modifier.Value);

                LastDamage = new DamageData(Damager, modifier);
                if (Root) Root.LastDamage = LastDamage;

                modifier.ModifyStat(st);

                if (react)  reaction?.React(gameObject);     //Let the Damagee to apply a reaction

                //Debug.Log("DamageReceived" + modifier.Value.Value);
            }
        }

        internal void SetDamageable(Vector3 Direction, Transform Damager)
        {
            HitDirection = Direction;
            this.Damager = Damager;
        }

        [System.Serializable]
        public class damagerEvents
        {
            // public UnityEvent BeforeReceivingDamage = new UnityEvent();
            public FloatEvent OnReceivingDamage = new FloatEvent();
            // public UnityEvent AfterReceivingDamage = new UnityEvent();
            public UnityEvent OnCriticalDamage = new UnityEvent();
        }

        public struct DamageData
        {
            /// <summary>  Who made the Damage ? </summary>
            public Transform Damager;
            /// <summary>  Final Stat Modifier ? </summary>
            public StatModifier stat;
            /// <summary> Final value who modified the Stat</summary>
            public float Damage => stat.modify != StatOption.None ?  stat.Value.Value : 0f;

            public DamageData(Transform damager, StatModifier stat)
            {
                Damager = damager;
                this.stat = new StatModifier(stat);
            }
        }


#if UNITY_EDITOR

        private void OnValidate()
        {
            ShowRoot = transform.parent != null;
        }

        private void Reset()
        {
            reaction = MTools.GetInstance<ModeReaction>("Damaged");
            stats = this.FindComponent<Stats>();
            Root = transform.root.GetComponent<MDamageable>();     //Check if there's a Damageable on the Root
            if (Root == this)  Root = null;
        }
#endif
    }
}