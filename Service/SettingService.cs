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
    public class SettingService : ISettingService
    {
        private SettingDTO ToDTO(SettingEntity setting)
        {
            SettingDTO dto = new SettingDTO();
            dto.Id = setting.Id;
            dto.Name = setting.Name;
            dto.Value = setting.Value;
            dto.CreateDateTime = setting.CreateDateTIme;
            return dto;
        }

        public SettingDTO[] GetAll()
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<SettingEntity> bs = new BaseService<SettingEntity>(ctx);
               return  bs.GetAll().ToList().Select(s => ToDTO(s)).ToArray();
            }
        }

        public bool? GetBoolValue(string name)
        {
            var value = GetValue(name);
            if (value == null)
            {
                return null;
            }
            else
            {
                return Convert.ToBoolean(value);
            }
        }



        public int? GetIntValue(string name)
        {
            var  value =GetValue(name);
            if (value == null)
            {
                return null;
            }
            else
            {
                return Convert.ToInt32(value);
            }
        }

        public string GetValue(string name)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<SettingEntity> bs = new BaseService<SettingEntity>(ctx);
                var setting = bs.GetAll().Where(s => s.Name == name).SingleOrDefault();
                return setting == null ? null : setting.Value;
            }
        }

        public void SetBoolValue(string name, bool value)
        {
            SetValue(name, value.ToString());
        }

        public void SetIntValue(string name, int value)
        {
           SetValue(name,value.ToString());
        }

        public void SetValue(string name, string value)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<SettingEntity> bs = new BaseService<SettingEntity>(ctx);
                var setting =bs.GetAll().SingleOrDefault(s => s.Name == name);
                if (setting == null)
                {
                    ctx.Settings.Add(new SettingEntity {Name = name, Value = value});
                }
                else
                {
                    setting.Value = value;
                }

                ctx.SaveChanges();
            }

            
        }
    }
}
