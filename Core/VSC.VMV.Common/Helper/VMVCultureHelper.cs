using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VSC.VMV.Common.Models;

namespace VSC.VMV.Common.Helper
{
    public static class VMVCultureHelper
    {
        public static List<VMVCultureInfo> GetAvailableEnglishCultures()
        {
            List<CultureInfo> cultureInfos = CultureInfo.GetCultures(CultureTypes.SpecificCultures).ToList();
            cultureInfos.RemoveAll(c => !c.Name.Contains("en-"));
            List<VMVCultureInfo> cultures = new List<VMVCultureInfo>();
            foreach (var cultureInfo in cultureInfos)
            {
                var regionInfo = new RegionInfo(cultureInfo.Name);
                cultures.Add(new VMVCultureInfo()
                {
                    CultureName = cultureInfo.Name,
                    CountryName = regionInfo.DisplayName,
                    Currency = cultureInfo.NumberFormat.CurrencySymbol
                });
            }
            return cultures;
        }
    }
}
