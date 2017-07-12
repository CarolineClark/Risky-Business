using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	private CharacterController characterController;
	private Vector3 moveDirection = Vector3.zero;
	private float yaw = 0.0f;
	private float pitch = 0.0f;
	private CursorLockMode cursorLockMode;
	private KeyCode crouchKey = KeyCode.Tab;
	private bool isCrouching = false;
	private Transform spawnPoint;
	private bool onSqueakyFloorboard = false;

	// Make these public once doing level design
	private float speedH = 2.0f;
	private float speedV = 2.0f;
	private float speed = 2f;
	private float crouchSpeedFactor = 0.5f;
	private float jumpSpeed = 3.5F;
	private float gravity = 9.8F;
	private float quietSqueakVelocity = 1f;
	private float loudSqueakVelocity = 1.5f;

	void Start () {
		spawnPoint = GameObject.FindGameObjectWithTag(Constants.PLAYER_SPAWN_POINT_TAG).transform;
		characterController = GetComponent<CharacterController>();
		EventManager.StartListening(Constants.RESTART_GAME_EVENT, SetPlayerPositionToSpawnPoint);
		SetPlayerPositionToSpawnPoint();
	}

	public void SetPlayerPositionToSpawnPoint() {
		transform.position = spawnPoint.position;
	}

	void SetPlayerPositionToSpawnPoint(Hashtable h) {
		SetPlayerPositionToSpawnPoint();
	}

	public void SetIsOnSqueakyFloorboard(bool onSqueaky) {
		onSqueakyFloorboard = onSqueaky;
	}
	
	void Update () {
		RotatePlayer();
		MovePlayer();
		LockMouse();
		CheckForSqueaks();
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

			if (Input.GetButton("Jump") && !isCrouching) {
				moveDirection.y = jumpSpeed;    
			}

			CrouchPlayer();

			if (isCrouching) {
				moveDirection *= crouchSpeedFactor;
			}
		}
		moveDirection.y -= gravity * Time.deltaTime;
		characterController.Move(moveDirection * Time.deltaTime);
	}

	void CrouchPlayer() {
		if (Input.GetKeyDown(crouchKey)) {
			if (isCrouching) {
				transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * 2f, transform.localScale.z);
			} else {
				transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * 0.5f, transform.localScale.z);
				transform.position -= new Vector3(0, transform.position.y * 0.5f, 0);
			}
			isCrouching = !isCrouching;
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

	void CheckForSqueaks() {
		if (onSqueakyFloorboard) {
			if (characterController.velocity.magnitude > loudSqueakVelocity) {
				Debug.Log("loud squeak");
			}
			else if (characterController.velocity.magnitude > quietSqueakVelocity) {
				Debug.Log("quiet squeak");
			}
		}
	}
}
