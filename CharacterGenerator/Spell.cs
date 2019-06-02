using System.Runtime.Serialization;

namespace CharacterGenerator
{
    /// <summary>
    ///     A spell from the 5th edition of Dungeons and Dragons.
    /// </summary>
    [DataContract]
    public class Spell
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the casting time.
        /// </summary>
        /// <value>
        /// The casting time.
        /// </value>
        [DataMember]
        public string CastingTime { get; set; }

        /// <summary>
        /// Gets or sets the components.
        /// </summary>
        /// <value>
        /// The components.
        /// </value>
        [DataMember]
        public string Components { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        /// <value>
        /// The duration.
        /// </value>
        [DataMember]
        public string Duration { get; set; }

        /// <summary>
        /// Gets or sets the level.
        /// </summary>
        /// <value>
        /// The level.
        /// </value>
        [DataMember]
        public byte Level { get; set; }

        /// <summary>
        /// Gets or sets the range.
        /// </summary>
        /// <value>
        /// The range.
        /// </value>
        [DataMember]
        public string Range { get; set; }

        /// <summary>
        /// Gets or sets the school.
        /// </summary>
        /// <value>
        /// The school.
        /// </value>
        [DataMember]
        public string School { get; set; }
    }
}
