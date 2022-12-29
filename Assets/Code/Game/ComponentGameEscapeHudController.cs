using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Code.Game
{
    public class ComponentGameEscapeHudController : MonoBehaviour
    {
        public GameObject EscapeHud;

        public void Start()
        {
            EscapeHud.SetActive(false);
        }

        private CursorLockMode lastLockModeBeforeMenu;

        public void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                EscapeHud.SetActive(!EscapeHud.activeSelf);

                if(EscapeHud.activeSelf)
                {
                    lastLockModeBeforeMenu = Cursor.lockState;
                    Cursor.lockState = CursorLockMode.None;
                }
                else
                {
                    Cursor.lockState = lastLockModeBeforeMenu;
                }
            }
        }

        public void OnContinue()
        {
            EscapeHud.SetActive(false);
            Cursor.lockState = lastLockModeBeforeMenu;
        }

        public void OnLeave()
        {
            NetworkManager.Singleton.Shutdown();
            SceneManager.LoadScene(Constants.TITLESCREEN_BUILD_INDEX);
        }
    }
}
