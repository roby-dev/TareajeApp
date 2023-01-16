using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tareaje.Data;

namespace Tareaje.Interfaces {
    public interface IOfficeService {
        Task<List<OfficeModel>> getAll();
        Task<bool> deleteOffice(long id);
    }
}
