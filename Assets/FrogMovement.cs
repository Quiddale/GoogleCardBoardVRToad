﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogMovement : MonoBehaviour {

	public float jumpElevationInDegrees = 45;
	public float jumpSpeedInCMPS = 5;
	public float jumpGroundClearance = 2;
	public float jumpSpeedTolerance = 5;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		bool isOnGround = Physics.Raycast (transform.position, -transform.up, jumpGroundClearance);
		Debug.DrawRay (transform.position, -transform.up * jumpGroundClearance);
		var speed = GetComponent<Rigidbody> ().velocity.magnitude;
		bool isNearStationary = speed < jumpSpeedTolerance;
		if (GvrViewer.Instance.Triggered && isOnGround && isNearStationary)
		{
			var camera = GetComponentInChildren<Camera>();
			var projectedLookDirection = Vector3.ProjectOnPlane(camera.transform.forward, Vector3.up);
			var radianToRotate = Mathf.Deg2Rad * jumpElevationInDegrees;
			var unnormalizedJumpDirection = Vector3.RotateTowards(projectedLookDirection, Vector3.up, radianToRotate, 0);
			var jumpVector = unnormalizedJumpDirection.normalized * jumpSpeedInCMPS;
			GetComponent<Rigidbody> ().AddForce(jumpVector, ForceMode.VelocityChange);
		}
	}
}
