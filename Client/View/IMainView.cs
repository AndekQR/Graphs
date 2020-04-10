using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.View {

    public interface IMainView {
        string PersonName { get; set; }
        string LastName { get; set; }
        string FullName { get; set; }
        string LogTextBox { get; set; }
    }
}