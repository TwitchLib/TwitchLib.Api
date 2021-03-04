namespace TwitchLib.Api.Helix.Models.Teams
{
    public class Team : TeamBase
    {
        public TeamMember[] Users { get; protected set; }
    }
}