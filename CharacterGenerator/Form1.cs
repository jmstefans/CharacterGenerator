using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
            string jsonString = File.ReadAllText(
                @"C:\Users\jstefanski\Source\Repos\CharacterGenerator\CharacterGenerator\spells.json");
            Dictionary<string, Spell> spellsDictionary = Spell.FromJson(jsonString);
            foreach (KeyValuePair<string, Spell> spell in spellsDictionary)
                SpellsComboBox.Items.Add(spell);
            SpellsComboBox.DisplayMember = "Key";
            SpellsComboBox.SelectedIndex = 0;
        }

        private void GenerateJsonBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var character = new NpcCharacter()
                {
                    ArmorClass = (int)ArmorClassNud.Value,
                    Actions = ActionsTextBox.Text,
                    Spells = SpellsTextBox.Text,
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
                    Skills = SkillsTextBox.Text,
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
                JsonTextBox.Text = JsonConvert.SerializeObject(character);
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

            var spellComboBoxItem = (KeyValuePair<string, Spell>)SpellsComboBox.SelectedItem;
            SpellsListBox.Items.Add(spellComboBoxItem.Key);
        }

        private void RemoveSpellBtn_Click(object sender, EventArgs e)
        {
            SpellsListBox.Items.Remove(SpellsListBox.SelectedItem);
        }

        private void SpellsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var spellComboBoxItem = (KeyValuePair<string, Spell>) SpellsComboBox.SelectedItem;
            SpellDescriptionTextBox.Text = spellComboBoxItem.Value.ToString();
        }
    }
}
