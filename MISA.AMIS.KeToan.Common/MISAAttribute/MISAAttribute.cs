﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.Common.MISAAttribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class NotEmpty:Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyName : Attribute
    {
        public string? Name = string.Empty;

        public PropertyName(string name)
        {
            this.Name = name;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class ValidateDateTime : Attribute
    {

    }
}
