using System.Text.RegularExpressions;
using Player;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class ShopManager : MonoBehaviour
    {
        [SerializeField] private PlayerAttack _player;
        [SerializeField] private TMP_Text _savings;

        private Wallet _wallet;
        
        private void Start()
        {
            _wallet = new Wallet(20);
            UpdateSavings();
            EventManager.OnEnemyDied.AddListener(OnEnemyDied);
        }

        private void OnEnemyDied(Transform enemy) //TODO: create effect money
        {
            _wallet.AddSoftCurrency(Random.Range(10, 20));
            UpdateSavings();
        }
        
        public void BuySpeedAttack(TMP_Text tmpText)
        {
            var resultString = Regex.Match(tmpText.text, @"\d+").Value;
            
            if(!int.TryParse(resultString, out var price)) return;
            if(!_wallet.SubtractSoftCurrency(price)) return;
            
            _player.ChangeRateAttack(1.5f);
            UpdateSavings();
        }
        
        public void BuyPowerAttack(TMP_Text tmpText)
        {
            var resultString = Regex.Match(tmpText.text, @"\d+").Value;
            
            if(!int.TryParse(resultString, out var price)) return;
            if(!_wallet.SubtractSoftCurrency(price)) return;
            
            _player.ChangePowerAttack(30);
            UpdateSavings();
        }

        private void UpdateSavings()
        {
            _savings.text = _wallet.GetSavings().ToString();
        }
    }
}