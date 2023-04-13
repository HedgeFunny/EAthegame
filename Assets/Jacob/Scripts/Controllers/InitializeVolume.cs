using Jacob.Scripts.Data;
using UnityEngine;

namespace Jacob.Scripts.Controllers
{
    public class InitializeVolume : MonoBehaviour
    {
        private void Awake()
        {
           SetVolume(); 
        }

        /// <summary>
        /// Sets the Volume according to what the current set Volume is in the SettingsManager.
        /// </summary>
        public static void SetVolume()
        {
            if (AudioListener.volume == SettingsManager.Settings.Volume) return;
            var volume = SettingsManager.Settings.Volume;
            AudioListener.volume = volume;
            print($"Volume has been set to {volume}");
        }
    }
}
