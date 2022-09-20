using CodeBase.Data;

namespace CodeBase.Services.PersistentProgress
{
    public sealed class PersistentProgressService : IPersistentProgressService
    {
        public PlayerProgress Progress { get; set; }
    }
}