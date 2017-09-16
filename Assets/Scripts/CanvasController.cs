using System.Collections;
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

        public GameObject GameOverText;

        private Text _parrotsCountText;
        private Text _waterCountText;

        public Text ShopCoinsText;
        public Button BuyWaterButton;
        public Button BuyParrotButton;
        public Text ShopParrotsCountText;
        public Text ShopWaterCountText;

        public GameObject ShopPanel;

        private bool _isMaxParrots;
        private bool _isMaxWater;


        private void Awake ()
        {
            _parrotsCountText = ParrotButton.GetComponentInChildren<Text>();
            _waterCountText = WaterButton.GetComponentInChildren<Text>();
        }

        public void SetInGameCanvas()
        {
            MenuPanel.SetActive(false);
            CurrentScoreText.gameObject.SetActive(true);
            GameOverText.SetActive(false);
        }

        public void SetDieCanvas()
        {
            SetParrotBoostCanvasActive(false);
            TryAgainButton.gameObject.SetActive(true);
            StartCoroutine(SetActiveAfterTime(GameOverText, true, 1f));
        }

        private IEnumerator SetActiveAfterTime(GameObject gObject, bool isActive, float time)
        {
            yield return new WaitForSeconds(time);

            gObject.SetActive(isActive);
        }


        public void SetCoins(int value)
        {
            CoinsText.text = value.ToString();
            ShopCoinsText.text = value.ToString();
            BuyParrotButton.interactable = (GameController.PARROT_COST <= value && !_isMaxParrots);
            BuyWaterButton.interactable = (GameController.WATER_COST <= value && !_isMaxWater);
        }

        public void SetCurrentScore(int value)
        {
            CurrentScoreText.text = value.ToString();
        }
        public void SetHightScore(int value)
        {
            BestHighScoreText.text = "Best: " + value.ToString();
        }
        public void SetDailyScore(int value)
        {
            BestDailyScoreText.text = "Daily: " + value.ToString();
        }

        public void SetParrotsCount(int value)
        {
            _parrotsCountText.text = value.ToString();
            ParrotButton.interactable = value > 0;
                _isMaxParrots = value >= 10;
                BuyParrotButton.interactable = !_isMaxParrots;
            ShopParrotsCountText.text = value >= 10 ? "MAX" : value.ToString();
        }

        public void SetWaterCount(int value)
        {
            _waterCountText.text = value.ToString();
            WaterButton.interactable = value > 0;
                _isMaxWater = value >= 10;
                BuyWaterButton.interactable = !_isMaxWater;
            ShopWaterCountText.text = value >= 10 ? "MAX" : value.ToString();
        }

        public void SetDrunkseeCanvasActive(bool isActive)
        {
            WaterButton.gameObject.SetActive(isActive);
        }

        public void SetParrotBoostCanvasActive(bool isActive)
        {
            ParrotButton.gameObject.SetActive(isActive);
        }

        public void ShowShopPanel()
        {
            ShopPanel.SetActive(true);
        }
    }
}
