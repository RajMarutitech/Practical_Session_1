using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class DestinationSchema
    {
        public int Id {  get; set; } 
        public int Foreign_Key { get; set; }
        public float Sum { get; set; }

        public DestinationSchema(int id, int fk, float sum) {
            Id = id;
            Foreign_Key = fk;
            Sum = sum;
        }
    }
}
