using Game.Engine.EngineModels;
using Game.Models;

namespace Game.Engine.EngineInterfaces
{
    /// <summary>
    /// Interface for Battle Engine 
    /// </summary>
   public interface IBattleEngineInterface
    {
        IRoundEngineInterface Round { get; set; }

        EngineSettingsModel EngineSettings { get;} 

        bool PopulateCharacterList(CharacterModel data);

        bool StartBattle(bool isAutoBattle);

        bool EndBattle();
    }
}
