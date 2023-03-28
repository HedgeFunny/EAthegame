using System;
using System.Collections;
using Jacob.Scripts.Data;
using UnityEngine;

namespace Jacob.Scripts.Controllers
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class Player : MonoBehaviour
	{
		public float jumpForce;
		public float moveSpeed;
		public bool topDown;

		[Header("Sprint Properties")] public float maxMoveSpeed;
		public float secondsUntilFullSprint;

		[Header("Ground Check Properties")] public bool checkForGround;
		[HideInInspector] public string groundTag;

		[HideInInspector] public string animationParameter;
		[HideInInspector] public AnimationType animationType;
		[HideInInspector] public TrackInput inputToTrack;


		[HideInInspector] public KeyCode onFireAbilityKey;
		[HideInInspector] public bool onFireAbilityUnlocked;
		[HideInInspector] public float onFireTimer;

		internal Vector2 Direction = Vector2.right;
		private Rigidbody2D _rigidbody;
		private Animator _animator;
		private float _horizontalInput;
		private bool _canJump = true;
		private bool _canControlMovement = true;
		private float _baseJumpForce;
		private float _baseMoveSpeed;
		private PlayerOnFire _playerOnFire;
		internal bool HasAnimator;
		internal bool HasOnFire;
		private bool _isOnFire;
		private float _verticalInput;

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
				if (topDown)
					_verticalInput = Input.GetAxis("Vertical");
			}

			AnimatorCheck();
			TopDownCheck();
			OnFireAbilityCheck();
			SprintCheck();
		}

		private void FixedUpdate()
		{
			Movement();
		}

		private void OnCollisionEnter2D(Collision2D col)
		{
			GroundCheck(col);
		}

		/// <summary>
		/// Gets the Components the Player needs and stores them.
		/// </summary>
		private void GetComponents()
		{
			_rigidbody = GetComponent<Rigidbody2D>();

			if (GetComponent<Animator>())
			{
				_animator = GetComponent<Animator>();
				HasAnimator = true;
			}
			else
			{
				HasAnimator = false;
			}

			if (GetComponent<PlayerOnFire>())
			{
				_playerOnFire = GetComponent<PlayerOnFire>();
				HasOnFire = true;
			}
			else
			{
				HasOnFire = false;
			}
		}

		/// <summary>
		/// Setup properties of the Rigidbody2D the Script needs.
		/// </summary>
		private void SetupRigidbody()
		{
			_rigidbody.freezeRotation = true;
		}

		/// <summary>
		/// Record the Base stats of the Player when the Script starts.
		/// </summary>
		private void RecordBaseStats()
		{
			_baseJumpForce = jumpForce;
			_baseMoveSpeed = moveSpeed;
		}

		/// <summary>
		/// Sets the animationParameter that you set in the script to the _horizontalInput float so you can check
		/// if you're walking and animate the character by checking if the animationParameter is greater than 0 or
		/// less than -0. The Boolean feature also uses the _horizontalInput float but it uses a boolean based on if
		/// _horizontalInput is greater than 0 or less than -0. It also decides to track either _horizontalInput or
		/// _verticalInput based on what you set in inputToTrack.
		/// </summary>
		private void AnimatorCheck()
		{
			if (!HasAnimator) return;

			var inputType = inputToTrack switch
			{
				TrackInput.HorizontalInput => _horizontalInput,
				TrackInput.VerticalInput => _verticalInput,
				_ => throw new ArgumentOutOfRangeException()
			};

			switch (animationType)
			{
				case AnimationType.Float:
					_animator.SetFloat(animationParameter, inputType);
					break;
				case AnimationType.Boolean:
					_animator.SetBool(animationParameter, inputType is > 0 or < -0);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		/// <summary>
		/// Checks if you have the topDown bool enabled and changes the Player controller to act like a top down
		/// controller with no Gravity Scale and no Jumping.
		/// </summary>
		private void TopDownCheck()
		{
			if (!topDown)
			{
				JumpCheck();
			}

			_rigidbody.gravityScale = topDown ? 0 : 1;
		}

		/// <summary>
		/// This method implements the On Fire Ability. It checks if you have the component first, then it checks if the
		/// ability is unlocked, then it checks if you're pressing the ability key, and then it checks if you're already
		/// on fire (it will return if you are). If all of those conditionals are true (except the onFire one), the
		/// player will go on fire and it will start a Coroutine.
		/// </summary>
		private void OnFireAbilityCheck()
		{
			if (!HasOnFire) return;
			if (!onFireAbilityUnlocked) return;
			if (!Input.GetKeyDown(onFireAbilityKey)) return;
			if (_isOnFire) return;

			_playerOnFire.SetOnFire();
			StartCoroutine(OnFireCoroutine());
		}

		/// <summary>
		/// A Coroutine that sets the _isOnFire bool to true and waits the set amount of time on the onFireTimer float
		/// and then it Extinguishes the Fire and it sets _isOnFire to false.
		/// </summary>
		/// <returns>A Class that suspends the Coroutines execution for the set amount of time in onFireTimer</returns>
		private IEnumerator OnFireCoroutine()
		{
			_isOnFire = true;
			yield return new WaitForSeconds(onFireTimer);
			_playerOnFire.ExtinguishFire();
			_isOnFire = false;
		}

		/// <summary>
		/// Checks if you're pressing the Left Shift key, and if you are, it will activate a Sprint coroutine which
		/// works by changing your moveSpeed to the maxMoveSpeed dynamically in 2 seconds using some Math.
		/// </summary>
		private void SprintCheck()
		{
			if (Input.GetKeyUp(KeyCode.LeftShift))
			{
				StartCoroutine(SlowDownCoroutine());
			}

			if (!Input.GetKeyDown(KeyCode.LeftShift)) return;
			print(Time.fixedDeltaTime);

			StartCoroutine(SprintCoroutine());
		}

		private IEnumerator SprintCoroutine()
		{
			var time = 1 / Time.fixedDeltaTime * secondsUntilFullSprint;
			while (moveSpeed < maxMoveSpeed)
			{
				moveSpeed = Mathf.Clamp(moveSpeed += maxMoveSpeed / time, moveSpeed, maxMoveSpeed);
				yield return new WaitForFixedUpdate();
			}
		}

		private IEnumerator SlowDownCoroutine()
		{
			var time = 1 / Time.fixedDeltaTime * secondsUntilFullSprint;
			while (moveSpeed > _baseMoveSpeed)
			{
				moveSpeed = Mathf.Clamp(moveSpeed -= maxMoveSpeed / time, _baseMoveSpeed, moveSpeed);
				yield return new WaitForFixedUpdate();
			}
		}

		/// <summary>
		/// Checks if you are pressing your horizontalInput keys and changes your velocity with the value of
		/// horizontalInput which either is -1 for left, and 1 for right times the moveSpeed.
		/// </summary>
		private void Movement()
		{
			_rigidbody.velocity = new Vector2(_horizontalInput * moveSpeed,
				topDown ? _verticalInput * moveSpeed : _rigidbody.velocity.y);
			Direction = _horizontalInput switch
			{
				> 0 => Vector2.right,
				< -0 => Vector2.left,
				_ => Direction
			};
		}

		/// <summary>
		/// Checks if the checking for Ground feature is enabled and it checks if you landed on the ground with the
		/// specified tag and resets the canJump boolean to true.
		/// </summary>
		/// <param name="col">The collision object. OnCollisionEnter2D should provide this to this method.</param>
		private void GroundCheck(Collision2D col)
		{
			if (!checkForGround) return;
			if (!col.collider.CompareTag(groundTag)) return;
			_canJump = true;
		}

		/// <summary>
		/// Checks if you are pressing your "Jump" key, which is Space on PC and checks if you canJump and it jumps.
		/// </summary>
		private void JumpCheck()
		{
			if (!Input.GetButtonDown("Jump")) return;
			if (!_canJump) return;
			_rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
			if (checkForGround) _canJump = false;
		}

		/// <summary>
		/// This method lets you get the Player component on the object of the name you give to the function.
		/// </summary>
		/// <param name="nameOfPlayer">The GameObject name that has a Player component on it.</param>
		/// <returns>A Player component object.</returns>
		public static Player Get(string nameOfPlayer)
		{
			return GameObject.Find(nameOfPlayer).GetComponent<Player>();
		}

		/// <summary>
		/// This method disables the ability to let the player control movement.
		/// </summary>
		public void DisableControllingMovement()
		{
			_canControlMovement = false;
			_horizontalInput = 0;
		}

		/// <summary>
		/// This method enabled the ability to let the player control movement.
		/// </summary>
		public void EnableControllingMovement()
		{
			_horizontalInput = 0;
			_canControlMovement = true;
		}

		/// <summary>
		/// Set the _horizontalInput var.
		/// </summary>
		/// <param name="horizontalInput">A number around -1 and 1.</param>
		public void SetHorizontalInput(float horizontalInput)
		{
			_horizontalInput = horizontalInput;
		}

		/// <summary>
		/// Flip the player. 
		/// </summary>
		public void FlipPlayer()
		{
			transform.localEulerAngles = transform.localEulerAngles.y switch
			{
				0 => new Vector3(transform.localEulerAngles.x, 180, transform.localEulerAngles.z),
				180 => new Vector3(transform.localEulerAngles.x, 0, transform.localEulerAngles.z),
				_ => transform.localEulerAngles
			};
		}

		/// <summary>
		/// Flips the _horizontalInput float.
		/// </summary>
		public void FlipHorizontalInput()
		{
			_horizontalInput = _horizontalInput switch
			{
				1 => -1,
				-1 => 1,
				_ => _horizontalInput
			};
		}

		/// <summary>
		/// Increases the jumpForce float by 10% of the base jumpForce.
		/// </summary>
		public void UpgradeJumpForce()
		{
			jumpForce += CalculatePercentage(_baseJumpForce, 10);
		}

		/// <summary>
		/// Increases the moveSpeed float by 10% of the base moveSpeed.
		/// </summary>
		public void UpgradeMoveSpeed()
		{
			moveSpeed += CalculatePercentage(_baseMoveSpeed, 10);
		}

		/// <summary>
		/// Calculate a Percentage of a number using a math equation.
		/// </summary>
		/// <param name="number">The number you want to calculate the percentage of.</param>
		/// <param name="percentage">The percentage that you want to get the percentage of the number.</param>
		/// <returns>A number that is a percentage of the number you provided.</returns>
		private static float CalculatePercentage(float number, float percentage)
		{
			return number * percentage / 100;
		}

		/// <summary>
		/// Fling the Player in a Direction stopping at a distance represented in Meters.
		/// </summary>
		/// <param name="direction">The Direction you want to fling the Player.</param>
		/// <param name="meters">How many meters you want to fling them.</param>
		public void FlingPlayer(Vector2 direction, float meters)
		{
			var originalTransform = new Vector3(0, 0, 0) + transform.position;
			DisableControllingMovement();
			moveSpeed = 30;
			SetHorizontalInput(direction == Vector2.right ? 1 : -1);
			StartCoroutine(FlingCoroutine(direction, originalTransform, meters));
		}

		/// <summary>
		/// A Coroutine that checks while you can't control your movement, if your position is past the distance the
		/// player will be flung and it will re-enable movement and it will stop the fling.
		/// </summary>
		/// <param name="direction">The Direction you want to fling the Player.</param>
		/// <param name="meters">How many meters you want to fling them.</param>
		/// <param name="originalTransform">The original Transform of the player.</param>
		/// <returns></returns>
		private IEnumerator FlingCoroutine(Vector2 direction, Vector3 originalTransform, float meters)
		{
			while (!_canControlMovement)
			{
				if (direction == Vector2.right
					    ? transform.position.x > originalTransform.x + meters
					    : transform.position.x < originalTransform.x - meters)
				{
					moveSpeed = _baseMoveSpeed;
					EnableControllingMovement();
				}
				else
				{
					yield return new WaitForFixedUpdate();
				}
			}
		}

		/// <summary>
		/// Disable jumping.
		/// </summary>
		public void DisableJumping()
		{
			_canJump = false;
		}

		/// <summary>
		/// Enable jumping.
		/// </summary>
		public void EnableJumping()
		{
			_canJump = true;
		}
	}
}