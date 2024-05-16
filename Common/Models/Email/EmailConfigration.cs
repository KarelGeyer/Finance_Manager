using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.Email
{
    public class EmailConfigration
    {
        /// <summary>
        /// Email address of the sender
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// SMTP server address
        /// </summary>
        public string Smtp { get; set; }

        /// <summary>
        /// Port of the SMTP server
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Should use SSL or not
        /// </summary>
        public bool ShouldUseSSL { get; set; }

        /// <summary>
        /// SSL
        /// </summary>
        public string SSL { get; set; }

        /// <summary>
        /// Username of the sender
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Password of the sender
        /// </summary>
        public string Password { get; set; }
    }
}
