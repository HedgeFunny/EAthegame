using System;
using System.Collections;
using Jacob.Scripts.Data;
using UnityEngine;

namespace Jacob.Scripts.Controllers
{
	public partial class Player
	{
		/// <summary>
		/// Gets the Components the Player needs and stores them.
		/// </summary>
		private void GetComponents()
		{
			Rigidbody = GetComponent<Rigidbody2D>();
			_hasAnimator = TryGetComponent(out _animator);
			_hasOnFire = TryGetComponent(out _playerOnFire);
			_spriteRenderer = GetComponent<SpriteRenderer>();
			_hasAudioSource = TryGetComponent(out _audioSource);
			_hasWallClimbing = TryGetComponent(out _wallClimbing);
			HasCollider2D = TryGetComponent(out Collider2D);
		}

		/// <summary>
		/// Setup properties of the Rigidbody2D the Script needs.
		/// </summary>
		private void SetupRigidbody()
		{
			Rigidbody.freezeRotation = true;
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
		/// Check if you're walking and plays audio in a loop until you've stopped walking.
		/// </summary>
		private void WalkingAudioCheck()
		{
			// If you don't have an Audio Source attached to the Player's GameObject, exit the method.
			if (!_hasAudioSource) return;
			// If you don't have a walkingSoundEffect, exit the method.
			if (!walkingSoundEffect) return;
			switch (_audioSource.isPlaying)
			{
				case false when IsWalking:
					// Set the Audio Source's clip to the walkingSoundEffect clip.
					_audioSource.clip = walkingSoundEffect;
					// Loop the clip.
					_audioSource.loop = true;
					// Start playing the clip
					_audioSource.Play();
					break;
				case true when !IsWalking || IsJumping:
					// Stop looping the clip. This should mean the audio will stop playing after the walking sound
					// effect is done
					_audioSource.loop = false;
					break;
			}
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
			// Checks if the animationParameter string is empty or null, and exits the method if it is.
			if (string.IsNullOrWhiteSpace(animationParameter)) return;
			// Exits the method if you don't have an animator
			if (!_hasAnimator) return;

			// A float that either tracks the _horizontalInput or the _verticalInput according to what axis you set
			// the Player Controller to track in the inspector.
			var inputType = inputToTrack switch
			{
				TrackInput.HorizontalInput => HorizontalInput,
				TrackInput.VerticalInput => _verticalInput,
				_ => throw new ArgumentOutOfRangeException()
			};

			switch (animationType)
			{
				case AnimationType.Float:
					// Set's the animationParameter's float value to the value of inputType
					_animator.SetFloat(animationParameter, inputType);
					break;
				case AnimationType.Boolean:
					// Sets the animationParameter's bool value to true if inputType is > 0 or < -0.
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
			if (_hasWallClimbing && _wallClimbing.TouchingWall) return;
			
			// If you don't have topDown enabled, check for jumping.
			if (!topDown)
			{
				JumpCheck();
			}
			
			// Disables gravity if you have topDown enabled.
			Rigidbody.gravityScale = topDown ? 0 : 1;
		}

		/// <summary>
		/// This method implements the On Fire Ability. It checks if you have the component first, then it checks if the
		/// ability is unlocked, then it checks if you're pressing the ability key, and then it checks if you're already
		/// on fire (it will return if you are). If all of those conditionals are true (except the onFire one), the
		/// player will go on fire and it will start a Coroutine.
		/// </summary>
		private void OnFireAbilityCheck()
		{
			if (!_hasOnFire) return;
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
			if (!enableSprinting) return;

			if (Input.GetKeyUp(KeyCode.LeftShift))
			{
				StartCoroutine(SlowDownCoroutine());
			}

			if (!Input.GetKeyDown(KeyCode.LeftShift)) return;
			print(Time.fixedDeltaTime);

			StartCoroutine(SprintCoroutine());
		}

		/// <summary>
		/// Flips the Sprite of the Player if the flipWhenTurningDirection boolean is enabled.
		/// Tracks the Player's Direction variable.
		/// </summary>
		private void FlipCheck()
		{
			if (!_canControlMovement) return;
			if (!flipWhenTurningDirection) return;

			if (Direction == Vector2.right)
			{
				_spriteRenderer.flipX = false;
			}
			else if (Direction == Vector2.left)
			{
				_spriteRenderer.flipX = true;
			}
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
			Rigidbody.velocity = new Vector2(HorizontalInput * moveSpeed,
				topDown ? _verticalInput * moveSpeed : Rigidbody.velocity.y);
			Direction = HorizontalInput switch
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
			IsJumping = false;
			if (_hasAnimator && !string.IsNullOrWhiteSpace(jumpingAnimationParameter))
				_animator.SetBool(jumpingAnimationParameter, IsJumping);
		}

		/// <summary>
		/// Checks if you are pressing your "Jump" key, which is Space on PC and checks if you canJump and it jumps.
		/// </summary>
		private void JumpCheck()
		{
			if (!Input.GetButtonDown("Jump")) return;
			if (!_canJump) return;
			IsJumping = true;

			if (_hasAnimator && !string.IsNullOrWhiteSpace(jumpingAnimationParameter))
			{
				_animator.SetBool(jumpingAnimationParameter, IsJumping);
				if (_hasAudioSource && jumpingSoundEffect) _audioSource.PlayOneShot(jumpingSoundEffect);
			}

			Rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
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
			HorizontalInput = 0;
		}

		/// <summary>
		/// This method enabled the ability to let the player control movement.
		/// </summary>
		public void EnableControllingMovement()
		{
			HorizontalInput = 0;
			_canControlMovement = true;
		}

		/// <summary>
		/// Set the _horizontalInput var.
		/// </summary>
		/// <param name="horizontalInput">A number around -1 and 1.</param>
		public void SetHorizontalInput(float horizontalInput)
		{
			HorizontalInput = horizontalInput;
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
			HorizontalInput = HorizontalInput switch
			{
				1 => -1,
				-1 => 1,
				_ => HorizontalInput
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