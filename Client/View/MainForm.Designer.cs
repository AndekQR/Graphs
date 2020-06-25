namespace Client.View {
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
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bfs_fine_button = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.bfs_coarse_button = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // logTextBox
            // 
            this.logTextBox.Location = new System.Drawing.Point(636, 474);
            this.logTextBox.Multiline = true;
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.Size = new System.Drawing.Size(383, 99);
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
            this.button1.Location = new System.Drawing.Point(13, 474);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(169, 45);
            this.button1.TabIndex = 11;
            this.button1.Text = "Add graph";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.AddGraphButton_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(13, 525);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(169, 48);
            this.button2.TabIndex = 12;
            this.button2.Text = "Delete selected graph";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.DeleteGraphButton_Click);
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
            // panel1
            // 
            this.panel1.Controls.Add(this.bfs_fine_button);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Location = new System.Drawing.Point(190, 490);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(214, 83);
            this.panel1.TabIndex = 16;
            // 
            // bfs_fine_button
            // 
            this.bfs_fine_button.Location = new System.Drawing.Point(4, 31);
            this.bfs_fine_button.Name = "bfs_fine_button";
            this.bfs_fine_button.Size = new System.Drawing.Size(207, 24);
            this.bfs_fine_button.TabIndex = 1;
            this.bfs_fine_button.Text = "BFS Method";
            this.bfs_fine_button.UseVisualStyleBackColor = true;
            this.bfs_fine_button.Click += new System.EventHandler(this.bfs_fine_button_Click);
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(4, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(207, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "Using fine-grained calculations";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.bfs_coarse_button);
            this.panel2.Controls.Add(this.textBox2);
            this.panel2.Location = new System.Drawing.Point(416, 490);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(214, 83);
            this.panel2.TabIndex = 17;
            // 
            // bfs_coarse_button
            // 
            this.bfs_coarse_button.Location = new System.Drawing.Point(4, 30);
            this.bfs_coarse_button.Name = "bfs_coarse_button";
            this.bfs_coarse_button.Size = new System.Drawing.Size(207, 24);
            this.bfs_coarse_button.TabIndex = 2;
            this.bfs_coarse_button.Text = "BFS Method";
            this.bfs_coarse_button.UseVisualStyleBackColor = true;
            this.bfs_coarse_button.Click += new System.EventHandler(this.bfs_coarse_button_Click);
            // 
            // textBox2
            // 
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(4, 4);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(207, 20);
            this.textBox2.TabIndex = 1;
            this.textBox2.Text = "Using coarse-grained calculations";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1031, 588);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.graphsListView);
            this.Controls.Add(this.wpfHost);
            this.Controls.Add(this.logTextBox);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button bfs_fine_button;
        private System.Windows.Forms.Button bfs_coarse_button;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
    }
}

