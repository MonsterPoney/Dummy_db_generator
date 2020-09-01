namespace Dummy_db_generator {
    class ComboBoxItem {
        public long? Value { get; set; }
        public string Text { get; set; }
        public bool Selectable { get; set; }
    }

    // TODO : unsigned
    // TODO : BLOB,MEDIUMBLOB,LONGBLOB
    // TODO : ENUM ???? :(
    // TODO : Year
    // TODO : numeric
    class ComboBoxItemShare {
        public static object[] items = new[] {
            new ComboBoxItem() { Selectable = false, Text = "---Common---"},
            new ComboBoxItem() { Selectable = true, Text = "INT",Value=1},
            new ComboBoxItem() { Selectable = true, Text = "DECIMAL",Value=2}, // size,d
            new ComboBoxItem() { Selectable = true, Text = "CHAR",Value=2},
            new ComboBoxItem() { Selectable = true, Text = "VARCHAR",Value=2},
            new ComboBoxItem() { Selectable = true, Text = "DATETIME",Value=0},
            new ComboBoxItem() { Selectable = false, Text = "---Numbers---"},
            new ComboBoxItem() { Selectable = true, Text = "TINYINT",Value=1},
            new ComboBoxItem() { Selectable = true, Text = "SMALLINT",Value=1},
            new ComboBoxItem() { Selectable = true, Text = "MEDIUMINT",Value=1},
            new ComboBoxItem() { Selectable = true, Text = "BIGINT",Value=1},
            new ComboBoxItem() { Selectable = true, Text = "FLOAT",Value=2}, // size,d
            new ComboBoxItem() { Selectable = true, Text = "DOUBLE",Value=2}, // size,d
            new ComboBoxItem() { Selectable = true, Text = "BIT",Value=0},
            new ComboBoxItem() { Selectable = false, Text = "---Text---"},
            new ComboBoxItem() { Selectable = true, Text = "TEXT",Value=1},
            new ComboBoxItem() { Selectable = true, Text = "TINYTEXT",Value=1},
            new ComboBoxItem() { Selectable = true, Text = "MEDIUMTEXT",Value=1},
            new ComboBoxItem() { Selectable = true, Text = "LONGTEXT",Value=1},
            new ComboBoxItem() { Selectable = false, Text = "---Dates---"},
            new ComboBoxItem() { Selectable = true, Text = "DATE",Value=0},
            new ComboBoxItem() { Selectable = true, Text = "TIMESTAMP",Value=0},
            new ComboBoxItem() { Selectable = true, Text = "TIME",Value=0},
            new ComboBoxItem() { Selectable = false, Text = "---Special---"},
            new ComboBoxItem() { Selectable = true, Text = "FirstName(VARCHAR)",Value=3},
            new ComboBoxItem() { Selectable = true, Text = "LastName(VARCHAR)",Value=3},
            new ComboBoxItem() { Selectable = true, Text = "Email(VARCHAR)",Value=3}
        };
    }
}
