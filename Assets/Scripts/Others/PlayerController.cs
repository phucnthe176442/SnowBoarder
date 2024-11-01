using System.Collections;
using TMPro;
using UnityEngine;

namespace snow_boarder
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float torqueAmount = 1f;
        [SerializeField] float normalSpeed = 20f;
        [SerializeField] float boostSpeed = 40f;
        private float jumpingPower = 100f;

        SurfaceEffector2D surfaceEffector2D;
        bool canMove = true;

        private bool isJumping;

        private float coyoteTime = 0.2f;
        private float coyoteTimeCounter;

        private float jumpBufferTime = 0.2f;
        private float jumpBufferCounter;

        Rigidbody2D rb2d;
        CapsuleCollider2D boardCollider;

        private float rotationTime;
        public float RotationTime => rotationTime;

        // Start is called before the first frame update
        void Start()
        {
            rb2d = GetComponent<Rigidbody2D>();
            boardCollider = GetComponent<CapsuleCollider2D>();
            surfaceEffector2D = FindObjectOfType<SurfaceEffector2D>();
        }

        // Update is called once per frame
        void Update()
        {
            if (canMove)
            {
                RotatePlayer();
                RespondToBoost();
                RespondToJump();
            }
        }

        public void DisableControls()
        {
            canMove = false;
        }

        void RespondToBoost()
        {
            if (Input.GetKey(KeyCode.W)) surfaceEffector2D.speed = boostSpeed;
        }

        private float _lastTimeJump;

        private void RespondToJump()
        {
            if (IsTouchingGroundLayer()) coyoteTimeCounter = coyoteTime;
            else coyoteTimeCounter -= Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Space)) jumpBufferCounter = jumpBufferTime;
            else jumpBufferCounter -= Time.deltaTime;

            if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f && !isJumping)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpingPower);
                jumpBufferCounter = 0f;
                StartCoroutine(JumpCooldown());
            }

            if (Input.GetKey(KeyCode.Space) && rb2d.velocity.y > 0f)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y * 0.5f);
                coyoteTimeCounter = 0f;
            }
        }

        void RotatePlayer()
        {
            if (Input.GetKey(KeyCode.A))
            {
                rb2d.AddTorque(torqueAmount);
                GameManager.Instance.Score += Time.deltaTime; 
            }
            else if (Input.GetKey(KeyCode.D))
            {
                rb2d.AddTorque(-torqueAmount);
                GameManager.Instance.Score += Time.deltaTime; 
            }
        }
        bool IsTouchingGroundLayer()
        {
            return (boardCollider.IsTouchingLayers(LayerMask.GetMask("Ground")));
        }

        private IEnumerator JumpCooldown()
        {
            isJumping = true;
            yield return new WaitForSeconds(0.4f);
            isJumping = false;
        }
    }
}
