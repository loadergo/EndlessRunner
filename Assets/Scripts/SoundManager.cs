using System;
using Assets.Scripts.Common;
using UnityEngine;

namespace Assets.Scripts
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance = null;

        public AudioSource MainGamaMusic;
        public AudioSource JumpSound;
        public AudioSource CoinSound;
        public AudioSource RhumSound;
        public AudioSource GetAndLoseParrotSound;
        public AudioSource DieMainSound;
        public AudioSource DiePitFallSound;


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
                    GetAndLoseParrotSound.Play();
                    break;
                case Sounds.LoseParrot:
                    GetAndLoseParrotSound.Play();
                    break;
                case Sounds.Die:
                    DieMainSound.Play();
                    break;
                case Sounds.PitFall:
                    DiePitFallSound.Play();
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
