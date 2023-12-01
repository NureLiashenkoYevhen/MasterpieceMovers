using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Analysis
{
    public class AnalysisModel : IModel
    {
        public string Metrics { get; set; }

        public string Value { get; set; }

        public DateTime TimeSpan { get; set; }
    }
}
