using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace clocking.Backend
{
    class Project
    {
        private static List<Project> projects;
        public static List<Project> Projects
        {
            get
            {
                return projects;
            }
            set
            {
                projects = value;
            }
        }


        private string name;
        private int id;
        private webservisy.project projectSLS;

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
        public void getMy
        {
            new 
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
        public webservisy.project ProjectSLS
        {
            get
            {
                return this.projectSLS;
            }
            set
            {
                this.projectSLS = value;
            }
        }

        public Project(string name, int id)
        {
            this.name = name;
            this.id = id;
        }

        public void AddProject()
        {
            this.projectSLS = new clocking.webservisy.project();
            this.projectSLS.repositoryType = clocking.webservisy.repositoryType.SVN;
            this.projectSLS.repositoryTypeSpecified = true;
            this.projectSLS.name = this.name;

            //this.projectSLS = Program.Client.addProject(this.projectSLS, Program.Domain, Program.Invoker);

            //foreach(User u in User.Users)
            //{
            //    webservisy.projectMember pm = new clocking.webservisy.projectMember();
            //    pm.permissionsSet = Program.Client.getPermissionsSetById(2, Program.Domain, Program.Invoker);
            //    pm.project = this.projectSLS;
            //    pm.user = u.UserSLS;
            //    Program.Client.addProjectMember(pm, Program.Domain, Program.Invoker);
            //}

            Projects.Add(this);
            
        }
    }
}
