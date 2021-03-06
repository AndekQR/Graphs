﻿using GraphX.PCL.Common.Models;
using System;

namespace Client.Model {

    public class Node {

        public Node() {
        }

        public Node(string label, string uid) {
            Label = label;
            Uid = uid;
        }

        public int Id { get; set; }
        public string Label { get; set; }
        public string Uid { get; set; }

        public override string ToString() {
            return Label;
        }
    }
}