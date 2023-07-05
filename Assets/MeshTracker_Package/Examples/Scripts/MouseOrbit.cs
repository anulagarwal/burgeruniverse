using UnityEngine;
using System.Collections;

public class MouseOrbit : MonoBehaviour
{

    public Transform target;
    public Vector3 Offset;
    public bool IsRigidbodyTarget = true;
    [Space]
    public float distance = 5.0f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    public float distanceMin = .5f;
    public float distanceMax = 15f;

    float x = 0.0f;
    float y = 0.0f;

    public float SmoothSpeed_Rot = 12;
    public float SmoothSpeed_Pos = 12;

    // Use this for initialization
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    public void SwitchTargets(Transform targ)
    {
        target = targ;
    }

    void FixedUpdate()
    {
        if (IsRigidbodyTarget == false)
            return;
        if (target)
        {
            x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);

            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 15, distanceMin, distanceMax);

            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;

            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, SmoothSpeed_Rot * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, position+ Offset, SmoothSpeed_Pos * Time.deltaTime);
        }
    }
    void Update()
    {
        if (IsRigidbodyTarget == true)
            return;
        if (target)
        {
            x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);

            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;

            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, SmoothSpeed_Rot * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, position + Offset, SmoothSpeed_Pos * Time.deltaTime);
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}