using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NuclearBand
{
    [CreateAssetMenu(fileName = "AudioLibrary", menuName = "AdditionalAssets/AudioLibrary")]
    public class AudioLibrary : SerializedScriptableObject
    {
        [SerializeField]
        private readonly Dictionary<string, AudioClip> _library = new();

        public AudioClip GetAudio(string name)
        {
            if (!_library.ContainsKey(name))
            {
                Debug.LogWarning($"AudioLibrary does not contain {name} sound");
                return null;
            }

            return _library[name];
        }

        [ShowInInspector] List<AudioClip> sourceList = new ();

        [Button("Populate library")]
        public void PopulateLibrary()
        {
            foreach (var source in sourceList)
                _library.Add(source.name, source);
            sourceList.Clear();
        }
    }
}