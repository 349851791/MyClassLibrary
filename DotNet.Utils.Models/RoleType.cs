using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.Utils.Models
{
    public class RoleType
    {
        public RoleType()
        {
            MyGlobe.IsChange.Clear();
        }
        private int id;
        public int Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
                MyGlobe.IsChange.Add("Id", value);
            }
        }

        private string names;
        public string Names
        {
            get
            {
                return this.names;
            }
            set
            {
                this.names = value;
                MyGlobe.IsChange.Add("Names", value);
            }
        }
    }
}
