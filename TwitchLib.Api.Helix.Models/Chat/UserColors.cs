namespace TwitchLib.Api.Helix.Models.Chat
{
    public class UserColors
    {
        private UserColors(string value) { Value = value; }

        public string Value { get; private set; }

        public static UserColors Blue { get { return new UserColors("blue"); } }
        public static UserColors BlueVoilet { get { return new UserColors("blue_violet"); } }
        public static UserColors CadetBlue { get { return new UserColors("cadet_blue"); } }
        public static UserColors Chocolate { get { return new UserColors("chocolate"); } }
        public static UserColors Coral { get { return new UserColors("coral"); } }
        public static UserColors DodgerBlue { get { return new UserColors("dodger_blue"); } }
        public static UserColors Firebrick { get { return new UserColors("firebrick"); } }
        public static UserColors GoldenRod { get { return new UserColors("golden_rod"); } }
        public static UserColors HotPink { get { return new UserColors("hot_pink"); } }
        public static UserColors OrangeRed { get { return new UserColors("orange_red"); } }
        public static UserColors Red { get { return new UserColors("red"); } }
        public static UserColors SeaGreen { get { return new UserColors("sea_green"); } }
        public static UserColors SpringGreen { get { return new UserColors("spring_green"); } }
        public static UserColors YellowGreen { get { return new UserColors("yellow_green"); } }
    }
}
