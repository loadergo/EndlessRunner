using System;
using Assets.Scripts.Common;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance = null;

        public AudioMixer MainAudioMixer;
        public Toggle MuteToggle;

        public AudioSource MainGamaMusic;
        public AudioSource JumpSound;
        public AudioSource CoinSound;
        public AudioSource RhumSound;
        public AudioSource WaterSound;
        public AudioSource GetParrotSound;
        public AudioSource LoseParrotSound;
        public AudioSource DieMainSound;
        public AudioSource DieBurnSound;
        public AudioSource DiePitFallSound;
        public AudioSource DiePitFallWithParrotSound;


        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
        }

        public void PlaySound(Sounds sound)
        {
            switch (sound)
            {
                case Sounds.Jump:
                    JumpSound.Play();
                    break;
                case Sounds.Coin:
                    CoinSound.Play();
                    break;
                case Sounds.Rhum:
                    RhumSound.Play();
                    break;
                case Sounds.GetParrot:
                    GetParrotSound.Play();
                    break;
                case Sounds.LoseParrot:
                    LoseParrotSound.Play();
                    break;
                case Sounds.Die:
                    DieMainSound.Play();
                    break;
                case Sounds.PitFall:
                    DiePitFallSound.Play();
                    break;
                case Sounds.Water:
                    WaterSound.Play();
                    break;
                case Sounds.DieBurn:
                    DieBurnSound.Play();
                    break;
                case Sounds.PitFallWithParrot:
                    DiePitFallWithParrotSound.Play();
                    break;
            }
        }

        public void PlayMainGameMusic()
        {
            MainGamaMusic.Play();
        }

        public void StopMainGameMusic()
        {
            MainGamaMusic.Stop();
        }

       
    }
}
