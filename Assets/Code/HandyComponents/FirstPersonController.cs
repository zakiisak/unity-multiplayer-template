using Unity.Netcode;
using UnityEngine;

namespace Assets.Code.HandyComponents
{
    public class FirstPersonController : NetworkBehaviour
    {
        private Camera Camera;
        public float lookSensitivity = 5.0f;
        public float radius = 10.0f;
        public bool invertYAxis = false;

        //Start looking down on the player from a 45 degree angle
        private float sin = -45, cos;

        // Start is called before the first frame update
        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Camera = Camera.main;
        }

        // Update is called once per frame
        void LateUpdate()
        {
            if (IsOwner && Cursor.lockState == CursorLockMode.Locked)
            {
                float x = Input.GetAxis("Mouse X");
                float y = Input.GetAxis("Mouse Y");

                cos += x * lookSensitivity * Time.deltaTime;
                sin += y * lookSensitivity * Time.deltaTime;

                Camera.transform.rotation = Quaternion.Euler(-sin, cos, 0);
            }
        }
    }
}
