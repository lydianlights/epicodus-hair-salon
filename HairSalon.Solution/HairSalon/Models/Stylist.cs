using System;
using System.Collections.Generic;

namespace HairSalon.Models
{
  public class Stylist
  {
      public int? DatabaseId {get; private set;}
      public string Name {get; private set;}
      public string Phone {get; private set;}
      public string Email {get; private set;}

      public Stylist(string name, string phone, string email)
      {
          Name = name;
          Phone = phone;
          Email = email;
          DatabaseId = null;
      }

      public override bool Equals(Object other)
      {
          if (!(other is Stylist))
          {
              return false;
          }
          else
          {
              Stylist otherStylist = (Stylist)other;
              return (
                this.Name == otherStylist.Name &&
                this.Phone == otherStylist.Phone &&
                this.Email == otherStylist.Email
              );
          }
      }

      public override int GetHashCode()
      {
          return
            this.Name.GetHashCode() +
            this.Phone.GetHashCode() +
            this.Email.GetHashCode();
      }

      public static List<Stylist> GetAll()
      {
          List<Stylist> output = new List<Stylist> {};
          return output;
      }
  }
}
