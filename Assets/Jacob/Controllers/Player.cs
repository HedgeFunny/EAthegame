using UnityEngine;

namespace Jacob.Controllers
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class Player : MonoBehaviour
	{
		public float jumpForce;
		public float moveSpeed;

		[Header("Ground Check Properties")] public bool checkForGround;
		public string groundTag;

		private Rigidbody2D _rigidbody;
		private float _horizontalInput;
		private bool _canJump = true;

		private void Awake()
		{
			_rigidbody = GetComponent<Rigidbody2D>();
			SetupRigidbody();
		}

		private void Update()
		{
			_horizontalInput = Input.GetAxis("Horizontal");
			JumpCheck();
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
		/// Setup properties of the Rigidbody2D the Script needs.
		/// </summary>
		private void SetupRigidbody()
		{
			_rigidbody.freezeRotation = true;
		}
		
		/// <summary>
		/// Checks if you are pressing your horizontalInput keys and changes your velocity with the value of
		/// horizontalInput which either is -1 for left, and 1 for right times the moveSpeed.
		/// </summary>
		private void Movement()
		{
			if (_horizontalInput is > 0.1f or < -0.1f)
			{
				_rigidbody.velocity = new Vector2(_horizontalInput * moveSpeed, _rigidbody.velocity.y);
			}
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
	}
}