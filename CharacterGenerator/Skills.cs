using System.ComponentModel;

namespace CharacterGenerator
{
    public class Skills
    {
        [Description("Acrobatics")]
        public byte Acrobatics { get; set; }
        [Description("Animal Handling")]
        public byte AnimalHandling { get; set; }
        [Description("Arcana")]
        public byte Arcana { get; set; }
        [Description("Athletics")]
        public byte Athletics { get; set; }
        [Description("Deception")]
        public byte Deception { get; set; }
        [Description("History")]
        public byte History { get; set; }
        [Description("Insight")]
        public byte Insight { get; set; }
        [Description("Intimidation")]
        public byte Intimidation { get; set; }
        [Description("Investigation")]
        public byte Investigation { get; set; }
        [Description("Medicine")]
        public byte Medicine { get; set; }
        [Description("Nature")]
        public byte Nature { get; set; }
        [Description("Perception")]
        public byte Perception { get; set; }
        [Description("Performance")]
        public byte Performance { get; set; }
        [Description("Persuasion")]
        public byte Persuasion { get; set; }
        [Description("Religion")]
        public byte Religion { get; set; }
        [Description("Sleight of Hand")]
        public byte SleightOfHand { get; set; }
        [Description("Stealth")]
        public byte Stealth { get; set; }
        [Description("Survival")]
        public byte Survival { get; set; }
    }
}
