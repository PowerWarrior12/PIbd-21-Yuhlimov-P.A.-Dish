﻿namespace DishProjectView
{
    partial class FormReportDishComponents
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
            this.Компонент = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Изделие = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Количество = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ButtonSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.BackgroundColor = System.Drawing.Color.Honeydew;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Компонент,
            this.Изделие,
            this.Количество});
            this.dataGridView.Location = new System.Drawing.Point(13, 42);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(515, 396);
            this.dataGridView.TabIndex = 0;
            // 
            // Компонент
            // 
            this.Компонент.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Компонент.HeaderText = "Компонент";
            this.Компонент.Name = "Компонент";
            // 
            // Изделие
            // 
            this.Изделие.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Изделие.HeaderText = "Изделие";
            this.Изделие.Name = "Изделие";
            // 
            // Количество
            // 
            this.Количество.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Количество.HeaderText = "Количество";
            this.Количество.Name = "Количество";
            // 
            // ButtonSave
            // 
            this.ButtonSave.Location = new System.Drawing.Point(13, 13);
            this.ButtonSave.Name = "ButtonSave";
            this.ButtonSave.Size = new System.Drawing.Size(141, 23);
            this.ButtonSave.TabIndex = 1;
            this.ButtonSave.Text = "Сохранить в Excel";
            this.ButtonSave.UseVisualStyleBackColor = true;
            this.ButtonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // FormReportDishComponents
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 450);
            this.Controls.Add(this.ButtonSave);
            this.Controls.Add(this.dataGridView);
            this.Name = "FormReportDishComponents";
            this.Text = "Компоненты по изделиям";
            this.Load += new System.EventHandler(this.FormReportDishComponents_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Компонент;
        private System.Windows.Forms.DataGridViewTextBoxColumn Изделие;
        private System.Windows.Forms.DataGridViewTextBoxColumn Количество;
        private System.Windows.Forms.Button ButtonSave;
    }
}