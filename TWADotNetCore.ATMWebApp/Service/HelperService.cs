using TWADotNetCore.ATMWebApp.Helper;
using Microsoft.EntityFrameworkCore;

namespace TWADotNetCore.ATMWebApp.Service
{
    public class HelperService
    {
        private readonly AtmDbContext _context;

        public HelperService(AtmDbContext context)
        {
            _context = context;
        }

        public string GetAtmCode()
        {
        regenerate:
            var atmCode = DevCode.GenerateAtmCode().SplitSpace();
            var existAtmCode = _context.AtmCard.AsNoTracking().Any(x => x.CardNo == atmCode);
            if (!existAtmCode) return atmCode;
            goto regenerate;
        }
    }
}
