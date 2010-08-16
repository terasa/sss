using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace clocking.Backend
{
    class TicketHistory
    {
        private User updatedBy;
        private string content;

        public User UpdateBy
        {
            get
            {
                return this.updatedBy;
            }
            set
            {
                this.updatedBy = value;
            }
        }
        public string Content
        {
            get
            {
                return this.content;
            }
            set
            {
                this.content = value;
            }
        }

        public TicketHistory(User updatedBy, string content)
        {
            this.updatedBy = updatedBy;
            this.content = content;
        }
    }
}
