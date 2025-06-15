using Sirenix.OdinInspector;
using UnityEngine;

namespace ATG.Save
{
    public class PrefsCleaner: MonoBehaviour
    {
        [Button("Clean")]
        public void Clean()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}