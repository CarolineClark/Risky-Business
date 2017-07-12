using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	private CharacterController characterController;
	private Vector3 moveDirection = Vector3.zero;
	private float yaw = 0.0f;
	private float pitch = 0.0f;
	private CursorLockMode cursorLockMode;

	// Make these public once doing level design
	private float speedH = 2.0f;
	private float speedV = 2.0f;
	private float speed = 2f;
	private float jumpSpeed = 3.5F;
	private float gravity = 9.8F;

	void Start () {
		characterController = GetComponent<CharacterController>();
	}
	
	void Update () {
		RotatePlayer();
		MovePlayer();
		DuckPlayer();
		LockMouse();
	 }

	void RotatePlayer() {
		if (cursorLockMode == CursorLockMode.Locked) {
			yaw += speedH * Input.GetAxis("Mouse X");
			pitch -= speedV * Input.GetAxis("Mouse Y");
			transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
		}
	}

	void MovePlayer() {
		if (characterController.isGrounded) {
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;
			if (Input.GetButton("Jump"))
				moveDirection.y = jumpSpeed;    
		}
		moveDirection.y -= gravity * Time.deltaTime;
		characterController.Move(moveDirection * Time.deltaTime);
	}

	void DuckPlayer() {
		if (Input.GetKeyDown(KeyCode.DownArrow)) {
			// TODO
		}
	}

	void LockMouse() {
		if (Input.GetMouseButtonDown(0)) {
			cursorLockMode = CursorLockMode.Locked;
			SetCursorState();
		}
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Cursor.lockState = cursorLockMode = CursorLockMode.None;
			SetCursorState();
		}
	}

	void SetCursorState() {
		Cursor.lockState = cursorLockMode;
		Cursor.visible = (CursorLockMode.Locked != cursorLockMode);
	}
}
