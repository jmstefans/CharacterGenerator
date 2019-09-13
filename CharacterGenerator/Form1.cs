using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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
            ChallengeRatingComboBox.SelectedIndex = 0;
            RaceComboBox.SelectedIndex = 0;
            ArmorComboBox.SelectedIndex = 0;
            InitializeSpells();
        }

        private void InitializeSpells()
        {
            // Create an array of the 5e spells from the JSON flat file.
            string jsonString;
            using (var r = new StreamReader(Application.StartupPath + @"/spells.json"))
                jsonString = r.ReadToEnd();
            RealSpell[] realSpells = RealSpell.FromJson(jsonString);

            // Populate the spell dropdowns by level.
            foreach (RealSpell spell in realSpells)
            {
                Level level = spell.Level;

                // We need at least one of the properties to not be null.
                if (level.Enum == null && level.Integer == null)
                    continue;

                if (level.Enum == Class.Cantrip)
                    CantripsComboBox.Items.Add(spell);
                else if (level.Enum == Class.Level1 || level.Integer == 1)
                    Level1ComboBox.Items.Add(spell);
                else if (level.Enum == Class.Level2 || level.Integer == 2)
                    Level2ComboBox.Items.Add(spell);
                else if (level.Enum == Class.Level3 || level.Integer == 3)
                    Level3ComboBox.Items.Add(spell);
                else if (level.Enum == Class.Level4 || level.Integer == 4)
                    Level4ComboBox.Items.Add(spell);
                else if (level.Enum == Class.Level5 || level.Integer == 5)
                    Level5ComboBox.Items.Add(spell);
                else if (level.Enum == Class.Level6 || level.Integer == 6)
                    Level6ComboBox.Items.Add(spell);
                else if (level.Enum == Class.Level7 || level.Integer == 7)
                    Level7ComboBox.Items.Add(spell);
                else if (level.Enum == Class.Level8 || level.Integer == 8)
                    Level8ComboBox.Items.Add(spell);
                else if (level.Enum == Class.Level9 || level.Integer == 9)
                    Level9ComboBox.Items.Add(spell);
            }

            // For every combo box and list box on the spells tab set the display member to the
            // name of the spell.
            foreach (Control control in SpellsTab.Controls)
            {
                if (control is ListControl listControl)
                {
                    listControl.DisplayMember = "Name";

                    // Set all of the combo boxes selected index to 0.
                    if (control is ComboBox comboBox)
                        comboBox.SelectedIndex = 0;
                }
            }
        }

        private void GenerateJsonBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var character = new NpcCharacter()
                {
                    AC = (int)ArmorClassNud.Value,
                    Armor = ArmorComboBox.SelectedItem.ToString(),
                    Actions = ActionsTextBox.Text,
                    Algn = AlignmentComboBox.SelectedItem.ToString(),
                    Personality = PersonalityTextBox.Text,
                    Faction = FactionTextBox.Text,
                    Faction_Leader = FactionLeaderCheckBox.Checked,
                    First_Name = FirstNameTextBox.Text,
                    First_Meeting = FirstMeetingTextBox.Text,
                    Racial_Features = RacialFeaturesTextBox.Text,
                    Size = SizeTextBox.Text,
                    Hp = HitPointsTextBox.Text,
                    Img = ImageTextBox.Text,
                    Last_Name = LastNameTextBox.Text,
                    Location = LocationTextBox.Text,
                    Race = RaceComboBox.SelectedItem.ToString(),
                    Appearance = AppearanceTextBox.Text,
                    Speed = SpeedTextBox.Text,
                    History = HistoryTextBox.Text,
                    Stats = new Abilities
                    {
                        Cha = CharismaNud.Value.ToString(CultureInfo.InvariantCulture),
                        Con = ConstitutionNud.Value.ToString(CultureInfo.InvariantCulture),
                        Dex = DexterityNud.Value.ToString(CultureInfo.InvariantCulture),
                        Int = IntellectNud.Value.ToString(CultureInfo.InvariantCulture),
                        Str = StrengthNud.Value.ToString(CultureInfo.InvariantCulture),
                        Wis = WisdomNud.Value.ToString(CultureInfo.InvariantCulture)
                    },
                    Title = TitleTextBox.Text,
                    Xp_Val = (int)XpNud.Value,
                    Class = ClassTextBox.Text,
                    Accolades = AccoladesTextBox.Text,
                    Motive = MotiveTextBox.Text,
                    Type = TypeTextBox.Text,
                    Languages = LanguagesTextBox.Text
                };

                // Add comma-separated spells names.
                SetCharacterSpells(ref character);

                // Add proficient skills.
                character.Skills = new Skills();
                AddSkillsToJson(ref character);
                
                // Serialize the character into JSON.
                JsonTextBox.Text = JsonConvert.SerializeObject(character);

                // Save JSON to clipboard for easy pasting.
                Clipboard.SetText(JsonTextBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        ///     Sets the character's spells by aggregating the information from the UI.
        /// </summary>
        /// <param name="character">A reference to the character whose spells are being set.</param>
        private void SetCharacterSpells(ref NpcCharacter character)
        {
            string cantrips = string.Empty;
            foreach (RealSpell spell in CantripsListBox.Items)
                cantrips += spell.Name + ",";
            cantrips = cantrips.TrimEnd(',');

            string level1 = string.Empty;
            foreach (RealSpell spell in Level1ListBox.Items)
                level1 += spell.Name + ",";
            level1 = level1.TrimEnd(',');

            string level2 = string.Empty;
            foreach (RealSpell spell in Level2ListBox.Items)
                level2 += spell.Name + ",";
            level2 = level2.TrimEnd(',');

            string level3 = string.Empty;
            foreach (RealSpell spell in Level3ListBox.Items)
                level3 += spell.Name + ",";
            level3 = level3.TrimEnd(',');

            string level4 = string.Empty;
            foreach (RealSpell spell in Level4ListBox.Items)
                level4 += spell.Name + ",";
            level4 = level4.TrimEnd(',');

            string level5 = string.Empty;
            foreach (RealSpell spell in Level5ListBox.Items)
                level5 += spell.Name + ",";
            level5 = level5.TrimEnd(',');

            string level6 = string.Empty;
            foreach (RealSpell spell in Level6ListBox.Items)
                level6 += spell.Name + ",";
            level6 = level6.TrimEnd(',');

            string level7 = string.Empty;
            foreach (RealSpell spell in Level7ListBox.Items)
                level7 += spell.Name + ",";
            level7 = level7.TrimEnd(',');

            string level8 = string.Empty;
            foreach (RealSpell spell in Level8ListBox.Items)
                level8 += spell.Name + ",";
            level8 = level8.TrimEnd(',');

            string level9 = string.Empty;
            foreach (RealSpell spell in Level9ListBox.Items)
                level9 += spell.Name + ",";
            level9 = level9.TrimEnd(',');
            
            character.Spells = new Spells
            {
                Cantrips = cantrips,
                Level1 = level1,
                Level2 = level2,
                Level3 = level3,
                Level4 = level4,
                Level5 = level5,
                Level6 = level6,
                Level7 = level7,
                Level8 = level8,
                Level9 = level9,
                Caster = CasterTextBox.Text
            };
        }

        private void AddSkillsToJson(ref NpcCharacter character)
        {
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
                        }
                    }
                }
            }
        }

        #region Add and Remove Spell Button Handlers

        private void AddCantripBtn_Click(object sender, EventArgs e)
        {
            if (CantripsComboBox.SelectedItem == null)
                return;

            var spell = (RealSpell)CantripsComboBox.SelectedItem;
            CantripsListBox.Items.Add(spell);
        }

        private void RemoveCantripBtn_Click(object sender, EventArgs e)
        {
            CantripsListBox.Items.Remove(CantripsListBox.SelectedItem);
        }

        private void AddLevel1Btn_Click(object sender, EventArgs e)
        {
            if (Level1ComboBox.SelectedItem == null)
                return;

            var spell = (RealSpell)Level1ComboBox.SelectedItem;
            Level1ListBox.Items.Add(spell);
        }

        private void RemoveLevel1Btn_Click(object sender, EventArgs e)
        {
            Level1ListBox.Items.Remove(Level1ListBox.SelectedItem);
        }

        private void AddLevel2Btn_Click(object sender, EventArgs e)
        {
            if (Level2ComboBox.SelectedItem == null)
                return;

            var spell = (RealSpell)Level2ComboBox.SelectedItem;
            Level2ListBox.Items.Add(spell);
        }

        private void RemoveLevel2Btn_Click(object sender, EventArgs e)
        {
            Level2ListBox.Items.Remove(Level2ListBox.SelectedItem);
        }

        private void AddLevel3Btn_Click(object sender, EventArgs e)
        {
            if (Level3ComboBox.SelectedItem == null)
                return;

            var spell = (RealSpell)Level3ComboBox.SelectedItem;
            Level3ListBox.Items.Add(spell);
        }

        private void RemoveLevel3Btn_Click(object sender, EventArgs e)
        {
            Level3ListBox.Items.Remove(Level3ListBox.SelectedItem);
        }

        private void AddLevel4Btn_Click(object sender, EventArgs e)
        {
            if (Level4ComboBox.SelectedItem == null)
                return;

            var spell = (RealSpell)Level4ComboBox.SelectedItem;
            Level4ListBox.Items.Add(spell);
        }

        private void RemoveLevel4Btn_Click(object sender, EventArgs e)
        {
            Level4ListBox.Items.Remove(Level4ListBox.SelectedItem);
        }

        private void AddLevel5Btn_Click(object sender, EventArgs e)
        {
            if (Level5ComboBox.SelectedItem == null)
                return;

            var spell = (RealSpell)Level5ComboBox.SelectedItem;
            Level5ListBox.Items.Add(spell);
        }

        private void RemoveLevel5Btn_Click(object sender, EventArgs e)
        {
            Level5ListBox.Items.Remove(Level5ListBox.SelectedItem);
        }

        private void AddLevel6Btn_Click(object sender, EventArgs e)
        {
            if (Level6ComboBox.SelectedItem == null)
                return;

            var spell = (RealSpell)Level6ComboBox.SelectedItem;
            Level6ListBox.Items.Add(spell);
        }

        private void RemoveLevel6Btn_Click(object sender, EventArgs e)
        {
            Level6ListBox.Items.Remove(Level6ListBox.SelectedItem);
        }

        private void AddLevel7Btn_Click(object sender, EventArgs e)
        {
            if (Level7ComboBox.SelectedItem == null)
                return;

            var spell = (RealSpell)Level7ComboBox.SelectedItem;
            Level7ListBox.Items.Add(spell);
        }

        private void RemoveLevel7Btn_Click(object sender, EventArgs e)
        {
            Level7ListBox.Items.Remove(Level7ListBox.SelectedItem);
        }

        private void AddLevel8Btn_Click(object sender, EventArgs e)
        {
            if (Level8ComboBox.SelectedItem == null)
                return;

            var spell = (RealSpell)Level8ComboBox.SelectedItem;
            Level8ListBox.Items.Add(spell);
        }

        private void RemoveLevel8Btn_Click(object sender, EventArgs e)
        {
            Level8ListBox.Items.Remove(Level8ListBox.SelectedItem);
        }

        private void AddLevel9Btn_Click(object sender, EventArgs e)
        {
            if (Level9ComboBox.SelectedItem == null)
                return;

            var spell = (RealSpell)Level9ComboBox.SelectedItem;
            Level9ListBox.Items.Add(spell);
        }

        private void RemoveLevel9Btn_Click(object sender, EventArgs e)
        {
            Level9ListBox.Items.Remove(Level9ListBox.SelectedItem);
        }

        #endregion Add and Remove Spell Button Handlers
    }
}
