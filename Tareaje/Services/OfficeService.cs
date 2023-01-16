using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tareaje.Interfaces;
using Tareaje.Data;

namespace Tareaje.Services {
    public class OfficeService : IOfficeService {
        static List<OfficeModel> offices = new List<OfficeModel>();
        public OfficeService() {          
            //offices.Add(new OfficeModel(1,"Soporte Remoto SAC", -18.080233, -70.296442, 1000));
            offices.Add(new OfficeModel(1,"ZofraTacna", -18.080517, -70.296932, 65));
        }

        public async Task<bool> deleteOffice(long id) {
            try {
                var index = offices.FindIndex(x => x.Id == id);
                offices.RemoveAt(index);
                return true;
            } catch (Exception) {
                return false;
            }
        }

        public async Task<List<OfficeModel>> getAll() {
            return offices;
        }
    }
}
