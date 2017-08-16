using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class CanvasController : MonoBehaviour
    {

        public Text CoinsText;
        public Text CurrentScoreText;
        public Text BestHighScoreText;
        public Text BestDailyScoreText;

        public GameObject MenuPanel;
        public Button WaterButton;
        public Button ParrotButton;
        public Button TryAgainButton;

        private Text _parrotsCountText;
        private Text _waterCountText;


        private void Awake ()
        {
            _parrotsCountText = ParrotButton.GetComponentInChildren<Text>();
            _waterCountText = WaterButton.GetComponentInChildren<Text>();
        }

        public void SetInGameCanvas()
        {
            MenuPanel.SetActive(false);
            CurrentScoreText.gameObject.SetActive(true);
            
        }

        public void SetDieCanvas()
        {
            TryAgainButton.gameObject.SetActive(true);
        }




        public void SetCoins(int value)
        {
            CoinsText.text = value.ToString();
        }

        public void SetCurrentScore(int value)
        {
            CurrentScoreText.text = value.ToString();
        }
        public void SetHightScore(int value)
        {
            BestHighScoreText.text = value.ToString();
        }
        public void SetDailyScore(int value)
        {
            BestDailyScoreText.text = value.ToString();
        }

        public void SetParrotsCount(int value)
        {
            _parrotsCountText.text = value.ToString();
            ParrotButton.interactable = value > 0;
        }

        public void SetWaterCount(int value)
        {
            _waterCountText.text = value.ToString();
            WaterButton.interactable = value > 0;
        }

        public void SetDrunkseeCanvasActive(bool isActive)
        {
            WaterButton.gameObject.SetActive(isActive);
        }
    }
}
