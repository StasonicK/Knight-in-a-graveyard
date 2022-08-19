using CodeBase.StaticData;

namespace CodeBase.Services.SaveLoad
{
    public interface IStaticDataService : IService
    {
        void LoadMonsters();
        MonsterStaticData ForMonster(MonsterTypeId typeId);
    }
}