using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tareaje.Data {
    public class OfficeModel {
        public long Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude{ get; set; }
        public double Range { get; set; } = 1000;
        public double[] LatLng { get => new double[] { Latitude, Longitude }; }
        public string Coordenadas{ get => $"{Latitude}, {Longitude}"; }

        public OfficeModel(long id, string name, double latitude, double longitude, double range) {
            Id = id;
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
            Range = range;
        }
    }
}
