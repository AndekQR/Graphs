﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Service.Models {

    public class GraphPart {
        public int Id { get; set; }
        public virtual Node Node { get; set; }
        public virtual ICollection<Edge> Edge { get; set; }
        public string Uid { get; set; }

        
        [JsonIgnore]
        public virtual Graph Graph { get; set; }

        public GraphPart() {
        }
    }
}