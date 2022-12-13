namespace Player
{
    public class Wallet
    {
        private int _savings;

        public Wallet(int savings) => _savings = savings;

        public int GetSavings() => _savings;

        public void AddSoftCurrency(int added)
        {
            _savings += added;
        }
        
        public bool SubtractSoftCurrency(int subtraction)
        {
            if (_savings <= 0 || _savings - subtraction <= 0) return false;

            _savings -= subtraction;
            return true;
        }
    }
}