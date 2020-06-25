using Client.Model;
using Client.Presenter;
using System;
using System.Threading;
using System.Windows.Forms;

namespace Client.View {

    public partial class NewGraphForm : Form, INewGraphView {
        private Thread CreateGraphTread;

        public NewGraphForm() {
            InitializeComponent();
        }

        private void GenerateAndSaveButton_Click(object sender, EventArgs e) {
            MainPresenter mainPresenter = new MainPresenter();
            int vertices = 0;
            bool weights = weights_checkbox.Checked;
            if (Int32.TryParse(nodes_textBox.Text, out vertices)) {
                CreateGraphTread = new Thread(async () => {
                    Graph graph = mainPresenter.CreateRandomoGraphAsync(vertices, weights).Result;
                    ListViewItem item = new ListViewItem(graph.ToString());
                    item.Tag = graph;
                    MainForm owner = Owner as MainForm;
                    if (owner.graphsListViewProperty.InvokeRequired) {
                        owner.graphsListViewProperty.Invoke(new Action(() => {
                            owner.graphsListViewProperty.Items.Add(item);
                        }));
                    } else {
                        owner.graphsListViewProperty.Items.Add(item);
                    }
                });
                CreateGraphTread.Start();
            }
            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}