using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using umbraco.businesslogic;
using umbraco.BusinessLogic.Actions;
using umbraco.interfaces;
using Umbraco.Web.Models.Trees;
using Umbraco.Web.Mvc;
using Umbraco.Web.Trees;

namespace iRefundUmbraco7.Custom.Section
{
    [Application("ApplicationForms", "ApplicationForms", "icon-car", 2)]
    public class ApplicationFormSection : IApplication
    {
    }

    [PluginController("ApplicationForms")]
    [Umbraco.Web.Trees.Tree("ApplicationForms", "ApplicationFormsTree", "iRefund Application Forms", iconClosed: "icon-doc")]
    public class CustomSectionTreeController : TreeController
    {
        protected override TreeNodeCollection GetTreeNodes(string id, FormDataCollection queryStrings)
        {
            var nodes = new TreeNodeCollection();
            var item = this.CreateTreeNode("dashboard", id, queryStrings, "Applicaion Forms", "icon-truck", true);
            nodes.Add(item);
            return nodes;
        }

        protected override MenuItemCollection GetMenuForNode(string id, FormDataCollection queryStrings)
        {
            var menu = new MenuItemCollection();
            menu.DefaultMenuAlias = ActionNew.Instance.Alias;
            menu.Items.Add<ActionNew>("Create");
            return menu;
        }
    }
}
