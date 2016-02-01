namespace Chef.Profiles
{
    public class ProfileStyle
    {
        public static ProfileStyle GetDefault()
        {
            return new ProfileStyle { 
             // profile box
            Profbox_x = 0,
            Profbox_y = 0,
            Profbox_opacity = 40,
            Profbox_bgcolor = "#000",
            // edit box
            Name_color = "#eee",
            Name_font = "Arial",
            Name_size = 66,
            Headline_color = "#eee",
            Headline_font = "Arial",
            Headline_size = 24,
            Bio_color = "#eee",
            Bio_font = "Arial",
            Bio_size = 18,
            Links_color = "#eee",
            Links_font = "Arial",
            Links_size = 16,
            };
        }

        public int Id { get; set; }

        // -------------------------------------------------------------------- profile box

        /// <summary>
        /// Profile box X coordinate on page, default value: 0.
        /// </summary>
        public int Profbox_x { get; set; }
        /// <summary>
        /// Profile box Y coordinate on page, default value: 0.
        /// </summary>
        public int Profbox_y { get; set; }
        /// <summary>
        /// Profile box opacity, default value: 40.
        /// </summary>
        public int Profbox_opacity { get; set; }
        /// <summary>
        /// Profile box background color, default value: #000
        /// </summary>
        public string Profbox_bgcolor { get; set; }

        // -------------------------------------------------------------------- edit box

        /// <summary>
        /// First name, last name color, default value: #eee
        /// </summary>
        public string Name_color { get; set; }
        /// <summary>
        /// First name, last name font, default value: Arial
        /// </summary>
        public string Name_font { get; set; }
        /// <summary>
        /// First name, last name size, default value: 66
        /// </summary>
        public int Name_size { get; set; }
        /// <summary>
        /// Headline color, default value: #eee
        /// </summary>
        public string Headline_color { get; set; }
        /// <summary>
        /// Headline font, default value: Arial
        /// </summary>
        public string Headline_font { get; set; }
        /// <summary>
        /// Headline size, default value: 24
        /// </summary>
        public int Headline_size { get; set; }
        /// <summary>
        /// Bio color, default value: #eee
        /// </summary>
        public string Bio_color { get; set; }
        /// <summary>
        /// Bio font, default value: Arial
        /// </summary>
        public string Bio_font { get; set; }
        /// <summary>
        /// Bio size, default value: 18
        /// </summary>
        public int Bio_size { get; set; }
        /// <summary>
        /// Links color, default value: #eee
        /// </summary>
        public string Links_color { get; set; }
        /// <summary>
        /// Links font, default value: Arial
        /// </summary>
        public string Links_font { get; set; }
        /// <summary>
        /// Links size, default value: 15
        /// </summary>
        public int Links_size { get; set; }

    }
}
