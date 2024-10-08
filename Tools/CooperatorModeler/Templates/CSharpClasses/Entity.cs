<%
foreach (BaseTreeNode entityNode in Model.Children) {
    EntityNode currentEntity = (EntityNode)entityNode;
    if (currentEntity.GenerateEntity && currentEntity.GenerateObject && (currentEntity.PrimaryKeyFields.Count != 0 || currentEntity.GenerateAsReadOnly)) {%>
//------------------------------------------------------------------------------
// This file was generated by Cooperator Modeler, version <%Response.Write(parameters["GeneratorVersion"]);%>
// Created: <%Response.Write(DateTime.Today.ToShortDateString());%> - <%Response.Write(DateTime.Now.ToShortTimeString());%>
// This is a partial class file. The other one is <%Response.Write(currentEntity.GenerateAs);%>Entity.Auto.cs
// You can edit this file as your wish.
//------------------------------------------------------------------------------

using System;
using Cooperator.Framework.Core.Exceptions;

namespace <%Response.Write(parameters["RulesProjectName"]);%>.Entities<%Response.Write(currentEntity.FormattedNamespace);%>
{
    /// <summary>
    /// This class represents the <%Response.Write(currentEntity.GenerateAs);%> entity.
    /// </summary>
    [Serializable]
    public partial class <%Response.Write(currentEntity.GenerateAs);%>
        // : IValidable
    {
        // /// <summary>
        // /// When IValidable is implemented, this method is invoked by Gateway before Insert or Update to validate Object.
        // /// </summary>
        // public void Validate()
        // {
        //     //Example:
        //     if (string.IsNullOrEmpty(this.Name)) throw new RuleValidationException("Name can't be null");
        // }
    }

    /// <summary>
    /// This class represents a collection of <%Response.Write(currentEntity.GenerateAs);%> entity.
    /// </summary>
    public partial class <%Response.Write(currentEntity.GenerateAs);%>List
    {
         // /// <summary>
         // /// Returns a typed Dataset based on its content.
         // /// </summary>
         //public override System.Data.DataSet ToDataSet()
         //{
         //    YOUR_TYPED_DATASET MyDataSet = new YOUR_TYPED_DATASET();
         //    ObjectListHelper<<%Response.Write(currentEntity.GenerateAs);%>, <%Response.Write(currentEntity.GenerateAs);%>List> Exporter = new ObjectListHelper<<%Response.Write(currentEntity.GenerateAs);%>, <%Response.Write(currentEntity.GenerateAs);%>List>();
         //    Exporter.FillDataSet(MyDataSet, this);
         //    return MyDataSet;
         //}
    }
}

namespace <%Response.Write(parameters["RulesProjectName"]);%>.Views<%Response.Write(currentEntity.FormattedNamespace);%>
{
    /// <summary>
    /// This class represents a view of an collection of <%Response.Write(currentEntity.Name);%> entities.
    /// </summary>
    public partial class <%Response.Write(currentEntity.GenerateAs);%>ListView
    {
    }
}

<%
Response.SaveBuffer("\\Entities\\" + currentEntity.FullGenerateAs + ".cs");
}
}
%>
