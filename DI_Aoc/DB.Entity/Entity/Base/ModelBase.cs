using System;
using System.Collections.Generic;
using System.Text;

namespace DB.Entity.Model.Base
{
    public interface IModel<TPrimaryKey>//约束
    {
        TPrimaryKey ID { get; set; }
    }     
  

  
    public class ModelBase : IModel<int>
    {
        public int ID { get; set; }
    }
    
}
