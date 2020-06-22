using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.View {

    public interface IMainView {
        string LogTextBox { get; set; }
        ListView graphsListViewProperty { get; }
    }
}