using BLL;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var context = new EntityContext();
            var service = new EntityService(context);
            var menu = new Menu(service);

            menu.MainMenu();
        }
    }
}
