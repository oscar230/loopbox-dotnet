using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loopbox
{
    public class LoopboxLib
    {
        private Config config;
        public LoopboxLib()
        {

        }

        public bool Load(string filepath)
        {
            try
            {
                config = new Config(filepath);
                Debug.WriteLine("Succeeded loading config from: " + filepath);
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                Debug.WriteLine("Error loading config from " + filepath);
                config = null;
                return false;
            }
        }
    }
}
