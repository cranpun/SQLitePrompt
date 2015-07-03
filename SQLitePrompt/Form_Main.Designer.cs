namespace SQLitePrompt
{
    partial class Form_Main
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView_prompt = new System.Windows.Forms.DataGridView();
            this.textBox_prompt = new System.Windows.Forms.TextBox();
            this.textBox_log = new System.Windows.Forms.TextBox();
            this.button_exec = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_prompt)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView_prompt
            // 
            this.dataGridView_prompt.AllowUserToAddRows = false;
            this.dataGridView_prompt.AllowUserToDeleteRows = false;
            this.dataGridView_prompt.AllowUserToOrderColumns = true;
            this.dataGridView_prompt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_prompt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_prompt.Location = new System.Drawing.Point(1, 1);
            this.dataGridView_prompt.Name = "dataGridView_prompt";
            this.dataGridView_prompt.ReadOnly = true;
            this.dataGridView_prompt.RowTemplate.Height = 21;
            this.dataGridView_prompt.Size = new System.Drawing.Size(863, 373);
            this.dataGridView_prompt.TabIndex = 0;
            // 
            // textBox_prompt
            // 
            this.textBox_prompt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_prompt.Location = new System.Drawing.Point(1, 430);
            this.textBox_prompt.Multiline = true;
            this.textBox_prompt.Name = "textBox_prompt";
            this.textBox_prompt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_prompt.Size = new System.Drawing.Size(863, 84);
            this.textBox_prompt.TabIndex = 1;
            // 
            // textBox_log
            // 
            this.textBox_log.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_log.Location = new System.Drawing.Point(1, 380);
            this.textBox_log.Multiline = true;
            this.textBox_log.Name = "textBox_log";
            this.textBox_log.ReadOnly = true;
            this.textBox_log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_log.Size = new System.Drawing.Size(863, 50);
            this.textBox_log.TabIndex = 2;
            // 
            // button_exec
            // 
            this.button_exec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_exec.Location = new System.Drawing.Point(777, 520);
            this.button_exec.Name = "button_exec";
            this.button_exec.Size = new System.Drawing.Size(75, 23);
            this.button_exec.TabIndex = 3;
            this.button_exec.Text = "exec(&e)";
            this.button_exec.UseVisualStyleBackColor = true;
            this.button_exec.Click += new System.EventHandler(this.button_exec_Click);
            // 
            // Form_Main
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 545);
            this.Controls.Add(this.button_exec);
            this.Controls.Add(this.textBox_log);
            this.Controls.Add(this.textBox_prompt);
            this.Controls.Add(this.dataGridView_prompt);
            this.Name = "Form_Main";
            this.Text = "SQLitePrompt";
            this.Load += new System.EventHandler(this.Form_Main_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form_Main_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form_Main_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_prompt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_prompt;
        private System.Windows.Forms.TextBox textBox_prompt;
        private System.Windows.Forms.TextBox textBox_log;
        private System.Windows.Forms.Button button_exec;
    }
}

