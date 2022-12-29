using Unity.Netcode;
using UnityEngine;

namespace Assets.Code.HandyComponents
{
    public class ThirdPersonController : NetworkBehaviour
    {
        private Camera Camera;
        private float lookSensitivity = 150.0f;
        public float radius = 10.0f;
        private float destRadius = 10.0f;
        private float minZoom = 1.0f;
        public bool invertYAxis = false;

        private float sin = -45, cos;

        // Start is called before the first frame update
        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Camera = Camera.main;
        }

        public void SetDestRadius(float radius)
        {
            this.destRadius = radius;
        }

        public float GetDestRadius()
        {
            return this.destRadius;
        }

        //Make it have some default value in case some one calls the GoThirdPerson without having called the GoFirstPerson first.
        private float previousRadius = 5f;
        public void GoFirstPerson()
        {
            previousRadius = radius;
            minZoom = 0.0f;
            SetDestRadius(0.0f);
        }

        public void GoThirdPerson()
        {
            minZoom = 1.0f;
            SetDestRadius(previousRadius);
        }


        void LateUpdate()
        {
            if (IsOwner && Cursor.lockState == CursorLockMode.Locked)
            {
                float x = Input.GetAxis("Mouse X");
                float y = Input.GetAxis("Mouse Y");

                float zoom = Input.mouseScrollDelta.y;
                if (zoom != 0)
                {
                    this.destRadius -= zoom;
                    if (this.destRadius < minZoom)
                        this.destRadius = minZoom;
                }

                if (radius != destRadius)
                {
                    float diff = this.destRadius - this.radius;
                    this.radius += diff * Time.deltaTime * 10.0f;
                }
                if (radius < 0.0001f)
                    radius = 0.0001f;

                cos += x * lookSensitivity * Time.deltaTime;
                sin += y * lookSensitivity * Time.deltaTime;
                if (sin < -87)
                    sin = -87;
                if (sin > 87)
                    sin = 87;


                if (Mathf.Abs(radius) < 1)
                {
                    Camera.transform.position = transform.position;
                    Camera.transform.rotation = Quaternion.Euler(sin * -(invertYAxis ? -1 : 1), 180 + cos, 0);
                }
                else
                {
                    Quaternion rotation = Quaternion.Euler(sin * (invertYAxis ? -1 : 1), cos, 0);
                    float tempRadius = radius;
                    float stopRadius = 3;
                    if (sin > 0 || sin < -70)
                        stopRadius = 0;
                    for (; tempRadius > stopRadius; tempRadius -= 2)
                    {
                        if (!Physics.Raycast(transform.position - Camera.transform.forward * 3, -Camera.transform.forward, tempRadius))
                        {
                            break;
                        }
                    }
                    if (tempRadius < 1)
                        tempRadius = 1f;

                    Camera.transform.position = transform.position - (rotation * new Vector3(0, 0, -tempRadius));
                    Camera.transform.LookAt(transform.position);
                }
            }
        }
    }
}
