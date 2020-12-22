using System.Windows.Forms;

namespace Dummy_db_generator {
    partial class Form1 {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent() {
            this.Box_tableName = new System.Windows.Forms.TextBox();
            this.Box_iterationNumber = new System.Windows.Forms.TextBox();
            this.Check_create = new System.Windows.Forms.CheckBox();
            this.Box_column0 = new System.Windows.Forms.TextBox();
            this.Box_size0 = new System.Windows.Forms.TextBox();
            this.Combo_type0 = new System.Windows.Forms.ComboBox();
            this.Check_nonNull0 = new System.Windows.Forms.CheckBox();
            this.Btn_generate = new System.Windows.Forms.Button();
            this.Btn_add = new System.Windows.Forms.Button();
            this.Check_primary0 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Check_futureDate = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Box_maxYear = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Box_randomMin0 = new System.Windows.Forms.TextBox();
            this.Box_randomMax0 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.Label_error = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.Btn_delete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Box_tableName
            // 
            this.Box_tableName.Location = new System.Drawing.Point(88, 12);
            this.Box_tableName.Name = "Box_tableName";
            this.Box_tableName.Size = new System.Drawing.Size(100, 20);
            this.Box_tableName.TabIndex = 0;
            // 
            // Box_iterationNumber
            // 
            this.Box_iterationNumber.Location = new System.Drawing.Point(497, 12);
            this.Box_iterationNumber.Name = "Box_iterationNumber";
            this.Box_iterationNumber.ShortcutsEnabled = false;
            this.Box_iterationNumber.Size = new System.Drawing.Size(50, 20);
            this.Box_iterationNumber.TabIndex = 1;
            this.Box_iterationNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Box_onlyDigit);
            // 
            // Check_create
            // 
            this.Check_create.AutoSize = true;
            this.Check_create.Location = new System.Drawing.Point(202, 14);
            this.Check_create.Name = "Check_create";
            this.Check_create.Size = new System.Drawing.Size(83, 17);
            this.Check_create.TabIndex = 17;
            this.Check_create.TabStop = false;
            this.Check_create.Text = "Create table";
            this.Check_create.UseVisualStyleBackColor = true;
            this.Check_create.CheckedChanged += new System.EventHandler(this.Check_create_CheckedChanged);
            // 
            // Box_column0
            // 
            this.Box_column0.Location = new System.Drawing.Point(12, 142);
            this.Box_column0.Name = "Box_column0";
            this.Box_column0.Size = new System.Drawing.Size(100, 20);
            this.Box_column0.TabIndex = 2;
            this.Box_column0.Tag = 0;
            // 
            // Box_size0
            // 
            this.Box_size0.Enabled = false;
            this.Box_size0.Location = new System.Drawing.Point(245, 142);
            this.Box_size0.Name = "Box_size0";
            this.Box_size0.Size = new System.Drawing.Size(52, 20);
            this.Box_size0.TabIndex = 4;
            this.Box_size0.Tag = 0;
            this.Box_size0.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Box_numericOnly);
            // 
            // Combo_type0
            // 
            this.Combo_type0.DisplayMember = "Text";
            this.Combo_type0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Combo_type0.FormattingEnabled = true;
            this.Combo_type0.Location = new System.Drawing.Point(118, 142);
            this.Combo_type0.Name = "Combo_type0";
            this.Combo_type0.Size = new System.Drawing.Size(121, 21);
            this.Combo_type0.TabIndex = 3;
            this.Combo_type0.Tag = 0;
            this.Combo_type0.ValueMember = "Value";
            this.Combo_type0.SelectedIndexChanged += new System.EventHandler(this.Combo_type_SelectedIndexChanged);
            // 
            // Check_nonNull0
            // 
            this.Check_nonNull0.AutoSize = true;
            this.Check_nonNull0.Location = new System.Drawing.Point(457, 144);
            this.Check_nonNull0.Name = "Check_nonNull0";
            this.Check_nonNull0.Size = new System.Drawing.Size(65, 17);
            this.Check_nonNull0.TabIndex = 7;
            this.Check_nonNull0.Tag = 0;
            this.Check_nonNull0.Text = "Non null";
            this.Check_nonNull0.UseVisualStyleBackColor = true;
            // 
            // Btn_generate
            // 
            this.Btn_generate.Location = new System.Drawing.Point(688, 8);
            this.Btn_generate.Name = "Btn_generate";
            this.Btn_generate.Size = new System.Drawing.Size(75, 23);
            this.Btn_generate.TabIndex = 16;
            this.Btn_generate.TabStop = false;
            this.Btn_generate.Tag = 0;
            this.Btn_generate.Text = "Generate";
            this.Btn_generate.UseVisualStyleBackColor = true;
            this.Btn_generate.Click += new System.EventHandler(this.Btn_generate_Click);
            // 
            // Btn_add
            // 
            this.Btn_add.Location = new System.Drawing.Point(713, 138);
            this.Btn_add.Name = "Btn_add";
            this.Btn_add.Size = new System.Drawing.Size(75, 23);
            this.Btn_add.TabIndex = 15;
            this.Btn_add.TabStop = false;
            this.Btn_add.Text = "Add column";
            this.Btn_add.UseVisualStyleBackColor = true;
            this.Btn_add.Click += new System.EventHandler(this.Btn_add_Click);
            // 
            // Check_primary0
            // 
            this.Check_primary0.AutoSize = true;
            this.Check_primary0.Enabled = false;
            this.Check_primary0.Location = new System.Drawing.Point(529, 144);
            this.Check_primary0.Name = "Check_primary0";
            this.Check_primary0.Size = new System.Drawing.Size(80, 17);
            this.Check_primary0.TabIndex = 6;
            this.Check_primary0.Tag = 0;
            this.Check_primary0.Text = "Primary key";
            this.Check_primary0.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Table name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(408, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Iteration number";
            // 
            // Check_futureDate
            // 
            this.Check_futureDate.AutoSize = true;
            this.Check_futureDate.Location = new System.Drawing.Point(15, 56);
            this.Check_futureDate.Name = "Check_futureDate";
            this.Check_futureDate.Size = new System.Drawing.Size(119, 17);
            this.Check_futureDate.TabIndex = 12;
            this.Check_futureDate.TabStop = false;
            this.Check_futureDate.Text = "Possible future date";
            this.Check_futureDate.UseVisualStyleBackColor = true;
            this.Check_futureDate.CheckedChanged += new System.EventHandler(this.Check_futureDate_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Max Year :";
            // 
            // Box_maxYear
            // 
            this.Box_maxYear.Enabled = false;
            this.Box_maxYear.Location = new System.Drawing.Point(76, 76);
            this.Box_maxYear.MaxLength = 4;
            this.Box_maxYear.Name = "Box_maxYear";
            this.Box_maxYear.ShortcutsEnabled = false;
            this.Box_maxYear.Size = new System.Drawing.Size(58, 20);
            this.Box_maxYear.TabIndex = 10;
            this.Box_maxYear.TabStop = false;
            this.Box_maxYear.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Box_onlyDigit);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Column name";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(144, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Type";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(254, 126);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Size";
            // 
            // Box_randomMin0
            // 
            this.Box_randomMin0.Location = new System.Drawing.Point(303, 142);
            this.Box_randomMin0.MaxLength = 19;
            this.Box_randomMin0.Name = "Box_randomMin0";
            this.Box_randomMin0.Size = new System.Drawing.Size(68, 20);
            this.Box_randomMin0.TabIndex = 5;
            this.Box_randomMin0.Tag = "0";
            this.Box_randomMin0.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Box_numericOnly);
            // 
            // Box_randomMax0
            // 
            this.Box_randomMax0.Location = new System.Drawing.Point(377, 142);
            this.Box_randomMax0.MaxLength = 19;
            this.Box_randomMax0.Name = "Box_randomMax0";
            this.Box_randomMax0.Size = new System.Drawing.Size(68, 20);
            this.Box_randomMax0.TabIndex = 6;
            this.Box_randomMax0.Tag = "0";
            this.Box_randomMax0.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Box_numericOnly);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(305, 126);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Random min";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(379, 126);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Random max";
            // 
            // Label_error
            // 
            this.Label_error.AutoSize = true;
            this.Label_error.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Label_error.ForeColor = System.Drawing.Color.Red;
            this.Label_error.Location = new System.Drawing.Point(198, 72);
            this.Label_error.Name = "Label_error";
            this.Label_error.Size = new System.Drawing.Size(543, 20);
            this.Label_error.TabIndex = 1;
            this.Label_error.Text = "Some minor errors has occurred, check the end of the script for more details";
            this.Label_error.Visible = false;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(250, 217);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(300, 25);
            this.progressBar.TabIndex = 0;
            this.progressBar.TabStop = false;
            this.progressBar.Visible = false;
            // 
            // Btn_delete
            // 
            this.Btn_delete.Location = new System.Drawing.Point(713, 180);
            this.Btn_delete.Name = "Btn_delete";
            this.Btn_delete.Size = new System.Drawing.Size(75, 37);
            this.Btn_delete.TabIndex = 18;
            this.Btn_delete.TabStop = false;
            this.Btn_delete.Text = "Delete last column";
            this.Btn_delete.UseVisualStyleBackColor = true;
            this.Btn_delete.Click += new System.EventHandler(this.Btn_delete_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Btn_delete);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.Label_error);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.Box_randomMax0);
            this.Controls.Add(this.Box_randomMin0);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Box_maxYear);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Check_futureDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Check_primary0);
            this.Controls.Add(this.Btn_add);
            this.Controls.Add(this.Btn_generate);
            this.Controls.Add(this.Check_nonNull0);
            this.Controls.Add(this.Combo_type0);
            this.Controls.Add(this.Box_size0);
            this.Controls.Add(this.Box_column0);
            this.Controls.Add(this.Check_create);
            this.Controls.Add(this.Box_iterationNumber);
            this.Controls.Add(this.Box_tableName);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Box_tableName;
        private System.Windows.Forms.TextBox Box_iterationNumber;
        private System.Windows.Forms.CheckBox Check_create;
        private System.Windows.Forms.TextBox Box_column0;
        private System.Windows.Forms.TextBox Box_size0;
        private System.Windows.Forms.ComboBox Combo_type0;
        private System.Windows.Forms.CheckBox Check_nonNull0;
        private System.Windows.Forms.Button Btn_generate;
        private System.Windows.Forms.Button Btn_add;
        private CheckBox Check_primary0;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private TextBox Box_randomMin0;
        private TextBox Box_randomMax0;
        private Label label7;
        private Label label8;
        private Label Label_error;
        private ProgressBar progressBar;
        public CheckBox Check_futureDate;
        public TextBox Box_maxYear;
        private Button Btn_delete;
    }
}

