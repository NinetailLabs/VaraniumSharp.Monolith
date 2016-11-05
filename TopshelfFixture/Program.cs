using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaraniumSharp.Monolith.Configuration;
using VaraniumSharp.Monolith.HostSetup;
using VaraniumSharp.Monolith.Interfaces;

namespace TopshelfFixture
{
    class Program
    {
        static void Main(string[] args)
        {
            var runner = new TopShelfRunner(new TopShelfSerivce());
        }

        public class TopShelfSerivce : ITopShelfService
        {
            public ITopShelfConfiguration Configuration { get; }
            public void Start()
            {
                throw new NotImplementedException();
            }

            public void Stop()
            {
                throw new NotImplementedException();
            }
        }
    }
}
