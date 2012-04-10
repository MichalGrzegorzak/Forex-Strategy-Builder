using System;
using System.Collections.Generic;
using System.Text;
using Forex_Strategy_Builder.Enumerations;

namespace Forex_Strategy_Builder.MoneyManaging
{
    public class MoneyManager
    {
        MoneyManageStrategy mm = MoneyManageStrategy.None;

        public MoneyManager(MoneyManageStrategy mm) 
        { 
            this.mm = mm;
        
        }

        public double CountAmount(double entryAmount, double multipler, int consecutiveLosses, int consecutiveWins)
        {
            switch (mm)
            {
                default: return entryAmount;
                case MoneyManageStrategy.Martingale:
                {
                    if (consecutiveLosses > 0)
                    {
                        entryAmount = entryAmount * Math.Pow(multipler, consecutiveLosses);
                    }
                    break;
                }
                case MoneyManageStrategy.Martingale5:
                {
                    if (consecutiveLosses > 5)
                        consecutiveLosses = 0;

                    if (consecutiveLosses > 0)
                    {
                        entryAmount = entryAmount * Math.Pow(multipler, consecutiveLosses);
                    }
                    break;
                }
                case MoneyManageStrategy.AntyMartingale:
                    {
                        if (consecutiveWins > 5 || consecutiveLosses > 5)
                            consecutiveLosses = consecutiveWins = 0;
                        
                    if (consecutiveWins > 0)
                    {
                        entryAmount = entryAmount * multipler * consecutiveWins;
                    }
                    if (consecutiveLosses > 0)
                    {
                        entryAmount = entryAmount / (multipler * consecutiveLosses);
                    }
                    break;
                }

            }
            return entryAmount;
        }
    }
}
