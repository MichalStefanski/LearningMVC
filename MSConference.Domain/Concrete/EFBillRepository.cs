using System.Collections.Generic;
using MSConference.Domain.Abstract;
using MSConference.Domain.Entities;

namespace MSConference.Domain.Concrete
{
    public class EFBillRepository : IBillRepository
    {
        private EfDbContext context = new EfDbContext();

        public IEnumerable<Bill> Bills
        {
            get { return context.Bills; }
        }
    }
}
