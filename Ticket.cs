using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace clocking.Backend
{
    class Ticket
    {
        private string name;
        private string content;
        private Project projectIn;
        private User createdBy;
        private List<User> assigned;
        private List<Label> labels;
        private webservisy.TicketStatus status;
        private webservisy.TicketPriority priority;
        private int id;
        private List<TicketHistory> history;
        private webservisy.ticket ticketSLS;

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
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
        public Project ProjectIn
        {
            get
            {
                return this.projectIn;
            }
            set
            {
                this.projectIn = value;
            }
        }
        public List<User> Assigned
        {
            get
            {
                return this.assigned;
            }
            set
            {
                this.assigned = value;
            }
        }
        public User CreatedBy
        {
            get
            {
                return this.createdBy;
            }
            set
            {
                this.createdBy = value;
            }
        }
        public List<Label> Labels
        {
            get
            {
                return this.labels;
            }
            set
            {
                this.labels = value;
            }
        }
        public webservisy.TicketStatus Status
        {
            get
            {
                return this.status;
            }
            set
            {
                this.status = value;
            }
        }
        public webservisy.TicketPriority Priority
        {
            get
            {
                return this.priority;
            }
            set
            {
                this.priority = value;
            }
        }
        public int Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }
        public List<TicketHistory> History
        {
            get
            {
                return this.history;
            }
            set
            {
                this.history = value;
            }
        }
        public webservisy.ticket TicketSLS
        {
            get
            {
                return this.ticketSLS;
            }
            set
            {
                this.ticketSLS = value;
            }
        }

        public Ticket(string name, string content, User createdBy, Project projectIn, webservisy.TicketStatus status, webservisy.TicketPriority priority, int id)
        {
            this.name = name;
            this.content = content;
            this.projectIn = projectIn;
            this.createdBy = createdBy;

            this.status = status;
            this.priority = priority;
            this.id = id;
        }

        public void AddTicketWithHistory()
        {
            
            this.ticketSLS = new clocking.webservisy.ticket();
            //replace przecinaka tez wyjebac
            this.ticketSLS.title = this.name.Replace(",", "").Replace("<","").Replace(">","").Replace("&","");
            //obcinanie nazwy do wyjebania
            if(this.ticketSLS.title.Length > 50)
                this.ticketSLS.title = this.name.Replace(",", "").Replace("<", "").Replace(">", "").Replace("&", "").Substring(0, 50);

            this.ticketSLS.content = this.content.Replace("\r\n", "<br/>").Replace("\n", "<br/>"); ;
            //obcinanie contentu do wyjebania
            if(this.ticketSLS.content.Length > 1000)
                this.ticketSLS.content = this.content.Substring(0, 1000);
            this.ticketSLS.project = this.projectIn.ProjectSLS;

            this.ticketSLS.assignment = new clocking.webservisy.user[this.assigned.Count];
            for(int i = 0; i < this.assigned.Count; i++)
            {
                this.ticketSLS.assignment[i] = this.assigned[i].UserSLS;
            }

            this.ticketSLS.labels = new clocking.webservisy.label[this.labels.Count];
            for (int i = 0; i < this.labels.Count; i++)
            {
                this.ticketSLS.labels[i] = new clocking.webservisy.label();
                this.ticketSLS.labels[i].name = this.labels.ElementAt(i).Name;
            }

            this.ticketSLS.statusSpecified = true;
            this.ticketSLS.status = this.status;
            this.ticketSLS.prioritySpecified = true;
            this.ticketSLS.priority = this.priority;
            this.ticketSLS.updateComment = "";



            this.ticketSLS = Program.Client.addTicket(this.ticketSLS, Program.Domain, Program.Invoker);
            this.ticketSLS = Program.Client.getTicketById(this.ticketSLS.id, Program.Domain, Program.Invoker);


            for(int h = this.History.Count - 1; h >= 0; h--)
            {
                webservisy.ticket updateTicket = new clocking.webservisy.ticket();
                updateTicket.id = ticketSLS.id;
                updateTicket.idSpecified = true;
                updateTicket.priority = ticketSLS.priority;
                updateTicket.prioritySpecified = true;
                updateTicket.status = ticketSLS.status;
                updateTicket.statusSpecified = true;
                updateTicket.project = ticketSLS.project;


                TicketHistory hist = this.History[h];

                //tmp

                switch (hist.UpdateBy.Id)
                {
                    //darek
                    case 95679:
                        hist.UpdateBy.Id = 4;
                        break;
                    //marcin
                    case 61855:
                        hist.UpdateBy.Id = 2;
                        break;
                    //michał
                    case 66387:
                        hist.UpdateBy.Id = 3;
                        break;
                    //george
                    case 71412:
                        hist.UpdateBy.Id = 8;
                        break;
                    //szymon
                    case 61854:
                        hist.UpdateBy.Id = 1;
                        break;
                    //ja
                    case 64539:
                        hist.UpdateBy.Id = 6;
                        break;
                    //tomo
                    case 99232:
                        hist.UpdateBy.Id = 7;
                        break;
                    //s4
                    case 62045:
                        hist.UpdateBy.Id = 5;
                        break;
                }


                updateTicket.updateComment = hist.Content.Replace("\r\n", "<br/>").Replace("\n", "<br/>");
                //obciananie komentarza wyjebac jak naprawia
                if(updateTicket.updateComment.Length > 1000)
                    updateTicket.updateComment = hist.Content.Substring(0, 1000);

                //wyjebac jak poprawi backend. maja to spierdolone i po dodaniu nie zwraca labelek i assignow
                updateTicket.assignment = new clocking.webservisy.user[this.assigned.Count];
                for (int i = 0; i < this.assigned.Count; i++)
                {
                    updateTicket.assignment[i] = this.assigned[i].UserSLS;
                }

                updateTicket.labels = new clocking.webservisy.label[this.labels.Count];
                for (int i = 0; i < this.labels.Count; i++)
                {
                    updateTicket.labels[i] = new clocking.webservisy.label();
                    updateTicket.labels[i].name = this.labels.ElementAt(i).Name;
                }
                //az do tad
                //---------------------------------------------------

                try
                {
                    Program.Client.updateTicket(updateTicket, Program.Domain, hist.UpdateBy.UserSLS.id);
                }
                catch
                {
                    updateTicket.content = "updated by:  hist.UpdateBy.Email \n" + updateTicket.content;
                    Program.Client.updateTicket(updateTicket, Program.Domain, Program.Invoker);
                }
            }
        }

    }
}
