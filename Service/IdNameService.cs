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
    class IdNameService : IIdNameService
    {
        public IdNameDTO[] GetAll(string name)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                var idNames= ctx.Set<IdNameEntity>().Where(e => e.IsDeleted == false&&e.TypeName==name).AsNoTracking().ToList().Select(c => ToDTO(c)).ToArray();
                return idNames;
            }
        }

        private IdNameDTO ToDTO(IdNameEntity entity)
        {
            IdNameDTO dto = new IdNameDTO();
            dto.Id = entity.Id;
            dto.Name = entity.Name;
            dto.TypeName = entity.TypeName;
            dto.CreateDateTime = entity.CreateDateTIme;
            return dto;
        }
    }
}
