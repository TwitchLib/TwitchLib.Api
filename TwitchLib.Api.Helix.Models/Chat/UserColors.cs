using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Chat
{
    public class UserColors
    {
        private UserColors(string value) { Value = value; }

        public string Value { get; private set; }

        public static UserColors Blue { get { return new UserColors("blue"); } }
        public static UserColors Blue_Voilet { get { return new UserColors("blue_violet"); } }
        public static UserColors Cadet_Blue { get { return new UserColors("cadet_blue"); } }
        public static UserColors Chocolate { get { return new UserColors("chocolate"); } }
        public static UserColors Coral { get { return new UserColors("coral"); } }
        public static UserColors Dodger_Blue { get { return new UserColors("dodger_blue"); } }
        public static UserColors Firebrick { get { return new UserColors("firebrick"); } }
        public static UserColors Golden_Rod { get { return new UserColors("golden_rod"); } }
        public static UserColors Hot_Pink { get { return new UserColors("hot_pink"); } }
        public static UserColors Orange_Red { get { return new UserColors("orange_red"); } }
        public static UserColors Red { get { return new UserColors("red"); } }
        public static UserColors Sea_Green { get { return new UserColors("sea_green"); } }
        public static UserColors Spring_Green { get { return new UserColors("spring_green"); } }
        public static UserColors Yellow_Green { get { return new UserColors("yellow_green"); } }
    }
}
