<%
foreach (BaseTreeNode entityNode in Model.Children) {
    EntityNode currentEntity = (EntityNode)entityNode;
    if (currentEntity.GenerateObject && (currentEntity.PrimaryKeyFields.Count != 0 || currentEntity.GenerateAsReadOnly)) {%>
//------------------------------------------------------------------------------
// This file was generated by Cooperator Modeler, version <%Response.Write(parameters["GeneratorVersion"]);%>
// Created: <%Response.Write(DateTime.Today.ToShortDateString());%> - <%Response.Write(DateTime.Now.ToShortTimeString());%>
// This is a partial class file. The other one is <%Response.Write(currentEntity.GenerateAs);%>Gateway.Auto.cs
// You can edit this file as your wish.
//------------------------------------------------------------------------------

using <%Response.Write(parameters["RulesProjectName"]);%>.Objects<%Response.Write(currentEntity.FormattedNamespace);%>;
using Cooperator.Framework.Core;
using Cooperator.Framework.Data;
using Cooperator.Framework.Data.Exceptions;
using System.Data.Common;
using System.Reflection;
using System;

namespace <%Response.Write(parameters["RulesProjectName"]);%>.Gateways<%Response.Write(currentEntity.FormattedNamespace);%>
{

    /// <summary>
    /// Gateway between <%Response.Write(currentEntity.GenerateAs);%>Object class and <%Response.Write(currentEntity.Name);%> table.
    /// This class provide CRUD methods for <%Response.Write(currentEntity.Name);%> table.
    /// </summary>
    public partial class <%Response.Write(currentEntity.GenerateAs);%>Gateway
    {
        // /// <summary>
        // /// Enables GetObjectBySQLText and GetObjectListBySQLText methods.
        // /// </summary>
        //protected override bool SQLQueriesEnabled()
        //{
        //    return true;
        //}


        // /// <summary>
        // /// Checks for security ritghs
        // /// </summary>
        //protected override bool CheckForSecurityRights(SecurityRights action, <%Response.Write(currentEntity.GenerateAs);%>ObjectList ObjectListOrEntityList)
        //{
        //    switch (action)
        //    {
        //        case SecurityRights.Read:
        //            return true;
        //        case SecurityRights.Insert:
        //            return true;
        //        case SecurityRights.Update:
        //            return true;
        //        case SecurityRights.Delete:
        //            return true;
        //    }
        //    return false;
        //}

    }

}


<%
Response.SaveBuffer("\\Gateways\\" + currentEntity.FullGenerateAs + "Gateway.cs");
}
}
%>
