using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQIE.OnlineVote.Models
{
    public class SystemMenu
    {
        public int Id { get; set; }
        public int? ParentID { get; set; }
        public string? MenuName {  get; set; }
        public string? Url { get; set; }
        public ICollection<MenuRole>? MenuRoles { get; set; }
        public virtual SystemMenu? Parent { get; set; }

        /// <summary>
        /// 子功能项
        /// </summary>
        public virtual ICollection<SystemMenu> SubMenus { get; set; } = new HashSet<SystemMenu>();

    }
}
