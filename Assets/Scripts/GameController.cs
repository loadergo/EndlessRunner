using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Background;
using Assets.Scripts.Common;
using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class GameController : MonoBehaviour
    {
        private bool _runIsStarted;
        private bool _haveParrot;

        private int _coins;
        private int _currentScore;
        private int _highScore;
        //private int _dailyBest;
        private int _parrotCount;
        private int _waterCount;

        private PlayerController _playerController;
        private LevelGenerator _levelGenerator;
        private CanvasController _canvasController;
        private List<SunDependent> _sunDependentElements;
        private BlackScreen _blackScreen;
        private Sun _sun;

        private static bool _IS_FIRST_LAUNCH = true;
        public static int WATER_COST = 50;
        public static int PARROT_COST = 100;

        public int Coins
        {
            get {return _coins;}
            set {_coins = value;_canvasController.SetCoins(_coins);}
        }

        public int CurrentScore
        {
            get {return _currentScore;}
            set {_currentScore = value; _canvasController.SetCurrentScore(_currentScore);}
        }

        public int HighScore
        {
            get {return _highScore;}
            set {_highScore = value; _canvasController.SetHightScore(_highScore);}
        }

        //public int DailyBest
        //{
        //    get {return _dailyBest;}
        //    set {_dailyBest = value; _canvasController.SetDailyScore(_dailyBest);}
        //}

        public int Parrots
        {
            get {return _parrotCount;}
            set {_parrotCount = value; _canvasController.SetParrotsCount(_parrotCount);}
        }

        public int WaterCount
        {
            get {return _waterCount;}
            set {_waterCount = value; _canvasController.SetWaterCount(_waterCount);}
        }

        private void Start ()
        {
            _levelGenerator = GetComponent<LevelGenerator>();
            _levelGenerator.GenerateStartWay();
            _sunDependentElements = FindObjectsOfType<SunDependent>().ToList();
            _playerController = FindObjectOfType<PlayerController>();
            _canvasController = FindObjectOfType<CanvasController>();
            _blackScreen = FindObjectOfType<BlackScreen>();
            _sun = FindObjectOfType<Sun>();
            InitializeScore();
            _blackScreen.OpenScreen();
        }

        private void Update()
        {
            CheckInput();
        }

        private void InitializeScore()
        {
            Coins = PPrefs.Coins;
            HighScore = PPrefs.Highscore;
            //DailyBest = PPrefs.Dailybest;
            Parrots = PPrefs.Parrots;
            WaterCount = PPrefs.Water;

            if (_IS_FIRST_LAUNCH)
            {
                Parrots = 5;
                PPrefs.Parrots = Parrots;
                Coins = 2000;
                PPrefs.Coins = Coins;
                WaterCount = 5;
                PPrefs.Water = WaterCount;


                _IS_FIRST_LAUNCH = false;
            }
            
        }

        public void StartRun()
        {
            _playerController.StartRun();
            //if (_haveParrot)
            //{
            //    PPrefs.Parrots = Parrots;
            //}
            _canvasController.SetInGameCanvas();
            BackgroungStartMove();
            _runIsStarted = true;
            SoundManager.Instance.PlayMainGameMusic();
        }

        private void CheckInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (_runIsStarted)
                {
                    ClickOnTheScreen();
                }
                else
                {
                    StartRun();
                }
            }

            if (Input.GetKeyDown(KeyCode.W) && _playerController.IsDrunk)
            {
                UseWater();
            }

            if (Input.GetKeyDown(KeyCode.E) && !_playerController.HaveParrot)
            {
                GetParrot();
            }
        }


        private void BackgroungStartMove()
        {
            _sunDependentElements.ForEach(x => x.Move());
            _sun.CanMove(true);
        }
        private void BackgroungStopMove()
        {
            _sunDependentElements.ForEach(x => x.Stop());
            _sun.CanMove(false);
        }

        public void ClickOnTheScreen()
        {
            if (_runIsStarted)
            {
                _playerController.ClickedToJump();
            }
            else
            {
                StartRun();
            }

        }

        public void ChangeDay(bool isNight)
        {
            _levelGenerator.ChangeDay(isNight);
            _sunDependentElements.ForEach(x => x.ChangeDay(isNight));
        }

        public void GetCoin()
        {
            SoundManager.Instance.PlaySound(Sounds.Coin);
            Coins++;
            PPrefs.Coins = Coins;
            _canvasController.SetCoins(Coins);
        }

        public void GetParrot()
        {
            if (Parrots <= 0) return;
                //if (_haveParrot) return;
                //if (Parrots <= 0)
                //{
                //    _canvasController.ShowShopPanel();
                //    return;
                //}

            if (!_playerController.IsAlive || _playerController.HaveParrot) return;
            _playerController.GetParrot();
            Parrots--;
            PPrefs.Parrots = Parrots;
            //_haveParrot = true;
        }
        

        public void AddRunScore(int amount)
        {
            CurrentScore += amount;
            if (CurrentScore > HighScore)
            {
                HighScore = CurrentScore;
                PPrefs.Highscore = HighScore;
            }
            //if (CurrentScore > DailyBest)
            //{
            //    DailyBest = CurrentScore;
            //    PPrefs.Dailybest = DailyBest;
            //}
        }

        public void UseWater()
        {
            if (WaterCount <= 0) return;

            WaterCount--;
            PPrefs.Water = WaterCount;
            _playerController.SetPlayerIsNotDrunked();
        }

        public void RestartLevel()
        {
            _blackScreen.CloseScreen(() => SceneManager.LoadScene(1));
        }

        public void PlayerIsDead()
        {
            _canvasController.SetDieCanvas();
            BackgroungStopMove();
            SoundManager.Instance.StopMainGameMusic();
        }

        public void BuyWater()
        {
            if (Coins < WATER_COST) return;

            Coins -= WATER_COST;
            PPrefs.Coins = Coins;
            WaterCount++;
            PPrefs.Water = WaterCount;
        }

        public void BuyParrot()
        {
            if (Coins < PARROT_COST) return;

            Coins -= PARROT_COST;
            PPrefs.Coins = Coins;
            Parrots++;
            PPrefs.Parrots = Parrots;
        }
    }
}
