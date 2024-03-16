﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RwaLib.Models
{
    [Serializable]
    public class TagType
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }
    }
}