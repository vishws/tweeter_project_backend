//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LoginAPI
{
    using System;
    using System.Collections.Generic;
    
    public partial class Retweet
    {
        public int RetweetID { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> TweetID { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
    
        public virtual Employeemaster Employeemaster { get; set; }
        public virtual Tweet Tweet { get; set; }
    }
}
