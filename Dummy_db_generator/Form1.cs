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
            Combo_type0.Items.AddRange(ComboBoxItemShare.items);

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

        // Generate full request
        private void Btn_generate_Click(object sender, EventArgs e) {
            request.Clear();
            error.Clear();
            error.Append("/*");
            Label_error.Visible = false;
            request.AppendLine("/* Automatically generated script");
            request.AppendLine("All data is random");
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
                        //  Column names
                        request.Append(listColumn[i].Text);
                        // Column types, if parentheses, extract text between
                        request.Append(" " + (listType[i].Text.Contains("(") ? listType[i].Text.Split('(', ')')[1] : listType[i].Text));
                        // Column sizes
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
                // Column names
                foreach (TextBox text in listColumn) {
                    if (!String.IsNullOrWhiteSpace(text.Text))
                        request.Append(text.Text + ",");
                }
                request.Length--;
                request.AppendLine(")VALUES");

                // Iterate x times, random values generation               
                progressBar.Visible = true;
                progressBar.Maximum = int.Parse(Box_iterationNumber.Text);
                bool futureDateChecked = Check_futureDate.Checked;
                for (int i = 0; i < int.Parse(Box_iterationNumber.Text); i++) {
                    progressBar.Value = i;
                    request.Append("(");
                    for (int c = 0; c < colNb; c++) {
                        if (!listMendatory[c].Checked) {
                            // One chance on 10 -> NULL
                            if (random.Next(1, 10) == 5) {
                                request.Append("NULL,");
                                continue;
                            }
                        }
                        try {
                            switch (listType[c].Text) {
                                case "INT":
                                case "TINYINT":
                                case "SMALLINT":
                                case "MEDIUMINT":
                                case "BIGINT":
                                    long minNum = (listRandomMin[c].Text.Trim() == "") ? -9223372036854775808 : long.Parse(listRandomMin[c].Text);
                                    long maxNum = (listRandomMax[c].Text.Trim() == "") ? 9223372036854775807 : long.Parse(listRandomMax[c].Text);
                                    request.Append("'" + Generator.GenInt(listType[c].Text, random, minNum, maxNum) + "',");
                                    break;
                                case "FLOAT":
                                case "DOUBLE":
                                case "DECIMAL":
                                    double minDec = double.Parse(listRandomMin[c].Text);
                                    double maxDec = double.Parse(listRandomMax[c].Text);
                                    request.Append("'" +
                                    Generator.GenDouble(int.Parse(listSize[c].Text.Split(',')[0]),
                                              int.Parse(listSize[c].Text.Split(',')[1]),
                                              minDec,
                                              maxDec, random) + "'");
                                    break;
                                case "BIT":
                                    request.Append("'" + ((random.NextDouble() > 0.5) ? "1" : "0") + "'");
                                    break;
                                case "TEXT":
                                case "CHAR":
                                case "VARCHAR":
                                case "TINYTEXT":
                                case "MEDIUMTEXT":
                                case "LONGTEXT":
                                    if (listSize[c].Text == "") listSize[c].Text = "42";
                                    request.Append("'" + Generator.GenLorem(
                                        long.Parse(listSize[c].Text), random) + "',");
                                    break;
                                case "DATE":
                                    request.Append("'" + Generator.GenDateTime(true, false, random,futureDateChecked,futureDateChecked ? int.Parse(Box_maxYear.Text) : DateTime.Now.Year) + "',");
                                    break;
                                case "DATETIME":
                                    request.Append("'" + Generator.GenDateTime(true, true, random,Check_futureDate.Checked, futureDateChecked ? int.Parse(Box_maxYear.Text) : DateTime.Now.Year) + "',");
                                    break;
                                case "TIMESTAMP":
                                    request.Append("'" + (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds.ToString() + "',");
                                    break;
                                case "TIME":
                                    request.Append("'" + Generator.GenDateTime(false, true, random,Check_futureDate.Checked, futureDateChecked ? int.Parse(Box_maxYear.Text) : DateTime.Now.Year) + "',");
                                    break;
                                case "First Name(VARCHAR)":
                                    request.Append("'" + Generator.GenName("first", random) + "',");
                                    break;
                                case "Last Name(VARCHAR)":
                                    request.Append("'" + Generator.GenName("last", random) + "',");
                                    break;
                                case "Email(VARCHAR)":
                                    request.Append("'" + Generator.GenMail(random) + "',");
                                    break;
                                case "Id(VARCHAR)":
                                    request.Append("'" + i + "',");
                                    break;
                            }
                        }
                        catch (Exception ex) {
                            Console.WriteLine("Exception Btn_add_Click()" + ex.Message + ex.StackTrace);
                        }
                    }
                    request.Length--;
                    request.AppendLine("),");
                }
                request.Length -= 3;
                request.AppendLine("");

                // if error(s)
                if (error.Length > 3) {
                    request.AppendLine("/*Errors or warnings : */");
                    error.Append("*/");
                    request.Append(error.ToString());
                }

                // End of generation, show request
                request.AppendLine("/*** End of script ***/");
                string req = request.ToString();
                RequestForm requestForm = new RequestForm(req);
                requestForm.Show();
                progressBar.Visible = false;
            }
        }

        // Add a new column, new components
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
                size.KeyPress += new KeyPressEventHandler(this.Box_numericOnly);
                size.TabIndex = tabIndex;
                tabIndex++;
                listSize.Add(size);

                TextBox randomMin = new TextBox();
                randomMin.Location = new System.Drawing.Point(Box_randomMin0.Location.X, Box_randomMin0.Location.Y + (27 * colNb));
                randomMin.Name = "Box_randomMin" + colNb.ToString();
                randomMin.Tag = colNb;
                randomMin.Size = new System.Drawing.Size(68, 20);
                randomMin.KeyPress += new KeyPressEventHandler(this.Box_numericOnly);
                listRandomMin.Add(randomMin);

                TextBox randomMax = new TextBox();
                randomMax.Location = new System.Drawing.Point(Box_randomMax0.Location.X, Box_randomMax0.Location.Y + (27 * colNb));
                randomMax.Name = "Box_randomMax" + colNb.ToString();
                randomMax.Tag = colNb;
                randomMax.Size = new System.Drawing.Size(68, 20);
                randomMax.KeyPress += new KeyPressEventHandler(this.Box_numericOnly);
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

                Controls.AddRange(new Control[] { column, type, size, mendatory, primary, randomMin, randomMax });
                colNb++;
            }
            catch (Exception ex) {
                Console.WriteLine("Exception Btn_add_Click()" + ex.Message + ex.StackTrace);
            }
        }

        // Delete last column
        private void Btn_delete_Click(object sender, EventArgs e) {
            if (colNb > 1) {
            }
        }

        private bool VerifInputs() {

            // Table name
            if (Box_tableName.Text.Trim().Contains(" "))
                Box_tableName.Text = Box_tableName.Text.Replace(" ", "_");

            if (Array.Exists(StaticArray.sqlKeywords, keyword => keyword == Box_tableName.Text.Trim().ToLower())) {
                MessageBox.Show($"The table name is a reserved keyword", "Error", MessageBoxButtons.OK);
                return false;
            }

            // Columns name
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
            // Checkboxes
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
            // White spaces
            if (String.IsNullOrWhiteSpace(Box_tableName.Text)) {
                MessageBox.Show("Table name must be present", "Error", MessageBoxButtons.OK);
                return false;
            }

            if (String.IsNullOrWhiteSpace(Box_iterationNumber.Text)) {
                MessageBox.Show("Iteration number must be present", "Error", MessageBoxButtons.OK);
                return false;
            }

            // Textboxes cohereance
            foreach (TextBox box in listSize) {
                string type = listType[int.Parse(box.Tag.ToString())].Text;
                if (box.Enabled) {
                    if (string.IsNullOrWhiteSpace(box.Text)) {
                        MessageBox.Show($"Size must be present for the column {listColumn[int.Parse(box.Tag.ToString())].Text}", "Error", MessageBoxButtons.OK);
                        return false;
                    }
                }
                if (type == "DOUBLE" || type == "DECIMAL" || type == "FLOAT") {
                    if (box.Text.Split(',').Length != 2) {
                        MessageBox.Show($"Size and precision must be present for the column {listColumn[int.Parse(box.Tag.ToString())].Text}", "Error", MessageBoxButtons.OK);
                        return false;
                    }
                }
            }

            //  Random min boxes
            foreach (TextBox box in listRandomMin) {
                try {
                    switch (listType[int.Parse(box.Tag.ToString())].Text) {
                        case "TINYINT":
                            if (box.Text.Trim() == "")
                                box.Text = "-128";
                            if (long.Parse(box.Text) < -128) {
                                error.AppendLine($"Minimun random number of column : {listColumn[int.Parse(box.Tag.ToString())].Text} overflow his type, value changed to -128");
                                box.Text = "-128";
                                Label_error.Visible = true;
                            }
                            break;
                        case "SMALLINT":
                            if (box.Text.Trim() == "")
                                box.Text = "-32768";
                            if (long.Parse(box.Text) < -32768) {
                                error.AppendLine($"Minimun random number of column : {listColumn[int.Parse(box.Tag.ToString())].Text} overflow his type, value changed to -32768");
                                box.Text = "-32768";
                                Label_error.Visible = true;
                            }
                            break;
                        case "MEDIUMINT":
                            if (box.Text.Trim() == "")
                                box.Text = "-1283886088";
                            if (long.Parse(box.Text) < -8388608) {
                                error.AppendLine($"Minimun random number of column : {listColumn[int.Parse(box.Tag.ToString())].Text} overflow his type, value changed to -8388608");
                                box.Text = "-1283886088";
                                Label_error.Visible = true;
                            }
                            break;
                        case "INT":
                            if (box.Text.Trim() == "")
                                box.Text = "-2147483648";
                            if (long.Parse(box.Text) < -2147483648) {
                                error.AppendLine($"Minimun random number of column : {listColumn[int.Parse(box.Tag.ToString())].Text} overflow his type, value changed to -2147483648");
                                box.Text = "-2147483648";
                                Label_error.Visible = true;
                            }
                            break;
                        case "BIGINT":
                            if (box.Text.Trim() == "")
                                box.Text = "-9223372036854775808";
                            break;
                    }
                }
                catch (OverflowException) {
                    MessageBox.Show("Overflow exception, a number is below -9223372036854775808", "Error", MessageBoxButtons.OK);
                    return false;
                }

            }

            // Random max boxes
            foreach (TextBox box in listRandomMax) {
                try {
                    switch (listType[int.Parse(box.Tag.ToString())].Text) {
                        case "TINYINT":
                            if (box.Text.Trim() == "")
                                box.Text = "127";
                            if (long.Parse(box.Text) > 127) {
                                error.AppendLine($"Maximum random number of column : {listColumn[int.Parse(box.Tag.ToString())].Text} overflow his type, value changed to 127");
                                box.Text = "127";
                                Label_error.Visible = true;
                            }
                            break;
                        case "SMALLINT":
                            if (box.Text.Trim() == "")
                                box.Text = "32767";
                            if (long.Parse(box.Text) > 32767) {
                                error.AppendLine($"Maximum random number of column : {listColumn[int.Parse(box.Tag.ToString())].Text} overflow his type, value changed to 32767");
                                box.Text = "32767";
                                Label_error.Visible = true;
                            }
                            break;
                        case "MEDIUMINT":
                            if (box.Text.Trim() == "")
                                box.Text = "8388607";
                            if (long.Parse(box.Text) > 8388607) {
                                error.AppendLine($"Maximum random number of column : {listColumn[int.Parse(box.Tag.ToString())].Text} overflow his type, value changed to 8388607");
                                box.Text = "8388607";
                                Label_error.Visible = true;
                            }
                            break;
                        case "INT":
                            if (box.Text.Trim() == "")
                                box.Text = "8388607";
                            if (long.Parse(box.Text) > 2147483647) {
                                error.AppendLine($"Maximum random number of column : {listColumn[int.Parse(box.Tag.ToString())].Text} overflow his type, value changed to ");
                                box.Text = "2147483647";
                                Label_error.Visible = true;
                            }
                            break;
                        case "BIGINT":
                            if (box.Text.Trim() == "")
                                box.Text = "9223372036854775807";
                            break;
                    }
                }
                catch (OverflowException) {
                    MessageBox.Show("Overflow exception, a number is higher than 9223372036854775807", "Error", MessageBoxButtons.OK);
                    return false;
                }
            }

            // TODO : Crash empty column

            // Max < min
            foreach (TextBox box in listRandomMin) {
                if (box.Enabled) {
                    if (long.Parse(box.Text) > long.Parse(listRandomMax[int.Parse(box.Tag.ToString())].Text)) {
                        MessageBox.Show("A random min is bigger than the random max", "Error", MessageBoxButtons.OK);
                        return false;
                    }
                }
            }

            return true;
        }

        #region Events w/ buttons
        // +=Possible futures dates
        private void Check_futureDate_CheckedChanged(object sender, EventArgs e) {
            Box_maxYear.Enabled = ((CheckBox)sender).Checked;
        }

        // +=Create table
        private void Check_create_CheckedChanged(object sender, EventArgs e) {
            foreach (CheckBox checkBox in listPrimary) {
                checkBox.Enabled = ((CheckBox)sender).Checked;
            }
            foreach (TextBox box in listSize) {
                if ((ComboBoxItem)listType[(int)box.Tag].SelectedItem != null)
                    box.Enabled = (((ComboBoxItem)listType[(int)box.Tag].SelectedItem).Value == 0 || ((ComboBoxItem)listType[(int)box.Tag].SelectedItem).Value == 1) ? false : ((CheckBox)sender).Checked;
            }
        }

        // Accept only digit
        private void Box_onlyDigit(object sender, KeyPressEventArgs e) {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) {
                e.Handled = true;
            }
        }

        // Accept only numeric
        private void Box_numericOnly(object sender, KeyPressEventArgs e) {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ',')) {
                e.Handled = true;
            }
        }

        // +=Type combo box
        private void Combo_type_SelectedIndexChanged(object sender, EventArgs e) {
            var cb = sender as ComboBox;

            if (cb.SelectedItem != null && cb.SelectedItem is ComboBoxItem && ((ComboBoxItem)cb.SelectedItem).Selectable == false) {
                // Deselect item
                cb.SelectedIndex += 1;
                return;
            }
            if (((ComboBoxItem)cb.SelectedItem).Value == 0) {
                listSize[(int)cb.Tag].Enabled = false;
                listRandomMin[(int)cb.Tag].Enabled = false;
                listRandomMax[(int)cb.Tag].Enabled = false;
            }
            else if (((ComboBoxItem)cb.SelectedItem).Value == 1) {
                listSize[(int)cb.Tag].Enabled = false;
                listRandomMin[(int)cb.Tag].Enabled = true;
                listRandomMax[(int)cb.Tag].Enabled = true;
            }
            else if (((ComboBoxItem)cb.SelectedItem).Value == 2) {
                if (Check_create.Checked)
                    listSize[(int)cb.Tag].Enabled = true;
                listRandomMin[(int)cb.Tag].Enabled = true;
                listRandomMax[(int)cb.Tag].Enabled = true;
            }
            else if (((ComboBoxItem)cb.SelectedItem).Value == 3) {
                if (Check_create.Checked)
                    listSize[(int)cb.Tag].Enabled = true;
                listRandomMin[(int)cb.Tag].Enabled = false;
                listRandomMax[(int)cb.Tag].Enabled = false;
            }
        }

    }
    #endregion
}
