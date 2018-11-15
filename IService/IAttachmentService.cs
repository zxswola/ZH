using DTO;

namespace IService
{
    public interface IAttachmentService:IServiceSupport
    {
        //获取所有设施
        AttachmentDTO[] GetAll();
        AttachmentDTO[] GetAttachments(long houseId);
    }
}