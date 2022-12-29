using Unity.Netcode;
using UnityEngine;

namespace Assets.Code
{
    public class ComponentCameraFollower : NetworkBehaviour
    {
        private float xAngle = 30;
        public float Zoom = 50f;

        public void Start()
        {
        }

        public void LateUpdate()
        {
            if(IsOwner)
            {
                Vector3 direction = Camera.main.transform.forward;
                Camera.main.transform.rotation = Quaternion.Euler(xAngle, 0, 0);
                Camera.main.transform.position = transform.position - direction * Zoom;

                if(Input.mouseScrollDelta.y != 0)
                {
                    Zoom -= Input.mouseScrollDelta.y * 2f;
                    if (Zoom < 2)
                        Zoom = 2;
                    if (Zoom > 50)
                        Zoom = 50;

                }
            }
        }
    }
}
