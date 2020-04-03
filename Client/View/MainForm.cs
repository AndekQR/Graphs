using Client.Presenter;
using System;
using System.Windows.Forms;

namespace Client.View {

    public partial class MainForm : Form, IMainView {

        public MainForm() {
            InitializeComponent();

            /*Binding binding = new Binding("Text", nameTextBox, "Text");
            lastnameTextBox.DataBindings.Add(binding);*/
        }

        public string PersonName {
            get { return this.nameTextBox.Text; }
            set { this.nameTextBox.Text = value; }
        }

        public string LastName {
            get { return this.lastnameTextBox.Text; }
            set { this.lastnameTextBox.Text = value; }
        }

        public string FullName {
            get { return this.fullnameTextbox.Text; }
            set { this.fullnameTextbox.Text = value; }
        }

        private void button1_Click(object sender, EventArgs e) {
            MainPresenter mainPresenter = new MainPresenter(this);
            mainPresenter.makeFullName();
        }
    }
}