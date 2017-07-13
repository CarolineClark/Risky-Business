using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speedH = 2.0f;
	public float speedV = 2.0f;
	public float speed = 2f;
	public float crouchSpeedFactor = 0.5f;
	public float jumpSpeed = 3.5F;
	public float gravity = 9.8F;
	public float quietSqueakVelocity = 1f;
	public float loudSqueakVelocity = 1.5f;

	private CharacterController characterController;
	private Vector3 moveDirection = Vector3.zero;
	private float yaw = 0.0f;
	private float pitch = 0.0f;
	private CursorLockMode cursorLockMode = CursorLockMode.Locked;
	private bool isCrouching = false;
	private Transform spawnPoint;
	private bool onSqueakyFloorboard = false;
	private AudioClip quietSqueak;
	private AudioClip loudSqueak;
	private bool frozen = true;
	private AudioClip[] squeakClips;
	private bool playingSoundFlag = false;

	void Start() {
		spawnPoint = GameObject.FindGameObjectWithTag(Constants.PLAYER_SPAWN_POINT_TAG).transform;
		characterController = GetComponent<CharacterController>();
		// quietSqueak = Resources.Load<AudioClip>("Sounds/draft-floorboards-creaking-sound");
		// loudSqueak = Resources.Load<AudioClip>("Sounds/draft-loud-floorboard-squeak");
		AudioClip clip1 = Resources.Load<AudioClip>("FinalSounds/Floor creak");
		AudioClip clip2 = Resources.Load<AudioClip>("FinalSounds/Floor creak 2");
		AudioClip clip3 = Resources.Load<AudioClip>("FinalSounds/Floor creak 3");
		AudioClip clip4 = Resources.Load<AudioClip>("FinalSounds/Floor creak 4");
		squeakClips = new AudioClip[] {clip1, clip2, clip3, clip4};
		SetPlayerPositionToSpawnPoint();
	}

	public void SetPlayerPositionToSpawnPoint() {
		transform.position = spawnPoint.position;
	}

	public void SetIsOnSqueakyFloorboard(bool onSqueaky) {
		onSqueakyFloorboard = onSqueaky;
	}

	public void SetPlayerFrozen(bool isFrozen) {
		frozen = isFrozen;
	}

	void Update() {
		if (!frozen) {
			RotatePlayer();
			MovePlayer();
			LockMouse();
			CheckForSqueaks();
		}
	}

	void RotatePlayer() {
		if (cursorLockMode == CursorLockMode.Locked) {
			yaw += speedH * Input.GetAxis("Mouse X");
			pitch -= speedV * Input.GetAxis("Mouse Y");
			transform.eulerAngles = new Vector3(Mathf.Clamp(pitch, -90, 90), yaw, 0.0f);
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
		if (Input.GetButton("Crouch")) {
			if (!isCrouching) {
				isCrouching = true;
				transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * 0.5f, transform.localScale.z);
				transform.position -= new Vector3(0, transform.position.y * 0.5f, 0);
			}
		}
		else
		{
			if (isCrouching) {
				isCrouching = false;
				transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * 2f, transform.localScale.z);
			}
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
			if (characterController.velocity.magnitude > quietSqueakVelocity) {
				StartCoroutine(PlaySoundTriggerEventAndWait());
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == Constants.SQUEAKY_FLOORBOARD_TAG) {
			SetIsOnSqueakyFloorboard(true);
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == Constants.SQUEAKY_FLOORBOARD_TAG) {
			SetIsOnSqueakyFloorboard(false);
		}
	}

	IEnumerator PlaySoundTriggerEventAndWait() {
		if (!playingSoundFlag) {
			playingSoundFlag = true;
			SoundManager.instance.RandomizeSfx(squeakClips);
			EventManager.TriggerEvent(Constants.SQUEAKY_FLOORBOARD_QUIET_EVENT);
			yield return new WaitForSeconds (1f);
			playingSoundFlag = false;
		}
	}
}
