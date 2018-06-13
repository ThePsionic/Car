using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour {

    public Transform target;
    public float smoothTime = 0.01f;
    private Vector3 velocity = Vector3.zero;
	
	void Update () {
        Vector3 targetPos = target.TransformPoint(new Vector3(0, 2, -1.2f));
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(30, target.transform.rotation.eulerAngles.y, 0), Time.time * 0.1f);
	}
}
