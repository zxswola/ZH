using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using IService;
using Service.Entities;

namespace Service
{
   public class AttachmentService : IAttachmentService
    {
        public AttachmentDTO[] GetAll()
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<AttachmentEntity> bs = new BaseService<AttachmentEntity>(ctx);
               return bs.GetAll().AsNoTracking().ToList().Select(e => ToDTO(e)).ToArray();
            }
            //throw new NotImplementedException();
        }

        private AttachmentDTO ToDTO(AttachmentEntity entity)
        {
            AttachmentDTO dto = new AttachmentDTO();
            dto.Id = entity.Id;
            dto.Name = entity.Name;
            dto.IconName = entity.IconName;
            dto.CreateDateTime = entity.CreateDateTIme;
            return dto;
        }

        public AttachmentDTO[] GetAttachments(long houseId)
        {
            throw new NotImplementedException();
        }
    }
}
