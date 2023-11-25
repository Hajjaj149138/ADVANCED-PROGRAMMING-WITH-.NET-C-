using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeroHungerV2.EF;

namespace ZeroHungerV2.Annotations
{
    public class UniqueUsername : ValidationAttribute
    {
        public override bool IsValid(object value)
        {

                var username = value.ToString();

              
                var db = new Assignment_Zero_HungerEntities();
                var data = (from u in db.Users 
                            where u.Username == username
                            select u).SingleOrDefault();
                if (data == null )
                 {
                return true;
                 }
            return false;

               
            

           
        }
    }
}