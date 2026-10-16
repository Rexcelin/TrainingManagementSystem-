using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingManagementSystem.exs

{
    public class SearchByCodeException : Exception
    {
        public SearchByCodeException(string msg):base(msg) {
            Console.WriteLine("Invaild searchcode");
        
        }
    }
}
