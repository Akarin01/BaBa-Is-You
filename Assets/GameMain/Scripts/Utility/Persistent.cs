using UnityEngine;

namespace BabaIsYou
{
    public class Persistent : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}