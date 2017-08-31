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
            BuyParrotButton.interactable = GameController.PARROT_COST <= value;
            BuyWaterButton.interactable = GameController.WATER_COST <= value;
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
            ShopParrotsCountText.text = value.ToString();
            //ParrotButton.interactable = value > 0;
        }

        public void SetWaterCount(int value)
        {
            _waterCountText.text = value.ToString();
            ShopWaterCountText.text = value.ToString();
            WaterButton.interactable = value > 0;
        }

        public void SetDrunkseeCanvasActive(bool isActive)
        {
            WaterButton.gameObject.SetActive(isActive);
        }

        public void ShowShopPanel()
        {
            ShopPanel.SetActive(true);
        }
    }
}
