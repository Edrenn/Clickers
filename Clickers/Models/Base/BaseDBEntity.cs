using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clickers.Models.Base
{
    public class BaseDBEntity : BaseEntity
    {
        private Int32 id;

        public Int32 Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }
    }
}
