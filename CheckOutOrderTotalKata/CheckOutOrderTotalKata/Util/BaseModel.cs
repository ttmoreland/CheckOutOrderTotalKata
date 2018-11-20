using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CheckOutOrderTotalKata.Util
{
    public abstract class BaseModel
    {
        [Required]
        public string Name { get; private set; }

        public BaseModel(string Name)
        {
            this.Name = Name;
        }
    }
}
