using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Dummy_db_generator {
    public partial class Form1 : Form {

        private int colNb = 1;
        private int tabIndex = 7;
        private List<TextBox> listColumn, listSize, listRandomMin, listRandomMax;
        private List<ComboBox> listType;
        private List<CheckBox> listMendatory, listPrimary;
        private StringBuilder request;
        private StringBuilder error;
        private Random random;
        public Form1() {
            InitializeComponent();
            listColumn = new List<TextBox>();
            listRandomMin = new List<TextBox>();
            listRandomMax = new List<TextBox>();
            listSize = new List<TextBox>();
            listType = new List<ComboBox>();
            listMendatory = new List<CheckBox>();
            listPrimary = new List<CheckBox>();

            listColumn.Add(Box_column0);
            listSize.Add(Box_size0);
            listType.Add(Combo_type0);
            listMendatory.Add(Check_nonNull0);
            listPrimary.Add(Check_primary0);
            listRandomMin.Add(Box_randomMin0);
            listRandomMax.Add(Box_randomMax0);

            byte[] bytes = new byte[12];
            RandomNumberGenerator.Create().GetBytes(bytes);
            int seed = BitConverter.ToInt32(bytes, 0);
            random = new Random(seed);

            request = new StringBuilder();
            error = new StringBuilder();
        }

        private void Btn_generate_Click(object sender, EventArgs e) {
            request.Clear();
            error.Clear();
            error.Append("/*");
            Label_error.Visible = false;
            request.AppendLine("/* Automatically generated script");
            request.AppendLine("All data makes no sense");
            request.AppendLine("And it should be used for development and testing only");
            request.AppendLine("Check https://github.com/MonsterPoney/Dummy_db_generator for updates */");

            if (VerifInputs()) {
                // Remove "line" if column name empty 
                foreach (TextBox text in listColumn) {
                    if (String.IsNullOrWhiteSpace(text.Text)) {
                        int index = listColumn.IndexOf(text);
                        listColumn.RemoveAt(index);
                        listSize.RemoveAt(index);
                        listType.RemoveAt(index);
                        listMendatory.RemoveAt(index);
                        listPrimary.RemoveAt(index);
                    }
                }

                // If table need to be created
                if (Check_create.Checked) {
                    request.AppendLine("CREATE TABLE " + Box_tableName.Text + "(");
                    for (int i = 0; i < listColumn.Count; i++) {
                        request.Append(listColumn[i].Text);
                        request.Append(" " + listType[i].Text);
                        request.Append((String.IsNullOrWhiteSpace(listSize[i].Text)) ? "" : " (" + listSize[i].Text + ")");
                        if (listMendatory[i].Checked)
                            request.Append(" NOT NULL");
                        if (listPrimary[i].Checked)
                            request.Append(" PRIMARY KEY");
                        request.AppendLine(",");
                    }
                    request.Length -= 3;
                    request.AppendLine(")");
                }
                request.AppendLine("INSERT INTO " + Box_tableName.Text + "(");
                foreach (TextBox text in listColumn) {
                    if (!String.IsNullOrWhiteSpace(text.Text))
                        request.Append(text.Text + ",");
                }
                request.Length--;
                request.AppendLine(")VALUES");

                // TODO : generate values and add them
                for (int i = 0; i < int.Parse(Box_iterationNumber.Text); i++) {
                    request.Append("(");
                    for (int c = 0; c < colNb; c++) {
                        if (!listMendatory[c].Checked) {
                            // One chance on 10 -> NULL
                            if (random.Next(1, 10) == 5) {
                                request.Append("NULL,");
                                continue;
                            }
                        }
                        switch (listType[c].Text) {
                            case "INT":
                            case "TINYINT":
                            case "SMALLINT":
                            case "MEDIUMINT":
                            case "BIGINT":
                                long min = (listRandomMin[c].Text.Trim() == "") ? -9223372036854775808 : long.Parse(listRandomMin[c].Text);
                                long max = (listRandomMax[c].Text.Trim() == "") ? 9223372036854775807 : long.Parse(listRandomMax[c].Text);
                                request.Append("'" + GenInt(listType[c].Text, min, max) + "',");
                                break;
                            case "FLOAT":
                            case "DOUBLE":
                            case "DECIMAL":
                                request.Append("'" +
                                GenDouble(int.Parse(listSize[c].Text.Split(',')[0]),
                                          int.Parse(listSize[c].Text.Split(',')[1]),
                                          double.Parse(listRandomMin[c].Text),
                                          double.Parse(listRandomMax[c].Text)) + "'");
                                break;
                            case "TEXT":
                            case "CHAR":
                            case "VARCHAR":
                            case "TINYTEXT":
                            case "MEDIUMTEXT":
                            case "LONGTEXT":
                                request.Append("'" + GenLorem(long.Parse(listSize[c].Text)) + "',");
                                break;
                            case "DATE":
                                request.Append("'" + GenDateTime(true, false) + "',");
                                break;
                            case "DATETIME":
                                request.Append("'" + GenDateTime(true, true) + "',");
                                break;
                            case "TIMESTAMP":
                                request.Append("'" + (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds.ToString() + "',");
                                break;
                            case "TIME":
                                request.Append("'" + GenDateTime(false, true) + "',");
                                break;
                            case "FirstName(VARCHAR)":
                                request.Append("'" + GenName("first") + "',");
                                break;
                            case "LastName(VARCHAR)":
                                request.Append("'" + GenName("last") + "',");
                                break;
                            case "Email(VARCHAR)":
                                request.Append("'" + GenMail() + "',");
                                break;
                        }
                    }
                    request.Length--;
                    request.AppendLine("),");
                }
                request.Length -= 3;
                request.AppendLine("");

                if (error.Length > 3) {
                    request.AppendLine("/*Errors or warnings : */");
                    error.Append("*/");
                    request.Append(error.ToString());
                }
                request.AppendLine("/*** End of script ***/");
                string req = request.ToString();
                RequestForm requestForm = new RequestForm(req);
                requestForm.Show();
            }
        }

        private void Btn_add_Click(object sender, EventArgs e) {
            try {
                TextBox column = new TextBox();
                column.Location = new System.Drawing.Point(Box_column0.Location.X, Box_column0.Location.Y + (27 * colNb));
                column.Name = "box_column" + colNb.ToString();
                column.Tag = colNb;
                column.Size = new System.Drawing.Size(100, 20);
                column.TabIndex = tabIndex;
                tabIndex++;
                listColumn.Add(column);

                ComboBox type = new ComboBox();
                type.FormattingEnabled = true;
                type.DropDownStyle = ComboBoxStyle.DropDownList;
                type.ValueMember = "Value";
                type.DisplayMember = "Text";
                type.Location = new System.Drawing.Point(Combo_type0.Location.X, Combo_type0.Location.Y + (27 * colNb));
                type.Items.AddRange(ComboBoxItemShare.items);
                type.SelectedIndexChanged += new System.EventHandler(this.Combo_type_SelectedIndexChanged);
                type.Name = "box_type" + colNb.ToString();
                type.Tag = colNb;
                type.Size = new System.Drawing.Size(121, 21);
                type.TabIndex = tabIndex;
                tabIndex++;
                listType.Add(type);

                TextBox size = new TextBox();
                size.Location = new System.Drawing.Point(Box_size0.Location.X, Box_size0.Location.Y + (27 * colNb));
                size.Name = "box_size" + colNb.ToString();
                size.Enabled = Check_create.Checked;
                size.Tag = colNb;
                size.Size = new System.Drawing.Size(52, 20);
                size.TabIndex = tabIndex;
                tabIndex++;
                listSize.Add(size);

                TextBox randomMin = new TextBox();
                randomMin.Location = new System.Drawing.Point(Box_randomMin0.Location.X, Box_randomMin0.Location.Y + (27 * colNb));
                randomMin.Name = "Box_randomMin" + colNb.ToString();
                randomMin.Tag = colNb;
                randomMin.Size = new System.Drawing.Size(68, 20);
                listRandomMin.Add(randomMin);

                TextBox randomMax = new TextBox();
                randomMax.Location = new System.Drawing.Point(Box_randomMax0.Location.X, Box_randomMax0.Location.Y + (27 * colNb));
                randomMax.Name = "Box_randomMax" + colNb.ToString();
                randomMax.Tag = colNb;
                randomMax.Size = new System.Drawing.Size(68, 20);
                listRandomMax.Add(randomMax);

                CheckBox mendatory = new CheckBox();
                mendatory.AutoSize = true;
                mendatory.Location = new System.Drawing.Point(Check_nonNull0.Location.X, Check_nonNull0.Location.Y + (27 * colNb));
                mendatory.Name = "check_nonNull" + colNb.ToString();
                mendatory.Tag = colNb;
                mendatory.Size = new System.Drawing.Size(65, 17);
                mendatory.Text = "Non null";
                mendatory.UseVisualStyleBackColor = true;
                listMendatory.Add(mendatory);

                CheckBox primary = new CheckBox();
                primary.AutoSize = true;
                primary.Enabled = Check_create.Checked;
                primary.Location = new System.Drawing.Point(Check_primary0.Location.X, Check_primary0.Location.Y + (27 * colNb));
                primary.Name = "Check_primary" + colNb.ToString();
                primary.Tag = colNb;
                primary.Size = new System.Drawing.Size(80, 17);
                primary.TabIndex = 6;
                primary.Text = "Primary key";
                primary.UseVisualStyleBackColor = true;
                listPrimary.Add(primary);


                Controls.Add(column);
                Controls.Add(type);
                Controls.Add(size);
                Controls.Add(mendatory);
                Controls.Add(primary);
                Controls.Add(randomMin);
                Controls.Add(randomMax);
                colNb++;
            }
            catch (Exception ex) {
                Console.WriteLine("Exception Btn_add_Click()" + ex.Message + ex.StackTrace);
            }
        }

        // TODO : Complete method
        private bool VerifInputs() {
            if (Box_tableName.Text.Trim().Contains(" "))
                Box_tableName.Text = Box_tableName.Text.Replace(" ", "_");

            if (Array.Exists(StaticArray.sqlKeywords, keyword => keyword == Box_tableName.Text.Trim().ToLower())) {
                MessageBox.Show($"The table name is a reserved keyword", "Error", MessageBoxButtons.OK);
                return false;
            }

            foreach (TextBox box in listColumn) {
                if (box.Text.Contains(" "))
                    box.Text = box.Text.Trim().Replace(" ", "_");
            }

            foreach (TextBox box in listColumn) {
                if (Array.Exists(StaticArray.sqlKeywords, keyword => keyword == box.Text.Trim().ToLower())) {
                    MessageBox.Show($"The column name {box.Text.Trim()} is a reserved keyword", "Error", MessageBoxButtons.OK);
                    return false;
                }
            }

            if (Check_create.Checked) {
                int primaryNb = 0;
                foreach (CheckBox primary in listPrimary) {

                    if (primary.Checked)
                        primaryNb++;
                    if (primaryNb > 1) {
                        MessageBox.Show("More than one primary key present", "Error", MessageBoxButtons.OK);
                        return false;
                    }

                }
            }

            if (String.IsNullOrWhiteSpace(Box_tableName.Text)) {
                MessageBox.Show("Table name must be present", "Error", MessageBoxButtons.OK);
                return false;
            }

            if (String.IsNullOrWhiteSpace(Box_iterationNumber.Text)) {
                MessageBox.Show("Iteration number must be present", "Error", MessageBoxButtons.OK);
                return false;
            }

            foreach (TextBox box in listRandomMin) {
                if (box.Text.Trim() != "") {
                    try {
                        switch (listType[int.Parse(box.Tag.ToString())].Text) {
                            case "TINYINT":
                                if (long.Parse(box.Text) < -128) {
                                    error.AppendLine($"Minimun random number of column : {listColumn[int.Parse(box.Tag.ToString())].Text} overflow his type, value changed to -128");
                                    box.Text = "-128";
                                    Label_error.Visible = true;
                                }
                                break;
                            case "SMALLINT":
                                if (long.Parse(box.Text) < -32768) {
                                    error.AppendLine($"Minimun random number of column : {listColumn[int.Parse(box.Tag.ToString())].Text} overflow his type, value changed to -32768");
                                    box.Text = "-32768";
                                    Label_error.Visible = true;
                                }
                                break;
                            case "MEDIUMINT":
                                if (long.Parse(box.Text) < -8388608) {
                                    error.AppendLine($"Minimun random number of column : {listColumn[int.Parse(box.Tag.ToString())].Text} overflow his type, value changed to -8388608");
                                    box.Text = "-1283886088";
                                    Label_error.Visible = true;
                                }
                                break;
                            case "INT":
                                if (long.Parse(box.Text) < -2147483648) {
                                    error.AppendLine($"Minimun random number of column : {listColumn[int.Parse(box.Tag.ToString())].Text} overflow his type, value changed to -2147483648");
                                    box.Text = "-2147483648";
                                    Label_error.Visible = true;
                                }
                                break;
                        }
                    }
                    catch (OverflowException oe) {
                        MessageBox.Show("Overflow exception, a number is below -9223372036854775808", "Error", MessageBoxButtons.OK);
                        return false;
                    }
                }
            }

            foreach (TextBox box in listRandomMax) {
                if (box.Text.Trim() != "") {
                    try {
                        switch (listType[int.Parse(box.Tag.ToString())].Text) {
                            case "TINYINT":
                                if (long.Parse(box.Text) > 127) {
                                    error.AppendLine($"Maximum random number of column : {listColumn[int.Parse(box.Tag.ToString())].Text} overflow his type, value changed to 127");
                                    box.Text = "127";
                                    Label_error.Visible = true;
                                }
                                break;
                            case "SMALLINT":
                                if (long.Parse(box.Text) > 32767) {
                                    error.AppendLine($"Maximum random number of column : {listColumn[int.Parse(box.Tag.ToString())].Text} overflow his type, value changed to 32767");
                                    box.Text = "32767";
                                    Label_error.Visible = true;
                                }
                                break;
                            case "MEDIUMINT":
                                if (long.Parse(box.Text) > 8388607) {
                                    error.AppendLine($"Maximum random number of column : {listColumn[int.Parse(box.Tag.ToString())].Text} overflow his type, value changed to 8388607");
                                    box.Text = "8388607";
                                    Label_error.Visible = true;
                                }
                                break;
                            case "INT":
                                if (long.Parse(box.Text) > 2147483647) {
                                    error.AppendLine($"Maximum random number of column : {listColumn[int.Parse(box.Tag.ToString())].Text} overflow his type, value changed to ");
                                    box.Text = "2147483647";
                                    Label_error.Visible = true;
                                }
                                break;
                        }
                    }
                    catch (OverflowException oe) {
                        MessageBox.Show("Overflow exception, a number is higher than 9223372036854775807", "Error", MessageBoxButtons.OK);
                        return false;
                    }
                }
            }

            return true;
        }

        private string GenInt(string size, long min = -9223372036854775808, long max = 9223372036854775807) {
            try {

                switch (size) {
                    case "TINYINT":
                        if (min == -9223372036854775808)
                            min = -128;
                        if (max == 9223372036854775807)
                            max = 127;
                        return random.Next((int)((min < -128) ? -128 : min), (int)((max > 127) ? 127 : max)).ToString();
                    case "SMALLINT":
                        if (min == -9223372036854775808)
                            min = -32768;
                        if (max == 9223372036854775807)
                            max = 32767;
                        return random.Next((int)((min < -32768) ? -32768 : min), (int)((max > 32767) ? 32767 : max)).ToString();
                    case "MEDIUMINT":
                        if (min == -9223372036854775808)
                            min = -8388608;
                        if (max == 9223372036854775807)
                            max = 8388607;
                        return random.Next((int)((min < -8388608) ? -8388608 : min), (int)((max > 8388607) ? 8388607 : max)).ToString();
                    case "INT":
                        if (min == -9223372036854775808)
                            min = -2147483648;
                        if (max == 9223372036854775807)
                            max = 2147483647;
                        return random.Next((int)((min < -2147483648) ? -2147483648 : min), (int)((max > 2147483647) ? 2147483647 : max)).ToString();
                    case "BIGINT":
                        long range = max - min;
                        long longRand;
                        do {
                            byte[] buf = new byte[8];
                            random.NextBytes(buf);
                            longRand = (long)BitConverter.ToInt64(buf, 0);
                        } while (longRand > long.MaxValue - ((long.MaxValue % range) + 1) % range);
                        longRand = (longRand % range) + min;

                        if (longRand % 5 < 0)
                            longRand = longRand * -1;
                        return longRand.ToString();
                    default:
                        return "42";
                }
            }
            catch (Exception e) {
                Console.WriteLine("Exception GenInt()" + e.Message + e.StackTrace);
            }
            return null;
        }

        private string GenMail() {
            return $"{GenName("first")}.{GenName("last")}@mail.com";
        }

        private string GenName(string type) {
            string name = "";
            if (type == "first")
                name = StaticArray.firstNames[random.Next(StaticArray.firstNames.Length + 1)];
            else if (type == "last")
                name = StaticArray.lastNames[random.Next(StaticArray.lastNames.Length + 1)];
            return name;
        }

        private string GenDouble(int maxSize, int precision, double min, double max) {
            string result = "";
            try {
                double nextDouble = random.NextDouble() * (max - min) + min;

                if (nextDouble % 5 < 0)
                    nextDouble = nextDouble * -1;
                result = nextDouble.ToString();
                if (result.Length > maxSize)
                    result = result.Substring(0, result.Length - (result.Length - maxSize));
                else if (result.Split(',')[1].Length > precision)
                    result = result.Substring(0, result.Length - (result.Split('.')[1].Length - precision));
            }
            catch (Exception e) {
                Console.WriteLine("Exception GenDouble()" + e.Message + e.StackTrace);
            }
            return result;
        }

        private string GenLorem(long maxSize) {
            StringBuilder result = new StringBuilder();
            try {

                byte[] buf = new byte[8];
                random.NextBytes(buf);
                long longRand = BitConverter.ToInt64(buf, 0);
                long randWord = (Math.Abs(longRand % (maxSize - 1)) + 1);

                var words = new[]{"lorem", "ipsum", "dolor", "sit", "amet", "consectetuer",
                "adipiscing", "elit", "sed", "diam", "do",",", "nonummy", "nibh", "euismod",
                "tincidunt", "ut", "laoreet", "dolore", "magna", "aliquam", "erat",".","eiusmod",
                "tempor","incididunt","labore","et","aliqua","enim","ad","minim","veniam",
                "quis","nostrud","exercitation","ullamco","laboris","nisi","aliquip","ex","ea",
                "commodo","consequat","duis","aute","irure","in","reprehenderit","voluptate",
                "velit","esse","cillum","eu","fugiat","nulla","pariatur","excepteur","sint",
                "occaecat","cupidatat","non","proident","sunt","culpa","qui","officia",
                "deserunt","mollit","anim","id","est","laborum"};

                for (long w = 0; w < randWord; w++) {
                    if ((maxSize - result.Length) <= 13)
                        continue;
                    if (w > 0)
                        result.Append(" ");
                    result.Append(words[random.Next(words.Length)]);
                }
            }
            catch (Exception e) {
                Console.WriteLine("Exception GenLorem()" + e.Message + e.StackTrace);
            }

            return result.ToString();
        }

        private string GenDateTime(bool date, bool time) {
            DateTime dateTime = new DateTime();
            try {

                if (date) {
                    dateTime = dateTime.AddYears(random.Next(1900, (Check_futureDate.Checked) ? int.Parse(Box_maxYear.Text) : DateTime.Now.Year));
                    dateTime = dateTime.AddMonths(random.Next(1, (Check_futureDate.Checked) ? 12 : DateTime.Now.Month));
                    dateTime = dateTime.AddDays(random.Next(1, (Check_futureDate.Checked) ? 31 : DateTime.Now.Day));
                    if (!time)
                        return dateTime.ToShortDateString();
                }
                if (time) {
                    dateTime = dateTime.AddHours(random.Next(0, 24));
                    dateTime = dateTime.AddMinutes(random.Next(0, 60));
                    dateTime = dateTime.AddSeconds(random.Next(0, 60));
                    if (!date)
                        return dateTime.ToLongTimeString();
                }
            }
            catch (Exception e) {
                Console.WriteLine("Exception GenDateTime()" + e.Message + e.StackTrace);
            }

            return dateTime.ToString();
        }

        #region Events
        private void Check_futureDate_CheckedChanged(object sender, EventArgs e) {
            Box_maxYear.Enabled = ((CheckBox)sender).Checked;
        }

        private void Check_create_CheckedChanged(object sender, EventArgs e) {
            foreach (CheckBox checkBox in listPrimary) {
                checkBox.Enabled = ((CheckBox)sender).Checked;
            }
            foreach (TextBox box in listSize) {
                if ((ComboBoxItem)listType[(int)box.Tag].SelectedItem != null)
                    box.Enabled = (((ComboBoxItem)listType[(int)box.Tag].SelectedItem).Value == 0 || ((ComboBoxItem)listType[(int)box.Tag].SelectedItem).Value == 1) ? false : ((CheckBox)sender).Checked;
            }
        }

        private void Box_onlyDigit(object sender, KeyPressEventArgs e) {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) {
                e.Handled = true;
            }
        }

        private void Box_numericOnly(object sender, KeyPressEventArgs e) {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ',')) {
                e.Handled = true;
            }
        }

        private void Combo_type_SelectedIndexChanged(object sender, EventArgs e) {
            var cb = sender as ComboBox;

            if (cb.SelectedItem != null && cb.SelectedItem is ComboBoxItem && ((ComboBoxItem)cb.SelectedItem).Selectable == false) {
                // deselect item
                cb.SelectedIndex += 1;
                return;
            }
            if (((ComboBoxItem)cb.SelectedItem).Value == 0) {
                listSize[(int)cb.Tag].Enabled = false;
                listRandomMin[(int)cb.Tag].Enabled = false;
                listRandomMax[(int)cb.Tag].Enabled = false;
            } else if (((ComboBoxItem)cb.SelectedItem).Value == 1) {
                listSize[(int)cb.Tag].Enabled = false;
                listRandomMin[(int)cb.Tag].Enabled = true;
                listRandomMax[(int)cb.Tag].Enabled = true;
            } else if (((ComboBoxItem)cb.SelectedItem).Value == 2) {
                if (Check_create.Checked)
                    listSize[(int)cb.Tag].Enabled = true;
                listRandomMin[(int)cb.Tag].Enabled = true;
                listRandomMax[(int)cb.Tag].Enabled = true;
            } else if (((ComboBoxItem)cb.SelectedItem).Value == 3) {
                if (Check_create.Checked)
                    listSize[(int)cb.Tag].Enabled = true;
                listRandomMin[(int)cb.Tag].Enabled = false;
                listRandomMax[(int)cb.Tag].Enabled = false;
            }
        }

    }
    #endregion
}
