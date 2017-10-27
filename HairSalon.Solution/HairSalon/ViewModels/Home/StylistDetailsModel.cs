using System;
using System.Collections.Generic;
using HairSalon.Models;

namespace HairSalon.ViewModels
{
    public class StylistDetailsModel
    {
        public Stylist CurrentStylist {get; private set;}

        public StylistDetailsModel(int stylistId)
        {
            CurrentStylist = Stylist.FindById(stylistId);
        }
    }
}
