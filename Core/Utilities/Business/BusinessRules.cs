using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Business
{
    public class BusinessRules
    {
        public static IResult Run(params IResult[] logics)
        {
            //Parametre ile gönderilen iş kurallarından başarız olanları business'a haberdar ediyoruz.
            foreach (var logic in logics)
            {
                if (!logic.Success)
                {
                    return logic; //Hata olan logic'i return ederiz.
                }
            }
            return null;
        } 
    }
}
