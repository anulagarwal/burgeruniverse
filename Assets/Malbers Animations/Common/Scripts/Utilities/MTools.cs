﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using System.Linq;
using System;
using UnityEngine.Events;
using System.Reflection;
using System.CodeDom;
using System.Text.RegularExpressions;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace MalbersAnimations
{
    /// <summary>Redundant functions to be used all over the assets</summary>
    public static class MTools
    {
        public static bool IsPrefab(GameObject go) => !go.scene.IsValid();

#if UNITY_EDITOR
       
#endif

        public static bool IsParent(Transform childObject, Transform parent)
        {
            Transform t = childObject.transform;

            while (t.parent != null)
            {
                if (t.parent == parent)
                {
                    return true;
                }
                t = t.parent.transform;
            }
            return false; // Could not its parent
        }
         

     


       


        public static List<Type> GetAllTypes<T>()  
        {
            // Store the States type.
            Type classtype = typeof(T);

            // Get all the types that are in the same Assembly (all the runtime scripts) as the Reaction type.
            var allTypes = classtype.Assembly.GetTypes();

            // Create an empty list to store all the types that are subtypes of Reaction.
            var SubTypeList = new List<Type>();

            // Go through all the types in the Assembly...
            for (int i = 0; i < allTypes.Length; i++)
            {
                // ... and if they are a non-abstract subclass of Reaction then add them to the list.
                if (allTypes[i].IsSubclassOf(classtype) && !allTypes[i].IsAbstract)
                {
                    SubTypeList.Add(allTypes[i]);
                }
            }

            // Convert the list to an array and store it.
           return SubTypeList;
        }

        public static List<Type> GetAllTypes(Type type)
        {
            // Get all the types that are in the same Assembly (all the runtime scripts) as the Reaction type.
            var allTypes = type.Assembly.GetTypes();

            // Create an empty list to store all the types that are subtypes of Reaction.
            var SubTypeList = new List<Type>();

            // Go through all the types in the Assembly...
            for (int i = 0; i < allTypes.Length; i++)
            {
                // ... and if they are a non-abstract subclass of Reaction then add them to the list.
                if (allTypes[i].IsSubclassOf(type) && !allTypes[i].IsAbstract)
                {
                    SubTypeList.Add(allTypes[i]);
                }
            }

            // Convert the list to an array and store it.
            return SubTypeList;
        }


        #region Find References
        public static Camera FindMainCamera()
        {
            var MainCamera = Camera.main;
            if (MainCamera == null)
                MainCamera = GameObject.FindObjectOfType<Camera>();

            return MainCamera;
        }


        #endregion

        #region Resources
        public static List<T> GetAllResources<T>() where T : ScriptableObject
        {
            var reOfType = Resources.FindObjectsOfTypeAll<T>();

            if (reOfType != null) return reOfType.ToList();

            return null;
        }

        public static T GetResource<T>(string name) where T : ScriptableObject
        {
            var allInstances = GetAllResources<T>();

            T found = allInstances.Find(x => x.name == name);

            return found;
        }
        #endregion

        #region Layers
        /// <summary>True if the colliders layer is on the layer mask</summary>
        public static bool CollidersLayer(Collider collider, LayerMask layerMask) => layerMask == (layerMask | (1 << collider.gameObject.layer));


        /// <summary> Set a Layer to the Game Object and all its children</summary>
        public static void SetLayer(Transform root, int layer)
        {
            root.gameObject.layer = layer;
            foreach (Transform child in root)
                SetLayer(child, layer);
        }

        /// <summary>True if the colliders layer is on the layer mask</summary>
        public static bool Layer_in_LayerMask(int layer, LayerMask layerMask) => layerMask == (layerMask | (1 << layer));
        #endregion

        #region Comparizon
        /// <summary> Makes and OR comparison of an Int Value with other Ints</summary>
        public static bool CompareOR(int source, params int[] comparison)
        {
            foreach (var item in comparison)
                if (source == item) return true;

            return false;
        }

        /// <summary> Makes and AND comparison of an INT Value with other INTs</summary>
        public static bool CompareAND(int source, params int[] comparison)
        {
            foreach (var item in comparison)
                if (source != item) return false;

            return true;
        }


        /// <summary> Makes and OR comparison of an Bool Value with other Bools</summary>
        public static bool CompareOR(bool source, params bool[] comparison)
        {
            foreach (var item in comparison)
                if (source == item) return true;

            return false;
        }

        /// <summary> Makes and AND comparison of an Bool Value with other Bools</summary>
        public static bool CompareAND(bool source, params bool[] comparison)
        {
            foreach (var item in comparison)
                if (source != item) return false;

            return true;
        }
        #endregion

        #region Serialization
        /// <summary> Serialize a Class to XML</summary>
        public static string Serialize<T>(this T toSerialize)
        {
            XmlSerializer xml = new XmlSerializer(typeof(T));
            StringWriter writer = new StringWriter();
            xml.Serialize(writer, toSerialize);

            return writer.ToString();
        }

        /// <summary>Finds a bit (index) on a Integer</summary>
        public static bool IsBitActive(int IntValue, int index) => (IntValue & (1 << index)) != 0;

        /// <summary> DeSerialize a Class with xml</summary>
        public static T Deserialize<T>(this string toDeserialize)
        {
            XmlSerializer xml = new XmlSerializer(typeof(T));
            StringReader reader = new StringReader(toDeserialize);

            return (T)xml.Deserialize(reader);
        }

        #endregion

        #region Camera Direction
        /// <summary>
        /// Calculate the direction from the center of the Screen
        /// </summary>
        /// <param name="origin">The start point to calculate the direction</param>
        ///  <param name="hitmask">Just use this layers</param>
        public static Vector3 DirectionFromCamera(Transform origin, float x, float y, out RaycastHit hit, LayerMask hitmask)
        {
            Camera cam = Camera.main;

            hit = new RaycastHit();

            Ray ray = cam.ScreenPointToRay(new Vector2(x * cam.pixelWidth, y * cam.pixelHeight));
            Vector3 dir = ray.direction;

            hit.distance = float.MaxValue;

            RaycastHit[] hits;

            hits = Physics.RaycastAll(ray, 100, hitmask);

            foreach (RaycastHit item in hits)
            {
                if (item.transform.root == origin.transform.root) continue; //Dont Hit anything in this hierarchy
                if (Vector3.Distance(cam.transform.position, item.point) < Vector3.Distance(cam.transform.position, origin.position)) continue; //If I hit something behind me skip
                if (hit.distance > item.distance) hit = item;
            }

            if (hit.distance != float.MaxValue)
            {
                dir = (hit.point - origin.position).normalized;
            }

            return dir;
        }

        /// <summary>
        /// Calculate the direction from the ScreenPoint of the Screen and also saves the RaycastHit Info
        /// </summary>
        /// <param name="origin">The start point to calculate the direction</param>
        ///  <param name="hitmask">Just use this layers</param>
        public static Vector3 DirectionFromCamera(Camera cam,Transform origin, Vector3 ScreenPoint, out RaycastHit hit, LayerMask hitmask, Transform Ignore = null)
        {
            Ray ray = cam.ScreenPointToRay(ScreenPoint);
            Vector3 dir = ray.direction;

            hit = new RaycastHit
            {
                distance = float.MaxValue,
                point = ray.GetPoint(100)
            };
            RaycastHit[] hits;

            hits = Physics.RaycastAll(ray, 100, hitmask);

            foreach (RaycastHit item in hits)
            {
              
                if (item.transform.root == Ignore) continue;                                     //Dont Hit anything the Ingore
                if (item.transform.root == origin.transform.root) continue;                      //Dont Hit anything in this hierarchy
                if (Vector3.Distance(cam.transform.position, item.point) < Vector3.Distance(cam.transform.position, origin.position)) continue; //If I hit something behind me skip
                if (hit.distance > item.distance) hit = item;
            }

            if (hit.distance != float.MaxValue)
            {
                dir = (hit.point - origin.position).normalized;
            }

            return dir;
        }

        /// <summary>Calculate the direction from the center of the Screen </summary>
        /// <param name="origin">The start point to calculate the direction</param>
        public static Vector3 DirectionFromCamera(Transform origin)
        {
            return DirectionFromCamera(origin, 0.5f * Screen.width, 0.5f * Screen.height, out _, -1);
        }


        /// <summary> Calculate the direction from the center of the Screen </summary>
        /// <param name="origin">The start point to calculate the direction</param>
        public static Vector3 DirectionFromCamera(Transform origin, LayerMask layerMask) => 
            DirectionFromCamera(origin, 0.5f * Screen.width, 0.5f * Screen.height, out _, layerMask);

        #endregion

        #region RayCasting
        public static RaycastHit RayCastHitToCenter(Camera cam, Transform origin, Vector3 ScreenCenter, int layerMask = 0)
        {
            RaycastHit hit = new RaycastHit();

            Ray ray = cam.ScreenPointToRay(ScreenCenter);

            hit.distance = float.MaxValue;

            RaycastHit[] hits = Physics.RaycastAll(ray, 100, layerMask);

            foreach (RaycastHit rayhit in hits)
            {
                if (rayhit.transform.root == origin.root) continue; //Dont Hit anything in this hierarchy
                if (Vector3.Distance(cam.transform.position, rayhit.point) < Vector3.Distance(cam.transform.position, origin.position)) continue; //If I hit something behind me skip
                if (hit.distance > rayhit.distance) hit = rayhit;
            }


            return hit;
        }

        public static Vector3 DirectionFromCameraNoRayCast(Camera cam, Vector3 ScreenCenter)
        {
            Ray ray = cam.ScreenPointToRay(ScreenCenter);

            return ray.direction;
        }

        /// <summary> Returns a RaycastHit to the center of the screen</summary>
        public static RaycastHit RayCastHitToCenter(Camera cam, Transform origin)
        {
            var Center = new Vector3(0.5f * Screen.width, 0.5f * Screen.height);

            return RayCastHitToCenter(cam, origin, Center);
        }


        /// <summary> Returns a RaycastHit to the center of the screen</summary>
        public static RaycastHit RayCastHitToCenter(Camera cam, Transform origin, LayerMask layerMask)
        {
            var Center = new Vector3(0.5f * Screen.width, 0.5f * Screen.height);

            return RayCastHitToCenter(cam, origin, Center, layerMask);
        }

        #endregion

        #region Vector Math

        public static float PowerFromAngle(Vector3 OriginPos, Vector3 TargetPos, float angle)
        {
            Vector2 OriginPos2 = new Vector2(OriginPos.x, OriginPos.z);
            Vector2 TargetPos2 = new Vector2(TargetPos.x, TargetPos.z);

            float distance = Vector2.Distance(OriginPos2, TargetPos2);
            float gravity = Physics.gravity.y;

            float OriginHeight = OriginPos.y;
            float TargetHeight = TargetPos.y;

            float cos = Mathf.Cos(angle * Mathf.Deg2Rad);
            float tan = Mathf.Tan(angle * Mathf.Deg2Rad);

            float SquareSpeed = gravity * Mathf.Pow(distance, 2) / (2 * Mathf.Pow(cos, 2) * (TargetHeight - OriginHeight - distance * tan));

            if (SquareSpeed <= 0.0f) return 0.0f; //Check there's no negative value

            return Mathf.Sqrt(SquareSpeed);
        }

        public static Vector3 VelocityFromPower(Vector3 OriginPos, float Power, float angle, Vector3 pos)
        {
            Vector3 hitPos = pos;
            OriginPos.y = 0f;
            hitPos.y = 0f;

            Vector3 dir = (hitPos - OriginPos).normalized;
            Quaternion Rot3D = Quaternion.FromToRotation(Vector3.right, dir);
            Vector3 vec = Power * Vector3.right;
            vec = Rot3D * Quaternion.AngleAxis(angle, Vector3.forward) * vec;

            return vec;
        }

        public static float DeltaAngle(Vector3 rFrom, Vector3 rTo, bool toDegrees = true)
        {
            if (rTo == rFrom) { return 0f; }

            Vector3 lCross = Vector3.Cross(rFrom, rTo);
            float lSign = (lCross.y < -0.0001f ? -1 : 1);

            float lDot = Vector3.Dot(rFrom, rTo);

            return lSign * Mathf.Atan2(lCross.magnitude, lDot) * (toDegrees ? Mathf.Rad2Deg : 1);
        }


        public static Vector3 NullVector = new Vector3(float.MinValue, float.MinValue, float.MinValue);
        /// <summary>Calculate a Direction from an origin to a target</summary>
        public static Vector3 DirectionTarget(Transform origin, Transform Target, bool normalized = true) =>
            DirectionTarget(origin.position, Target.position, normalized);


        public static Vector3 Quaternion_to_AngularVelocity(Quaternion quaternion)
        {
            quaternion.ToAngleAxis(out float angleInDegrees, out Vector3 rotationAxis);

            Vector3 angularDisplacement = rotationAxis * angleInDegrees * Mathf.Deg2Rad;
            Vector3 angularVelocity = angularDisplacement / Time.deltaTime;

            return angularVelocity;
        }

        public static Vector3 DirectionTarget(Vector3 origin, Vector3 Target, bool normalized = true)
        {
            if (normalized)
                return (Target - origin).normalized;

            return (Target - origin);
        }
        /// <summary>
        /// Gets the horizontal angle between two vectors. The calculation
        /// removes any y components before calculating the angle.
        /// </summary>
        /// <returns>The signed horizontal angle (in degrees).</returns>
        /// <param name="From">Angle representing the starting vector</param>
        /// <param name="To">Angle representing the resulting vector</param>
        public static float HorizontalAngle(Vector3 From, Vector3 To, Vector3 Up)
        {
            float lAngle = Mathf.Atan2(Vector3.Dot(Up, Vector3.Cross(From, To)), Vector3.Dot(From, To));
            lAngle *= Mathf.Rad2Deg;

            if (Mathf.Abs(lAngle) < 0.0001f) { lAngle = 0f; }

            return lAngle;
        }


        /// <summary>The angle between dirA and dirB around axis</summary>
        public static float AngleAroundAxis(Vector3 dirA, Vector3 dirB, Vector3 axis)
        {
            // Project A and B onto the plane orthogonal target axis
            dirA -= Vector3.Project(dirA, axis);
            dirB -= Vector3.Project(dirB, axis);

            // Find (positive) angle between A and B
            float angle = Vector3.Angle(dirA, dirB);

            // Return angle multiplied with 1 or -1
            return angle * (Vector3.Dot(axis, Vector3.Cross(dirA, dirB)) < 0 ? -1 : 1);
        }

        public static Vector3 ClosestPointOnLine(Vector3 vA, Vector3 vB, Vector3 vPoint)
        {
            var vVector1 = vPoint - vA;
            var vVector2 = (vB - vA).normalized;

            var d = Vector3.Distance(vA, vB);
            var t = Vector3.Dot(vVector2, vVector1);

            if (t <= 0)   return vA;

            if (t >= d)   return vB;

            var vVector3 = vVector2 * t;

            var vClosestPoint = vA + vVector3;

            return vClosestPoint;
        }
        #endregion

        #region Reflexion
        /// <summary> Creates a Delegate for the Property Set  </summary>
        public static UnityAction<T> Property_Set_UnityAction<T>(UnityEngine.Object component, string propName)
        {
            //Get property info required to fetch the setter method
            PropertyInfo prop = component.GetType().GetProperty(propName);

            //Create a Reference to the Setter of the property
            UnityAction<T> active = (UnityAction<T>)
            System.Delegate.CreateDelegate(typeof(UnityAction<T>), component, prop.GetSetMethod());

            return active;
        }
        #endregion

        #region Alignment Coroutines

        public static IEnumerator AlignTransform_Position(Transform t1, Vector3 NewPosition, float time, AnimationCurve curve = null)
        {
            float elapsedTime = 0;

            Vector3 CurrentPos = t1.position;

            while ((time > 0) && (elapsedTime <= time))
            {
                float result = curve != null ? curve.Evaluate(elapsedTime / time) : elapsedTime / time;               //Evaluation of the Pos curve
                t1.position = Vector3.LerpUnclamped(CurrentPos, NewPosition, result);
                elapsedTime += Time.deltaTime;

                yield return null;
            }
            t1.position = NewPosition;
        }

        public static IEnumerator AlignLookAtTransform(Transform t1, Transform t2, float time, AnimationCurve curve = null)
        {
            float elapsedTime = 0;

            Quaternion CurrentRot = t1.rotation;
            Vector3 direction = (t2.position - t1.position).normalized;
            direction.y = t1.forward.y;
            Quaternion FinalRot = Quaternion.LookRotation(direction);
            while ((time > 0) && (elapsedTime <= time))
            {
                float result = curve != null ? curve.Evaluate(elapsedTime / time) : elapsedTime / time;               //Evaluation of the Pos curve

                t1.rotation = Quaternion.SlerpUnclamped(CurrentRot, FinalRot, result);

                elapsedTime += Time.deltaTime;

                yield return null;
            }
            t1.rotation = FinalRot;
        }

        public static IEnumerator AlignLookAtTransform(Transform t1, Vector3 targetPosition, float time, AnimationCurve curve = null)
        {
            float elapsedTime = 0;

            Quaternion CurrentRot = t1.rotation;
            Vector3 direction = (targetPosition - t1.position).normalized;

            direction.y = t1.forward.y;

            Quaternion FinalRot = Quaternion.LookRotation(direction);


            while ((time > 0) && (elapsedTime <= time))
            {
                float result = curve != null ? curve.Evaluate(elapsedTime / time) : elapsedTime / time;               //Evaluation of the Pos curve

                t1.rotation = Quaternion.SlerpUnclamped(CurrentRot, FinalRot, result);

                elapsedTime += Time.deltaTime;

                yield return null;
            }
            t1.rotation = FinalRot;
        }

        public static IEnumerator AlignTransformRadius(Transform TargetToAlign, Transform AlignOrigin, float time, float radius, AnimationCurve curve = null)
        {
            float elapsedTime = 0;

            Vector3 CurrentPos = TargetToAlign.position;

            var Direction = (TargetToAlign.position - AlignOrigin.position).normalized;

            Debug.DrawRay(AlignOrigin.position, Direction, Color.cyan, 2);

            Ray TargetRay = new Ray(AlignOrigin.position, Direction);


            Vector3 TargetPos = TargetRay.GetPoint(radius);


            while ((time > 0) && (elapsedTime <= time))
            {
                float result = curve != null ? curve.Evaluate(elapsedTime / time) : elapsedTime / time;               //Evaluation of the Pos curve

                TargetToAlign.position = Vector3.LerpUnclamped(CurrentPos, TargetPos, result);

                elapsedTime += Time.deltaTime;

                yield return null;
            }
            TargetToAlign.position = TargetPos;
        }

        public static IEnumerator AlignTransform_Rotation(Transform t1, Quaternion NewRotation, float time, AnimationCurve curve = null)
        {
            float elapsedTime = 0;

            Quaternion CurrentRot = t1.rotation;

            while ((time > 0) && (elapsedTime <= time))
            {
                float result = curve != null ? curve.Evaluate(elapsedTime / time) : elapsedTime / time;               //Evaluation of the Pos curve
                t1.rotation = Quaternion.LerpUnclamped(CurrentRot, NewRotation, result);
                elapsedTime += Time.deltaTime;

                yield return null;
            }
            t1.rotation = NewRotation;
        }

        /// <summary> Aligns a transform to a new Local Position Rotation on its parents  </summary>
        public static IEnumerator AlignTransform(Transform obj, Vector3 LocalPos, Vector3 LocalRot, float time)
        {
            float elapsedtime = 0;
            Vector3 startPos = obj.localPosition;
            Quaternion startRot = obj.localRotation;

            while (elapsedtime < time)
            {
                obj.localPosition = Vector3.Slerp(startPos, LocalPos, Mathf.SmoothStep(0, 1, elapsedtime / time));
                obj.localRotation = Quaternion.Slerp(startRot, Quaternion.Euler(LocalRot), elapsedtime / time);
                elapsedtime += Time.deltaTime;
                yield return null;
            }

            obj.localPosition = LocalPos;
            obj.localEulerAngles = LocalRot;
        }

        public static IEnumerator AlignTransform(Transform obj, Vector3 LocalPos, Vector3 LocalRot, Vector3 localScale, float time)
        {
            float elapsedtime = 0;
            Vector3 startPos = obj.localPosition;
            Quaternion startRot = obj.localRotation;
            Vector3 startScale = obj.localScale;

            while (elapsedtime < time)
            {
                obj.localPosition = Vector3.Slerp(startPos, LocalPos, Mathf.SmoothStep(0, 1, elapsedtime / time));
                obj.localRotation = Quaternion.Slerp(startRot, Quaternion.Euler(LocalRot), elapsedtime / time);
                obj.localScale = Vector3.Lerp(startScale, localScale, Mathf.SmoothStep(0, 1, elapsedtime / time));

                elapsedtime += Time.deltaTime;
                yield return null;
            }

            obj.localPosition = LocalPos;
            obj.localEulerAngles = LocalRot;
            obj.localScale = localScale;
        }

        public static IEnumerator AlignTransform(Transform obj, TransformOffset offset, float time)
        {
            yield return AlignTransform(obj, offset.Position, offset.Rotation, offset.Scale, time);
        }

        #endregion

        #region Animator
        public static Keyframe[] DefaultCurve = { new Keyframe(0, 0), new Keyframe(1, 1) };

        public static bool SearchParameter(AnimatorControllerParameter[] parameters, string name)
        {
            foreach (AnimatorControllerParameter item in parameters)
            {
                if (item.name == name) return true;
            }
            return false;
        }

#if UNITY_EDITOR
        public static void AddParametersOnAnimator(UnityEditor.Animations.AnimatorController AnimController, UnityEditor.Animations.AnimatorController Mounted)
        {
            AnimatorControllerParameter[] parameters = AnimController.parameters;
            AnimatorControllerParameter[] Mountedparameters = Mounted.parameters;

            foreach (var param in Mountedparameters)
            {
                if (!SearchParameter(parameters, param.name))
                {
                    AnimController.AddParameter(param);
                }
            }
        }
#endif



        /// <summary> Resets all the Float Parameters on an Animator Controller </summary>

        public static void ResetFloatParameters(Animator animator)
        {
            foreach (AnimatorControllerParameter parameter in animator.parameters)                          //Set All Float values to their defaut (For all the Float Values on the Controller
            {
                if (animator.IsParameterControlledByCurve(parameter.name)) continue;

                if (parameter.type == AnimatorControllerParameterType.Float)
                {
                    animator.SetFloat(parameter.nameHash, parameter.defaultFloat);
                }
            }
        }

        /// <summary>  Finds if a parameter exist on a Animator Controller using its name </summary>
        public static bool FindAnimatorParameter(Animator animator, AnimatorControllerParameterType type, string ParameterName)
        {
            foreach (AnimatorControllerParameter parameter in animator.parameters)
            {
                if (parameter.type == type && parameter.name == ParameterName) return true;
            }
            return false;
        }

        /// <summary>Finds if a parameter exist on a Animator Controller using its nameHash </summary>
        public static bool FindAnimatorParameter(Animator animator, AnimatorControllerParameterType type, int hash)
        {
            foreach (AnimatorControllerParameter parameter in animator.parameters)
            {
                if (parameter.type == type && parameter.nameHash == hash) return true;
            }
            return false;
        }

        #endregion

        /// <summary>Check if x Seconds have elapsed since the Started Time </summary>
        internal static bool ElapsedTime(float StartTime, float intervalTime) => (Time.time - StartTime) >= intervalTime;

        ///------------------------------------------------------------EDITOR ONLY ------------------------------------------------------------

        #region Debug

        public static void DrawWireSphere(Vector3 position, Color color, float radius = 1.0f, float drawDuration = 0)
        {
#if UNITY_EDITOR
            float angle = 36;

            var x = new Vector3(position.x, position.y + radius * Mathf.Sin(0), position.z + radius * Mathf.Cos(0));
            var y = new Vector3(position.x + radius * Mathf.Cos(0), position.y, position.z + radius * Mathf.Sin(0));
            var z = new Vector3(position.x + radius * Mathf.Cos(0), position.y + radius * Mathf.Sin(0), position.z);

            Vector3 new_x;
            Vector3 new_y;
            Vector3 new_z;

            for (int i = 1; i <= 10; i++)
            {
                new_x = new Vector3(position.x, position.y + radius * Mathf.Sin(angle * i * Mathf.Deg2Rad), position.z + radius * Mathf.Cos(angle * i * Mathf.Deg2Rad));
                new_y = new Vector3(position.x + radius * Mathf.Cos(angle * i * Mathf.Deg2Rad), position.y, position.z + radius * Mathf.Sin(angle * i * Mathf.Deg2Rad));
                new_z = new Vector3(position.x + radius * Mathf.Cos(angle * i * Mathf.Deg2Rad), position.y + radius * Mathf.Sin(angle * i * Mathf.Deg2Rad), position.z);

                Debug.DrawLine(x, new_x, color, drawDuration);
                Debug.DrawLine(y, new_y, color, drawDuration);
                Debug.DrawLine(z, new_z, color, drawDuration);

                x = new_x;
                y = new_y;
                z = new_z;
            }
#endif
        }

        public static void DrawTriggers(Transform transform, Collider Trigger, Color DebugColor, bool always = false)
        {
            Gizmos.color = DebugColor;
            var DColorFlat = new Color(DebugColor.r, DebugColor.g, DebugColor.b, 1f);

            Gizmos.matrix = transform.localToWorldMatrix;

            if (Trigger && Trigger.enabled)
            {
                if (Trigger is BoxCollider)
                {
                    BoxCollider _C = Trigger as BoxCollider;
                    var sizeX = transform.lossyScale.x * _C.size.x;
                    var sizeY = transform.lossyScale.y * _C.size.y;
                    var sizeZ = transform.lossyScale.z * _C.size.z;
                    Matrix4x4 rotationMatrix = Matrix4x4.TRS(_C.bounds.center, transform.rotation, new Vector3(sizeX, sizeY, sizeZ));

                    Gizmos.matrix = rotationMatrix;
                    if (always || Application.isPlaying)
                    {
                        Gizmos.DrawCube(Vector3.zero, Vector3.one);
                        Gizmos.color = DColorFlat;
                        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
                    }
                }
                else if (Trigger is SphereCollider)
                {
                    SphereCollider _C = Trigger as SphereCollider;

                    Gizmos.matrix = transform.localToWorldMatrix;
                    if (always || Application.isPlaying)
                    {
                        Gizmos.DrawSphere(Vector3.zero + _C.center, _C.radius);
                        Gizmos.color = DColorFlat;
                        Gizmos.DrawWireSphere(Vector3.zero + _C.center, _C.radius);
                    }
                }
            }
        }

        public static void DebugCross(Vector3 center, float radius, Color color)
        {
            Debug.DrawLine(center - new Vector3(0, radius, 0), center + new Vector3(0, radius, 0), color);
            Debug.DrawLine(center - new Vector3(radius, 0, 0), center + new Vector3(radius, 0, 0), color);
            Debug.DrawLine(center - new Vector3(0, 0, radius), center + new Vector3(0, 0, radius), color);
        }

        public static void DebugPlane(Vector3 center, float radius, Color color, bool cross = false)
        {
            Debug.DrawLine(center - new Vector3(radius, 0, 0), center + new Vector3(0, 0, -radius), color);
            Debug.DrawLine(center - new Vector3(radius, 0, 0), center + new Vector3(0, 0, radius), color);
            Debug.DrawLine(center + new Vector3(0, 0, radius), center - new Vector3(-radius, 0, 0), color);
            Debug.DrawLine(center - new Vector3(0, 0, radius), center + new Vector3(radius, 0, 0), color);

            if (cross)
            {
                Debug.DrawLine(center - new Vector3(radius, 0, 0), center + new Vector3(radius, 0, 0), color);
                Debug.DrawLine(center - new Vector3(0, 0, radius), center + new Vector3(0, 0, radius), color);
            }
        }

        public static void DebugTriangle(Vector3 center, float radius, Color color)
        {
            Debug.DrawLine(center - new Vector3(radius, 0, 0), center + new Vector3(radius, 0, 0), color);
            Debug.DrawLine(center - new Vector3(0, 0, radius), center + new Vector3(0, 0, radius), color);

            Debug.DrawLine(center - new Vector3(0, -radius, 0), center + new Vector3(radius, 0, 0), color);
            Debug.DrawLine(center - new Vector3(0, -radius, 0), center + new Vector3(-radius, 0, 0), color);
            Debug.DrawLine(center - new Vector3(0, -radius, 0), center + new Vector3(0, 0, radius), color);
            Debug.DrawLine(center - new Vector3(0, -radius, 0), center + new Vector3(0, 0, -radius), color);

            Debug.DrawLine(center - new Vector3(radius, 0, 0), center + new Vector3(0, 0, -radius), color);
            Debug.DrawLine(center - new Vector3(radius, 0, 0), center + new Vector3(0, 0, radius), color);
            Debug.DrawLine(center + new Vector3(0, 0, radius), center - new Vector3(-radius, 0, 0), color);
            Debug.DrawLine(center - new Vector3(0, 0, radius), center + new Vector3(radius, 0, 0), color);
        }

        #endregion


        #region Serialized Property and Serialized Objects
#if UNITY_EDITOR

        public static GUIStyle Style(Color color)
        {
            GUIStyle currentStyle = new GUIStyle(GUI.skin.box) { border = new RectOffset(-1, -1, -1, -1) };


            Color[] pix = new Color[1];
            pix[0] = color;
            Texture2D bg = new Texture2D(1, 1);
            bg.SetPixels(pix);
            bg.Apply();


            currentStyle.normal.background = bg;

#if UNITY_2019_4 || UNITY_2020
            // MW 04-Jul-2020: Check if system supports newer graphics formats used by Unity GUI
            Texture2D bgActual = currentStyle.normal.scaledBackgrounds[0];

            if (SystemInfo.IsFormatSupported(bgActual.graphicsFormat, UnityEngine.Experimental.Rendering.FormatUsage.Sample) == false)
            {
                currentStyle.normal.scaledBackgrounds = new Texture2D[] { }; // This can't be null
            }
#endif
            return currentStyle;
        }



        public static object GetValue(this SerializedProperty property)
        {
            System.Type parentType = property.serializedObject.targetObject.GetType();
            System.Reflection.FieldInfo fi = parentType.GetField(property.propertyPath);
            return fi.GetValue(property.serializedObject.targetObject);
        }


        public static void SetValue(this SerializedProperty property, object value)
        {
            System.Type parentType = property.serializedObject.targetObject.GetType();
            System.Reflection.FieldInfo fi = parentType.GetField(property.propertyPath);//this FieldInfo contains the type.
            fi.SetValue(property.serializedObject.targetObject, value);
        }



        public static System.Type Get_Type(SerializedProperty property)
        {
            System.Type parentType = property.serializedObject.targetObject.GetType();
            var fi = parentType.GetFieldViaPath(property.propertyPath);
            return fi.FieldType;
        }
         

        public static FieldInfo GetFieldViaPath(this Type type, string path)
        {
            var flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            var parent = type;
            var fi = parent.GetField(path, flags);
            var paths = path.Split('.');

            for (int i = 0; i < paths.Length; i++)
            {
                fi = parent.GetField(paths[i], flags);

                // there are only two container field type that can be serialized:
                // Array and List<T>
                if (fi.FieldType.IsArray)
                {
                    parent = fi.FieldType.GetElementType();
                    i += 2;
                    continue;
                }

                if (fi.FieldType.IsGenericType)
                {
                    parent = fi.FieldType.GetGenericArguments()[0];
                    i += 2;
                    continue;
                }

                if (fi != null)
                {
                    parent = fi.FieldType;
                }
                else
                {
                    return null;
                }

            }

            return fi;
        }

        public static object GetValue(object source, string name)
        {
            if (source == null)  return null;
              

            var type = source.GetType();
            while (type != null)
            {
                var field = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                if (field != null)
                {
                    return field.GetValue(source);
                }
                var property = type.GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public |
                                                      BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (property != null)
                {
                    return property.GetValue(source, null);
                }
                type = type.BaseType;
            }
            return null;
        }

        public static void DrawScriptableObject(ScriptableObject serializedObject, bool showscript = true, int skip = 0)
        {
            if (serializedObject == null) return;

            SerializedObject serialied_element;
            serialied_element = new SerializedObject(serializedObject);
            serialied_element.Update();

            EditorGUI.BeginChangeCheck();

            var property = serialied_element.GetIterator();
            property.NextVisible(true);

            if (!showscript) property.NextVisible(true);

            for (int i = 0; i < skip; i++)
                property.NextVisible(true);

            do
            {
                EditorGUILayout.PropertyField(property, true);
            } while (property.NextVisible(false));

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(serializedObject, "Scriptable Object Changed");
                serialied_element.ApplyModifiedProperties();
                if (serializedObject != null) EditorUtility.SetDirty(serializedObject);
            }
        }

        public static void DrawScriptableObject(SerializedProperty property, bool internalInspector = true, string labelOverride = "")
        {
            if (property == null || property.propertyType != SerializedPropertyType.ObjectReference ||
                (property.objectReferenceValue != null && !(property.objectReferenceValue is ScriptableObject))) 
            {
                Debug.LogErrorFormat("Is not a ScriptableObject");
                return;
            }

            if (property.objectReferenceValue != null)
            {
                if (internalInspector)
                {
                    GUILayout.BeginHorizontal();
                    var title = string.IsNullOrEmpty(labelOverride) ? property.displayName : labelOverride;
                    property.isExpanded = EditorGUILayout.Foldout(property.isExpanded, title, true);

                    EditorGUILayout.PropertyField(property, GUIContent.none, true);

                    GUILayout.EndHorizontal();

                    if (GUI.changed) property.serializedObject.ApplyModifiedProperties();

                    if (property.objectReferenceValue == null) GUIUtility.ExitGUI();

                    if (property.isExpanded) DrawObjectReferenceInspector(property);
                }
                else
                {
                    EditorGUILayout.PropertyField(property);
                }
            }
            else
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.ObjectField(property);

                if (GUILayout.Button(new GUIContent("+", "Create"), GUILayout.Width(22), GUILayout.Height(18)))
                {
                    string selectedAssetPath = "Assets";

                    CreateAssetWithPath(property, selectedAssetPath);
                }
                EditorGUILayout.EndHorizontal();
            }

            property.serializedObject.ApplyModifiedProperties();
        }

        public static void AddScriptableAssetContextMenu(SerializedProperty property, Type type, string path)
        {
            var StatesType = MTools.GetAllTypes(type);

            StatesType.OrderBy(t => t.Name);

           var addMenu = new GenericMenu();

            for (int i = 0; i < StatesType.Count; i++)
            {
                Type st = StatesType[i];

                string name =  Regex.Replace(st.Name, @"([a-z])([A-Z])", "$1 $2");

                addMenu.AddItem(new GUIContent(name), false, () => CreateScriptableAsset(property, st, path ));
            }

            

            addMenu.ShowAsContext();
        }
        
        public static void AddScriptableAssetContextMenuInternal(SerializedProperty property, Type type)
        {
            var StatesType = MTools.GetAllTypes(type);

           var addMenu = new GenericMenu();

            for (int i = 0; i < StatesType.Count; i++)
            {
                Type st = StatesType[i];
                addMenu.AddItem(new GUIContent(st.Name), false, () => CreateScriptableAssetInternal(property, st));
            }

            addMenu.ShowAsContext();
        }


        public static string GetSelectedPathOrFallback()
        {
            string path = "Assets";

            foreach (UnityEngine.Object obj in Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets))
            {
                path = AssetDatabase.GetAssetPath(obj);
                if (!string.IsNullOrEmpty(path) && File.Exists(path))
                {
                    path = Path.GetDirectoryName(path);
                    break;
                }
            }
            return path;
        }

        public static void CreateAssetWithPath(SerializedProperty property, string selectedAssetPath)
        {
            Type type = Get_Type(property); //Get all the Types from an Abstract class

            if (type.IsAbstract)
            {
                var allTypes = MTools.GetAllTypes(type);

                var addMenu = new GenericMenu();

                for (int i = 0; i < allTypes.Count; i++)
                {
                    Type st = allTypes[i];

                    var Rname = st.Name;
                    addMenu.AddItem(new GUIContent(Rname), false, () => CreateScriptableAsset(property, st, selectedAssetPath));

                }

                addMenu.ShowAsContext();
                EditorGUILayout.EndHorizontal();
                property.serializedObject.ApplyModifiedProperties();
                return;
            }
            else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
            {
                type = type.GetGenericArguments()[0];
            }
            property.objectReferenceValue = CreateAssetWithSavePrompt(type, selectedAssetPath);
        }

        public static void DrawObjectReferenceInspector(SerializedProperty property)
        {
            if (property != null && property.objectReferenceValue != null)
                Editor.CreateEditor(property.objectReferenceValue).OnInspectorGUI();
        }

        public static void CreateScriptableAsset(SerializedProperty property,  Type type, string selectedAssetPath)
        {
            property.objectReferenceValue = CreateAssetWithSavePrompt(type, selectedAssetPath);
            property.serializedObject.ApplyModifiedProperties();
        }

        public static void CreateScriptableAsset(SerializedProperty property, Type type)
        {
            CreateScriptableAsset(property, type, MTools.GetSelectedPathOrFallback());
        }

        public static ScriptableObject CreateScriptableAsset(Type type)
        {
            return CreateAssetWithSavePrompt(type, MTools.GetSelectedPathOrFallback());
        }

        public static void CreateScriptableAssetInternal(SerializedProperty property, Type type)
        {
            property.objectReferenceValue = ScriptableObject.CreateInstance(type);
            property.serializedObject.ApplyModifiedProperties();
        }

        public static void CreateAsset(Type AssetType)
        {
            var asset = ScriptableObject.CreateInstance(AssetType);
            AssetDatabase.CreateAsset(asset, "Assets/New " + AssetType.Name + ".asset");
        }


        // Creates a new ScriptableObject via the default Save File panel
        static ScriptableObject CreateAssetWithSavePrompt(Type type, string path)
        {
            if (type == null) return null; //HACK

            string defaultName = string.Format("New {0}.asset", type.Name);
            string message = string.Format("Enter a file name for the {0} ScriptableObject.", type.Name);
            path = EditorUtility.SaveFilePanelInProject("Save ScriptableObject", defaultName, "asset", message, path);

            if (string.IsNullOrEmpty(path)) return null;
          

            ScriptableObject asset = ScriptableObject.CreateInstance(type);
            AssetDatabase.CreateAsset(asset, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
            EditorGUIUtility.PingObject(asset);
            return asset;
        }


        // Use this to add an object to an object array represented by a SerializedProperty.
        public static void AddObjectToArray<T>(this SerializedProperty arrayProperty, T elementToAdd)
            where T : UnityEngine.Object
        {
            // If the SerializedProperty this is being called from is not an array, throw an exception.
            if (!arrayProperty.isArray)
                throw new UnityException("SerializedProperty " + arrayProperty.name + " is not an array.");

            // Pull all the information from the target of the serializedObject.
            arrayProperty.serializedObject.Update();

            // Add a null array element to the start of the array then populate it with the object parameter.
            arrayProperty.InsertArrayElementAtIndex(0);
            arrayProperty.GetArrayElementAtIndex(0).objectReferenceValue = elementToAdd;

            //// Add a null array element to the end of the array then populate it with the object parameter.
            //arrayProperty.InsertArrayElementAtIndex(arrayProperty.arraySize);
            //arrayProperty.GetArrayElementAtIndex(arrayProperty.arraySize - 1).objectReferenceValue = elementToAdd;

            // Push all the information on the serializedObject back to the target.
            arrayProperty.serializedObject.ApplyModifiedProperties();
        }


        // Use this to remove the object at an index from an object array represented by a SerializedProperty.
        public static void RemoveObjectFromArrayAt(this SerializedProperty arrayProperty, int index)
        {
            // If the index is not appropriate or the serializedProperty this is being called from is not an array, throw an exception.
            if (index < 0)
                throw new UnityException("SerializedProperty " + arrayProperty.name + " cannot have negative elements removed.");

            if (!arrayProperty.isArray)
                throw new UnityException("SerializedProperty " + arrayProperty.name + " is not an array.");

            if (index > arrayProperty.arraySize - 1)
                throw new UnityException("SerializedProperty " + arrayProperty.name + " has only " + arrayProperty.arraySize + " elements so element " + index + " cannot be removed.");

            // Pull all the information from the target of the serializedObject.
            arrayProperty.serializedObject.Update();

            // If there is a non-null element at the index, null it.
            if (arrayProperty.GetArrayElementAtIndex(index).objectReferenceValue)
                arrayProperty.DeleteArrayElementAtIndex(index);

            // Delete the null element from the array at the index.
            arrayProperty.DeleteArrayElementAtIndex(index);

            // Push all the information on the serializedObject back to the target.
            arrayProperty.serializedObject.ApplyModifiedProperties();
        }


        // Use this to remove an object from an object array represented by a SerializedProperty.
        public static void RemoveObjectFromArray<T>(this SerializedProperty arrayProperty, T elementToRemove)
            where T : UnityEngine.Object
        {
            // If either the serializedProperty doesn't represent an array or the element is null, throw an exception.
            if (!arrayProperty.isArray)
                throw new UnityException("SerializedProperty " + arrayProperty.name + " is not an array.");

            if (!elementToRemove)
                throw new UnityException("Removing a null element is not supported using this method.");

            // Pull all the information from the target of the serializedObject.
            arrayProperty.serializedObject.Update();

            // Go through all the elements in the serializedProperty's array...
            for (int i = 0; i < arrayProperty.arraySize; i++)
            {
                SerializedProperty elementProperty = arrayProperty.GetArrayElementAtIndex(i);

                // ... until the element matches the parameter...
                if (elementProperty.objectReferenceValue == elementToRemove)
                {
                    // ... then remove it.
                    arrayProperty.RemoveObjectFromArrayAt(i);
                    return;
                }
            }

            throw new UnityException("Element " + elementToRemove.name + "was not found in property " + arrayProperty.name);
        }



#endif
        #endregion

        /// <summary>  Removes a Method from a Unity Event  </summary>

        public static void RemovePersistentListener(UnityEvent _event, string methodName, UnityEngine.Object Methodtarget)
        {
#if UNITY_EDITOR
            int isThere = -1;

            for (int i = 0; i < _event.GetPersistentEventCount(); i++)
            {
                var L_methName = _event.GetPersistentMethodName(i);
                UnityEngine.Object targetListener = _event.GetPersistentTarget(i);

                Debug.Log("Method: " + L_methName + " Target: " + targetListener);
                if (L_methName == methodName && targetListener == Methodtarget)
                {
                    isThere = i;
                    break;
                }
            }

            if (isThere != -1) UnityEditor.Events.UnityEventTools.RemovePersistentListener(_event, isThere);
#endif
        }

        /// <summary> Returns all the Instances created on the Project for an Scriptable Asset. WORKS ON EDITOR ONLY </summary>
        public static List<T> GetAllInstances<T>() where T : ScriptableObject
        {
#if UNITY_EDITOR
            if (Application.isEditor)
            {
                string[] guids = UnityEditor.AssetDatabase.FindAssets("t:" + typeof(T).Name);  //FindAssets uses tags check documentation for more info
                T[] a = new T[guids.Length];

                for (int i = 0; i < guids.Length; i++)         //probably could get optimized 
                {
                    string path = UnityEditor.AssetDatabase.GUIDToAssetPath(guids[i]);
                    a[i] = UnityEditor.AssetDatabase.LoadAssetAtPath<T>(path);
                }
                var aA = a.ToList();

                return aA;
            }
#endif
            return null;
        }

        /// <summary>Returns the Instance of an Scriptable Object by its name. WORKS ON EDITOR ONLY</summary>
        public static T GetInstance<T>(string name) where T : ScriptableObject
        {
#if UNITY_EDITOR
            if (Application.isEditor)
            {
                var allInstances = GetAllInstances<T>();

                T found = allInstances.Find(x => x.name == name);

                return found;
            }
#endif
            return null;
        }

        //------------------------------------------------------------------------------------------------------------------------------

    }


    [System.Serializable]
    public struct OverrideCapsuleCollider
    {
        public bool enabled;
        public bool isTrigger;  
        public Vector3 center;
        public float height;
        public int direction;
        public float radius;
        public PhysicMaterial material;

        [Utilities.Flag]
        public CapsuleModifier modify;

        public OverrideCapsuleCollider(CapsuleCollider collider)
        {
            enabled = collider.enabled;
            isTrigger = collider.isTrigger;
            center = collider.center;
            height = collider.height;
            radius = collider.radius;
            direction = collider.direction;
            material = collider.material;
            modify = 0;
        }


        public void Modify(CapsuleCollider collider)
        {
            if ((int)modify == 0) return; //Means that the animal have no modification

            if (Modify(CapsuleModifier.enabled)) collider.enabled = enabled;
            if (Modify(CapsuleModifier.isTrigger)) collider.isTrigger = isTrigger;
            if (Modify(CapsuleModifier.center)) collider.center = center;
            if (Modify(CapsuleModifier.height)) collider.height = height;
            if (Modify(CapsuleModifier.radius)) collider.radius = radius;
            if (Modify(CapsuleModifier.direction)) collider.direction = direction;
            if (Modify(CapsuleModifier.material)) collider.material = material;
        }


        public bool Modify(CapsuleModifier modifier)
        {
            return (modify & modifier) == modifier;
        }
    }

    public enum CapsuleModifier
    {
        enabled   = 1 << 0,
        isTrigger = 1 << 1,
        center    = 1 << 2,
        height    = 1 << 3,
        radius    = 1 << 4,
        direction = 1 << 5,
        material  = 1 << 6,
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(OverrideCapsuleCollider))]
    public class OverrideCapsuleColliderDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

           // GUI.Box(position, GUIContent.none, EditorStyles.helpBox);

            position.x += 2;
            position.width -= 2;

            position.y += 2;
            position.height -= 2;


            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            var height = EditorGUIUtility.singleLineHeight;

            #region Serialized Properties
            var modify = property.FindPropertyRelative("modify");
            var enabled = property.FindPropertyRelative("enabled");
            var isTrigger = property.FindPropertyRelative("isTrigger");
            var radius = property.FindPropertyRelative("radius");
            var center = property.FindPropertyRelative("center");
            var height1 = property.FindPropertyRelative("height");
            var direction = property.FindPropertyRelative("direction");
            var material = property.FindPropertyRelative("material");
            
            #endregion

            var line = position;
            var lineLabel = line;
            line.height = height;

            var foldout = lineLabel;
            foldout.width = 10;
            foldout.x += 10;

            EditorGUIUtility.labelWidth = 16;
            EditorGUIUtility.labelWidth = 0;


#if UNITY_2018_1_OR_NEWER
            modify.intValue = (int)(CapsuleModifier)EditorGUI.EnumFlagsField(line, label, (CapsuleModifier)(modify.intValue));
#else
            modify.intValue = (int)(CapsuleModifier)EditorGUI.EnumMaskField(line, label, (CapsuleModifier)(modify.intValue));
#endif

            line.y += height + 2;
         
            int ModifyValue = modify.intValue;

            if (Modify(ModifyValue, CapsuleModifier.enabled))
                DrawProperty(ref line, enabled);

            if (Modify(ModifyValue, CapsuleModifier.isTrigger))
                DrawProperty(ref line, isTrigger);

            if (Modify(ModifyValue, CapsuleModifier.material))
                DrawProperty(ref line, material);

            if (Modify(ModifyValue, CapsuleModifier.center))
                DrawProperty(ref line, center);

            if (Modify(ModifyValue, CapsuleModifier.radius))
                DrawProperty(ref line, radius);

            if (Modify(ModifyValue, CapsuleModifier.height))
                DrawProperty(ref line, height1);

            if (Modify(ModifyValue, CapsuleModifier.direction))
                DrawProperty(ref line, direction);



            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }

        private void DrawProperty(ref Rect rect, SerializedProperty property)
        {
            EditorGUI.PropertyField(rect, property);
            rect.y += EditorGUIUtility.singleLineHeight +2 ;
        }


        private bool Modify(int modify, CapsuleModifier modifier)
        {
            return ((modify & (int)modifier) == (int)modifier);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            int activeProperties = 0;

            var modify = property.FindPropertyRelative("modify");
            int ModifyValue = modify.intValue;

            if (Modify(ModifyValue, CapsuleModifier.enabled)) activeProperties++;
            if (Modify(ModifyValue, CapsuleModifier.center)) activeProperties++;
            if (Modify(ModifyValue, CapsuleModifier.height)) activeProperties++;
            if (Modify(ModifyValue, CapsuleModifier.radius)) activeProperties++;
            if (Modify(ModifyValue, CapsuleModifier.direction)) activeProperties++;
            if (Modify(ModifyValue, CapsuleModifier.isTrigger)) activeProperties++;
            if (Modify(ModifyValue, CapsuleModifier.material)) activeProperties++;
            float lines = (int)(activeProperties + 2);
            return base.GetPropertyHeight(property, label) * lines;// + (1 * lines);
        }
    }
#endif


}