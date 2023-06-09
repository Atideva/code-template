namespace Gameplay.Interface
{
    public interface IHasTeam
    {
        string Team { get; }
        void SetTeam(string newTeamTag);
    }
}