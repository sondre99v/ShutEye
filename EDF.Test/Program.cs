using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EDF;

namespace EDF.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            EDFFile file = new EDFFile();
            file.readFile("../../../ExampleData/36.rec");

            
        }
    }
}
