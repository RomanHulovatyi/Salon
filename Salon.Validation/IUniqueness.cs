﻿using System.Collections.Generic;

namespace Salon.Validation
{
    public interface IUniqueness
    {
        public List<string> ColumnName { get; set; }

        public bool IsUnique(object o);
    }
}
