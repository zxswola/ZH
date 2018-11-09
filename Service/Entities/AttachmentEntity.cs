using System.Collections.Generic;

namespace Service.Entities
{
    /// <summary>
    /// 房屋配置表 床 冰箱 空调等
    /// </summary>
    public class AttachmentEntity:BaseEntity
    {
        public string Name { get; set; }
        public string IconName { get; set; }

        public virtual ICollection<HouseEntity> Housers { get; set; } = new List<HouseEntity>();
    }
}