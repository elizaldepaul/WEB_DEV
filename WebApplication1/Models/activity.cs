//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class activity
    {
        public int activity_id { get; set; }
        public string activity_name { get; set; }
        public string activity_date { get; set; }
        public Nullable<System.TimeSpan> activity_time { get; set; }
        public string activity_location { get; set; }
        public string activity_ootd { get; set; }
        public Nullable<int> user_id { get; set; }
        public string remarks { get; set; }
        public string activity_status { get; set; }
    }
}