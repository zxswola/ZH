using DTO;

namespace IService
{
    public interface ISettingService:IServiceSupport
    {
        void SetValue(string name, string value);
        //获取配置项name的值
        string GetValue(string name);
        void SetIntValue(string name, int value);
        int? GetIntValue(string name);
        void SetBoolValue(string name, bool value);
        bool? GetBoolValue(string name);
        SettingDTO[] GetAll();

    }
}