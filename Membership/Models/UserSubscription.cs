//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Membership.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserSubscription
    {
        public int UserSubscriptionID { get; set; }
        public Nullable<int> SubscriptionID { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
    
        public virtual Subscription Subscription { get; set; }
    }
}
