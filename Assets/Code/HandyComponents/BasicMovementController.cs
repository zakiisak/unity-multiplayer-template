using Unity.Netcode;
using UnityEngine;

namespace Assets.Code.HandyComponents
{
    public class BasicMovementController : NetworkBehaviour
    {
        private Rigidbody rb;

        private int collisionCount;

        public void OnCollisionEnter(Collision collision)
        {
            collisionCount++;
        }

        public void OnCollisionExit(Collision collision)
        {
            collisionCount--;
        }

        public void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        public void Update()
        {
            if(IsOwner)
                UpdateControls();
        }

        private void UpdateControls()
        {
            const float speed = 15.0f;
            const float jumpSpeed = 5.0f;
            float mx = 0, my = 0, mz = 0;
            if(Input.GetKey(KeyCode.W) ||Input.GetKey(KeyCode.UpArrow))
            {
                mz += speed;
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                mz -= speed;
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                mx += speed;
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                mx -= speed;
            }
            if(Input.GetKey(KeyCode.Space) && collisionCount > 0)
            {
                my = jumpSpeed;
            }

            Vector2 circleDirection = MathUtils.Circle2D(Camera.main.transform.eulerAngles.y + 90);
            Vector3 direction = new Vector3(-circleDirection.x, 0, circleDirection.y);

            Vector3 velocityAdd = (direction * mz + Camera.main.transform.right * mx).normalized * speed;

            if (rb != null)
            {
                float newYVelocity = rb.velocity.y + my;

                if(my != 0 && newYVelocity > speed)
                    newYVelocity = speed;

                rb.velocity = new Vector3(velocityAdd.x, newYVelocity, velocityAdd.z);

            }
            else transform.position += velocityAdd;


        }

    }
}
