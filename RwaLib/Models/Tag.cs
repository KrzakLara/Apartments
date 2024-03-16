using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RwaLib.Models
{
    [Serializable]
    public class Tag
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public int TypeId { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }
        public string TagTypeNameEng { get; set; }
        public string Type { get; set; }
        public int TaggedApartments { get; set; }
        public bool IsUsed(object o)
        {
            if (this.TaggedApartments == 0) return false;
            return true;
        }
        public int Used { get; set; }
    }
}
