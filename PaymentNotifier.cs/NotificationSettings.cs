//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PaymentNotifier
{
    using System;
    using System.Collections.Generic;
    
    public partial class NotificationSettings
    {
        public NotificationSettings()
        {
            this.Notifications = new HashSet<Notifications>();
        }
    
        public int NotificationSettingsId { get; set; }
        public string Pattern { get; set; }
        public string Subject { get; set; }
        public int AverageConsumtion { get; set; }
        public int DefaultStartValue { get; set; }
        public string Addressee { get; set; }
    
        public virtual ICollection<Notifications> Notifications { get; set; }
    }
}
