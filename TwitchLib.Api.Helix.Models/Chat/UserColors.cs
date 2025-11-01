namespace TwitchLib.Api.Helix.Models.Chat;

/// <summary>
/// The list of color codes a user can use for their name.
/// </summary>
public class UserColors
{
    private UserColors(string value) { Value = value; }

    /// <summary>
    /// Value
    /// </summary>
    public string Value { get; private set; }

    /// <summary>
    /// Blue
    /// </summary>
    public static UserColors Blue { get { return new UserColors("blue"); } }

    /// <summary>
    /// Blue-Voilet
    /// </summary>
    public static UserColors BlueVoilet { get { return new UserColors("blue_violet"); } }

    /// <summary>
    /// Cadet Blue
    /// </summary>
    public static UserColors CadetBlue { get { return new UserColors("cadet_blue"); } }

    /// <summary>
    /// Chocolate
    /// </summary>
    public static UserColors Chocolate { get { return new UserColors("chocolate"); } }

    /// <summary>
    /// Coral
    /// </summary>
    public static UserColors Coral { get { return new UserColors("coral"); } }

    /// <summary>
    /// Dodger Blue
    /// </summary>
    public static UserColors DodgerBlue { get { return new UserColors("dodger_blue"); } }

    /// <summary>
    /// Firebrick
    /// </summary>
    public static UserColors Firebrick { get { return new UserColors("firebrick"); } }

    /// <summary>
    /// Golden Rod
    /// </summary>
    public static UserColors GoldenRod { get { return new UserColors("golden_rod"); } }

    /// <summary>
    /// Hot Pink
    /// </summary>
    public static UserColors HotPink { get { return new UserColors("hot_pink"); } }

    /// <summary>
    /// Orange-Red
    /// </summary>
    public static UserColors OrangeRed { get { return new UserColors("orange_red"); } }

    /// <summary>
    /// Red
    /// </summary>
    public static UserColors Red { get { return new UserColors("red"); } }

    /// <summary>
    /// Sea Green
    /// </summary>
    public static UserColors SeaGreen { get { return new UserColors("sea_green"); } }

    /// <summary>
    /// Spring Green
    /// </summary>
    public static UserColors SpringGreen { get { return new UserColors("spring_green"); } }

    /// <summary>
    /// Yellow-Green
    /// </summary>
    public static UserColors YellowGreen { get { return new UserColors("yellow_green"); } }
}
