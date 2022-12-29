using UnityEngine;

namespace Assets.Code
{
    public class PrefabManager : MonoBehaviour
    {
        public static PrefabManager Instance;

        //Add prefabs here

        //Networking
        public GameObject PlayerPrefab;

        public void Awake()
        {
            Instance = this;
        }


    }
}

