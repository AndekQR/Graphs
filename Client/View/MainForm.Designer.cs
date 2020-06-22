﻿namespace Client.View {
    partial class MainForm {
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
            this.logTextBox = new System.Windows.Forms.TextBox();
            this.wpfHost = new System.Windows.Forms.Integration.ElementHost();
            this.graphsListView = new System.Windows.Forms.ListView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // logTextBox
            // 
            this.logTextBox.Location = new System.Drawing.Point(719, 486);
            this.logTextBox.Multiline = true;
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.Size = new System.Drawing.Size(287, 87);
            this.logTextBox.TabIndex = 8;
            // 
            // wpfHost
            // 
            this.wpfHost.Location = new System.Drawing.Point(319, 13);
            this.wpfHost.Name = "wpfHost";
            this.wpfHost.Size = new System.Drawing.Size(700, 441);
            this.wpfHost.TabIndex = 9;
            this.wpfHost.Text = "elementHost2";
            this.wpfHost.Child = null;
            // 
            // graphsListView
            // 
            this.graphsListView.BackColor = System.Drawing.SystemColors.Window;
            this.graphsListView.FullRowSelect = true;
            this.graphsListView.GridLines = true;
            this.graphsListView.HideSelection = false;
            this.graphsListView.Location = new System.Drawing.Point(13, 13);
            this.graphsListView.MultiSelect = false;
            this.graphsListView.Name = "graphsListView";
            this.graphsListView.Size = new System.Drawing.Size(300, 440);
            this.graphsListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.graphsListView.TabIndex = 10;
            this.graphsListView.UseCompatibleStateImageBehavior = false;
            this.graphsListView.View = System.Windows.Forms.View.Details;
            this.graphsListView.Click += new System.EventHandler(this.graphsListView_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 486);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "Add graph";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.AddGraphButton_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 537);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(97, 36);
            this.button2.TabIndex = 12;
            this.button2.Text = "Delete selected graph";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.DeleteGraphButton_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(155, 506);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(244, 56);
            this.button3.TabIndex = 13;
            this.button3.Text = " Using fine-grained calculations.";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(405, 506);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(244, 56);
            this.button4.TabIndex = 14;
            this.button4.Text = "Using coarse-grained calculations.";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(187, 474);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(443, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Determining the node from which the sum of the minimum roads to the other nodes i" +
    "s minimal.";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1031, 588);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.graphsListView);
            this.Controls.Add(this.wpfHost);
            this.Controls.Add(this.logTextBox);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.TextBox logTextBox;

        #endregion

        private System.Windows.Forms.Integration.ElementHost elementHost1;
        private System.Windows.Forms.Integration.ElementHost wpfHost;
        private System.Windows.Forms.ListView graphsListView;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label1;
    }
}

