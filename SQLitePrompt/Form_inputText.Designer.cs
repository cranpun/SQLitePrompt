namespace SQLitePrompt
{
    partial class Form_inputText
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox_input = new System.Windows.Forms.TextBox();
            this.button_action = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox_input
            // 
            this.textBox_input.Location = new System.Drawing.Point(12, 12);
            this.textBox_input.Name = "textBox_input";
            this.textBox_input.Size = new System.Drawing.Size(260, 19);
            this.textBox_input.TabIndex = 0;
            this.textBox_input.UseSystemPasswordChar = true;
            // 
            // button_action
            // 
            this.button_action.Location = new System.Drawing.Point(197, 37);
            this.button_action.Name = "button_action";
            this.button_action.Size = new System.Drawing.Size(75, 23);
            this.button_action.TabIndex = 1;
            this.button_action.Text = "OK";
            this.button_action.UseVisualStyleBackColor = true;
            this.button_action.Click += new System.EventHandler(this.button_action_Click);
            // 
            // Form_inputText
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 68);
            this.Controls.Add(this.button_action);
            this.Controls.Add(this.textBox_input);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form_inputText";
            this.Text = "パスワード";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_action;
        public System.Windows.Forms.TextBox textBox_input;
    }
}