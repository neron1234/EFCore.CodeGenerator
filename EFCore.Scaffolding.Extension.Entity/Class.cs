﻿using System;
using System.Collections.Generic;

namespace Entities
{
    /// <summary>
    /// 班级.
    /// </summary>
    public partial class Class
    {
        public Class()
        {
            this.Student = new HashSet<Student>();
        }

        /// <summary>
        /// 主键.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 名称.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 班主任ID.
        /// </summary>
        public int? HeadTeacherId { get; set; }

        /// <summary>
        /// 创建时间.
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 更新时间.
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 年级.
        /// </summary>
        public int? GradeId { get; set; }

        /// <summary>
        /// 地址.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// 是否已删除.
        /// </summary>
        public bool IsDeleted { get; set; }

        public virtual Grade Grade { get; set; }

        public virtual Teacher HeadTeacher { get; set; }

        public virtual ICollection<Student> Student { get; set; }
    }
}
