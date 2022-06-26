using System;
using System.Collections;
using System.Collections.Generic;
using Hellmade.Sound;
using UniRx;
using UnityEngine;

namespace NuclearBand
{
    public static class AudioManager
    {
        public static AudioLibrary AudioLibrary;
        private static IDisposable soundtrackCoroutine;

        public static int PlayMusic(string name, bool loop)
        {
            return EazySoundManager.PlayMusic(AudioLibrary.GetAudio(name), 1.0f, loop, false);
        }

        public static void PlaySoundtrack(List<string> names, bool loop)
        {
            IEnumerator SoundtrackCoroutine(List<string> _names, bool _loop)
            {
                do
                {
                    foreach (var name in _names)
                    {
                        var clip = AudioLibrary.GetAudio(name);
                        EazySoundManager.PlayMusic(clip);
                        yield return new WaitForSeconds(clip.length);
                    }
                } while (_loop);

            }

            StopSoundtrack();
            soundtrackCoroutine = Observable.FromCoroutine(() => SoundtrackCoroutine(names, loop)).Subscribe();
        }

        public static void StopSoundtrack()
        {
            soundtrackCoroutine?.Dispose();
            EazySoundManager.StopAllMusic();
        }

        public static int PlaySound(string name)
        {
            return EazySoundManager.PlayUISound(AudioLibrary.GetAudio(name));
        }

        public static void TurnMusicOn()
        {
            EazySoundManager.GlobalMusicVolume = 1.0f;
        }

        public static void TurnMusicOff()
        {
            EazySoundManager.GlobalMusicVolume = 0.0f;
        }

        public static void TurnSoundOn()
        {
            EazySoundManager.GlobalUISoundsVolume = 1.0f;
        }

        public static void TurnSoundOff()
        {
            EazySoundManager.GlobalUISoundsVolume = 0.0f;
        }
    }
}