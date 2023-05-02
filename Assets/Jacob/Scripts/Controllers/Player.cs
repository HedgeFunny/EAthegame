using System;
using System.Collections;
using Jacob.Scripts.Data;
using UnityEngine;

namespace Jacob.Scripts.Controllers
{
	[RequireComponent(typeof(Rigidbody2D))]
	public partial class Player : MonoBehaviour
	{
		// Movement Properties
		public float jumpForce;
		public float moveSpeed;
		public bool topDown;
		public bool flipWhenTurningDirection;

		// Sprint Properties
		public bool enableSprinting;
		public float maxMoveSpeed;
		public float secondsUntilFullSprint;

		// Jumping Properties
		public bool checkForGround;
		public string groundTag;

		// Animation Properties
		public string animationParameter;
		public AnimationType animationType;
		public TrackInput inputToTrack;

		// Animation Properties (continued)
		public string jumpingAnimationParameter;

		// On Fire Ability Properties
		public KeyCode onFireAbilityKey;
		public bool onFireAbilityUnlocked;
		public float onFireTimer;

		// Sound Effect Properties
		public AudioClip walkingSoundEffect;
		public AudioClip jumpingSoundEffect;

		internal bool IsWalking;
		internal bool IsJumping;
		internal Vector2 Direction = Vector2.right;
		private Rigidbody2D _rigidbody;
		private Animator _animator;
		private float _horizontalInput;
		private bool _canJump = true;
		private bool _canControlMovement = true;
		private float _baseJumpForce;
		private float _baseMoveSpeed;
		private PlayerOnFire _playerOnFire;
		private bool _hasAnimator;
		private bool _hasOnFire;
		private bool _isOnFire;
		private float _verticalInput;
		private bool _firedAnimationError;
		private SpriteRenderer _spriteRenderer;
		private AudioSource _audioSource;
		private bool _hasAudioSource;

		private void Awake()
		{
			GetComponents();
			SetupRigidbody();
			RecordBaseStats();
		}

		private void Update()
		{
			if (_canControlMovement)
			{
				_horizontalInput = Input.GetAxis("Horizontal");
				IsWalking = _horizontalInput is > 0 or < -0;
				WalkingAudioCheck();


				if (topDown)
					_verticalInput = Input.GetAxis("Vertical");
			}

			AnimatorCheck();
			TopDownCheck();
			OnFireAbilityCheck();
			SprintCheck();
			FlipCheck();
		}

		private void FixedUpdate()
		{
			Movement();
		}

		private void OnCollisionEnter2D(Collision2D col)
		{
			GroundCheck(col);
		}
	}
}