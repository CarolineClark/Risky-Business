using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	private CharacterController characterController;
	private Vector3 moveDirection = Vector3.zero;
	private float yaw = 0.0f;
    private float pitch = 0.0f;
	private CursorLockMode wantedMode;
	
	// Make these public once doing level design
	private float speedH = 2.0f;
    private float speedV = 2.0f;
	private float speed = 2f;

	void Start () {
		characterController = GetComponent<CharacterController>();
	}
	
	void Update () {
		if (wantedMode == CursorLockMode.Locked) {
			yaw += speedH * Input.GetAxis("Mouse X");
        	pitch -= speedV * Input.GetAxis("Mouse Y");
        	transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
		}
		if (Input.GetMouseButtonDown(0)) {
			wantedMode = CursorLockMode.Locked;
			SetCursorState();
		}
		if (Input.GetKeyDown(KeyCode.Escape)) {
            Cursor.lockState = wantedMode = CursorLockMode.None;
			SetCursorState();
		}
 	}

	void SetCursorState() {
        Cursor.lockState = wantedMode;
        Cursor.visible = (CursorLockMode.Locked != wantedMode);
    }

	void FixedUpdate () {
		moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		characterController.Move(moveDirection * Time.deltaTime * speed);
	}
}
