namespace Client.View {
    partial class NewGraphForm {
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
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.directed_checkbox = new System.Windows.Forms.CheckBox();
            this.nodes_textBox = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "How many nodes?";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(15, 117);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 42);
            this.button1.TabIndex = 1;
            this.button1.Text = "Generate and save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.GenerateAndSaveButton_Click);
            // 
            // directed_checkbox
            // 
            this.directed_checkbox.AutoSize = true;
            this.directed_checkbox.Location = new System.Drawing.Point(15, 84);
            this.directed_checkbox.Name = "directed_checkbox";
            this.directed_checkbox.Size = new System.Drawing.Size(66, 17);
            this.directed_checkbox.TabIndex = 2;
            this.directed_checkbox.Text = "Directed";
            this.directed_checkbox.UseVisualStyleBackColor = true;
            // 
            // nodes_textBox
            // 
            this.nodes_textBox.Location = new System.Drawing.Point(15, 42);
            this.nodes_textBox.Name = "nodes_textBox";
            this.nodes_textBox.Size = new System.Drawing.Size(200, 20);
            this.nodes_textBox.TabIndex = 3;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(118, 117);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(97, 42);
            this.button2.TabIndex = 4;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // NewGraphForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(232, 173);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.nodes_textBox);
            this.Controls.Add(this.directed_checkbox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Name = "NewGraphForm";
            this.Text = "NewGraphForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox directed_checkbox;
        private System.Windows.Forms.TextBox nodes_textBox;
        private System.Windows.Forms.Button button2;
    }
}