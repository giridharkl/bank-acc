using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Sample.Bank.Service
{
    public class AccountNumber
    {
        private string _accNum = string.Empty;
        private string pattern = @"\d{6}\-\d{2,8}";
        private string _accNumLongFmt = string.Empty;
        private enum BankType { CommercialBanks, SavingCoOpBanks };

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

        public string getLongFormat()
        {   
            Regex ex = new Regex(pattern, RegexOptions.IgnoreCase);
            if (!ex.IsMatch(_accNum))
            {
                throw new ArgumentException();
            }
            addPaddingZeros();
            return _accNumLongFmt;
        }   

        private BankType accountType()
        {
            BankType type = _accNum[0] == '4' || _accNum[0] == '5' ? 
                BankType.SavingCoOpBanks : BankType.CommercialBanks;
            return type; 
        }

        private void addPaddingZeros()
        {
            StringBuilder partTwo = new StringBuilder();
            string padedAccNum = string.Empty;
            string partOne = string.Format(@"{0}", _accNum.Split('-')[0]);
            string temp = string.Format(@"{0}",  _accNum.Split('-')[1]);
            if (accountType() == BankType.CommercialBanks)
            {
                //Copy Zeros 
                for (int i = 0; i < 8 - temp.Length; i++) {
                    partTwo.Append("0");
                }
                //Copy Digits
                foreach (var ch in temp) {
                    partTwo.Append(ch);
                }
            }
            else // BankType.SavingCoOpBanks
            {
                //Copy to 7th position
                partTwo.Append(temp[0]);
                //Copy Zeros
                for(int i = 0; i < 8 - temp.Length; i++)
                {
                    partTwo.Append("0");
                }
                //Copy remaining Digits
                for(int i= 1; i < temp.Length; i++)
                {
                    partTwo.Append(temp[i]);
                }
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
