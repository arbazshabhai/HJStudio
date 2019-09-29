using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HJStudio.Models
{
    public class MenuModel
    {
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public string FranchiseeName { get; set; }
        public string MenuList { get; set; }
        public List<int> MenuIdList { get; set; }

        public List<MenuModel> MenuModelList { get; set; }
    }

    public class JsTree3Node
    {
        public string id;
        public string text;
        public string uri;
        public string icon;
        public State state;
        public List<JsTree3Node> children;

        public static JsTree3Node NewNode(int id, string name, bool Selected, string Uri = "#", string Icon = "")
        {
            return new JsTree3Node()
            {
                id = id.ToString(),
                text = string.Format("{0}", name),
                children = new List<JsTree3Node>(),
                state = new State(true, false, Selected),
                uri = Uri,
                icon = Icon
            };
        }


        public class State
        {
            public bool opened = false;
            public bool disabled = false;
            public bool selected = false;

            public State(bool Opened, bool Disabled, bool Selected)
            {
                opened = Opened;
                disabled = Disabled;
                selected = Selected;
            }


        }

    }
}
