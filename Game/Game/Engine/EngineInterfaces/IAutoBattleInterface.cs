using System.Threading.Tasks;

namespace Game.Engine.EngineInterfaces
{
    /// <summary>
    /// Holds data structures for Auto Battle Engine
    /// </summary>
    public interface IAutoBattleInterface
    {
        IBattleEngineInterface Battle { get;}

        Task<bool> RunAutoBattle();
        bool DetectInfinateLoop();
        bool CreateCharacterParty();
    }
}