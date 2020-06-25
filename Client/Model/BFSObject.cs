using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.helpers {

    public class BFSObject {

        public BFSObject() {
        }

        public int graphId { get; set; }
        public String startNodeLabel { get; set; }
        public String endNodeLabel { get; set; }
    }
}