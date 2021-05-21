
namespace DishProjectView
{
    partial class FormLetters
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
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.ButtonBack = new System.Windows.Forms.Button();
            this.ButtonForward = new System.Windows.Forms.Button();
            this.buttonPage1 = new System.Windows.Forms.Button();
            this.buttonPage2 = new System.Windows.Forms.Button();
            this.buttonPage3 = new System.Windows.Forms.Button();
            this.buttonPage4 = new System.Windows.Forms.Button();
            this.buttonPage5 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(13, 13);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(775, 365);
            this.dataGridView.TabIndex = 0;
            // 
            // ButtonBack
            // 
            this.ButtonBack.Location = new System.Drawing.Point(118, 406);
            this.ButtonBack.Name = "ButtonBack";
            this.ButtonBack.Size = new System.Drawing.Size(155, 23);
            this.ButtonBack.TabIndex = 1;
            this.ButtonBack.Text = "Назад";
            this.ButtonBack.UseVisualStyleBackColor = true;
            this.ButtonBack.Click += new System.EventHandler(this.ButtonBack_Click);
            // 
            // ButtonForward
            // 
            this.ButtonForward.Location = new System.Drawing.Point(484, 406);
            this.ButtonForward.Name = "ButtonForward";
            this.ButtonForward.Size = new System.Drawing.Size(156, 23);
            this.ButtonForward.TabIndex = 2;
            this.ButtonForward.Text = "Вперёд";
            this.ButtonForward.UseVisualStyleBackColor = true;
            this.ButtonForward.Click += new System.EventHandler(this.ButtonForward_Click);
            // 
            // buttonPage1
            // 
            this.buttonPage1.Location = new System.Drawing.Point(279, 406);
            this.buttonPage1.Name = "buttonPage1";
            this.buttonPage1.Size = new System.Drawing.Size(35, 23);
            this.buttonPage1.TabIndex = 3;
            this.buttonPage1.Text = "button1";
            this.buttonPage1.UseVisualStyleBackColor = true;
            // 
            // buttonPage2
            // 
            this.buttonPage2.Location = new System.Drawing.Point(320, 406);
            this.buttonPage2.Name = "buttonPage2";
            this.buttonPage2.Size = new System.Drawing.Size(35, 23);
            this.buttonPage2.TabIndex = 4;
            this.buttonPage2.Text = "button2";
            this.buttonPage2.UseVisualStyleBackColor = true;
            // 
            // buttonPage3
            // 
            this.buttonPage3.Location = new System.Drawing.Point(361, 406);
            this.buttonPage3.Name = "buttonPage3";
            this.buttonPage3.Size = new System.Drawing.Size(35, 23);
            this.buttonPage3.TabIndex = 5;
            this.buttonPage3.Text = "button3";
            this.buttonPage3.UseVisualStyleBackColor = true;
            // 
            // buttonPage4
            // 
            this.buttonPage4.Location = new System.Drawing.Point(402, 406);
            this.buttonPage4.Name = "buttonPage4";
            this.buttonPage4.Size = new System.Drawing.Size(35, 23);
            this.buttonPage4.TabIndex = 6;
            this.buttonPage4.Text = "button4";
            this.buttonPage4.UseVisualStyleBackColor = true;
            // 
            // buttonPage5
            // 
            this.buttonPage5.Location = new System.Drawing.Point(443, 406);
            this.buttonPage5.Name = "buttonPage5";
            this.buttonPage5.Size = new System.Drawing.Size(35, 23);
            this.buttonPage5.TabIndex = 7;
            this.buttonPage5.Text = "button5";
            this.buttonPage5.UseVisualStyleBackColor = true;
            // 
            // FormLetters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonPage5);
            this.Controls.Add(this.buttonPage4);
            this.Controls.Add(this.buttonPage3);
            this.Controls.Add(this.buttonPage2);
            this.Controls.Add(this.buttonPage1);
            this.Controls.Add(this.ButtonForward);
            this.Controls.Add(this.ButtonBack);
            this.Controls.Add(this.dataGridView);
            this.Name = "FormLetters";
            this.Text = "Письма";
            this.Load += new System.EventHandler(this.FormLetters_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button ButtonBack;
        private System.Windows.Forms.Button ButtonForward;
        private System.Windows.Forms.Button buttonPage1;
        private System.Windows.Forms.Button buttonPage2;
        private System.Windows.Forms.Button buttonPage3;
        private System.Windows.Forms.Button buttonPage4;
        private System.Windows.Forms.Button buttonPage5;
    }
}