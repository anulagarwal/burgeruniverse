using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotObjects : MonoBehaviour {

    public Texture Track_Graphic;
	
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject newSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            newSphere.transform.localScale = Vector3.one / 2;
            Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 2));
            newSphere.transform.position = pos;

            newSphere.AddComponent<MeshTracker.MeshTracker_Track>();
            newSphere.GetComponent<MeshTracker.MeshTracker_Track>().TrackLayer.Add(new MeshTracker.MeshTracker_Track.mmt_TrackLayer());
            newSphere.GetComponent<MeshTracker.MeshTracker_Track>().TrackLayer[0].mmt_TrackSize = 80;
            newSphere.GetComponent<MeshTracker.MeshTracker_Track>().TrackLayer[0].mmt_TrackGraphic = Track_Graphic;
            newSphere.GetComponent<MeshTracker.MeshTracker_Track>().TrackLayer[0].mmt_RayDirection = MeshTracker.MeshTracker_Track.mmt_TrackLayer.mmt_RayDirectionE.Down;
            newSphere.GetComponent<MeshTracker.MeshTracker_Track>().TrackLayer[0].mmt_RayDistance = 1;

            newSphere.AddComponent<Rigidbody>().AddForce(Vector3.forward * 300);
        }
    }
}
