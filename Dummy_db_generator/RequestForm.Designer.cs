namespace Dummy_db_generator {
    partial class RequestForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.Box_request = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Box_request
            // 
            this.Box_request.Location = new System.Drawing.Point(12, 12);
            this.Box_request.MaxLength = 2147483647;
            this.Box_request.Multiline = true;
            this.Box_request.Name = "Box_request";
            this.Box_request.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.Box_request.Size = new System.Drawing.Size(438, 438);
            this.HScroll = true;
            this.Box_request.TabIndex = 0;
            // 
            // RequestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 462);
            this.Controls.Add(this.Box_request);
            this.Name = "RequestForm";
            this.Text = "RequestForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Box_request;
    }
}