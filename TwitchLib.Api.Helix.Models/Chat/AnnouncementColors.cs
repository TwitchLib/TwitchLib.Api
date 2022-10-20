namespace TwitchLib.Api.Helix.Models.Chat
{
    public class AnnouncementColors
    {
        private AnnouncementColors(string value) { Value = value; }

        public string Value { get; private set; }

        public static AnnouncementColors Blue { get { return new AnnouncementColors("blue"); } }
        public static AnnouncementColors Green { get { return new AnnouncementColors("green"); } }
        public static AnnouncementColors Orange { get { return new AnnouncementColors("orange"); } }
        public static AnnouncementColors Purple { get { return new AnnouncementColors("purple"); } }
        public static AnnouncementColors Primary { get { return new AnnouncementColors("primary"); } }
    }
}
