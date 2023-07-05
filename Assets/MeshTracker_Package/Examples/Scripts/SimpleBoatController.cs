using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBoatController : MonoBehaviour {

    public GameObject rudderControl;

    public float speed = 1;
    public float acceleration = 1;
    public float maxspeed = 2;
    public float minspeed = -0.25f;
    private float heading = 0;
    private float rudder = 0;
    public float rudderDelta = 2;
    public float maxRudder = 6;
    public float bob = 0.1f;
    private float bobFrequency = 0.2f;

    private float elapsed = 0;
    private float seaLevel = 0;
    private float rudderAngle = 0;

    void Update()
    {
        // Bobbing
        elapsed += Time.deltaTime;

        Vector3 pos = transform.position;
        Vector3 eulerrot = transform.eulerAngles;

        pos.y = seaLevel + bob * Mathf.Sin(elapsed * bobFrequency * (Mathf.PI * 2));

        // Steering
        rudder += Input.GetAxis("Horizontal") * rudderDelta * Time.deltaTime;
        if (rudder > maxRudder)
        {
            rudder = maxRudder;
        }
        else if (rudder < -maxRudder)
        {
            rudder = -maxRudder;
        }
        if (rudderControl)
        {
            rudderAngle = rudder / maxRudder;
            rudderControl.transform.localEulerAngles = new Vector3(0, (70 * rudderAngle) % 360,0);
        }
        heading = (heading + rudder * Time.deltaTime * Mathf.Sqrt(speed)) % 360;

        eulerrot.y = heading;
        eulerrot.z = -rudder;

        // Sail
        speed += Input.GetAxis("Vertical") * acceleration * Time.deltaTime;
        if (speed > maxspeed)
        {
            speed = maxspeed;
        }
        else if (speed < minspeed)
        {
            speed = minspeed;
        }

        transform.position = pos;
        transform.eulerAngles = eulerrot;

        if (speed >= 9)
            bobFrequency = 1;
        else
            bobFrequency = 0.1f;

        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    void Awake()
    {
        seaLevel = transform.position.y;
    }

}
