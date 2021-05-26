using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeleBotService.Repository
{
    interface iLottoRepository : IDisposable
    {
        string GetLastestFirstPrice();
        string GetLastestLottNumber();

        string GetRecommendedNumbers();


    }
}
