﻿using System;
using System.Collections.Generic;

namespace Entities
{
    public partial class Grade
    {
        public Grade()
        {
            this.Class = new HashSet<Class>();
        }

        /// <summary>
        /// 主键.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 名称.
        /// </summary>
        public string Name { get; set; }

        public virtual ICollection<Class> Class { get; set; }
    }
}
