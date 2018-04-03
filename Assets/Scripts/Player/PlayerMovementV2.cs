using Assets.Scripts.Game;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerMovementV2 : MonoBehaviour
    {
        public Transform ToFlip;

        private LayerMask _whatIsGround;
        private const float MaxSpeed = 400f;
        private const float JumpForce = 20f;


        private Transform _groundCheck;
        private const float GroundedRadius = .2f;
        private bool _grounded;
        private Transform _ceilingCheck;
        private const float CeilingRadius = .01f;
        private bool _jump;

        private Animator _animator;
        private Rigidbody2D _rb;
        //private SpriteRenderer _sr;

        private bool _facingRight = true;

        private void Awake()
        {
            _whatIsGround = LayerMask.GetMask("World");
            _groundCheck = transform.Find("GroundCheck");
            _ceilingCheck = transform.Find("CeilingCheck");
            _animator = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody2D>();
            //_sr = GetComponent<SpriteRenderer>();

            GameController.OnPlayerKilled += () => _rb.velocity = Vector2.zero;
        }

        private void Update()
        {
            if (GameController.IsPlayerDead)
                return;

            if (!_jump)
                _jump = Input.GetButtonDown("Jump");
            //_jump = CrossPlatformInputManager.GetButtonDown("Jump");
        }

        private void FixedUpdate()
        {
            if (GameController.IsPlayerDead)
                return;

            ConfigureGrounded();

            Move();

            _jump = false;
        }

        private void Move()
        {
            //var moveHor = CrossPlatformInputManager.GetAxis("Horizontal");
            var moveHor = Input.GetAxis("Horizontal");

            _animator.SetFloat("Speed", Mathf.Abs(moveHor));

            _rb.velocity = new Vector2(moveHor * MaxSpeed * Time.fixedDeltaTime, _rb.velocity.y);

            

            if (moveHor > 0)
                ToFlip.localScale = new Vector3(1, 1, 0);
            else if (moveHor < 0)
                ToFlip.localScale = new Vector3(-1, 1, 0);

            if (_grounded && _jump) // && _animator.GetBool("Ground"))
            {
                _grounded = false;
                //_animator.SetBool("Ground", false);
                _rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
                //_animator.SetBool("Idle", false);
                //_animator.SetBool("Running", false);
                //_animator.SetBool("Jumping", true);
                return;
            }

            //_animator.SetBool("Jumping", false);
            //_animator.SetBool(moveHor != 0f ? "Idle" : "Running", false);
            //_animator.SetBool(moveHor == 0f ? "Idle" : "Running", true); // suspect need some vertical movement check here too, perhaps just _rb.velocity?
        }

        private void ConfigureGrounded()
        {
            _grounded = false;

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            var colliders = Physics2D.OverlapCircleAll(_groundCheck.position, GroundedRadius, _whatIsGround);
            for (int i = 0; i < colliders.Length; i++)
                if (colliders[i].gameObject != gameObject)
                    _grounded = true;

            //_animator.SetBool("Ground", _grounded);
            //_animator.SetFloat("vSpeed", _rb.velocity.y);
        }

        // Keep in mind, this might not be ideal, i think sprite has a flip option
        private void Flip()
        {
            //_sr.flipX = !_sr.flipX;
        }
    }
}