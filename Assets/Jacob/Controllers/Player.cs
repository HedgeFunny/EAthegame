using System;
using UnityEngine;

namespace Jacob.Controllers
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class Player : MonoBehaviour
	{
		public float jumpForce;
		public float moveSpeed;

		private Rigidbody2D _rigidbody;
		private float _horizontalInput;

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

		/// <summary>
		/// Setup properties of the Rigidbody2D the Script needs.
		/// </summary>
		private void SetupRigidbody()
		{
			_rigidbody.freezeRotation = true;
		}

		private void FixedUpdate()
		{
			Movement();
		}

		private void Movement()
		{
			if (_horizontalInput is > 0.1f or < -0.1f)
			{
				_rigidbody.velocity = new Vector2(_horizontalInput * moveSpeed, _rigidbody.velocity.y);
			}
		}

		private void JumpCheck()
		{
			if (!Input.GetButton("Jump")) return;
			_rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
		}
	}
}