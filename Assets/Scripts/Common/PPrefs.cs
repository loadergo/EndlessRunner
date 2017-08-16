using System;
using UnityEngine;

namespace Assets.Scripts.Common
{
    public static class PPrefs
    {

        public static int Coins
        {
            get { return PlayerPrefs.GetInt("Coins"); }
            set { PlayerPrefs.SetInt("Coins", value); }
        }

        public static int Highscore
        {
            get { return PlayerPrefs.GetInt("Highscore"); }
            set { PlayerPrefs.SetInt("Highscore", value); }
        }

        public static int Dailybest
        {
            get { return PlayerPrefs.GetInt("Dailybest"); }
            set { PlayerPrefs.SetInt("Dailybest", value); }
        }

        public static int Parrots
        {
            get { return PlayerPrefs.GetInt("Parrots"); }
            set { PlayerPrefs.SetInt("Parrots", value); }
        }
        public static int Water
        {
            get { return PlayerPrefs.GetInt("Water"); }
            set { PlayerPrefs.SetInt("Water", value); }
        }

        //public static int LastPlayedDate
        //{
        //    get { return PlayerPrefs.GetInt("LastPlayedDate"); }
        //    set { PlayerPrefs.SetInt("Parrots", value); }
        //}
    
    }
}
