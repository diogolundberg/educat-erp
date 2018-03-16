using System;

namespace SSO.ViewModels
{
    public class ResetToken 
    {
        public DateTimeOffset Expirated { get; set; }
        public Guid UserId { get; set; }

        public bool IsValid ()
        {
            return DateTimeOffset.Now <= Expirated;
        }
    }
}