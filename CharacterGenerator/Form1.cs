﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace CharacterGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            AlignmentComboBox.SelectedIndex = 0;
            InitializeSpells();
        }

        private void InitializeSpells()
        {
            string jsonString;
            using (var r = new StreamReader(Application.StartupPath + @"/spells.json"))
                jsonString = r.ReadToEnd();
            Spell[] spells = Spell.FromJson(jsonString);
            foreach (Spell spell in spells)
                SpellsComboBox.Items.Add(spell);
            SpellsComboBox.DisplayMember = "Name";
            SpellsListBox.DisplayMember = "Name";
            SpellsComboBox.SelectedIndex = 0;
            ChallengeRatingComboBox.SelectedIndex = 0;
        }

        private void GenerateJsonBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var character = new NpcCharacter()
                {
                    ArmorClass = (int)ArmorClassNud.Value,
                    Actions = ActionsTextBox.Text,
                    Algn = AlignmentComboBox.SelectedItem.ToString(),
                    Character_Traits = TraitsTextBox.Text,
                    Chall = (int)ChallNud.Value,
                    Char_Id = (int)ChallNud.Value,
                    Faction = FactionTextBox.Text,
                    Faction_Leader = FactionLeaderTextBox.Text,
                    First_Name = FirstNameTextBox.Text,
                    First_Meeting = FirstMeetingTextBox.Text,
                    Racial_Features = RacialFeaturesTextBox.Text,
                    Racket = RacketTextBox.Text,
                    Size = SizeTextBox.Text,
                    Hp = (int)HitPointsNud.Value,
                    Img = ImageTextBox.Text,
                    Last_Name = LastNameTextBox.Text,
                    Location = LocationTextBox.Text,
                    Race = RaceTextBox.Text,
                    Appearance = AppearanceTextBox.Text,
                    Speed = (int)SpeedNud.Value,
                    Stats = new Abilities
                    {
                        Cha = (int)CharismaNud.Value,
                        Con = (int)ConstitutionNud.Value,
                        Dex = (int)DexterityNud.Value,
                        Int = (int)IntellectNud.Value,
                        Str = (int)StrengthNud.Value,
                        Wis = (int)WisdomNud.Value
                    },
                    Title = TitleTextBox.Text,
                    Title_Order = (int)TitleOrderNud.Value,
                    Xp_Val = (int)XpNud.Value,
                    Class = ClassTextBox.Text,
                    Accolades = AccoladesTextBox.Text,
                    Motive = MotiveTextBox.Text,
                    Type = TypeTextBox.Text,
                };

                // Add comma-separated spells names.
                foreach (Spell spell in SpellsListBox.Items)
                    character.Spells += spell.Name + ",";
                character.Spells = character.Spells?.TrimEnd(',');

                // Add proficient skills.
                character.Skills = new Skills();
                List<string> proficientSkills = AddSkillsToJson(ref character);

                // Add all of the rest of the character properties to the allowed property list for
                //the contract resolver (excludes the one property of NpcCharacter that should not be output).
                List<string> charProps = character.GetType().GetProperties().Select(prop => prop.Name)
                    .Where(prop => !prop.Equals("ChallengeRating")).ToList();

                // Create final list of allowed properties to be output.
                proficientSkills.AddRange(charProps);
                List<string> allOutputPropertyNames = proficientSkills;

                // Create the JSON from the object and copy it to the clipboard for easy pasting.
                JsonTextBox.Text = JsonConvert.SerializeObject(character,
                    new JsonSerializerSettings
                    {
                        ContractResolver = new CustomContractResolver(allOutputPropertyNames)
                    });
                
                // Save JSON to clipboard for easy pasting.
                Clipboard.SetText(JsonTextBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void AddSpellBtn_Click(object sender, EventArgs e)
        {
            if (SpellsComboBox.SelectedItem == null)
                return;

            var spell = (Spell)SpellsComboBox.SelectedItem;
            SpellsListBox.Items.Add(spell);
        }

        private void RemoveSpellBtn_Click(object sender, EventArgs e)
        {
            SpellsListBox.Items.Remove(SpellsListBox.SelectedItem);
        }

        private void SpellsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var spell = (Spell) SpellsComboBox.SelectedItem;
            SpellDescriptionTextBox.Text = spell.ToString();
        }

        private List<string> AddSkillsToJson(ref NpcCharacter character)
        {
            var proficientSkills = new List<string>();

            // Set the character's CR with the value from the UI, since we'll need that later to calculate a proficiency bonus.
            character.ChallengeRating = ChallengeRatingComboBox.SelectedItem.ToString();

            // For all of the possible skills...
            foreach (object skillCb in SkillsGroupBox.Controls)
            {
                // If the skill is proficient...
                if (skillCb is CheckBox box && box.Checked)
                {
                    // For all of the skill properties of the character object...
                    foreach (PropertyInfo propertyInfo in character.Skills.GetType().GetProperties())
                    {
                        // If we found the appropriate character object's skill property...
                        IEnumerable<Attribute> attributes = propertyInfo.GetCustomAttributes();
                        if (box.Text.Equals(((DescriptionAttribute) attributes.ElementAt(0)).Description))
                        {
                            // Set the character object's skill to the appropriate bonus.
                            propertyInfo.SetValue(character.Skills, character.GetCrProficiencyBonus());

                            // Save the property name for the JSON contract resolver.
                            proficientSkills.Add(propertyInfo.Name);
                        }
                    }
                }
            }

            return proficientSkills;
        }
    }
}
