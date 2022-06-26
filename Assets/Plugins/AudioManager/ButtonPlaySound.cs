#nullable enable
using UnityEngine;
using UnityEngine.UI;

namespace NuclearBand
{
    public class ButtonPlaySound : MonoBehaviour
    {
        [SerializeField]
        private string _soundName = string.Empty;
        
        private void Awake()
        {
            var button = GetComponent<Button>();
            button.onClick.AddListener(PlaySound);
        }

        private void PlaySound()
        {
            AudioManager.PlaySound(_soundName);
        }
    }
}