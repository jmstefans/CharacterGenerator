using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CharacterGenerator
{
    public class NpcCharacter
    {
        public int AC { get; set; }
        public string Actions { get; set; }
        public string Algn { get; set; }
        public string Character_Traits { get; set; }
        public string Char_Id { get; set; }
        public string Faction { get; set; }
        public bool Faction_Leader { get; set; }
        public string First_Name { get; set; }
        public string Hp { get; set; }
        public string Img { get; set; }
        public string Last_Name { get; set; }
        public string Location { get; set; }
        public string Race { get; set; }
        public string Appearance { get; set; }
        public Skills Skills { get; set; }
        public string Speed { get; set; }
        public Abilities Stats { get; set; }
        public string Title { get; set; }
        public int Title_Order { get; set; }
        public int Xp_Val { get; set; }
        public string Class { get; set; }
        public string Accolades { get; set; }
        public string Motive { get; set; }
        public string First_Meeting { get; set; }
        public string Racial_Features { get; set; }
        public string Racket { get; set; }
        public string Size { get; set; }
        public string Type { get; set; }
        public Spells Spells { get; set; }
        public string Languages { get; set; }

        // Not supposed to be output.
        [JsonProperty("Chall")]
        public string ChallengeRating { get; set; }

        public byte GetCrProficiencyBonus()
        {
            switch (ChallengeRating)
            {
                case "0":
                case "1/8":
                case "1/4":
                case "1/2":
                case "1":
                case "2":
                case "3":
                case "4":
                    return 2;
                case "5":
                case "6":
                case "7":
                case "8":
                    return 3;
                case "9":
                case "10":
                case "11":
                case "12":
                    return 4;
                case "13":
                    return 5;
            }

            throw new Exception(
                "Couldn't calculate the proficiency bonus based on the current challenge rating.");
        }
    }
}
