using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farming.Model
{
    public class Symptom
    {
        public int SymptomId
        {
            get;
            set;
        }

        public string SymptomDescription
        {
            get;
            set;
        }

        public string Resolution
        {
            get;
            set;
        }

        public int ResolutionId
        {
            get;
            set;
        }

    }
}
