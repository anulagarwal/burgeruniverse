﻿using System.Collections.Generic;
using UnityEngine;

namespace RayFire
{
    [System.Serializable]
    public class RFParticleNoise
    {
        [Header("  Main")]
        [Space (3)]
        
        public bool enabled;
        [Space (1)]
        public ParticleSystemNoiseQuality quality;
        
        [Header("  Strength")]
        [Space (3)]
        
        [Range (0f, 3f)] public float strengthMin;
        [Space (1)]
        [Range (0f, 3f)] public float strengthMax;
        
        [Header("  Other")]
        [Space (3)]
        
        [Range (0.001f, 3f)] public float frequency;
        [Space (1)]
        [Range (0f, 2f)] public float scrollSpeed;
        [Space (1)]
        public bool damping;
        
        // Constructor
        public RFParticleNoise()
        {
            enabled     = false;
            strengthMin = 0.3f;
            strengthMax = 0.6f;
            frequency   = 0.3f;
            scrollSpeed = 0.7f;
            damping     = true;
            quality     = ParticleSystemNoiseQuality.High;
        }
        
        // Copy from
        public void CopyFrom (RFParticleNoise source)
        {
            enabled     = source.enabled;
            strengthMin = source.strengthMin;
            strengthMax = source.strengthMax;
            frequency   = source.frequency;
            scrollSpeed = source.scrollSpeed;
            damping     = source.damping;
            quality     = source.quality;
        }
    }

    [System.Serializable]
    public class RFParticleRendering
    {
        [Header("  Shadows")]
        [Space (3)]
        
        public bool castShadows;
        [Space (1)]
        public bool receiveShadows;

        // Constructor
        public RFParticleRendering()
        {
             castShadows = true;
             receiveShadows = true;
        }
        
        // Copy from
        public void CopyFrom (RFParticleRendering source)
        {
            castShadows = source.castShadows;
            receiveShadows = source.receiveShadows;
        }
    }
    
    [System.Serializable]
    public class RFParticleDynamicDebris
    {
        [Header("  Speed")]
        [Space (3)]
        
        // TODO Speed by size, separate speed for Impact
        [Range(0f, 10f)] public float speedMin;
        [Space (1)]
        [Range(0f, 10f)] public float speedMax;
        
        [Header("  Inherit Velocity")]
        [Space (3)]
        
        [Range(0f, 3f)] public float velocityMin;
        [Space (1)]
        [Range(0f, 3f)] public float velocityMax;
        
        [Header("  Gravity Modifier")]
        [Space (3)]
        
        [Range(-2f, 2f)] public float gravityMin;
        [Space (1)]
        [Range(-2f, 2f)] public float gravityMax;

        [Header("  Rotation")]
        [Space (3)]
        
        [Range(0f, 1f)] public float rotationSpeed;
        
        // Constructor
        public RFParticleDynamicDebris()
        {
            speedMin = 1f;
            speedMax = 4f;
            velocityMin = 0.5f;
            velocityMax = 1.5f;
            rotationSpeed = 0.5f;
            gravityMin = 0.8f;
            gravityMax = 1.1f;
        }
        
        // Copy from
        public void CopyFrom (RFParticleDynamicDebris source)
        {
            speedMin        = source.speedMin;
            speedMax        = source.speedMax;
            velocityMin     = source.velocityMin;
            velocityMax     = source.velocityMax;
            rotationSpeed   = source.rotationSpeed;
            gravityMin      = source.gravityMin;
            gravityMax      = source.gravityMax;
        }
    }
    
    [System.Serializable]
    public class RFParticleDynamicDust
    {
        [Header("  Velocity")]
        [Space (3)]
        
        // TODO Speed by size, separate speed for Impact
        [Range(0f, 10f)] public float speedMin;
        [Space (1)]
        [Range(0f, 10f)] public float speedMax;

        [Header("  Gravity Modifier")]
        [Space (3)]
        
        [Range(-2f, 2f)] public float gravityMin;
        [Space (1)]
        [Range(-2f, 2f)] public float gravityMax;

        // Constructor
        public RFParticleDynamicDust()
        {
            speedMin        = 0f;
            speedMax        = 0.5f;
            gravityMin      = -0.1f;
            gravityMax      = 0.7f;
        }
        
        // Copy from
        public void CopyFrom (RFParticleDynamicDust source)
        {
            speedMin        = source.speedMin;
            speedMax        = source.speedMax;
            gravityMin      = source.gravityMin;
            gravityMax      = source.gravityMax;
        }
    }
    
    [System.Serializable]
    public class RFParticleEmission
    {
        [Header("  Burst")]
        [Space (3)]
        
        public RFParticles.BurstType burstType;
        [Space (1)]
        [Range(0, 500)] public int burstAmount;
        
        [Header("  Distance")]
        [Space (3)]
        
        [Range(0f, 5f)] public float distanceRate;
        [Space (1)]
        [Range(0.5f, 10)] public float duration;
        
        [Header("  Lifetime")]
        [Space (3)]
        
        [Range(1f, 60f)] public float lifeMin;
        [Space (1)]
        [Range(1f, 60f)] public float lifeMax;

        [Header("  Size")]
        [Space (3)]
        
        [Range(0.1f, 10f)] public float sizeMin;
        [Space (1)]
        [Range(0.1f, 10f)] public float sizeMax;
        
        // Constructor
        public RFParticleEmission()
        {
            burstType = RFParticles.BurstType.PerOneUnitSize;
            burstAmount = 20;
            duration = 4;
            distanceRate = 1f;
            lifeMin = 2f;
            lifeMax = 13f;
            sizeMin = 0.5f;
            sizeMax = 2.5f;
        }
        
        // Copy from
        public void CopyFrom (RFParticleEmission source)
        {
            burstType = source.burstType;
            burstAmount = source.burstAmount;
            distanceRate = source.distanceRate;
            lifeMin = source.lifeMin;
            lifeMax = source.lifeMax;
            sizeMin = source.sizeMin;
            sizeMax = source.sizeMax;
        }
    }
    
    [System.Serializable]
    public class RFParticleLimitations
    {
        [Header("  Particle system")]
        [Space (3)]
        
        [Range(3, 100)] public int minParticles;
        [Space (1)]
        [Range(5, 100)] public int maxParticles;
        
        [Header("  Fragments")]
        [Space (3)]
                
        [Range(10, 100)] public int percentage;
        [Space (1)]
        [Range(0.05f, 5)] public float sizeThreshold;
        [Space (1)]
        [Range(0, 5)] public int demolitionDepth;

        // Constructor
        public RFParticleLimitations()
        {
            minParticles = 3;
            maxParticles = 20;
            percentage = 50;
            sizeThreshold = 0.5f;
            demolitionDepth = 2;
        }
        
        // Copy from
        public void CopyFrom (RFParticleLimitations source)
        {
            minParticles = source.minParticles;
            maxParticles = source.maxParticles;
            percentage    = source.percentage;
            sizeThreshold = source.sizeThreshold;
            demolitionDepth = source.demolitionDepth;
        }
    }

    [System.Serializable]
    public class RFParticleCollisionDebris
    {
        // death on collision
        // dynamic collision
        
        public enum RFParticleCollisionWithType
        {
            Everything = 0,
            Nothing    = 1
        }
        
        public enum RFParticleCollisionMatType
        {
            ByPhysicalMaterial = 0,
            ByProperties       = 1
        }
        
        [Header("  Common")]
        [Space (3)]
        
        public RFParticleCollisionWithType collidesWith;
        [Space (1)]
        public ParticleSystemCollisionQuality quality;
        [Space (1)]
        [Range(0.1f, 2f)] public float radiusScale;
        
        [Header("  Dampen")]
        [Space (3)]
        
        public RFParticleCollisionMatType dampenType;
        [Space (1)]
        [Range (0f, 1f)] public float dampenMin;
        [Space (1)]
        [Range (0f, 1f)] public float dampenMax;
        
        [Header("  Bounce")]
        [Space (3)]
        
        public RFParticleCollisionMatType bounceType;
        [Space (1)]
        [Range (0f, 1f)] public float bounceMin;
        [Space (1)]
        [Range (0f, 1f)] public float bounceMax;
        
        
        [HideInInspector] public bool propertiesSet = false;
        
        // Constructor
        public RFParticleCollisionDebris()
        {
            collidesWith = RFParticleCollisionWithType.Everything;
            quality = ParticleSystemCollisionQuality.High;
            radiusScale = 0.1f;

            dampenType = RFParticleCollisionMatType.ByProperties;
            dampenMin = 0.1f;
            dampenMax = 0.4f;
            
            bounceType = RFParticleCollisionMatType.ByProperties;
            bounceMin = 0.2f;
            bounceMax = 0.4f;
        }
        
        // Copy from
        public void CopyFrom (RFParticleCollisionDebris source)
        {
            collidesWith = source.collidesWith;
            quality      = source.quality;
            radiusScale  = source.radiusScale;

            dampenType = source.dampenType;
            dampenMin  = source.dampenMin;
            dampenMax  = source.dampenMax;
            
            bounceType = source.bounceType;
            bounceMin  = source.bounceMin;
            bounceMax  = source.bounceMax;

            propertiesSet = source.propertiesSet;
        }
        
        // Set material properties
        public void SetMaterialProps (RayfireDebris debris)
        {
            // Properties was set. Exclude this step
            if (propertiesSet == true)
                return;
            
            // Do this method once
            propertiesSet = true;
            
            // No need to set collision properties. NO collision expected
            if (debris.collision.collidesWith == RFParticleCollisionWithType.Nothing)
                return;

            // own props should be used
            if (dampenType == RFParticleCollisionMatType.ByProperties && bounceType == RFParticleCollisionMatType.ByProperties)
                return;
            
            // Set collider to take properties
            Collider collider;
            if (debris.rigid == null || debris.rigid.physics.meshCollider == null)
                collider = debris.GetComponent<Collider>();
            else
                collider = debris.rigid.physics.meshCollider;

            // No collider
            if (collider == null)
                return;

            // No collider material
            if (collider.sharedMaterial == null)
                return;
            
            // Set dampen
            if (dampenType == RFParticleCollisionMatType.ByPhysicalMaterial)
            {
                dampenMin = collider.sharedMaterial.dynamicFriction;
                dampenMax = dampenMin * 0.05f + dampenMin;
            }
            
            // Set bounce
            if (bounceType == RFParticleCollisionMatType.ByPhysicalMaterial)
            {
                bounceMin = collider.sharedMaterial.bounciness;
                bounceMax = bounceMin * 0.05f + bounceMin;
            }
        }
    }
    
    [System.Serializable]
    public class RFParticleCollisionDust
    {
        [Header("  Common")]
        [Space (3)]
        
        public RFParticles.RFParticleCollisionWithType collidesWith;
        [Space (1)]
        public ParticleSystemCollisionQuality quality;
        [Space (1)]
        [Range(0.1f, 2f)] public float radiusScale;

        // Constructor
        public RFParticleCollisionDust()
        {
            collidesWith = RFParticles.RFParticleCollisionWithType.Everything;
            quality = ParticleSystemCollisionQuality.High;
            radiusScale = 1f;
        }
        
        // Copy from
        public void CopyFrom (RFParticleCollisionDust source)
        {
            collidesWith = source.collidesWith;
            quality      = source.quality;
            radiusScale  = source.radiusScale;
        }
    }
    
    [System.Serializable]
    public class RFParticles
    {
        public enum RFParticleCollisionWithType
        {
            Everything = 0,
            Nothing    = 1
        }
        
        public enum RFParticleCollisionMatType
        {
            ByPhysicalMaterial = 0,
            ByProperties       = 1
        }
        
        public enum BurstType
        {
            None            = 0,
            TotalAmount     = 1,
            PerOneUnitSize  = 2,
            FragmentAmount  = 3
        }

        /// /////////////////////////////////////////////////////////
        /// Demolition
        /// /////////////////////////////////////////////////////////
        
        // Init particles on demolition
        public static void InitDemolitionParticles(RayfireRigid source)
        { 
            // No frags
            if (source.HasFragments == false)
                return;
            
            // Create debris particles
            if (source.HasDebris == true)
                CreateDemolitionDebris (source);
                
            // Create dust particles
            if (source.HasDust == true)
                CreateDemolitionDust (source);
            
            // Detach child particles in case object has child particles and about to be deleted
            DetachParticles(source);
        }

        // Create debris particle system
        public static void CreateDemolitionDebris(RayfireRigid source)
        {
            for (int i = 0; i < source.debrisList.Count; i++)
            {
                // Skip if no demolition debris required
                if (source.debrisList[i].onDemolition == false)
                    continue;
                
                // Skip if has no child debris
                if (source.debrisList[i].HasChildren == false)
                    continue;
                
                // Debris hosts
                List<RayfireDebris> particlesTargets = GetDebrisTargets (source.debrisList[i].children, source.debrisList[i].limitations.sizeThreshold, source.debrisList[i].limitations.percentage, 0);

                // No targets
                if (particlesTargets.Count == 0)
                    continue;
                
                // Get amount list
                SetDebrisFinalAmount (particlesTargets, source.debrisList[i].emission.burstType, source.debrisList[i].emission.burstAmount);
                
                // Variables
                int seed = 0;

                // Create particle systems
                for (int p = 0; p < particlesTargets.Count; p++)
                {
                    // Set random seed
                    Random.InitState (seed++);

                    // Create single debris particle system
                    CreateDebrisRigid (particlesTargets[p]);
                }
            }
        }
        
        // Create dust particle system
        public static void CreateDemolitionDust(RayfireRigid source)
        {
            for (int i = 0; i < source.dustList.Count; i++)
            {
                // Skip if no demolition debris required
                if (source.dustList[i].onDemolition == false)
                    continue;
                
                // Skip if has no child debris
                if (source.dustList[i].HasChildren == false)
                    continue;
                
                // Dust hosts
                List<RayfireDust> particlesHosts = GetDustTargets (source.dustList[i].children, source.dustList[i].limitations.sizeThreshold, source.dustList[i].limitations.percentage, 1);

                // Get amount list
                SetDustFinalAmount (particlesHosts, source.dustList[i].emission.burstType, source.dustList[i].emission.burstAmount);
                
                // Create particle systems
                int seed = 0;

                // Create particle systems
                for (int p = 0; p < particlesHosts.Count; p++)
                {
                    // Set random seed
                    Random.InitState (seed++);
                    
                    // Create single dust particle system
                    CreateDustRigid (particlesHosts[p]);
                }
            }
        }
        
        // Create single debris particle system
        public static void CreateDebrisRigid(RayfireDebris target)
        {
            // No particles
            if (target.amountFinal < target.limitations.minParticles && target.emission.distanceRate == 0)
                return;
            
            // Particle system
            ParticleSystem ps = target.CreateParticleSystem (target);

            // Get emit material index
            int emitMatIndex = GetEmissionMatIndex (target.rigid.meshRenderer, target.emissionMaterial);

            // Create debris
            target.CreateDebris (target.rigid.transForm, target, target.rigid.meshFilter, emitMatIndex, ps);
        }
        
        // Create single dust particle system
        public static void CreateDustRigid(RayfireDust target)
        {
            // No particles
            if (target.amountFinal < target.limitations.minParticles && target.emission.distanceRate == 0)
                return;
            
            // Particle system
            ParticleSystem ps = target.CreateParticleSystem (target);

            // Get emit material index
            int emitMatIndex = GetEmissionMatIndex (target.rigid.meshRenderer, target.emissionMaterial);

            // Create debris
            target.CreateDust (target.rigid.transForm, target, target.rigid.meshFilter, emitMatIndex, ps);
        }
        
        /// /////////////////////////////////////////////////////////
        /// Activation
        /// /////////////////////////////////////////////////////////
        
        // Init particles on activation
        public static void InitActivationParticles(RayfireRigid source)
        {
            // Create debris particles
            if (source.HasDebris == true)
                for (int i = 0; i < source.debrisList.Count; i++)
                    if (source.debrisList[i].onActivation == true)
                        CreateDebrisRigid (source.debrisList[i]);
                
            // Create dust particles
            if (source.HasDust == true)
                for (int i = 0; i < source.dustList.Count; i++)
                    if (source.dustList[i].onActivation == true)
                        CreateDustRigid (source.dustList[i]);
        }

        /// /////////////////////////////////////////////////////////
        /// Impact
        /// /////////////////////////////////////////////////////////
        
        // Create single debris particle system
        public static void CreateDebrisImpact(RayfireDebris debris, Vector3 impactPos, Vector3 impactNormal)
        {
            // Particle system
            ParticleSystem ps = debris.CreateParticleSystem (debris);

            // Align over impact
            debris.hostTm.position = impactPos;
            debris.hostTm.LookAt (impactPos + impactNormal);

            // Set amount
            debris.amountFinal = debris.emission.burstAmount;
            
            // Create debris
            debris.CreateDebris (debris.rigid.transForm, debris, null, -1, ps);
        }

        // Create single debris particle system
        public static void CreateDustImpact(RayfireDust dust, Vector3 impactPos, Vector3 impactNormal)
        {
            // Particle system
            ParticleSystem ps = dust.CreateParticleSystem (dust);

            // Align over impact
            dust.hostTm.position = impactPos;
            dust.hostTm.LookAt (impactPos + impactNormal);

            // Set amount
            dust.amountFinal = dust.emission.burstAmount;
            
            // Create debris
            dust.CreateDust (dust.rigid.transForm, dust, null, -1, ps);
        }
        
        /// /////////////////////////////////////////////////////////
        /// Other
        /// /////////////////////////////////////////////////////////
        
        // Detach child particles in case object has child particles and about to be deleted
        static void DetachParticles(RayfireRigid source)
        {
            // Detach debris particle system if fragment was already demolished/activated before
            if (source.HasDebris == true) 
            {
                for (int i = 0; i < source.debrisList.Count; i++)
                {
                    if (source.debrisList[i].hostTm != null)
                    {
                        source.debrisList[i].hostTm.parent     = null;
                        source.debrisList[i].hostTm.localScale = Vector3.one;
                    }
                }
            }

            // Detach dust particle system if fragment was already demolished/activated before
            if (source.HasDust == true)
            {
                for (int i = 0; i < source.dustList.Count; i++)
                {
                    if (source.dustList[i].hostTm != null)
                    {
                        source.dustList[i].hostTm.parent     = null;
                        source.dustList[i].hostTm.localScale = Vector3.one;
                    }
                }
            }
        }

        // Copy debris and dust
        public static void CopyParticles(RayfireRigid source, RayfireRigid target)
        {
            // Copy debris
            if (source.HasDebris == true) 
            {
                for (int i = 0; i < source.debrisList.Count; i++)
                {
                    RayfireDebris targetDebris = target.gameObject.AddComponent<RayfireDebris>();
                    targetDebris.CopyFrom (source.debrisList[i]);

                    if (source.debrisList[i].children == null)
                        source.debrisList[i].children = new List<RayfireDebris>();
                    source.debrisList[i].children.Add (targetDebris);
                }
            }
            
            // Copy dust
            if (source.HasDust == true)
            {
                for (int i = 0; i < source.dustList.Count; i++)
                {
                    RayfireDust targetDust = target.gameObject.AddComponent<RayfireDust>();
                    targetDust.CopyFrom (source.dustList[i]);

                    if (source.dustList[i].children == null)
                        source.dustList[i].children = new List<RayfireDust>();
                    source.dustList[i].children.Add (targetDust);
                }
            }
        }
        
        // Copy debris and dust
        public static void CopyRootMeshParticles(RayfireRigid source, List<RayfireRigid> targets)
        {
            // Copy debris. only initialized debris in this list
            if (source.HasDebris == true)
            {
                for (int d = 0; d < source.debrisList.Count; d++)
                {
                    // Set max amount
                    int maxAmount = targets.Count;
                    if (source.debrisList[d].limitations.percentage < 100)
                        maxAmount = targets.Count * source.debrisList[d].limitations.percentage / 100;

                    // Copy component
                    for (int i = 0; i < targets.Count; i++)
                    {
                        // Max amount reached
                        if (maxAmount <= 0)
                            break;

                        // TODO consider size threshold

                        // Filter by percentage
                        if (Random.Range (0, 100) > source.debrisList[d].limitations.percentage)
                            continue;

                        // Copy
                        RayfireDebris targetDebris = targets[i].gameObject.AddComponent<RayfireDebris>();
                        targetDebris.CopyFrom (source.debrisList[d]);
                        targetDebris.rigid = targets[i];
                        
                        // Collect debris for Rigid
                        if (targets[i].debrisList == null)
                            targets[i].debrisList = new List<RayfireDebris>();
                        targets[i].debrisList.Add (targetDebris);
                        
                        // Collect debris for parent debris
                        if (source.debrisList[d].children == null)
                            source.debrisList[d].children = new List<RayfireDebris>();
                        source.debrisList[d].children.Add (targetDebris);
                        
                        maxAmount--;
                    }
                    
                    // Get amount list
                    SetDebrisFinalAmount (source.debrisList[d].children, source.debrisList[d].emission.burstType, source.debrisList[d].emission.burstAmount);
                }
            }
            
            // Copy dust
            if (source.HasDust == true)
            {
                for (int d = 0; d < source.dustList.Count; d++)
                {
                    // Set max amount
                    int maxAmount = targets.Count;
                    if (source.dustList[d].limitations.percentage < 100)
                        maxAmount = targets.Count * source.dustList[d].limitations.percentage / 100;

                    for (int i = 0; i < targets.Count; i++)
                    {
                        // Max amount reached
                        if (maxAmount <= 0)
                            break;

                        // Filter by percentage
                        if (Random.Range (0, 100) > source.dustList[d].limitations.percentage)
                            continue;

                        // Copy
                        RayfireDust targetDust = targets[i].gameObject.AddComponent<RayfireDust>();
                        targetDust.CopyFrom (source.dustList[d]);
                        targetDust.rigid = targets[i];
                        
                        // Collect debris for Rigid
                        if (targets[i].dustList == null)
                            targets[i].dustList = new List<RayfireDust>();
                        targets[i].dustList.Add (targetDust);
                        
                        // Collect debris for parent debris
                        if (source.dustList[d].children == null)
                            source.dustList[d].children = new List<RayfireDust>();
                        source.dustList[d].children.Add (targetDust);
                        
                        maxAmount--;
                    }
                    
                    // Get amount list
                    SetDustFinalAmount (source.dustList[d].children, source.dustList[d].emission.burstType, source.dustList[d].emission.burstAmount);
                }
            }
        }
        
        /// /////////////////////////////////////////////////////////
        /// Main Module
        /// /////////////////////////////////////////////////////////

        // Set main module
        public static void SetMain (ParticleSystem.MainModule main, 
            float lifeMin, float lifeMax, 
            float sizeMin, float sizeMax, 
            float gravityMin, float gravityMax, 
            float speedMin, float speedMax,
            float divergence, int maxParticles,
            float duration)
        {
            main.duration = duration;
            main.loop = false;
            main.simulationSpace = ParticleSystemSimulationSpace.World;
            main.maxParticles = maxParticles;
            main.emitterVelocityMode = ParticleSystemEmitterVelocityMode.Transform;

            // Curve variation
            // main.startDelay = new ParticleSystem.MinMaxCurve(0.0f, 0.1f);
            main.startLifetime = new ParticleSystem.MinMaxCurve(lifeMin, lifeMax);
            main.startSpeed = new ParticleSystem.MinMaxCurve(speedMin, speedMax);
            main.startSize = new ParticleSystem.MinMaxCurve(sizeMin, sizeMax);
            main.startRotation = new ParticleSystem.MinMaxCurve(-divergence, divergence); // Max 6.25f = 360 degree
            main.gravityModifier = new ParticleSystem.MinMaxCurve(gravityMin, gravityMax);
        }

        /// /////////////////////////////////////////////////////////
        /// Emission
        /// /////////////////////////////////////////////////////////

        // Set emission
        public static void SetEmission(ParticleSystem.EmissionModule emissionModule, float distanceRate, int burstAmount)
        {
            emissionModule.enabled = true;
            emissionModule.rateOverTimeMultiplier = 0f;
            emissionModule.rateOverDistanceMultiplier = distanceRate;

            // Set burst
            if (burstAmount > 0)
            {
                ParticleSystem.Burst burst = new ParticleSystem.Burst(0.0f, (short)burstAmount, (short)burstAmount, 1, 999f);
                ParticleSystem.Burst[] bursts = new [] { burst };
                emissionModule.SetBursts(bursts);
            }
        }

        /// /////////////////////////////////////////////////////////
        /// Emission
        /// /////////////////////////////////////////////////////////

        // Set emitter mesh shape
        public static void SetShapeMesh (ParticleSystem.ShapeModule shapeModule, Mesh mesh, int emitMatIndex, Vector3 shapeScale)
        {
            shapeModule.normalOffset = 0f;
            shapeModule.shapeType = ParticleSystemShapeType.Mesh;
            shapeModule.meshShapeType = ParticleSystemMeshShapeType.Triangle;
            shapeModule.mesh = mesh;
            shapeModule.scale = shapeScale;
            
            // Emit from inner surface
            if (emitMatIndex > 0)
            {
                shapeModule.useMeshMaterialIndex = true;
                shapeModule.meshMaterialIndex = emitMatIndex;
            }
        }

        // Set emitter mesh shape
        public static void SetShapeObject(ParticleSystem.ShapeModule shapeModule)
        {
            shapeModule.shapeType = ParticleSystemShapeType.Hemisphere;
            shapeModule.radius = 0.2f;
            shapeModule.radiusThickness = 0f;
        }

        // Set emitter mesh shape
        public static int GetEmissionMatIndex(Renderer renderer, Material mat)
        {
            if (mat != null)
                for (int i = 0; i < renderer.sharedMaterials.Length; i++)
                      if (renderer.sharedMaterials[i] == mat)
                          return i;
            return -1;
        }
        
        /// /////////////////////////////////////////////////////////
        /// Velocity
        /// /////////////////////////////////////////////////////////

        // Set velocity
        public static void SetVelocity(ParticleSystem.InheritVelocityModule velocity, RFParticleDynamicDebris dynamic)
        {
            if (dynamic.velocityMin > 0 || dynamic.velocityMax > 0)
            {
                velocity.enabled = true;
                velocity.mode    = ParticleSystemInheritVelocityMode.Initial;
                velocity.curve   = new ParticleSystem.MinMaxCurve (dynamic.velocityMin, dynamic.velocityMax);
            }
        }

        /// /////////////////////////////////////////////////////////
        /// Size Over Life Time
        /// /////////////////////////////////////////////////////////

        // Set size over life time. same axis. Increase almost instantly particles after birth
        public static void SetSizeOverLifeTime(ParticleSystem.SizeOverLifetimeModule sizeOverLifeTime, float size)
        {
            sizeOverLifeTime.enabled = true;
            sizeOverLifeTime.size = GetCurveSizeOverLifeTime(size);
        }

        // Set size over life time. different axis. Increase almost instantly particles after birth
        public static void SetSizeOverLifeTime(ParticleSystem.SizeOverLifetimeModule sizeOverLifeTime, Vector3 size)
        {
            sizeOverLifeTime.enabled = true;
            sizeOverLifeTime.separateAxes = true;
            sizeOverLifeTime.x = GetCurveSizeOverLifeTime(size.x);
            sizeOverLifeTime.y = GetCurveSizeOverLifeTime(size.y);
            sizeOverLifeTime.z = GetCurveSizeOverLifeTime(size.z);
        }
        
        // Get Curve for Size Over Life Time
        public static ParticleSystem.MinMaxCurve GetCurveSizeOverLifeTime(float val)
        {
            Keyframe[] keysSize = new Keyframe[4];
            keysSize[0] = new Keyframe(0f, 0f);
            keysSize[1] = new Keyframe(0.01f, val);
            keysSize[2] = new Keyframe(0.95f, val);
            keysSize[3] = new Keyframe(1f, 0f);

            AnimationCurve curveSize = new AnimationCurve(keysSize);
            ParticleSystem.MinMaxCurve curveSizeTime = new ParticleSystem.MinMaxCurve(1f, curveSize);
            return curveSizeTime;
        }

        /// /////////////////////////////////////////////////////////
        /// Rotation by Speed
        /// /////////////////////////////////////////////////////////

        // Set Rotation by Speed
        public static void SetRotationBySpeed(ParticleSystem.RotationBySpeedModule rotationBySpeed, float rotationSpeed)
        {
            if (rotationSpeed > 0f)
            {
                rotationBySpeed.enabled = true;
                rotationBySpeed.range   = new Vector2 (1f, 0f);
                rotationBySpeed.z       = GetCurveRotationBySpeed (rotationSpeed);
            }
        }

        // Get Curve for Rotation by Speed
        public static ParticleSystem.MinMaxCurve GetCurveRotationBySpeed(float rotationSpeed)
        {
            // Value 1f = 57 degrees
            float maxVal = rotationSpeed * 40f;
            
            // Max curve
            Keyframe[] keys = new Keyframe[2];
            keys[0] = new Keyframe(0f, maxVal);
            keys[1] = new Keyframe(0.5f, 0f);
            AnimationCurve curveMax = new AnimationCurve (keys);
            
            // Min curve
            keys[0] = new Keyframe(0f, -maxVal);
            AnimationCurve curveMin = new AnimationCurve (keys);
            
            return new ParticleSystem.MinMaxCurve(1f, curveMin, curveMax);
        }

        /// /////////////////////////////////////////////////////////
        /// Color Over Lifetime
        /// /////////////////////////////////////////////////////////

        // Set color over life time
        public static void SetColorOverLife(ParticleSystem.ColorOverLifetimeModule colorLife, float opacity)
        {
            colorLife.enabled = true;
            GradientAlphaKey[] alphaKeys = new GradientAlphaKey[4];
            alphaKeys[0] = new GradientAlphaKey (0f,      0f);
            alphaKeys[1] = new GradientAlphaKey (opacity, 0.1f);
            alphaKeys[2] = new GradientAlphaKey (opacity, 0.2f);
            alphaKeys[3] = new GradientAlphaKey (0f,      1f);
            Gradient gradient = new Gradient();
            gradient.alphaKeys = alphaKeys;
            colorLife.color    = new ParticleSystem.MinMaxGradient (gradient);
        }

        /// /////////////////////////////////////////////////////////
        /// Noise
        /// /////////////////////////////////////////////////////////

        // Set particle system noise
        public static void SetNoise (ParticleSystem.NoiseModule psNoise, RFParticleNoise scrNoise)
        {
            if (scrNoise.enabled == true)
            {
                psNoise.enabled     = true;
                psNoise.strength    = new ParticleSystem.MinMaxCurve (scrNoise.strengthMin, scrNoise.strengthMax);
                psNoise.frequency   = scrNoise.frequency;
                psNoise.scrollSpeed = scrNoise.scrollSpeed;
                psNoise.damping     = scrNoise.damping;
                psNoise.quality     = scrNoise.quality;


                psNoise.separateAxes = true;
                psNoise.strengthX = new ParticleSystem.MinMaxCurve (scrNoise.strengthMin, scrNoise.strengthMax);
                psNoise.strengthY = new ParticleSystem.MinMaxCurve (scrNoise.strengthMin * 0.3f, scrNoise.strengthMax * 0.3f);
                psNoise.strengthZ = new ParticleSystem.MinMaxCurve (scrNoise.strengthMin, scrNoise.strengthMax);
            }
        }

        /// /////////////////////////////////////////////////////////
        /// Collision
        /// /////////////////////////////////////////////////////////

        // Set collision for debris
        public static void SetCollisionDebris (ParticleSystem.CollisionModule psCollision, RFParticleCollisionDebris coll) {
            psCollision.enabled      = true;
            psCollision.type         = ParticleSystemCollisionType.World;
            psCollision.collidesWith = (int)coll.collidesWith - 1;
            psCollision.quality      = coll.quality;
            psCollision.radiusScale  = coll.radiusScale;
            psCollision.dampen       = new ParticleSystem.MinMaxCurve (coll.dampenMin, coll.dampenMax);
            psCollision.bounce       = new ParticleSystem.MinMaxCurve (coll.bounceMin, coll.bounceMax);

            //psCollision.enableDynamicColliders = false;
            //psCollision.lifetimeLoss = new ParticleSystem.MinMaxCurve(1f);
        }

        // Set collision for dust
        public static void SetCollisionDust (ParticleSystem.CollisionModule psCollision, RFParticleCollisionDust coll) {
            psCollision.enabled                = true;
            psCollision.type                   = ParticleSystemCollisionType.World;
            psCollision.collidesWith           = (int)coll.collidesWith - 1;
            psCollision.quality                = coll.quality;
            psCollision.radiusScale            = coll.radiusScale;
            psCollision.dampenMultiplier       = 0f;
            psCollision.bounceMultiplier       = 0f;
            psCollision.enableDynamicColliders = false;
        }
        
        /// /////////////////////////////////////////////////////////
        /// Common
        /// /////////////////////////////////////////////////////////
        
        // Get debris hosts
        public static List<RayfireDebris> GetDebrisTargets (List<RayfireDebris> targets, float sizeThreshold, int percentage, int pType)
        {
            // Filter particle sources
            List<RayfireDebris> particleHosts = new List<RayfireDebris>();

            // Set max amount
            int maxAmount = targets.Count;
            if (percentage < 100)
                maxAmount = targets.Count * percentage / 100;

            // Collect hosts list
            for (int i = 0; i < targets.Count; i++)
            {
                // Max amount reached
                if (particleHosts.Count >= maxAmount)
                    break;
                
                // Filter by size threshold
                if (targets[i].rigid.limitations.bboxSize < sizeThreshold)
                    continue;

                // Filter by percentage
                if (Random.Range(0, 100) > percentage)
                    continue;

                // Collect particle hosts
                particleHosts.Add(targets[i]);
            }
            return particleHosts;
        }
        
        // Get debris hosts
        public static List<RayfireDust> GetDustTargets (List<RayfireDust> targets, float sizeThreshold, int percentage, int pType)
        {
            // Filter particle sources
            List<RayfireDust> particleHosts = new List<RayfireDust>();

            // Set max amount
            int maxAmount = targets.Count;
            if (percentage < 100)
                maxAmount = targets.Count * percentage / 100;

            // Collect hosts list
            for (int i = 0; i < targets.Count; i++)
            {
                // Max amount reached
                if (particleHosts.Count >= maxAmount)
                    break;

                // Filter by size threshold
                if (targets[i].rigid.limitations.bboxSize < sizeThreshold)
                    continue;

                // Filter by percentage
                if (Random.Range(0, 100) > percentage)
                    continue;

                // Collect particle hosts
                particleHosts.Add(targets[i]);
            }
            return particleHosts;
        }

        /// /////////////////////////////////////////////////////////
        /// Copy
        /// /////////////////////////////////////////////////////////
        
        // Get amount list
        public static void SetDebrisFinalAmount(List<RayfireDebris> targets, BurstType burstType, int burstAmount)
        {
            // No burst
            if (burstType == BurstType.None)
                for (int i = 0; i < targets.Count; i++)
                    targets[i].amountFinal = 0;

            // Same burst amount for every fragment
            if (burstType == BurstType.FragmentAmount)
                for (int i = 0; i < targets.Count; i++)
                    targets[i].amountFinal = burstAmount;

            // Burst amount per particles per fragment size
            else if (burstType == BurstType.PerOneUnitSize)
                for (int i = 0; i < targets.Count; i++)
                    targets[i].amountFinal = (int)(burstAmount * targets[i].rigid.limitations.bboxSize);
            
            // Burst amount by total amount divided among hosts by their amount and size
            else if (burstType == BurstType.TotalAmount)
            {
                // Get sum of all sizes
                float totalSize = 0f;
                for (int i = 0; i < targets.Count; i++)
                    totalSize += targets[i].rigid.limitations.bboxSize;

                // Get size per particle
                float sizePerParticle = totalSize / burstAmount;
               
                // Get size for every host by it's size
                for (int i = 0; i < targets.Count; i++)
                    targets[i].amountFinal = (int)(targets[i].rigid.limitations.bboxSize / sizePerParticle);
            }
        }
        
        // Get amount list
        public static void SetDustFinalAmount(List<RayfireDust> targets, BurstType burstType, int burstAmount)
        {
            // No burst
            if (burstType == BurstType.None)
                for (int i = 0; i < targets.Count; i++)
                    targets[i].amountFinal = 0;

            // Same burst amount for every fragment
            if (burstType == BurstType.FragmentAmount)
                for (int i = 0; i < targets.Count; i++)
                    targets[i].amountFinal = burstAmount;

            // Burst amount per particles per fragment size
            else if (burstType == BurstType.PerOneUnitSize)
                for (int i = 0; i < targets.Count; i++)
                    targets[i].amountFinal = (int)(burstAmount * targets[i].rigid.limitations.bboxSize);
            
            // Burst amount by total amount divided among hosts by their amount and size
            else if (burstType == BurstType.TotalAmount)
            {
                // Get sum of all sizes
                float totalSize = 0f;
                for (int i = 0; i < targets.Count; i++)
                    totalSize += targets[i].rigid.limitations.bboxSize;

                // Get size per particle
                float sizePerParticle = totalSize / burstAmount;
               
                // Get size for every host by it's size
                for (int i = 0; i < targets.Count; i++)
                    targets[i].amountFinal = (int)(targets[i].rigid.limitations.bboxSize / sizePerParticle);
            }
        }

        // Check for positive amount
        public static bool AmountCheck(RayfireRigid source, int pType)
        {
            // Check debris burst amount
            if (pType == 0)
            {
                for (int i = 0; i < source.debrisList.Count; i++)
                {
                    if (source.debrisList[i].emission.burstType == BurstType.None && source.debrisList[i].emission.distanceRate == 0)
                    {
                        Debug.Log (source.name + " has debris enabled but has no amount");
                        return false;
                    }
                }
            }

            // Check dust burst amount
            if (pType == 1)
            {
                for (int i = 0; i < source.dustList.Count; i++)
                {
                    if (source.dustList[i].emission.burstType == BurstType.None && source.dustList[i].emission.distanceRate == 0)
                    {
                        Debug.Log (source.name + " has dust enabled but has no amount");
                        return false;
                    }
                }
            }

            return true;
        }
    }
}


