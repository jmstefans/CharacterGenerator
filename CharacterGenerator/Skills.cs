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

        public bool ShouldSerializeAcrobatics()
        {
            return Acrobatics > 0;
        }

        public bool ShouldSerializeAnimalHandling()
        {
            return AnimalHandling > 0;
        }

        public bool ShouldSerializeArcana()
        {
            return Arcana > 0;
        }

        public bool ShouldSerializeAthletics()
        {
            return Athletics > 0;
        }

        public bool ShouldSerializeDeception()
        {
            return Deception > 0;
        }

        public bool ShouldSerializeHistory()
        {
            return History > 0;
        }

        public bool ShouldSerializeInsight()
        {
            return Insight > 0;
        }

        public bool ShouldSerializeIntimidation()
        {
            return Intimidation > 0;
        }

        public bool ShouldSerializeInvestigation()
        {
            return Investigation > 0;
        }

        public bool ShouldSerializeMedicine()
        {
            return Medicine > 0;
        }

        public bool ShouldSerializeNature()
        {
            return Nature > 0;
        }

        public bool ShouldSerializePerception()
        {
            return Perception > 0;
        }

        public bool ShouldSerializePerformance()
        {
            return Performance > 0;
        }

        public bool ShouldSerializePersuasion()
        {
            return Persuasion > 0;
        }

        public bool ShouldSerializeReligion()
        {
            return Religion > 0;
        }

        public bool ShouldSerializeSleightOfHand()
        {
            return SleightOfHand > 0;
        }

        public bool ShouldSerializeStealth()
        {
            return Stealth > 0;
        }

        public bool ShouldSerializeSurvival()
        {
            return Survival > 0;
        }
    }
}
