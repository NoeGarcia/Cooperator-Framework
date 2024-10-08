<%
foreach (BaseTreeNode entityNode in Model.Children) {
    EntityNode currentEntity = (EntityNode)entityNode;
    if (currentEntity.GenerateEntity && currentEntity.GenerateObject && (currentEntity.PrimaryKeyFields.Count != 0 || currentEntity.GenerateAsReadOnly)) {%>
'------------------------------------------------------------------------------
' This file was generated by Cooperator Modeler, version <%Response.Write(parameters["GeneratorVersion"]);%>
' Created: <%Response.Write(parameters["AutoFilesDateAndTime"]);%>
' This is a partial class file. The other one is <%Response.Write(currentEntity.GenerateAs);%>Entity.cs
' You should not modifiy this file, please edit the other partial class file.
'------------------------------------------------------------------------------

Imports <%Response.Write(parameters["RulesProjectName"]);%>.Objects
<% foreach (string ns in DomainTreeHelper.GetNamespaceCollection(currentEntity.Parent)){
        Response.Write("Imports " + parameters["RulesProjectName"] + ".Objects." + ns + "\r\n");
        if (!currentEntity.FormattedNamespace.Contains(ns)){
            Response.Write("Imports " + parameters["RulesProjectName"] + ".Entities." + ns + "\r\n");
        }

   }
%>

Imports System
Imports System.Net
Imports System.Runtime.Serialization
Imports Cooperator.Framework.Core
Imports Cooperator.Framework.Core.LazyLoad

Namespace Entities<%Response.Write(currentEntity.FormattedNamespace);%>

    <DataContract()> _
    Public Partial Class <%Response.Write(currentEntity.GenerateAs);%>
        Inherits Objects.<%Response.Write(currentEntity.FullGenerateAs);%>Object
        Implements IMappeable<%Response.Write(currentEntity.GenerateAs);%>
        Implements IEquatable(Of <%Response.Write(currentEntity.GenerateAs);%>)
        Implements ICloneable

        #Region "Ctor"

        Public Sub New()
            MyBase.New()
            <%foreach (PropertyNode currentProperty in currentEntity.Children) {
            if (currentProperty.GenerateProperty && (currentProperty.IsCollection || currentProperty.IsEntity) && !currentProperty.IsOneToOneRelation) {
                bool allowNull = true;
                foreach (RelatedField relField in currentProperty.RelatedFields)
                    if (!DomainTreeHelper.FindPropertyInTree(currentProperty.Parent.Parent, currentProperty.Parent.Name, relField.Name).IsNullable) allowNull = false;

                    string entitiesOrObjects = "";
                    if (currentProperty.CLRType.EndsWith("Object") || currentProperty.CLRType.EndsWith("ObjectList")) entitiesOrObjects = "Objects.";
                    else entitiesOrObjects = "Entities.";

                    if (!allowNull) {
                    Response.Write("If _" + currentProperty.GenerateAs +" Is Nothing Then _" + currentProperty.GenerateAs + " = New " + entitiesOrObjects + currentProperty.CLRType + "()\r\n");
                }
            }}%>
        End Sub

        <% if (PK_PARAMS_FOR_OBJECTS_CTOR(currentEntity).Length > 0) { %>
        Public Sub New (<%Response.Write(PK_PARAMS_FOR_OBJECTS_CTOR(currentEntity));%>)
            MyBase.New()
<%Response.Write(PK_SET_VALUES_FOR_OBJECTS_CTOR(currentEntity));%>
            <%foreach (PropertyNode currentProperty in currentEntity.Children) {
            if (currentProperty.GenerateProperty && (currentProperty.IsCollection || currentProperty.IsEntity) && !currentProperty.IsOneToOneRelation) {
                bool allowNull = true;
                foreach (RelatedField relField in currentProperty.RelatedFields)
                    if (!DomainTreeHelper.FindPropertyInTree(currentProperty.Parent.Parent, currentProperty.Parent.Name, relField.Name).IsNullable) allowNull = false;

                    string entitiesOrObjects = "";
                    if (currentProperty.CLRType.EndsWith("Object") || currentProperty.CLRType.EndsWith("ObjectList")) entitiesOrObjects = "Objects.";
                    else entitiesOrObjects = "Entities.";

                    if (!allowNull) {
                    Response.Write("If _" + currentProperty.GenerateAs +" Is Nothing Then _" + currentProperty.GenerateAs + " = New " + entitiesOrObjects + currentProperty.CLRType + "()\r\n");
                }
            }}%>
            Call Initialized()
        End Sub
        <% } %>

        <% int noPkCounter = 0;
        foreach (PropertyNode currentProperty in currentEntity.Children)
            if (currentProperty.NativeType != "None" && currentProperty.GenerateProperty && !currentProperty.IsPrimaryKey && !currentProperty.IsForeignKey && !currentProperty.IsRowVersion)
                noPkCounter++;
        if (noPkCounter > 0) { %>
        Public Sub New(<%Response.Write(ALL_FIELDS_PARAMS_FOR_OBJECTS_CTOR(currentEntity));%>)
            MyBase.New()
<%Response.Write(ALL_FIELDS_SET_VALUES_FOR_OBJECTS_CTOR(currentEntity));%>
            <%foreach (PropertyNode currentProperty in currentEntity.Children) {
            if (currentProperty.GenerateProperty && (currentProperty.IsCollection || currentProperty.IsEntity) && !currentProperty.IsOneToOneRelation) {
                bool allowNull = true;
                foreach (RelatedField relField in currentProperty.RelatedFields)
                    if (!DomainTreeHelper.FindPropertyInTree(currentProperty.Parent.Parent, currentProperty.Parent.Name, relField.Name).IsNullable) allowNull = false;

                    string entitiesOrObjects = "";
                    if (currentProperty.CLRType.EndsWith("Object") || currentProperty.CLRType.EndsWith("ObjectList")) entitiesOrObjects = "Objects.";
                    else entitiesOrObjects = "Entities.";

                    if (!allowNull) {
                    Response.Write("If _" + currentProperty.GenerateAs +" Is Nothing Then _" + currentProperty.GenerateAs + " = New " + entitiesOrObjects + currentProperty.CLRType + "()\r\n");
                }
            }}%>
            Call Initialized()
        End Sub
        <%}%>
        #End Region

        #Region "Fields"

        <%foreach (PropertyNode currentProperty in currentEntity.Children) {
            if (currentProperty.NativeType == "None" && currentProperty.GenerateProperty) {
                string entitiesOrObjects = "";
                if (currentProperty.CLRType.EndsWith("Object") || currentProperty.CLRType.EndsWith("ObjectList")) entitiesOrObjects = "Objects.";
                else entitiesOrObjects = "Entities.";
                Response.Write("<DataMember()> _ " + "\r\n");
                Response.Write("Protected " + " _" + currentProperty.GenerateAs + " As " + entitiesOrObjects + currentProperty.CLRType +  "\r\n");
            }            
        }
        %>
        #End Region

        #Region "Properties"
        <%foreach (PropertyNode currentProperty in currentEntity.Children) {
            string entitiesOrObjects = "";
            if (currentProperty.CLRType.EndsWith("Object") || currentProperty.CLRType.EndsWith("ObjectList")) entitiesOrObjects = "Objects.";
            else entitiesOrObjects = "Entities.";
            if (currentProperty.NativeType == "None" && currentProperty.GenerateProperty ) { 
            if (currentProperty.GenerateAsLazyLoad && currentProperty.IsEntity) 
                 Response.Write("\r\nDim _" + currentProperty.GenerateAs + "Fetched As Boolean"); %>
        ''' <summary>
        ''' 
        ''' </summary>
        <DataMember()> _
        Public Overridable Property <%Response.Write(currentProperty.GenerateAs + " As " + entitiesOrObjects + currentProperty.CLRType);%>
            Get
                <%if (currentProperty.GenerateAsLazyLoad) {
                    if (currentProperty.IsEntity)
                    {
                        string param = ""; string sep2 = "";
                        string nulleables = "";
                        string sep3 = "";
                        foreach (RelatedField relField in currentProperty.RelatedFields)
                        {
                            foreach (PropertyNode currentProperty2 in currentEntity.Children) {
                                if (currentProperty2.Name == relField.Name) {
                                    if (currentProperty2.IsNullable) {
                                        if (currentProperty2.CLRType != "System.String") {
                                            nulleables += sep3 + "Me." + relField.GenerateAsForName + ".HasValue";
                                            param += sep2 + "Me." + relField.GenerateAsForName + ".Value";
                                        } else {
                                            nulleables += sep3 + "Not String.IsNullOrEmpty(Me." + relField.GenerateAsForName + ")";
                                            param += sep2 + "Me." + relField.GenerateAsForName;
                                        }
                                        sep2 = ", ";
                                        sep3 = " AndAlso ";

                                    } else {
                                        param += sep2 + "Me." + relField.GenerateAsForName;
                                        sep2 = ", ";                                        
                                        sep3 = " AndAlso ";
                                    }
                                }
                            }                            
                        }
                        if (!string.IsNullOrEmpty(nulleables)) nulleables = " AndAlso " + nulleables;
                        string lazyProvider;
                        string relatedEntity = "Dim _" + currentProperty.GenerateAs + "Temp As " + entitiesOrObjects + currentProperty.CLRType + " = New " + entitiesOrObjects + currentProperty.CLRType + "(" + param + ") \r\n";
                        if  (currentProperty.GenerateAsType == CollectionTypeEnum.Object)
                            lazyProvider = "Dim lazyProvider As ILazyProvider = LazyProviderFactory.Get(\""+parameters["DataProjectName"]+"\", \""+parameters["RulesProjectName"]+".LazyProviders.DefaultLazyProvider\")\r\n _{1} = CType(lazyProvider.GetEntity(GetType({0}Object), _" + currentProperty.GenerateAs + "Temp), {0}Object)";
                        else
                            lazyProvider = "Dim lazyProvider As ILazyProvider = LazyProviderFactory.Get(\""+parameters["DataProjectName"]+"\", \""+parameters["RulesProjectName"]+".LazyProviders.DefaultLazyProvider\")\r\n _{1} = CType(lazyProvider.GetEntity(GetType({0}), _" + currentProperty.GenerateAs + "Temp), {0}) "; 
                        string entityName = entitiesOrObjects + DomainTreeHelper.GetFullGenerateAsForEntity(currentProperty.Parent.Parent, currentProperty.RelatedTableName);
                        string askForFetched = "";
                        if (currentProperty.GenerateAsLazyLoad && currentProperty.IsEntity) 
                             askForFetched = " AndAlso Not _" + currentProperty.GenerateAs + "Fetched "; 
                        Response.Write("If _" + currentProperty.GenerateAs + " Is Nothing " + nulleables + askForFetched +" Then\r\n");
                        if (currentProperty.GenerateAsLazyLoad && currentProperty.IsEntity) 
                             Response.Write("_" + currentProperty.GenerateAs + "Fetched = True\r\n"); 
                        Response.Write(relatedEntity);
                        Response.Write(string.Format(lazyProvider, entityName, currentProperty.GenerateAs));
                        Response.Write("\r\nEnd If\r\n");
                    }
                    else
                    {
                        string lazyProvider;
                        if  (currentProperty.GenerateAsType == CollectionTypeEnum.EntityList)
                            lazyProvider = "Dim lazyProvider As ILazyProvider = LazyProviderFactory.Get(\""+parameters["DataProjectName"]+"\", \""+parameters["RulesProjectName"]+".LazyProviders.DefaultLazyProvider\")\r\n _{1} = CType(lazyProvider.GetList(GetType({0}), Me), {0}List)";
                        else
                            lazyProvider = "Dim lazyProvider As ILazyProvider = LazyProviderFactory.Get(\""+parameters["DataProjectName"]+"\", \""+parameters["RulesProjectName"]+".LazyProviders.DefaultLazyProvider\")\r\n _{1} = CType(lazyProvider.GetList(GetType({0}Object), Me), {0}ObjectList)";
                        string entityName = currentProperty.Parent.GenerateAs;
                        string collectionName = DomainTreeHelper.GetFullGenerateAsForEntity(currentProperty.Parent.Parent, currentProperty.RelatedTableName);
                        Response.Write("If _" +  currentProperty.GenerateAs + " Is Nothing Then\r\n  \t\t " + string.Format(lazyProvider, entitiesOrObjects + collectionName, currentProperty.GenerateAs) + "\r\n End If \r\n");
                    }
                }%>
                Return _<%Response.Write(currentProperty.GenerateAs);%>
            End Get
            Set (value As <%Response.Write(entitiesOrObjects + currentProperty.CLRType);%>)
                MyBase.PropertyModified()
                _<%Response.Write(currentProperty.GenerateAs);%> = value
                <%if (currentProperty.IsEntity && !currentProperty.IsOneToOneRelation) {
                    foreach (RelatedField relField in currentProperty.RelatedFields){
                        Response.Write("If value IsNot Nothing Then\r\n");
                        Response.Write("   _" + relField.GenerateAsForName + " = value." + relField.GenerateAsForRelatedName + "\r\n");
                        foreach (PropertyNode prop2 in currentEntity.Children)
                        {
                            if (prop2.GenerateProperty && prop2.IsDescriptionField && currentProperty.RelatedTableName == prop2.RelatedTableName && currentProperty.RelatedFields[0].ToString() == prop2.RelatedFields[0].ToString())
                            {
                                Response.Write("If CType(value." + DomainTreeHelper.GetDescriptionFieldNameForEntity(currentEntity.Parent,prop2.RelatedTableName) + ", Object) IsNot Nothing Then \r\n" );
                                Response.Write("_" + prop2.GenerateAs + " = value." + DomainTreeHelper.GetDescriptionFieldNameForEntity(currentEntity.Parent,prop2.RelatedTableName)+ ".ToString() \r\n" ) ;
                                Response.Write("Else\r\n");
                                Response.Write("    _" + prop2.GenerateAs + " = \"\" \r\n" );
                                Response.Write("End If\r\n");
                            }
                        }
                        Response.Write("\r\nElse \r\n");
                        if (DomainTreeHelper.FindPropertyInTree(currentProperty.Parent.Parent, currentProperty.Parent.Name, relField.Name).IsNullable) {
                            Response.Write("   _" + relField.GenerateAsForName + " = Nothing\r\n");
                        } else {
                            if (DomainTreeHelper.FindPropertyInTree(currentProperty.Parent.Parent, currentProperty.RelatedTableName, relField.RelatedName).CLRType == "System.String")
                                Response.Write("   _" + relField.GenerateAsForName + " = \"\"\r\n");
                            else if (DomainTreeHelper.FindPropertyInTree(currentProperty.Parent.Parent, currentProperty.RelatedTableName, relField.RelatedName).CLRType == "System.Guid")
                                Response.Write("   _" + relField.GenerateAsForName + " = System.Guid.Empty\r\n");
                            else 
                                Response.Write("   _" + relField.GenerateAsForName + " = " + DomainTreeHelper.FindPropertyInTree(currentProperty.Parent.Parent, currentProperty.RelatedTableName, relField.RelatedName).CLRType.ToString() + ".MinValue\r\n");
                        }
                        foreach (PropertyNode prop2 in currentEntity.Children)
                        {
                            if (prop2.GenerateProperty && prop2.IsDescriptionField && currentProperty.RelatedTableName == prop2.RelatedTableName && currentProperty.RelatedFields[0].ToString() == prop2.RelatedFields[0].ToString())
                                Response.Write("_" + prop2.GenerateAs + " = \"\"" );
                        }
                        Response.Write("\r\nEnd If\r\n");
                    }
                }%>
                            <%if (currentProperty.IsEntity) Response.Write("CorrectDescriptionField(value)\r\n");%>

            End Set
        End Property
        <%}}%>
        #End Region

        ''' <summary>
        ''' Returns de original value of entity since was created or restored. 
        ''' </summary>
        Public Shadows Function OriginalValue() As <%Response.Write(currentEntity.GenerateAs);%> 
                Return CType(MyBase.OriginalValue, <%Response.Write(currentEntity.GenerateAs);%>)
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        Protected Overrides Function Clone() As Object 
            Dim newObject As <%Response.Write(currentEntity.GenerateAs);%> 
            <%foreach(PropertyNode currentProperty in currentEntity.Children) {
                string objectsOrEntities = (currentProperty.CLRType.EndsWith("ObjectList")) ? "Objects" : "Entities";
                objectsOrEntities = parameters["RulesProjectName"] + "." + objectsOrEntities + ".";
                if (currentProperty.IsCollection && currentProperty.GenerateProperty) 
                    Response.Write("Dim new" + currentProperty.GenerateAs + " As " + objectsOrEntities + currentProperty.CLRType + "\r\n");
            } %>

            newObject = CType(Me.MemberwiseClone(), <%Response.Write(currentEntity.GenerateAs);%>)
            ' Entities
            <%foreach(PropertyNode currentProperty in currentEntity.Children) {
                string entitiesOrObjects = "";
                if (currentProperty.CLRType.EndsWith("Object") || currentProperty.CLRType.EndsWith("ObjectList")) entitiesOrObjects = "Objects.";
                else entitiesOrObjects = "Entities.";
                if (currentProperty.IsEntity  && currentProperty.GenerateProperty) { %>             
            If Me._<%Response.Write(currentProperty.GenerateAs);%> IsNot Nothing Then
                newObject._<%Response.Write(currentProperty.GenerateAs);%> = CType(CType(Me._<%Response.Write(currentProperty.GenerateAs);%>, ICloneable).Clone(),<%Response.Write(entitiesOrObjects + currentProperty.CLRType);%>)
            End If
            <%}}%>
            ' Colections
            <%foreach(PropertyNode currentProperty in currentEntity.Children) {
                if (currentProperty.IsCollection && currentProperty.GenerateProperty) {%>
            If Me._<%Response.Write(currentProperty.GenerateAs);%> IsNot Nothing Then
                <% string objectsOrEntities = (currentProperty.CLRType.EndsWith("ObjectList")) ? "Objects" : "Entities"; 
                objectsOrEntities = parameters["RulesProjectName"] + "." + objectsOrEntities + "."; %>
                new<%Response.Write(currentProperty.GenerateAs);%> = New <%Response.Write(objectsOrEntities + currentProperty.CLRType);%>()
                <%string rowType = (currentProperty.CLRType.EndsWith("List")) ? currentProperty.CLRType.Substring(0, currentProperty.CLRType.Length-4) : currentProperty.CLRType.Substring(0, currentProperty.CLRType.Length-3); %>
                For Each row As <%Response.Write(objectsOrEntities + rowType);%> in Me._<%Response.Write(currentProperty.GenerateAs);%>
                    new<%Response.Write(currentProperty.GenerateAs);%>.Add(CType(CType(Row, ICloneable).Clone(), <%Response.Write(objectsOrEntities + rowType);%>))
                Next 
                newObject._<%Response.Write(currentProperty.GenerateAs);%> = new<%Response.Write(currentProperty.GenerateAs);%>

				' Clonamos los items deleteados
				For Each item As <%Response.Write(objectsOrEntities + rowType);%> In CType(Me.<%Response.Write(currentProperty.GenerateAs);%>, IObjectList(Of <%Response.Write(objectsOrEntities + rowType);%>)).DeletedItems()
					CType(newObject.<%Response.Write(currentProperty.GenerateAs);%>, IObjectList(Of <%Response.Write(objectsOrEntities + rowType);%>)).DeletedItems().Add(
						CType(CType(item, ICloneable).Clone, <%Response.Write(objectsOrEntities + rowType);%>)
						)
				Next
            End If
            <%}}%>
            ' OriginalValue
            Dim newOriginalValue As <%Response.Write(currentEntity.GenerateAs);%>
            If MyBase.InternalOriginalValue IsNot Nothing Then
                newOriginalValue = CType(Me.OriginalValue().MemberwiseClone(), <%Response.Write(currentEntity.GenerateAs);%>)
                ' Entities
                <%foreach(PropertyNode currentProperty in currentEntity.Children) {
                string entitiesOrObjects = "";
                if (currentProperty.CLRType.EndsWith("Object") || currentProperty.CLRType.EndsWith("ObjectList")) entitiesOrObjects = "Objects.";
                else entitiesOrObjects = "Entities.";
                if (currentProperty.IsEntity  && currentProperty.GenerateProperty) { %>             
                If Me.OriginalValue()._<%Response.Write(currentProperty.GenerateAs);%> IsNot Nothing Then
                    newOriginalValue._<%Response.Write(currentProperty.GenerateAs);%> = CType(CType(Me.OriginalValue()._<%Response.Write(currentProperty.GenerateAs);%>, ICloneable).Clone(), <%Response.Write(entitiesOrObjects + currentProperty.CLRType);%>)
                End If
                <%}}%>
                ' Colections
                <%foreach(PropertyNode currentProperty in currentEntity.Children) {
                if (currentProperty.IsCollection && currentProperty.GenerateProperty) {%>
                If Me.OriginalValue()._<%Response.Write(currentProperty.GenerateAs);%> IsNot Nothing Then
                    <% string objectsOrEntities = (currentProperty.CLRType.EndsWith("ObjectList")) ? "Objects" : "Entities";
                    objectsOrEntities = parameters["RulesProjectName"] + "." + objectsOrEntities + "."; %>
                    new<%Response.Write(currentProperty.GenerateAs);%> = new <%Response.Write(objectsOrEntities + currentProperty.CLRType);%>()
                    <%string rowType = (currentProperty.CLRType.EndsWith("List")) ? currentProperty.CLRType.Substring(0, currentProperty.CLRType.Length-4) : currentProperty.CLRType.Substring(0, currentProperty.CLRType.Length-3); %>
                    For Each row As <%Response.Write(objectsOrEntities + rowType);%> in Me.OriginalValue()._<%Response.Write(currentProperty.GenerateAs);%>
                        new<%Response.Write(currentProperty.GenerateAs);%>.Add(CType(CType(row, ICloneable).Clone(), <%Response.Write(objectsOrEntities + rowType);%>))
                    Next
                    newOriginalValue._<%Response.Write(currentProperty.GenerateAs);%> = new<%Response.Write(currentProperty.GenerateAs);%>
                End If
                <%}}%>            
                newObject.InternalOriginalValue = newOriginalValue

            End If
            Return newObject
        End Function



        Private Sub CompleteEntity(<%Response.Write(AGGREGATIONS_AND_TYPES_FOR_IMAPPEABLE(currentEntity));%>) Implements IMappeable<%Response.Write(currentEntity.GenerateAs);%>.CompleteEntity
        <%string sep ="";
        foreach (PropertyNode currentProperty in currentEntity.Children)
        {
            if (currentProperty.NativeType == "None" && currentProperty.GenerateProperty)
            {
                Response.Write(sep + "_" +currentProperty.GenerateAs + " = " + currentProperty.GenerateAs );
                sep = "\r\n";
            }
        }%>
        End Sub

        <%foreach (PropertyNode currentProperty in currentEntity.Children) {
        if (currentProperty.NativeType == "None" && currentProperty.GenerateProperty && currentProperty.GenerateAsLazyLoad) { %>
        Private Function Is<%Response.Write(currentProperty.GenerateAs);%>Null() As Boolean Implements IMappeable<%Response.Write(currentEntity.GenerateAs);%>.Is<%Response.Write(currentProperty.GenerateAs);%>Null
            Return IsNothing(_<%Response.Write(currentProperty.GenerateAs);%>)
        End Function
        <%}}%>

        Private Sub SetFKValuesForChilds(entity As <%Response.Write(currentEntity.GenerateAs);%>) Implements IMappeable<%Response.Write(currentEntity.GenerateAs);%>.SetFKValuesForChilds
        <%foreach (PropertyNode currentProperty in currentEntity.Children) {
            if (currentProperty.GenerateProperty && currentProperty.IsCollection) { %>
            If _<%Response.Write(currentProperty.GenerateAs);%> IsNot Nothing Then
                <%string itemtype = "";
                if (currentProperty.GenerateAsType == CollectionTypeEnum.ObjectList) itemtype = currentProperty.CLRType.Replace("ObjectList", "Object");
                if (currentProperty.GenerateAsType == CollectionTypeEnum.EntityList) itemtype = currentProperty.CLRType.Replace("List", "");
                string objectsOrEntities = (currentProperty.CLRType.EndsWith("ObjectList")) ? "Objects" : "Entities";
                objectsOrEntities = parameters["RulesProjectName"] + "." + objectsOrEntities + ".";
                %>
                For Each item As <%Response.Write(objectsOrEntities + itemtype);%> In _<%Response.Write(currentProperty.GenerateAs);%>
                        <%foreach (RelatedField relField in currentProperty.RelatedFields) {%>
                        <%PropertyNode relatedProperty = DomainTreeHelper.FindPropertyInTree(currentProperty.Parent.Parent, currentProperty.RelatedTableName, relField.RelatedName); %>
                        <%string nullvalue = (relatedProperty.IsNullable && relatedProperty.CLRType != "System.String") ? ".Value " : ""; %>
                        <%string nullcheck = (relatedProperty.IsNullable && relatedProperty.CLRType != "System.String") ? "Not item."+ relField.RelatedName + ".HasValue OrElse " : ""; %>
                    If <%Response.Write(nullcheck);%> item.<%Response.Write(relField.GenerateAsForRelatedName + nullvalue);%> <> entity.<%Response.Write(relField.GenerateAsForName);%> Then item.<%Response.Write(relField.GenerateAsForRelatedName);%> = entity.<%Response.Write(relField.GenerateAsForName);%>
                    <%}%>
                Next
            End If
            <%}
            if (currentProperty.GenerateProperty && currentProperty.IsOneToOneRelation) { %>
            if _<%Response.Write(currentProperty.GenerateAs);%> IsNot Nothing Then
                <%foreach (RelatedField relField in currentProperty.RelatedFields) {%>
                <%PropertyNode relatedProperty = DomainTreeHelper.FindPropertyInTree(currentProperty.Parent.Parent, currentProperty.RelatedTableName, relField.RelatedName); %>
                <%string nullvalue = (relatedProperty.IsNullable && relatedProperty.CLRType != "System.String") ? ".Value " : ""; %>
                <%string nullcheck = (relatedProperty.IsNullable && relatedProperty.CLRType != "System.String") ? "Not _"+ currentProperty.GenerateAs + "." + relField.RelatedName + ".HasValue OrElse " : ""; %>
                If <%Response.Write(nullcheck);%> _<%Response.Write(currentProperty.GenerateAs);%>.<%Response.Write(relatedProperty.GenerateAs + nullvalue);%> <> entity.<%Response.Write(relField.Name);%> Then _<%Response.Write(currentProperty.GenerateAs);%>.<%Response.Write(relatedProperty.GenerateAs);%> = entity.<%Response.Write(relField.Name);%>
                <%}%>
            End If
        <%}}%>        
        End Sub

        Public Overloads Function Equals(other As <%Response.Write(currentEntity.GenerateAs);%>) As Boolean Implements IEquatable(Of <%Response.Write(currentEntity.GenerateAs);%>).Equals
            Return UniqueIdentifierHelper.IsSameObject(CType(Me, IUniqueIdentifiable), CType(other, IUniqueIdentifiable))
        End Function

    End Class

    Public Interface IMappeable<%Response.Write(currentEntity.GenerateAs);%>
        Sub CompleteEntity(<%Response.Write(AGGREGATIONS_AND_TYPES_FOR_IMAPPEABLE(currentEntity));%>)
        <%foreach (PropertyNode currentProperty in currentEntity.Children) {
        if (currentProperty.NativeType == "None" && currentProperty.GenerateProperty && currentProperty.GenerateAsLazyLoad) { %>
        Function Is<%Response.Write(currentProperty.GenerateAs);%>Null() As Boolean
        <%}}%>
        Sub SetFKValuesForChilds(entity As <%Response.Write(currentEntity.GenerateAs);%> )
    End Interface

    <CollectionDataContract()> _
    Public Partial Class <%Response.Write(currentEntity.GenerateAs);%>List
        Inherits ObjectList(Of <%Response.Write(currentEntity.GenerateAs);%>)    
    End Class
End Namespace

Namespace Views<%Response.Write(currentEntity.FormattedNamespace);%>
    <CollectionDataContract()> _
    Partial Public Class <%Response.Write(currentEntity.GenerateAs);%>ListView
        Inherits ObjectListView(Of Entities.<%Response.Write(currentEntity.FullGenerateAs);%>)

        Sub New(ByVal list As Entities.<%Response.Write(currentEntity.FullGenerateAs);%>List)
            MyBase.New(list)
        End Sub
    End Class
End NameSpace

<%
Response.SaveBuffer("\\Entities\\Auto\\" + currentEntity.FullGenerateAs + ".Auto.vb");
}
}
%>
