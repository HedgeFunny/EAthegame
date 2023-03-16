using UnityEngine;

namespace Jacob.Controllers
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public class Player : MonoBehaviour
    {
        public float jumpForce;
        public float moveSpeed;

        [Header("Ground Check Properties")] public bool checkForGround;
        public string groundTag;

        [Header("Animation Properties")] public bool hasAnimator;
        public string animationParameter;

        private Rigidbody2D _rigidbody;
        private Animator _animator;
        private float _horizontalInput;
        private bool _canJump = true;
        private bool _canControlMovement = true;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            SetupRigidbody();
        }

        private void Update()
        {
            if (_canControlMovement)
                _horizontalInput = Input.GetAxis("Horizontal");
            AnimatorCheck();
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
            _rigidbody.velocity = new Vector2(_horizontalInput * moveSpeed, _rigidbody.velocity.y);
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
        /// Sets the animationParameter that you set in the script to the _horizontalInput float so you can check
        /// if you're walking and animate the character by checking if the animationParameter is greater than 0 or
        /// less than -0.
        /// </summary>
        private void AnimatorCheck()
        {
            if (!hasAnimator) return;
            _animator.SetFloat(animationParameter, _horizontalInput);
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
            _canControlMovement = true;
            _horizontalInput = 0;
        }

        /// <summary>
        /// Set the _horizontalInput var.
        /// </summary>
        /// <param name="horizontalInput">A number around -1 and 1.</param>
        public void SetHorizontalInput(long horizontalInput)
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
    }
}