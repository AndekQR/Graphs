using Client.Presenter;
using System;
using System.Windows.Forms;
using GraphX.PCL.Common.Models;

namespace Client.View {
    public partial class MainForm : Form, IMainView {

        public MainForm() {
            InitializeComponent();
            /*Binding binding = new Binding("Text", nameTextBox, "Text");
            lastnameTextBox.DataBindings.Add(binding);*/
            
        }

        public string PersonName {
            get => nameTextBox.Text;
            set => nameTextBox.Text = value;
        }

        public string LastName {
            get => lastnameTextBox.Text;
            set => lastnameTextBox.Text = value;
        }

        public string FullName {
            get => fullnameTextbox.Text;
            set => fullnameTextbox.Text = value;
        }

        public string LogTextBox {
            get => logTextBox.Text;
            set {
                if (logTextBox.InvokeRequired) {
                    logTextBox.Invoke(new Action(() => LogTextBox = value));
                }
                else {
                    logTextBox.Text = value;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            MainPresenter mainPresenter = new MainPresenter(this);
            mainPresenter.MakeFullName();

        }

    }
}