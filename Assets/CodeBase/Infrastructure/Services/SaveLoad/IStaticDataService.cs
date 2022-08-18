using CodeBase.StaticData;

namespace CodeBase.Infrastructure.Services.SaveLoad
{
    public interface IStaticDataService : IService
    {
        void LoadMonsters();
        MonsterStaticData ForMonster(MonsterTypeId typeId);
    }
}