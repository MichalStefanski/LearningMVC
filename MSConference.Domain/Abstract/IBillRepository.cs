using System.Collections.Generic;
using MSConference.Domain.Entities;

namespace MSConference.Domain.Abstract
{
    public interface IBillRepository
    {
        IEnumerable<Bill> Bills { get; }
    }
}
