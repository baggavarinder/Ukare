//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Models.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class SiteVisit
    {
        public int Id { get; set; }
        public string IPaddress { get; set; }
        public byte[] TimeStamp { get; set; }
        public System.DateTime LastVisitedDate { get; set; }
        public long VisitCount { get; set; }
    }
}
