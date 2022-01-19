using Microsoft.EntityFrameworkCore;
using VDT.TransactionDaily.API.BL.Interfaces;
using VDT.TransactionDaily.API.BLCore.Interfaces;
using VDT.TransactionDaily.API.Models;
using VDT.TransactionDaily.API.Models.Others;
using static VDT.TransactionDaily.API.Models.Enums.Enumarations;

namespace VDT.TransactionDaily.API.BL.Implements
{
    public class BaseBL<T> : IBaseBL<T> where T : BaseModel
    {
        protected string SubsystemCode { get; set; }
        protected uint UserID { get; set; }
        protected string UserName { get; set; }
        protected string Email { get; set; }

        protected readonly VdtContext _dbContext;

        protected DbSet<T> _entity;

        protected readonly IConfigService _configService;
        protected readonly IHttpContextService _httpContextService;
        protected readonly IAuthService _authService;

        public BaseBL(CoreService coreService)
        {
            SubsystemCode = string.Empty;
            _dbContext = coreService.VdtContext;
            _entity = _dbContext.Set<T>();
            _configService = coreService.ConfigService;
            _httpContextService = coreService.HttpContextService;
            _authService = coreService.AuthService;
        }

        public IEnumerable<T> GetAll()
        {
            return _entity.AsEnumerable();
        }

        public T Get(int id)
        {
            return _entity.FirstOrDefault(p => p.Id == id);
        }

        public bool Insert(T entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(T entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(List<int> id)
        {
            throw new NotImplementedException();
        }

        protected void BeforeSave(BaseModel baseModel)
        {
            if (baseModel.ModelState == ModelState.Insert)
            {
                baseModel.CreatedDate = DateTime.UtcNow;
                baseModel.CreatedBy = UserName;
            }

            if (baseModel.ModelState == ModelState.Update)
            {
                baseModel.ModifiedDate = DateTime.UtcNow;
                baseModel.ModifiedBy = UserName;
            }

            baseModel.UserId = UserID;
        }
    }
}
