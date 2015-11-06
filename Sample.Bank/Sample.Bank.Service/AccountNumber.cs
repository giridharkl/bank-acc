using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Sample.Bank.Service
{
    public class AccountNumber
    {
        private string _accNum = string.Empty;
        private string _pattern = @"\d{6}\-\d{2,8}";
        private string _accNumLongFmt = string.Empty;
        private enum BankType { CommercialBanks, SavingCoOpBanks };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accNum"></param>
        public AccountNumber(string accNum)
        {
            if (string.IsNullOrWhiteSpace(accNum))
            {
                throw new ArgumentNullException();
            }
            else if (accNum.Remove(6,1).Length > 14) {
                throw new ArgumentOutOfRangeException();
            }
            _accNum = accNum;   
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string getLongFormat()
        {   
            Regex ex = new Regex(_pattern, RegexOptions.IgnoreCase);
            if (!ex.IsMatch(_accNum))
            {
                throw new ArgumentException();
            }
            addPaddingZeros();
            return _accNumLongFmt;
        }   

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private BankType accountType()
        {
            // 4,5 - Savings, CoOp Banks
            BankType type = _accNum[0] == '4' || _accNum[0] == '5' ? 
                BankType.SavingCoOpBanks : BankType.CommercialBanks;
            return type; 
        }

        /// <summary>
        /// 
        /// </summary>
        private void addPaddingZeros()
        {
            // StringBuilder partTwo = new StringBuilder();
            string partTwo = string.Empty;
            string padedAccNum = string.Empty;
            string partOne = string.Format(@"{0}", _accNum.Split('-')[0]);
            string temp = string.Format(@"{0}",  _accNum.Split('-')[1]);
            if (accountType() == BankType.CommercialBanks)
            {
                partTwo = temp.PadLeft(8,'0');
            }
            else // BankType.SavingCoOpBanks
            {
                // Copy to 7th position
                partTwo = partTwo + temp[0];
                // Copy ZEROs and concatenate
                partTwo = partTwo + temp.Substring(1).PadLeft(7,'0');
            }
            _accNumLongFmt = partOne + partTwo;
            if (!checkDigit()) { throw new ArgumentException("Check Digit Mismatch!"); }
        }

        private bool checkDigit()
        {
            string WeightedCoeff = @"2121212121212";
            int cDigit = 0;
            int sum = 0;
            int i = 0;
            foreach (var wc in WeightedCoeff) {
                int product = int.Parse(wc.ToString()) * int.Parse(_accNumLongFmt[i].ToString());
                int digitSum = product < 10 ? product : ((product /10) + (product % 10)); 
                sum = sum + digitSum;
                i++;
            }
            cDigit = RoundOffTo10(sum) - sum;
            return cDigit.ToString().Equals(_accNumLongFmt[13].ToString()) ? true : false;
        }

        private int RoundOffTo10(int num)
        {
            return (((num + 10)/10)*10);
        }
    }
}
