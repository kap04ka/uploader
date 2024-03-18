using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uploader
{
    public interface IView
    {
        public string speed { get; set; }
        public float progress { get; set; }
        void speedCalculate(long bytes);
    }
}
