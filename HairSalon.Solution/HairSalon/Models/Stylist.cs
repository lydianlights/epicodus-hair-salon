using System;
using System.Collections.Generic;

namespace HairSalon.Models
{
  public class Stylist
  {
      public string Name {get; private set;}
      public string Phone {get; private set;}
      public string Email {get; private set;}

      public Stylist(string name, string phone, string email)
      {
          Name = name;
          Phone = phone;
          Email = email;
      }

      public static List<Stylist> GetAll()
      {
          List<Stylist> output = new List<Stylist> {};
          return output;
      }
  }
}
