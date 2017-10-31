using System;
using System.Collections.Generic;
using HairSalon.Models;

namespace HairSalon.ViewModels
{
    public class IndexModel
    {
        public List<Stylist> AllStylists {get; private set;}

        public IndexModel()
        {
            AllStylists = Stylist.GetAll();
        }
    }
}
