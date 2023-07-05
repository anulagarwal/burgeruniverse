﻿using MalbersAnimations.Scriptables;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace MalbersAnimations.Controller
{
    [CustomEditor(typeof(MAnimal)), CanEditMultipleObjects]
    public class MAnimalEditor : Editor 
    {
        public static GUIStyle StyleGray => MTools.Style(new Color(0.5f, 0.5f, 0.5f, 0.3f));
        public static GUIStyle StyleBlue => MTools.Style(new Color(0, 0.5f, 1f, 0.3f));


        private List<Type> StatesType = new List<Type>();
        private ReorderableList Reo_List_States;
      //  private ReorderableList Reo_List_Pivots;
        private ReorderableList Reo_List_Modes;
        private ReorderableList Reo_List_Speeds;
        private Dictionary<string, ReorderableList> innerListDict = new Dictionary<string, ReorderableList>();


        SerializedProperty
            S_StateList, S_PivotsList, Height, S_Mode_List, Editor_Tabs1, Editor_Tabs2, StartWithMode, OnEnterExitStances, OnEnterExitStates, RB, Anim,
            m_Vertical, m_Horizontal, m_IDFloat, m_ModeStatus, m_State, m_StateStatus, m_LastState, m_Mode, m_Grounded, m_Movement, m_Random, m_MdeFloat,
            m_SpeedMultiplier, m_UpDown, currentStance, defaultStance, m_Stance, m_Slope, m_Type, m_StateTime, m_DeltaAngle, m_StrafeString,
            lockInput, lockMovement, Rotator, AlignLoop, animalType, RayCastRadius, MainCamera, sleep, m_gravityTime, RootBone,

             m_CanStrafe, m_strafe, OnStrafe,


            alwaysForward,  AnimatorSpeed, OnMovementLocked, OnInputLocked, OnSprintEnabled, OnGrounded, OnStanceChange, OnStateChange, OnModeStart, OnModeEnd,
            OnSpeedChange, OnAnimationChange, GroundLayer, maxAngleSlope, AlignPosLerp, AlignRotLerp, m_gravity, m_gravityPower, useCameraUp, ground_Changes_Gravity,
             useSprintGlobal,  SmoothVertical, TurnMultiplier, UpDownLerp, Player, OverrideStartState, CloneStates, S_Speed_List, UseCameraInput,
            LockUpDownMovement, LockHorizontalMovement, LockForwardMovement;



        //EditorStuff
        SerializedProperty 
            ShowMovement, ShowAnimParametersOptional, ShowAnimParameters, SelectedMode, ModeShowEvents,showPivots, ShowGround,
            ShowLockInputs, ShowMisc, showReferences, showGravity, showExposedVariables;

        MAnimal m;
       // private MonoScript script;
        private GenericMenu addMenu;

        private void FindSerializedProperties()
        {
            S_PivotsList = serializedObject.FindProperty("pivots");
            sleep = serializedObject.FindProperty("sleep");
            S_Mode_List = serializedObject.FindProperty("modes");

            ground_Changes_Gravity = serializedObject.FindProperty("ground_Changes_Gravity");

            SelectedMode = serializedObject.FindProperty("SelectedMode");
            ShowMovement = serializedObject.FindProperty("ShowMovement");
            ShowGround = serializedObject.FindProperty("ShowGround");
            showExposedVariables = serializedObject.FindProperty("showExposedVariables");
            ShowMisc = serializedObject.FindProperty("ShowMisc");
            showReferences = serializedObject.FindProperty("showReferences");
            showGravity = serializedObject.FindProperty("showGravity");

            m_CanStrafe = serializedObject.FindProperty("m_CanStrafe");
            m_strafe = serializedObject.FindProperty("m_strafe");
            OnStrafe = serializedObject.FindProperty("OnStrafe");

            //  rootMotion = serializedObject.FindProperty("rootMotion");
            //  m_sprint = serializedObject.FindProperty("sprint");
            //  grounded = serializedObject.FindProperty("grounded");


            alwaysForward = serializedObject.FindProperty("alwaysForward");

            MainCamera = serializedObject.FindProperty("m_MainCamera");
            ShowAnimParametersOptional = serializedObject.FindProperty("ShowAnimParametersOptional");
            ShowAnimParameters = serializedObject.FindProperty("ShowAnimParameters");

            S_Speed_List = serializedObject.FindProperty("speedSets");

            currentStance = serializedObject.FindProperty("currentStance");
            defaultStance = serializedObject.FindProperty("defaultStance");

          
            RB = serializedObject.FindProperty("RB");
            Anim = serializedObject.FindProperty("Anim");

            UseCameraInput = serializedObject.FindProperty("useCameraInput");
            useCameraUp = serializedObject.FindProperty("useCameraUp");
            StartWithMode = serializedObject.FindProperty("StartWithMode");

            OnEnterExitStates = serializedObject.FindProperty("OnEnterExitStates");
            OnEnterExitStances = serializedObject.FindProperty("OnEnterExitStances");
            ModeShowEvents = serializedObject.FindProperty("ModeShowEvents");

            Height = serializedObject.FindProperty("height");
           // ModeIndexSelected = serializedObject.FindProperty("ModeIndexSelected");

            Editor_Tabs1 = serializedObject.FindProperty("Editor_Tabs1");
            Editor_Tabs2 = serializedObject.FindProperty("Editor_Tabs2");

            m_Vertical = serializedObject.FindProperty("m_Vertical");
           // Center = serializedObject.FindProperty("Center");
            m_Horizontal = serializedObject.FindProperty("m_Horizontal");
            m_IDFloat = serializedObject.FindProperty("m_StateFloat");
            m_ModeStatus = serializedObject.FindProperty("m_ModeStatus");
            m_State = serializedObject.FindProperty("m_State");
            m_StateStatus = serializedObject.FindProperty("m_StateStatus");
            m_LastState = serializedObject.FindProperty("m_LastState");
            m_Mode = serializedObject.FindProperty("m_Mode");
            m_Grounded = serializedObject.FindProperty("m_Grounded");
            m_Movement = serializedObject.FindProperty("m_Movement");
            m_Random = serializedObject.FindProperty("m_Random");
            m_MdeFloat = serializedObject.FindProperty("m_ModePower");
            m_StrafeString = serializedObject.FindProperty("m_Strafe");
            m_SpeedMultiplier = serializedObject.FindProperty("m_SpeedMultiplier");
            m_UpDown = serializedObject.FindProperty("m_UpDown");
            m_Stance = serializedObject.FindProperty("m_Stance");
            m_Slope = serializedObject.FindProperty("m_Slope");
            m_Type = serializedObject.FindProperty("m_Type");
            m_StateTime = serializedObject.FindProperty("m_StateTime");
            m_DeltaAngle = serializedObject.FindProperty("m_DeltaAngle");
            lockInput = serializedObject.FindProperty("lockInput");
            lockMovement = serializedObject.FindProperty("lockMovement");
            Rotator = serializedObject.FindProperty("Rotator");
            animalType = serializedObject.FindProperty("animalType");
            RayCastRadius = serializedObject.FindProperty("rayCastRadius");
            AlignLoop = serializedObject.FindProperty("AlignLoop"); 
            AnimatorSpeed = serializedObject.FindProperty("AnimatorSpeed");

            LockForwardMovement = serializedObject.FindProperty("lockForwardMovement");
            LockHorizontalMovement = serializedObject.FindProperty("lockHorizontalMovement");
            LockUpDownMovement = serializedObject.FindProperty("lockUpDownMovement");

            OnMovementLocked = serializedObject.FindProperty("OnMovementLocked");
            OnInputLocked = serializedObject.FindProperty("OnInputLocked");
            OnSprintEnabled = serializedObject.FindProperty("OnSprintEnabled");
            OnGrounded = serializedObject.FindProperty("OnGrounded");
            OnStanceChange = serializedObject.FindProperty("OnStanceChange");
            OnStateChange = serializedObject.FindProperty("OnStateChange");
            OnModeStart = serializedObject.FindProperty("OnModeStart");
            OnModeEnd = serializedObject.FindProperty("OnModeEnd"); 
            OnSpeedChange = serializedObject.FindProperty("OnSpeedChange");
            OnAnimationChange = serializedObject.FindProperty("OnAnimationChange");
            showPivots = serializedObject.FindProperty("showPivots");
           // ShowpivotColor = serializedObject.FindProperty("ShowpivotColor");
            GroundLayer = serializedObject.FindProperty("groundLayer");
            maxAngleSlope = serializedObject.FindProperty("maxAngleSlope");
            AlignPosLerp = serializedObject.FindProperty("AlignPosLerp");
            AlignRotLerp = serializedObject.FindProperty("AlignRotLerp");

            m_gravity = serializedObject.FindProperty("m_gravityDir");
            m_gravityPower = serializedObject.FindProperty("m_gravityPower");
            m_gravityTime = serializedObject.FindProperty("m_gravityTime");

            useSprintGlobal = serializedObject.FindProperty("useSprintGlobal");
            SmoothVertical = serializedObject.FindProperty("SmoothVertical");
            TurnMultiplier = serializedObject.FindProperty("TurnMultiplier");
            UpDownLerp = serializedObject.FindProperty("UpDownLerp");
            Player = serializedObject.FindProperty("isPlayer");
            OverrideStartState = serializedObject.FindProperty("OverrideStartState");
            CloneStates = serializedObject.FindProperty("CloneStates");
            RootBone = serializedObject.FindProperty("RootBone");


            ShowLockInputs = serializedObject.FindProperty("ShowLockInputs");
        }

        private void OnEnable()
        {
            m = (MAnimal)target;
          //  script = MonoScript.FromMonoBehaviour(target as MonoBehaviour);
            FindSerializedProperties();


            StatesType.Clear();
            StatesType = MTools.GetAllTypes<State>();

            S_StateList = serializedObject.FindProperty("states");
           
            Reo_List_States = new ReorderableList(serializedObject, S_StateList, true, true, false, false)
            {
                drawElementCallback = Draw_Element_State,
                onReorderCallback = OnReorderCallback_States,
                drawHeaderCallback = Draw_Header_State,
            };

            Reo_List_Modes = new ReorderableList(serializedObject, S_Mode_List, true, true, true, true)
            {
                drawElementCallback = Draw_Element_Modes,
                drawHeaderCallback = Draw_Header_Modes,
                onAddCallback = OnAdd_Modes,
                onRemoveCallback = OnRemoveCallback_Mode, 
                onSelectCallback = Selected_Mode,
            };

            Reo_List_Speeds = new ReorderableList(serializedObject, S_Speed_List, true, true, false, false)
            {
                drawElementCallback = Draw_Element_Speed,
                drawHeaderCallback = Draw_Header_Speed,
            };
        }

        private void Selected_Mode(ReorderableList list)
        {
            SelectedMode.intValue = list.index;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            var style = (Application.isPlaying && m.Sleep) ? StyleGray : StyleBlue;

            var descri = "Animal Controller" +( (Application.isPlaying && m.Sleep) ? " [SLEEP]" : string.Empty);

            EditorGUILayout.BeginVertical(style);;
            EditorGUILayout.HelpBox(descri, MessageType.None);
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(MalbersEditor.StyleGray);
            {
               // MalbersEditor.DrawScript(script);

                Editor_Tabs1.intValue = GUILayout.Toolbar(Editor_Tabs1.intValue, new string[] { "General", "Speeds", "States", "Modes" });
                if (Editor_Tabs1.intValue != 4) Editor_Tabs2.intValue = 4;

                Editor_Tabs2.intValue = GUILayout.Toolbar(Editor_Tabs2.intValue, new string[] { "Advanced", "Stances", "Events", "Debug" });
                if (Editor_Tabs2.intValue != 4) Editor_Tabs1.intValue = 4;

                //First Tabs
                int Selection = Editor_Tabs1.intValue;
                if (Selection == 0) ShowGeneral();
                else if (Selection == 1) ShowSpeeds();
                else if (Selection == 2) ShowStates();
                else if (Selection == 3) ShowModes();


                //2nd Tabs
                Selection = Editor_Tabs2.intValue;
                if (Selection == 0) ShowAdvanced();
                else if (Selection == 1) ShowStances();
                else if (Selection == 2) ShowEvents();
                else if (Selection == 3) ShowDebug();

            }
            EditorGUILayout.EndVertical();


            serializedObject.ApplyModifiedProperties();
        }

        private void ShowStances()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            {
                EditorGUILayout.LabelField("Stances", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(defaultStance, new GUIContent("Default Stance", "Default Stance ID to reset to when the animal exit an Stance"));
                EditorGUI.BeginChangeCheck();

                EditorGUILayout.PropertyField(currentStance, new GUIContent("Current Stance", "Current Stance ID the animal is on"));
                if (EditorGUI.EndChangeCheck())
                {
                    m.Stance = currentStance.intValue;
                }


                MalbersEditor.Arrays(OnEnterExitStances, new GUIContent("On Enter / Exit Stance Events"));
            }
            EditorGUILayout.EndVertical();
        }

        public void DropAreaGUI()
        {
            Event evt = Event.current;
            Rect drop_area = GUILayoutUtility.GetRect(0f,20.0f, GUILayout.ExpandWidth(true));
            GUI.Box(drop_area, "> Drag created States here < ");

            switch (evt.type)
            {
                case EventType.DragUpdated:
                    // ... change whether or not the drag *can* be performed by changing the visual mode of the cursor based on the IsDragValid function.
                    DragAndDrop.visualMode = IsDragValid() ? DragAndDropVisualMode.Generic : DragAndDropVisualMode.Rejected;
                    break;
                case EventType.DragPerform:
                    if (!drop_area.Contains(evt.mousePosition))
                        return;

                    DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

                    if (evt.type == EventType.DragPerform)
                    {
                        DragAndDrop.AcceptDrag();

                        foreach (UnityEngine.Object dragged_object in DragAndDrop.objectReferences)
                        {
                            if (dragged_object is State)
                            {
                                State newState = dragged_object as State;

                                if (m.states.Contains(newState))continue;
                             
                                EditorUtility.SetDirty(m);
                                S_StateList.AddToObjectArray(newState);
                                EditorUtility.SetDirty(newState);
                            }
                        }
                    }
                    break;
            }
        }

        private bool IsDragValid()
        {
            // Go through all the objects being dragged...
            for (int i = 0; i < DragAndDrop.objectReferences.Length; i++)
            {
                // ... and if any of them are not script assets, return that the drag is invalid.
                if (DragAndDrop.objectReferences[i].GetType().BaseType != typeof(State))  
                return false;
            }

            // If none of the dragging objects returned that the drag was invalid, return that it is valid.
            return true;
        }

        private void ShowAdvanced()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            MalbersEditor.Foldout(showReferences, "References", true);

            if (showReferences.boolValue)
            {
                EditorGUILayout.PropertyField(Anim, new GUIContent("Animator"));
                EditorGUILayout.PropertyField(RB, new GUIContent("RigidBody"));
                EditorGUILayout.PropertyField(MainCamera, new GUIContent("Main Camera"));
            }
          
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            
            MalbersEditor.Foldout(ShowMisc, "Misc", true);

            if (ShowMisc.boolValue)
            {
                EditorGUILayout.PropertyField(animalType, G_animalType);
                EditorGUILayout.PropertyField(Rotator, G_Rotator);
                EditorGUILayout.PropertyField(RootBone, G_RootBone);
            }
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            MalbersEditor.Foldout(ShowLockInputs, "Lock Inputs", true);
             
            if (ShowLockInputs.boolValue)
            {
                EditorGUILayout.PropertyField(sleep, new GUIContent("Sleep", "Disable internally the Controller wihout disabling the component"));
                EditorGUILayout.PropertyField(lockInput, G_LockInput);
                EditorGUILayout.PropertyField(lockMovement, G_lockMovement);
                EditorGUILayout.PropertyField(LockForwardMovement, new GUIContent("Lock Forward"));
                EditorGUILayout.PropertyField(LockHorizontalMovement, new GUIContent("Lock Horizontal"));
                EditorGUILayout.PropertyField(LockUpDownMovement, new GUIContent("Lock UpDown"));
            }
                EditorGUILayout.EndVertical();
            ShowAnimParam();
        }


      

        private void ShowAnimParam()
        {
         
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            MalbersEditor.Foldout(ShowAnimParameters, "Main Animator Parameters", true);
            if (ShowAnimParameters.boolValue)
            {
                EditorGUILayout.PropertyField(m_Vertical);
                EditorGUILayout.PropertyField(m_Horizontal);
                EditorGUILayout.Space();
                EditorGUILayout.PropertyField(m_State);
                EditorGUILayout.PropertyField(m_LastState);
                EditorGUILayout.PropertyField(m_StateStatus);
                EditorGUILayout.PropertyField(m_IDFloat);
                EditorGUILayout.Space();
                EditorGUILayout.PropertyField(m_Mode);
                EditorGUILayout.PropertyField(m_ModeStatus);
                EditorGUILayout.Space();
                EditorGUILayout.PropertyField(m_Grounded);
                EditorGUILayout.PropertyField(m_Movement);
                EditorGUILayout.PropertyField(m_SpeedMultiplier);
            }
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            {
                MalbersEditor.Foldout(ShowAnimParametersOptional, "Optional Animator Parameters", true);
                if (ShowAnimParametersOptional.boolValue)
                {
                    EditorGUILayout.PropertyField(m_UpDown);
                    EditorGUILayout.PropertyField(m_Stance);
                    EditorGUILayout.PropertyField(m_Slope);
                    EditorGUILayout.PropertyField(m_Type);
                    EditorGUILayout.PropertyField(m_StateTime);
                    EditorGUILayout.PropertyField(m_DeltaAngle);
                    EditorGUILayout.PropertyField(m_Random);
                    EditorGUILayout.PropertyField(m_MdeFloat);
                    EditorGUILayout.PropertyField(m_StrafeString);
                }
            }
            EditorGUILayout.EndVertical();
        }

        private void ShowEvents()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField("Events", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(OnMovementLocked);
            EditorGUILayout.PropertyField(OnInputLocked);
            EditorGUILayout.PropertyField(OnSprintEnabled, new GUIContent("On Sprint"));
            EditorGUILayout.PropertyField(OnGrounded);
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(OnStanceChange);
            EditorGUILayout.PropertyField(OnStateChange);
            EditorGUILayout.PropertyField(OnStrafe);
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(OnModeStart);
            EditorGUILayout.PropertyField(OnModeEnd);
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(OnSpeedChange);
            EditorGUILayout.PropertyField(OnAnimationChange);
            EditorGUILayout.EndVertical();
        }

        private void ShowDebug()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            var Deb = serializedObject.FindProperty("debugStates");
            var DebM = serializedObject.FindProperty("debugModes");
            var DebG = serializedObject.FindProperty("debugGizmos");

            EditorGUILayout.PropertyField(Deb, new GUIContent("Debug States"));
            EditorGUILayout.PropertyField(DebM, new GUIContent("Debug Modes"));
            EditorGUILayout.PropertyField(DebG, new GUIContent("Debug Gizmos"));
            EditorGUILayout.PropertyField(showPivots);

            if (Application.isPlaying)
            {
                EditorGUI.BeginDisabledGroup(true);

                if (m.ActiveState)
                {
                    EditorGUILayout.Space();
                    EditorGUILayout.ToggleLeft($"ActiveState [{m.ActiveState.ID.name}] - [CanExit] ", m.ActiveState.CanExit);
                   // EditorGUILayout.ObjectField("Queued State", m.StateQueued != null ? m.StateQueued.ID : null, typeof(StateID), true);
                    EditorGUILayout.Space();
                }

                EditorGUILayout.LabelField("RB Horiz Speed: " + m.HorizontalSpeed.ToString("F4"));
                EditorGUILayout.LabelField("Active Speed Modif : " + m.CurrentSpeedModifier.name);
                EditorGUILayout.LabelField("Active Speed Index: " + m.CurrentSpeedIndex);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("platform"));
                EditorGUILayout.ToggleLeft("RootMotion", m.RootMotion);
                EditorGUILayout.ToggleLeft("Grounded", m.Grounded);
                EditorGUILayout.Space();
                EditorGUILayout.ToggleLeft("Front Ray", m.FrontRay);
                EditorGUILayout.ToggleLeft("Hip Ray", m.MainRay);
                EditorGUILayout.Space();
                EditorGUILayout.ToggleLeft("Orient To Ground", m.UseOrientToGround);
                EditorGUILayout.ToggleLeft("Use Custom Rotation", m.UseCustomAlign);
                EditorGUILayout.Space();
                EditorGUILayout.Vector3Field("External Force", m.CurrentExternalForce);
                EditorGUILayout.Vector3Field("External Force Max", m.ExternalForce);
                EditorGUILayout.FloatField("External Force Acel", m.ExternalForceAcel);
                EditorGUILayout.Space();
                EditorGUILayout.ToggleLeft("Gravity", m.UseGravity);
                EditorGUILayout.FloatField("Gravity Speed", m.GravitySpeed);
                EditorGUILayout.Space();
                EditorGUILayout.ToggleLeft("Use Sprint", m.UseSprint);
                EditorGUILayout.ToggleLeft("Sprint", m.Sprint);
                EditorGUILayout.ToggleLeft("Update Direction Speed", m.UpdateDirectionSpeed);
                EditorGUILayout.ToggleLeft("Input Locked", m.LockInput);
                EditorGUILayout.ToggleLeft("Movement Locked", m.LockMovement);
                EditorGUILayout.ToggleLeft("Free Movement", m.FreeMovement);

                EditorGUILayout.Space();
                EditorGUILayout.FloatField("Pitch Angle",m.PitchAngle);
                EditorGUILayout.FloatField("Bank",m.Bank);

                EditorGUILayout.Space();
                EditorGUILayout.ToggleLeft("Is Playing Mode", m.IsPlayingMode);
                EditorGUILayout.LabelField("Input Mode?", m.InputMode != null ? (m.InputMode.Name + " Input: " + m.InputMode.InputValue) : "<Null>");
                EditorGUILayout.FloatField("Mode Time",m.ModeTime);
                EditorGUILayout.ToggleLeft("Mode In Transition", m.ActiveMode != null && m.ActiveMode.IsInTransition);
                EditorGUILayout.IntField("Last Mode", m.LastMode);
                EditorGUILayout.EnumPopup("Mode Status", m.ModeStatus);
          
                EditorGUILayout.ToggleLeft("Use Pos Speed", m.UseAdditivePos);
                EditorGUILayout.ToggleLeft("Use Rot Speed", m.UseAdditiveRot);
                EditorGUILayout.Space();
                EditorGUILayout.FloatField("Terrain Slope", m.TerrainSlope);
               // EditorGUILayout.FloatField("Main Pivot Slope", m.MainPivotSlope);
                EditorGUILayout.FloatField("Slope Normalized", m.SlopeNormalized);
                EditorGUILayout.Space();
                EditorGUILayout.Vector3Field("Movement" , m.MovementAxis);
                EditorGUILayout.Vector3Field("Movement Smooth" ,m.MovementAxisSmoothed);
                EditorGUILayout.Vector3Field("Additive Pos " ,m.AdditivePosition);
                EditorGUILayout.Vector3Field("Inertia " ,m.Inertia);
                EditorGUILayout.FloatField("Delta Angle" ,m.DeltaAngle);
                EditorGUILayout.Space();
                EditorGUILayout.IntField("Current Anim Tag", m.AnimStateTag);
                EditorGUILayout.Space();
                EditorGUILayout.IntField("Stance", m.Stance);
              //  EditorGUILayout.LabelField(m.ActiveMode == null ? " ActiveMode: Null" : ("ActiveMode: " + m.ActiveMode.ID.name));

                EditorGUI.EndDisabledGroup();
            }

            EditorGUILayout.EndVertical();
        }

        private void ShowModes()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.PropertyField(StartWithMode, G_StartWithMode);
            Reo_List_Modes.DoLayoutList();        //Paint the Reordable List
            EditorGUILayout.EndVertical();

            Reo_List_Modes.index = SelectedMode.intValue;

            var Index = Reo_List_Modes.index;


            if (Index != -1 && S_Mode_List.arraySize > 0)
            {
                var SelectedMode = S_Mode_List.GetArrayElementAtIndex(Index);

                var Input = SelectedMode.FindPropertyRelative("Input");
                var active = SelectedMode.FindPropertyRelative("active");
                var CoolDown = SelectedMode.FindPropertyRelative("CoolDown");
                var AbilityIndex = SelectedMode.FindPropertyRelative("m_AbilityIndex");
                var OnAbilityIndex = SelectedMode.FindPropertyRelative("OnAbilityIndex");
                var DefaultIndex = SelectedMode.FindPropertyRelative("DefaultIndex");
                var allowRotation = SelectedMode.FindPropertyRelative("allowRotation");
                var allowMovement = SelectedMode.FindPropertyRelative("allowMovement");
                var ResetToDefault = SelectedMode.FindPropertyRelative("ResetToDefault");
                var ignoreLowerModes = SelectedMode.FindPropertyRelative("ignoreLowerModes");
                var Abilities = SelectedMode.FindPropertyRelative("Abilities");
                var modifier = SelectedMode.FindPropertyRelative("modifier");
                var showGeneral = SelectedMode.FindPropertyRelative("showGeneral");
                var OnEnterMode = SelectedMode.FindPropertyRelative("OnEnterMode");
                var OnExitMode = SelectedMode.FindPropertyRelative("OnExitMode");

                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                MalbersEditor.Foldout(showGeneral, "General", true);

                if (showGeneral.boolValue)
                {
                    EditorGUI.indentLevel++;
                   // EditorGUILayout.PropertyField(active);
                    EditorGUILayout.PropertyField(Input);
                    //EditorGUILayout.PropertyField(animationTag);
                    EditorGUILayout.PropertyField(ignoreLowerModes, new GUIContent("Ignore Lower", "It will play this mode even if another Lower Priority Mode is playing"));
                    EditorGUILayout.PropertyField(CoolDown, G_CoolDown);
                    EditorGUILayout.PropertyField(allowRotation, new GUIContent("Allow Rotation", "Allows rotate while is on the Mode"));
                    EditorGUILayout.PropertyField(allowMovement, new GUIContent("Allow Movement", "Allows movement while is on the Mode"));
                    EditorGUILayout.PropertyField(modifier, G_Modifier);
                    EditorGUI.indentLevel--;
                }
                EditorGUILayout.EndVertical();


                EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
                EditorGUIUtility.labelWidth = 70;
                EditorGUILayout.PropertyField(AbilityIndex, G_AbilityIndex, GUILayout.MinWidth(50));
                EditorGUILayout.PropertyField(DefaultIndex, G_DefaultIndex, GUILayout.MinWidth(50));
                ResetToDefault.boolValue = GUILayout.Toggle(ResetToDefault.boolValue, G_ResetToDefault, EditorStyles.miniButton, GUILayout.MinWidth(20));
                EditorGUIUtility.labelWidth = 0;
                EditorGUILayout.EndHorizontal();


                DrawAbilities(Index, SelectedMode, Abilities);

                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                MalbersEditor.Foldout(ModeShowEvents, "Events", true);

                if (ModeShowEvents.boolValue)
                {
                    EditorGUILayout.PropertyField(OnEnterMode);
                    EditorGUILayout.PropertyField(OnExitMode);
                    EditorGUILayout.PropertyField(OnAbilityIndex, new GUIContent("On Ability Index Changed"));
                }
                EditorGUILayout.EndVertical();

            }
        }

        private void DrawAbilities(int ModeIndex, SerializedProperty SelectedMode, SerializedProperty Abilities)
        {
            ReorderableList Reo_AbilityList;
            string listKey = SelectedMode.propertyPath;

            if (innerListDict.ContainsKey(listKey))
            {
                // fetch the reorderable list in dict
                Reo_AbilityList = innerListDict[listKey];
            }
            else
            {
                Reo_AbilityList = new ReorderableList(SelectedMode.serializedObject, Abilities, true, true, true, true)
                {
                    drawElementCallback = (rect, index, isActive, isFocused) =>
                    {
                        rect.y += 2;

                        var element = Abilities.GetArrayElementAtIndex(index);

                        var IndexValue = element.FindPropertyRelative("Index");
                        var name = element.FindPropertyRelative("Name");
                        var Active = element.FindPropertyRelative("active");

                        var ConstValue = Active.FindPropertyRelative("ConstantValue");
                        var VarValue = Active.FindPropertyRelative("Variable");
                        var useConstant = Active.FindPropertyRelative("UseConstant").boolValue;
                        BoolVar variable = VarValue.objectReferenceValue as BoolVar;

                        var IDRect = new Rect(rect) { height = EditorGUIUtility.singleLineHeight };

                        var ActiveRect = new Rect(IDRect);
                        var NameRect = new Rect(IDRect);

                        ActiveRect.width = 20;

                        IDRect.x = rect.width / 4 *3+ 50 ;
                        IDRect.width = rect.width / 4 - 12;

                        NameRect.x += 24;
                        NameRect.width = rect.width / 4 * 3-50;

                        if (useConstant)
                        {
                            ConstValue.boolValue = EditorGUI.Toggle(ActiveRect, GUIContent.none, ConstValue.boolValue);

                            if (variable != null)
                            {
                                variable.Value = ConstValue.boolValue;
                                EditorUtility.SetDirty(variable);
                            }
                        }
                        else
                        {
                            if (variable != null)
                            {
                                variable.Value = EditorGUI.Toggle(ActiveRect, GUIContent.none, variable.Value);
                                ConstValue.boolValue = variable.Value;
                            }
                            else
                            {
                                ConstValue.boolValue = EditorGUI.Toggle(ActiveRect, GUIContent.none, ConstValue.boolValue);
                            }
                        }

                        EditorGUI.PropertyField(NameRect, name, GUIContent.none);

                        EditorGUIUtility.labelWidth = 56;
                        EditorGUI.PropertyField(IDRect, IndexValue,GUIContent.none);
                        EditorGUIUtility.labelWidth = 0;
                    },

                    drawHeaderCallback = rect =>
                    {
                        var IDRect = new Rect(rect)
                        {
                            height = EditorGUIUtility.singleLineHeight
                        };

                        var NameRect = new Rect(IDRect);

                        IDRect.x = rect.width / 4 * 3 + 40;
                        IDRect.width = 40;

                        NameRect.x += 24;
                        NameRect.width = rect.width / 4 * 3 - 50;


                        EditorGUI.LabelField(NameRect, "   Abilities");
                        EditorGUI.LabelField(IDRect, "Index");
                    },
                };

                innerListDict.Add(listKey, Reo_AbilityList);  //Store it on the Editor
            }

            Reo_AbilityList.DoLayoutList();

            int SelectedAbility = Reo_AbilityList.index;


            if (SelectedAbility != -1)
            {
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                SerializedProperty ability = Abilities.GetArrayElementAtIndex(SelectedAbility);

                EditorGUILayout.PropertyField(ability.FindPropertyRelative("active"));

                if (ability != null)
                {
                    var S_properties = ability.FindPropertyRelative("Properties");
                    EditorGUILayout.PropertyField(S_properties, /*GUIContent.none,*/ true);
                }

                if (GUILayout.Button("Override all Abilities Properties"))
                {
                    var ModeAbilities = m.modes[ModeIndex].Abilities;
                    var properties = ModeAbilities[SelectedAbility].Properties;

                    foreach (var ab in ModeAbilities)
                    {
                        ab.Properties = new ModeProperties(properties);
                    }

                    Debug.Log("All Abilities properties overwriten in the Mode: "+ m.modes[ModeIndex].Name);
                    EditorUtility.SetDirty(target);
                }
                EditorGUILayout.EndVertical();
            }
        }

        private void ShowStates()
        {
            EditorGUI.indentLevel++;
            var showStates = serializedObject.FindProperty("showStates");
            

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.BeginHorizontal();
            showStates.boolValue = EditorGUILayout.Foldout(showStates.boolValue, "States");
            CloneStates.boolValue = GUILayout.Toggle(CloneStates.boolValue, G_CloneStates, EditorStyles.miniButton, GUILayout.Width(85));
            EditorGUILayout.EndHorizontal();
            EditorGUI.indentLevel--;

            if (!CloneStates.boolValue)
            {
                EditorGUILayout.HelpBox("Disable Clone States only when you are setting values and debugging while playing. ", MessageType.Warning);
            }
             

            if (showStates.boolValue)
            {
                EditorGUILayout.PropertyField(OverrideStartState,G_OverrideStartState);
                Reo_List_States.DoLayoutList();        //Paint the Reordable List 
                DropAreaGUI();
            }
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            MalbersEditor.Arrays(OnEnterExitStates, new GUIContent("On Enter/Exit States Events"));
            EditorGUILayout.EndVertical();
             
        }

        private void ShowSpeeds()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            Reo_List_Speeds.DoLayoutList();        //Paint the Reordable List speeds

            if (Reo_List_Speeds.index != -1)
            {
                var SelectedSpeed = S_Speed_List.GetArrayElementAtIndex(Reo_List_Speeds.index);
                var states = SelectedSpeed.FindPropertyRelative("states");
                var stances = SelectedSpeed.FindPropertyRelative("stances");
                var StartVerticalSpeed = SelectedSpeed.FindPropertyRelative("StartVerticalIndex");
                var Speeds = SelectedSpeed.FindPropertyRelative("Speeds");
                var TopIndex = SelectedSpeed.FindPropertyRelative("TopIndex");
                EditorGUILayout.PropertyField(StartVerticalSpeed, new GUIContent("Start Index", "Which Speed the Set will start, This value is the Index for the Speed Modifier List, Starting the first index with (1) instead of (0)"));
                EditorGUILayout.PropertyField(TopIndex, new GUIContent("Top Index", "Set the Top Index when Increasing the Speed using SpeedUP"));
                EditorGUILayout.Space();
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(states, new GUIContent("States", "States that will activate these Speeds"), true);
                EditorGUILayout.PropertyField(stances, new GUIContent("Stances", "Stances that will activate these Speeds"), true);
                EditorGUILayout.PropertyField(Speeds, new GUIContent("Speeds", "Speeds for this speed Set"), true);
                EditorGUI.indentLevel--;    
            }
            EditorGUILayout.EndVertical();

            if (Application.isPlaying)
            {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.LabelField("Active Speed Modifier:");
                EditorGUILayout.PropertyField(serializedObject.FindProperty("currentSpeedModifier"),true);
                EditorGUI.EndDisabledGroup();
            }
        }

        private void ShowGeneral()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.PropertyField(Player, G_Player);
            EditorGUILayout.EndVertical();

             EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(S_PivotsList,true);
            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            {
                MalbersEditor.Foldout(ShowMovement, "Movement", true);
                if (ShowMovement.boolValue)
                {
                    EditorGUILayout.PropertyField(UseCameraInput, new GUIContent("Camera Input", "The Animal uses the Camera Forward Diretion to Move"));
                    EditorGUI.BeginChangeCheck();
                    EditorGUILayout.PropertyField(alwaysForward, new GUIContent("Always Forward", "If true the animal will always go forward. useful for infinite runners"));
                    if (EditorGUI.EndChangeCheck()) m.AlwaysForward = m.AlwaysForward;


                    EditorGUILayout.PropertyField(useCameraUp, new GUIContent("Use Camera Up", "Uses the Camera Up Vector to move UP or Down while flying or Swiming UnderWater. if this is false the Animal will need an UPDOWN Input to move higher or lower"));
                    EditorGUILayout.PropertyField(SmoothVertical, G_SmoothVertical);
                    EditorGUILayout.PropertyField(useSprintGlobal, G_useSprintGlobal);
                    EditorGUILayout.Space();
                    EditorGUILayout.PropertyField(TurnMultiplier, G_TurnMultiplier);
                    EditorGUILayout.PropertyField(UpDownLerp, G_UpDownLerp);
                    EditorGUILayout.PropertyField(AnimatorSpeed, G_AnimatorSpeed);

                    // EditorGUILayout.Space();
                }
            }
            EditorGUILayout.EndVertical();


            ShowStrafingVars();
         

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            {
                MalbersEditor.Foldout(ShowGround, "Ground", true);
                if (ShowGround.boolValue)
                {
                    EditorGUILayout.PropertyField(Height, G_Height);
                    EditorGUILayout.PropertyField(GroundLayer, G_GroundLayer);
                    EditorGUILayout.PropertyField(maxAngleSlope, G_maxAngleSlope);
                    EditorGUILayout.PropertyField(AlignPosLerp, G_AlignPosLerp);
                    EditorGUILayout.PropertyField(AlignRotLerp, G_AlignRotLerp);
                    EditorGUILayout.PropertyField(RayCastRadius, G_RayCastRadius);
                    EditorGUILayout.PropertyField(AlignLoop, G_AlignLoop);
                }
            }
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            MalbersEditor.Foldout(showGravity, "Gravity", true);
            if (showGravity.boolValue)
            {
                EditorGUILayout.LabelField("Gravity", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(m_gravity, G_gravityDirection);
                EditorGUILayout.PropertyField(m_gravityPower, G_GravityForce);
                EditorGUILayout.PropertyField(m_gravityTime, G_GravityCycle);
                EditorGUILayout.PropertyField(ground_Changes_Gravity, new GUIContent("Ground Changes Gravity", "The Ground will change the gravity direction, allowing the animals to move in any surface"));
            }
            EditorGUILayout.EndVertical();

        }


        private void ShowStrafingVars()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(m_CanStrafe, G_CanStrafe);
            if (GUILayout.Button("?", GUILayout.Width(20)))
            {
                Application.OpenURL("https://malbersanimations.gitbook.io/animal-controller/strafing");
            }
            EditorGUILayout.EndHorizontal();


            if (m.CanStrafe)
            { 
                EditorGUILayout.PropertyField(m_strafe, G_Strafe);
            }

            
            EditorGUILayout.EndVertical();

        }

        private void Draw_Header_Speed(Rect rect)
        {
            var height = EditorGUIUtility.singleLineHeight;
            var name = new Rect(rect.x, rect.y, rect.width / 2, height);

            EditorGUI.LabelField(name, "   Speed Sets");

            Rect R_2 = new Rect(rect.width - 8, rect.y, 22, EditorGUIUtility.singleLineHeight - 3);
            Rect R_3 = new Rect(rect.width + 15, rect.y, 22, EditorGUIUtility.singleLineHeight - 3);


            if (GUI.Button(R_2, new GUIContent("+", "New Speed Set"), EditorStyles.miniButton))
            {
                OnAddCallback_Speeds(Reo_List_Speeds);
            }

            if (GUI.Button(R_3, new GUIContent("-", "Remove Selected Speed Set"), EditorStyles.miniButton))
            {
                if (Reo_List_Speeds.index != -1) //If there's a selected Ability
                {
                    OnRemoveCallback_Speeds(Reo_List_Speeds);
                }
            }
        }

        private void OnRemoveCallback_Speeds(ReorderableList list)
        {
            S_Speed_List.DeleteArrayElementAtIndex(list.index);
            list.index -= 1;

            if (list.index == -1 && S_Speed_List.arraySize > 0)  //In Case you remove the first one
            {
                list.index = 0;
            }

            EditorUtility.SetDirty(m);
        }

        private void OnAddCallback_Speeds(ReorderableList reo_List_Speeds)
        {
            if (m.speedSets == null) m.speedSets = new List<MSpeedSet>();

            m.speedSets.Add(new MSpeedSet());

            EditorUtility.SetDirty(m);
        }
        
        private void Draw_Element_Speed(Rect rect, int index, bool isActive, bool isFocused)
        {
            if (S_Speed_List.arraySize <= index) return;

            var nameRect = rect;

            nameRect.y += 1;
            nameRect.height -= 3;
            Rect activeRect = nameRect;

            var speedSet = S_Speed_List.GetArrayElementAtIndex(index);
            var nameSpeedSet = speedSet.FindPropertyRelative("name");
            nameRect.width /= 2;
            nameRect.width += 10;
            EditorGUI.PropertyField(nameRect, nameSpeedSet, GUIContent.none);

            activeRect.x = rect.width / 2 + 60;
            activeRect.width = rect.width / 2 - 20;

            //    EditorGUI.TextField(activeRect, "(Active)");

            if (Application.isPlaying)
            {
                if (m.speedSets[index] == m.CurrentSpeedSet)
                {
                    EditorGUI.LabelField(activeRect, "(" + m.CurrentSpeedModifier.name + ")", EditorStyles.boldLabel);
                }
            }
        }


        #region Draw Pivots
        ////-------------------------PIVOTS-----------------------------------------------------------
        //private void DrawHeaderCallback_Pivots(Rect rect)
        //{
        //    var height = EditorGUIUtility.singleLineHeight;
        //    var name = new Rect(rect.x, rect.y, rect.width / 2 - 60, height);
        //    var poss = new Rect(rect.width / 2 - 45, rect.y, rect.width / 2, height - 1);


        //    EditorGUI.LabelField(name, "   Name");
        //    EditorGUI.LabelField(poss, "    Position");   
        //}
        //private void DrawElement_Pivots(Rect rect, int index, bool isActive, bool isFocused)
        //{
        //    rect.y += 2;
        //    if (S_PivotsList.arraySize <= index) return;
        //    var pivot = S_PivotsList.GetArrayElementAtIndex(index);
        //    rect.width += 30;
        //    EditorGUI.PropertyField(rect, pivot);
        //}
        //private void OnAddCallback_Pivots(ReorderableList list)
        //{
        //    if (m.pivots == null) m.pivots = new List<MPivots>();

        //    var newPivot = new MPivots("Pivot", Vector3.up, 1);

        //    m.pivots.Add(newPivot);

        //    Debug.Log(m.pivots.Count);

        //  EditorUtility.SetDirty(m);
        //}
        //private void OnRemoveCallback_Pivots(ReorderableList list)
        //{
        //   // var state = S_PivotsList.GetArrayElementAtIndex(list.index);

        //    // The reference value must be null in order for the element to be removed from the SerializedProperty array.
        //    S_PivotsList.DeleteArrayElementAtIndex(list.index);
        //    list.index -= 1;

        //    if (list.index == -1 && S_PivotsList.arraySize > 0)  //In Case you remove the first one
        //    {
        //        list.index = 0;
        //    }

        //    EditorUtility.SetDirty(m);
        //}
        //-------------------------PIVOTS-----------------------------------------------------------
        #endregion

        #region DrawStates 
        //-------------------------STATES-----------------------------------------------------------
        private void Draw_Header_State(Rect rect)
        {
            var r = rect;
            r.x += 13;
            EditorGUI.LabelField(r, new GUIContent("     States [Double clic to modify them]", "States are the common things the Animals can do but they cannot overlap each other"));

            Rect R_2 = new Rect(rect.width - 8, rect.y, 22, EditorGUIUtility.singleLineHeight - 3);
            Rect R_3 = new Rect(rect.width + 15, rect.y, 22, EditorGUIUtility.singleLineHeight - 3);


            if (GUI.Button(R_2, new GUIContent("+", "Creates and adds a new State"), EditorStyles.miniButton))
            {
                OnAddCallback_State(Reo_List_States);
            }

            if (GUI.Button(R_3, new GUIContent("-", "Remove the selected State"), EditorStyles.miniButton))
            {
                if (Reo_List_States.index != -1)
                {
                    if (EditorUtility.DisplayDialog("Warning!", "Are you sure you want to delete from the Project the selected state?\n If you choose 'No' it will simply remove it from the list ", "Yes", "No Just Remove it"))
                    {
                        OnRemove_State(Reo_List_States, true);       //If there's a selected Ability
                    }
                    else
                    {
                        OnRemove_State(Reo_List_States, false);       //If there's a selected Ability
                    }
                }
            }
        }
        private void Draw_Element_State(Rect rect, int index, bool isActive, bool isFocused)
        {
            rect.y += 2;
            if (S_StateList.arraySize <= index) return;

            var stateProperty = S_StateList.GetArrayElementAtIndex(index);
            State state = stateProperty.objectReferenceValue as State;


            var activeRect = rect;
            activeRect.width -= 20;
            activeRect.x += 20;

            var StateRect = new Rect(activeRect.x, activeRect.y, activeRect.width - 78, activeRect.height - 5);

            // Remove the ability if it no longer exists.
            if (state is null)
            {
                EditorGUI.ObjectField(StateRect, stateProperty, GUIContent.none);
                return;
            }
            state.Active = EditorGUI.Toggle(new Rect(rect.x, rect.y, 20, activeRect.height), GUIContent.none, state.Active);

            var active = "";

            if (Application.isPlaying)
            {
                if (m.ActiveState == state)
                {
                    if (state.IsPending)
                        active = " [Pending] ";
                    else
                        active = " [Active] ";
                }
                else if (state.IsSleepFromState)
                {
                    active = " [Sleep] ";
                }
                else if (state.IsSleepFromMode)
                {
                    active = " [SleepM] ";
                }
                else if (state.OnQueue)
                {
                    active = " [Queued] ";
                }

            }

            EditorGUI.ObjectField(StateRect, stateProperty, GUIContent.none);

            var style = new GUIStyle( EditorStyles.label);

            if (Application.isPlaying && m.isActiveAndEnabled && state != null)
            {
                var activestate = m.ActiveState;

                if (activestate != null)
                {
                    if (state.IsPersistent)
                    {
                        style.normal.textColor = Color.green;
                    }

                    if (state.Priority < activestate.Priority && activestate.IsPersistent)
                    {
                        style.normal.textColor = new Color(style.normal.textColor.r, style.normal.textColor.g, style.normal.textColor.b, style.normal.textColor.a / 2);
                    }
                }
            }


            EditorGUI.LabelField(new Rect(activeRect.width -20  ,activeRect.y,60,activeRect.height), active,style);
            EditorGUI.LabelField(new Rect(activeRect.width +40 ,activeRect.y,25,activeRect.height),  "(" + (S_StateList.arraySize - index-1) + ")",style);

            EditorGUI.BeginChangeCheck();
            activeRect = rect;
            activeRect.x += activeRect.width - 34;
            activeRect.width = 20;

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "MAnimal Inspector Changed");
               // EditorUtility.SetDirty(target);
            }
        }
        private void OnReorderCallback_States(ReorderableList list)
        {
            for (int i = 0; i < S_StateList.arraySize; ++i)
            {
                m.states[i].Index = i;
                EditorUtility.SetDirty(m);
            }
            EditorUtility.SetDirty(target);
        }
        private void OnAddCallback_State(ReorderableList list)
        {
            addMenu = new GenericMenu();

            for (int i = 0; i < StatesType.Count; i++)
            {
                Type st = StatesType[i];

                bool founded = false;
                for (int j = 0; j < m.states.Count; j++)
                {
                    if (m.states[j].GetType() == st)
                    {
                        founded = true;
                    }
                }

                if (!founded)
                {
                    addMenu.AddItem(new GUIContent(st.Name), false, () => AddState(st));
                }
            }

            addMenu.ShowAsContext();
        }
        #endregion

        //-------------------------MODES-----------------------------------------------------------
        private void Draw_Header_Modes(Rect rect)
        {
            var r = new Rect(rect);
            var a = new Rect(rect);
            a.width = 65;
            EditorGUI.LabelField(a, new GUIContent("  Active", "Is the Mode Enable or Disable"));
            r.x += 60;
            r.width = 60;
            EditorGUI.LabelField(r, new GUIContent("Mode", "Modes are the Animations that can be played on top of the States"));

            var activeRect = rect;
            activeRect.width -= 20;
            activeRect.x += 20;
            var prioRect = new Rect(activeRect.width + 30, activeRect.y, 45, activeRect.height);
            var IDRect = new Rect(activeRect.width+5, activeRect.y, 35, activeRect.height);

            EditorGUI.LabelField(IDRect, new GUIContent("ID","Mode ID:\n Numerical ID value for the Mode"));
            EditorGUI.LabelField(prioRect, new GUIContent("Pri","Priority:\n If A mode has 'Ignore Lower Modes' enabled, it will play even if a Lower Mode is Playing"));
        }
        private void Draw_Element_Modes(Rect rect, int index, bool isActive, bool isFocused)
        {
            rect.y += 2;
            if (S_Mode_List.arraySize <= index) return;
            
            EditorGUI.BeginChangeCheck();
            {
                var ModeProperty = S_Mode_List.GetArrayElementAtIndex(index);
                var active = ModeProperty.FindPropertyRelative("active");
                var ID = ModeProperty.FindPropertyRelative("ID");

                var activeRect = rect;
                activeRect.width -= 20;
                activeRect.x += 20;

                var activeRect1 = new Rect(rect.x, rect.y, 20, rect.height);
                var IDRect = new Rect(rect.x + 40, rect.y, rect.width - 90, EditorGUIUtility.singleLineHeight);
                
                var IDVal = new Rect(activeRect.width + 9, activeRect.y + 2, 35, activeRect.height);

                active.boolValue = EditorGUI.Toggle(activeRect1, GUIContent.none, active.boolValue);
              
                EditorGUI.PropertyField(IDRect, ID, GUIContent.none);

                var style = new GUIStyle(EditorStyles.boldLabel)
                { alignment = TextAnchor.UpperRight };

                if (m.modes[index].ID != null)
                {
                    EditorGUI.LabelField(IDVal, m.modes[index].ID.ID.ToString(), style);
                }

                var priorityRect = new Rect(activeRect.width + 42, activeRect.y - 2, 25, activeRect.height);

                EditorGUI.LabelField(priorityRect, "│" + (S_Mode_List.arraySize - index - 1));

            }
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Inspector");
                EditorUtility.SetDirty(target);
            }
        }
        private void OnAdd_Modes(ReorderableList list)
        {
            if (m.pivots == null) m.modes = new List<Mode>();


            Ability newAbility = new Ability()
            {
                 active = new BoolReference(true), 
                Index = new IntReference(1),
                Name = "AbilityName"
            };

            var newMode = new Mode()
            {  
                Abilities = new List<Ability>(1) { newAbility },   
            };

            m.modes.Add(newMode);
            EditorUtility.SetDirty(m);
        }
        private void OnRemoveCallback_Mode(ReorderableList list)
        {
            // The reference value must be null in order for the element to be removed from the SerializedProperty array.
            S_Mode_List.DeleteArrayElementAtIndex(list.index);
            list.index -= 1;

            if (list.index == -1 && S_Mode_List.arraySize > 0)  //In Case you remove the first one
            {
                list.index = 0;
            }
            SelectedMode.intValue--;
            list.index = Mathf.Clamp(list.index, 0, list.index - 1);

            EditorUtility.SetDirty(m);
        } 
        public void SetPivots()
        {
            m.Pivot_Hip = m.pivots.Find(item => item.name.ToUpper() == "HIP");
            m.Pivot_Chest = m.pivots.Find(item => item.name.ToUpper() == "CHEST");
        } 

        /// <summary> The ReordableList remove button has been pressed. Remove the selected ability.</summary>
        private void OnRemove_State(ReorderableList list, bool DeleteAsset)
        {
            State state = S_StateList.GetArrayElementAtIndex(list.index).objectReferenceValue as State;

            if (DeleteAsset)
            {
                if (state != null)
                {
                    string Path = AssetDatabase.GetAssetPath(state);
                    AssetDatabase.DeleteAsset(Path);

                    S_StateList.DeleteArrayElementAtIndex(list.index);
                    if (list.index != list.count)
                    {
                        S_StateList.DeleteArrayElementAtIndex(list.index);
                    }
                }
                else
                { 
                    S_StateList.DeleteArrayElementAtIndex(list.index);

                    if (list.index != list.count)
                    {
                        S_StateList.DeleteArrayElementAtIndex(list.index);
                    }

                    list.index -= 1;
                }
            }
            else
            {
                S_StateList.DeleteArrayElementAtIndex(list.index);

                if (state != null)
                    S_StateList.DeleteArrayElementAtIndex(list.index); //HACK
            }

            list.index = Mathf.Clamp(list.index,0,list.index - 1);

            EditorUtility.SetDirty(m);
        }

        /// <summary>Adds a new State of the specified type.</summary>       
        private void AddState(Type selectedState)
        {
            Debug.Log(selectedState);

            State state = (State)CreateInstance(selectedState);

            AssetDatabase.CreateAsset(state, "Assets/" + m.name + " " + selectedState.Name + ".asset");


           // state.SetAnimal(m);
            EditorUtility.SetDirty(m);
            S_StateList.AddToObjectArray(state);
            EditorUtility.SetDirty(state);
        }


        #region GUICONTENT

        //readonly GUIContent G_mainCamera = new GUIContent("Main Camera", "Stores the Camera.Main transform to use the Camera Direction for Input");
        readonly GUIContent G_LockInput = new GUIContent("Lock Input", "Locks Input on the Animal, Ingore inputs like Jumps, Attacks , Actions etc");
        readonly GUIContent G_lockMovement = new GUIContent("Lock Movement", "Locks the Movement Entries on the Animal (Horizontal, Vertical)");
        readonly GUIContent G_Rotator = new GUIContent("Rotator", "Used to add extra Rotations to the Animal");
        readonly GUIContent G_RootBone = new GUIContent("RootBone", "Bone to Identify the Main Root Bone of the Animal. Mainly Used for TimeLine and Flying Animals");
        //readonly GUIContent G_Center = new GUIContent("Center", "Center of the Animal to be used for AI and Targeting");
        readonly GUIContent G_RayCastRadius = new GUIContent("RayCast Radius", "Instead of using Raycast for checking the ground beneath the animal we use SphereCast, this is the Radius of that Sphere");
        readonly GUIContent G_AlignLoop = new GUIContent("Align Loop", "When the Animal is grounded the Controller will check every X frame for the Ground... Higher values better performance -> less acurancy");
        readonly GUIContent G_animalType = new GUIContent("Type", "Value for Additive Pose Fixing");
        //readonly GUIContent G_AnimatorUpdatePhysics = new GUIContent("Update Physics?", "if True the it will use FixedUpdate for all his calculations. Use this if you are using the creature as the Main Character");
        //readonly GUIContent G_UpdateParameters = new GUIContent("Update Params", "Update all Parameters in the Animator Controller");
        readonly GUIContent G_AnimatorSpeed = new GUIContent("Animator Speed", "Global multiplier for the Animator Speed");
        readonly GUIContent G_AbilityIndex = new GUIContent("Active", "Active Ability Index \n(if set to -99 it will Play a Random Ability )\n(if set to 0 it wont play anything)");
        readonly GUIContent G_DefaultIndex = new GUIContent("Default", "Default Ability Index to return to when exiting the mode \n(if set to -99 it will Play a Random Ability )");
        readonly GUIContent G_ResetToDefault = new GUIContent("R", "Reset to Default:\nWhen Exiting the Mode\nthe Active Index will reset\nto the Default");
        readonly GUIContent G_CloneStates = new GUIContent("Clone States", "Creates instances of the States so they cannot be overwriten by other animal using the same scriptable objects");
        readonly GUIContent G_Height = new GUIContent("Height", "Distance from Animal Hip to the ground");
        readonly GUIContent G_GroundLayer = new GUIContent("Ground Layer", "Layers the Animal considers ground");
        readonly GUIContent G_maxAngleSlope = new GUIContent("Max Angle Slope", "If the Terrain slope angle is greater than this value, the animal will fall");
        readonly GUIContent G_AlignPosLerp = new GUIContent("Align Pos Lerp", "Smoothness value to Snap to ground while Grounded");
        readonly GUIContent G_AlignRotLerp = new GUIContent("Align Rot Lerp", "Smoothness value to Align to ground slopes while Grounded");
        readonly GUIContent G_CoolDown = new GUIContent("Cool Down", "Elapsed time to be able to play the Mode Again.\n If = 0 then the Mode cannot be interrupted until it finish the Animation");
        readonly GUIContent G_Modifier = new GUIContent("Modifier", "Extra Logic to give the Animal when Entering or Exiting the Modes");

        readonly GUIContent G_gravityDirection = new GUIContent("Direction", "Direction of the Gravity applied to the animal");
        readonly GUIContent G_GravityForce = new GUIContent("Force", "Force of the Gravity, by Default it 9.8");
        readonly GUIContent G_GravityCycle = new GUIContent("Start Gravity Cycle", "Start the gravity with an extra time to push the animal down.... higher values stronger Gravity");

        readonly GUIContent G_useSprintGlobal = new GUIContent("Can Sprint", "Can the Animal Sprint?");
        readonly GUIContent G_CanStrafe = new GUIContent("Can Strafe", "Can the Animal Strafe?");
        readonly GUIContent G_Strafe = new GUIContent("Strafe", "Enables Strafing on the Animal.\nStrafing requires new sets of Strafe Animations, make sure you have proper animations to Use this feature");
        

        //readonly GUIContent G_sprint = new GUIContent("Sprint", " Sprint value of the Animal. Sprint allows increase 1 speed modifier");
        //readonly GUIContent G_Grounded = new GUIContent("Grounded", " Grounded value of the Animal. if True the animal will aling itself with the Terrain");


        readonly GUIContent G_SmoothVertical = new GUIContent("Smooth Vertical", "Used for Joysticks to increase the speed by the Stick Pressure");
        readonly GUIContent G_TurnMultiplier = new GUIContent("Turn Multiplier", "Global turn multiplier to increase rotation on the animal");
        readonly GUIContent G_UpDownLerp = new GUIContent("Up Down Lerp", "Lerp Value for the UpDown Axis");
        readonly GUIContent G_rootMotion = new GUIContent("Root Motion", "Enable Disable the Root motion on the Animator");

        readonly GUIContent G_Player = new GUIContent("Player", "True if this will be your main Character Player, used for Respawing characters");
        readonly GUIContent G_OverrideStartState = new GUIContent("Override Start State", "Overrides the Start State");
        readonly GUIContent G_StartWithMode = new GUIContent("Start with Mode", "On Start .. Plays a Mode");
        #endregion

        //-------------------------STATES-----------------------------------------------------------
        void OnSceneGUI()
        {
            foreach (var pivot in m.pivots)
            {
                if (pivot.EditorModify)
                {
                    Transform t = m.transform;
                    EditorGUI.BeginChangeCheck();

                    Vector3 piv = t.TransformPoint(pivot.position);
                    Vector3 NewPivPosition = Handles.PositionHandle(piv, t.rotation);
                    //   pivot.position = m.transform.InverseTransformPoint(NewPivPosition);

                    float multiplier = Handles.ScaleSlider(pivot.multiplier, piv, pivot.WorldDir(t), Quaternion.identity, HandleUtility.GetHandleSize(piv), 0f);

                    if (EditorGUI.EndChangeCheck())
                    {
                        Undo.RecordObject(m, "Pivots");
                        pivot.position = t.InverseTransformPoint(NewPivPosition);
                        pivot.multiplier = multiplier;
                    }
                }
            }
        }
    }
}