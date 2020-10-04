using System;
using System.Collections.Generic;
using System.Text;

namespace INService.Services
{
    public interface IINService
    {
        void RunTask();
    }
    public class INService : IINService
    {
        public void RunTask()
        {
            Console.WriteLine("This task is run from INService");
        }
    }
}
