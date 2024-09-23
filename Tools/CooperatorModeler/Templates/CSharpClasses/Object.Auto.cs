<%foreach (BaseTreeNode entityNode in Model.Children) {
    EntityNode currentEntity = (EntityNode)entityNode;
    if (currentEntity.GenerateObject && (currentEntity.PrimaryKeyFields.Count != 0 || currentEntity.GenerateAsReadOnly)) {%>
//------------------------------------------------------------------------------
// This file was generated by Cooperator Modeler, version <%Response.Write(parameters["GeneratorVersion"]);%>
// Created: <%Response.Write(parameters["AutoFilesDateAndTime"]);%>
// This is a partial class file. The other one is <%Response.Write(currentEntity.GenerateAs);%>Object.cs
// You should not modifiy this file, please edit the other partial class file.
//------------------------------------------------------------------------------

using System;
using System.Net;
using System.Runtime.Serialization;
using Cooperator.Framework.Core;
using System.ComponentModel;

namespace <%Response.Write(parameters["RulesProjectName"]);%>.Objects<%Response.Write(currentEntity.FormattedNamespace);%>
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract()]
    public partial class <%Response.Write(currentEntity.GenerateAs);%>Object : BaseObject, IMappeable<%Response.Write(currentEntity.GenerateAs);%>Object, IUniqueIdentifiable, IEquatable<<%Response.Write(currentEntity.GenerateAs);%>Object>, ICloneable
    {

        #region "Ctor"

        /// <summary>
        /// 
        /// </summary>
        public <%Response.Write(currentEntity.GenerateAs);%>Object(): base()
        {
<%Response.Write(SET_AUTO_VALUES_FOR_OBJECTS_CTOR(currentEntity));%>
        }

        <% if (PK_PARAMS_FOR_OBJECTS_CTOR(currentEntity).Length > 0) { %>
        /// <summary>
        /// 
        /// </summary>
        public <%Response.Write(currentEntity.GenerateAs);%>Object(<%Response.Write(PK_PARAMS_FOR_OBJECTS_CTOR(currentEntity));%>): base()
        {
<%Response.Write(PK_SET_VALUES_FOR_OBJECTS_CTOR(currentEntity));%>
            Initialized();
        }
        <% } %>

        <% int noPkCounter = 0;
        foreach (PropertyNode currentProperty in currentEntity.Children)
            if (currentProperty.NativeType != "None" && currentProperty.GenerateProperty && !currentProperty.IsPrimaryKey && !currentProperty.IsForeignKey  && !currentProperty.IsRowVersion)
                noPkCounter++;
        if (noPkCounter > 0) { %>
        /// <summary>
        /// 
        /// </summary>
        public <%Response.Write(currentEntity.GenerateAs);%>Object(<%Response.Write(ALL_FIELDS_PARAMS_FOR_OBJECTS_CTOR(currentEntity));%>): base()
        {
<%Response.Write(ALL_FIELDS_SET_VALUES_FOR_OBJECTS_CTOR(currentEntity));%>
            Initialized();
        }
        <%}%>

        #endregion

        #region "Events"
        
        <%foreach (PropertyNode currentProperty in currentEntity.Children) {
        if (currentProperty.IsDescriptionField && currentProperty.GenerateProperty) {%>
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<DescriptionFieldEventArg> Update_<%Response.Write(currentProperty.GenerateAs);%>;
        <%}}%>
        #endregion

        #region "Fields"

            <%Response.Write(FIELDS_DEFINITION_FOR_OBJECTS(currentEntity));%>
        #endregion

        #region "Properties"
        <%foreach (PropertyNode currentProperty in currentEntity.Children) {
            if (currentProperty.NativeType != "None" && (currentProperty.GenerateProperty || currentProperty.IsPrimaryKey || currentProperty.IsForeignKey)) { 
                string accessModifier = (((currentProperty.IsForeignKey || currentProperty.IsPrimaryKey) && !currentProperty.GenerateProperty) || currentProperty.GenerateAsProtected) ? "protected internal virtual" : "public virtual";
                if (currentProperty.IsNullable && currentProperty.CLRType != "System.String" && currentProperty.CLRType != "System.Object" && currentProperty.CLRType != "System.Byte[]") {%>
        /// <summary>
        /// Nullable property
        /// </summary>
        [DataMember()] 
        <%Response.Write(accessModifier + " System.Nullable<" + currentProperty.CLRType + "> " + currentProperty.GenerateAs);%>
        {
            get
            {
                return _<%Response.Write(currentProperty.GenerateAs);%>;
            }
            <% if (!currentProperty.GenerateAsReadOnly) { %>
            set
            {
                base.PropertyModified();
                _<%Response.Write(currentProperty.GenerateAs);%> = value;                
                <%foreach (PropertyNode currentProperty2 in currentEntity.Children) {
                if (currentProperty2.IsDescriptionField && currentProperty2.GenerateProperty) {
                foreach (RelatedField relField in currentProperty2.RelatedFields){
                if (relField.Name == currentProperty.Name) { %>                
                if (Update_<%Response.Write(currentProperty2.GenerateAs);%> != null)
                {                  
                  <% if (currentProperty.IsNullable && currentProperty.CLRType != "System.String") { %> if (value != null) { <%}%>
                    DescriptionFieldEventArg e = new DescriptionFieldEventArg(new <%Response.Write( DomainTreeHelper.GetFullGenerateAsForEntity(currentProperty2.Parent.Parent, currentProperty2.RelatedTableName) + "Object (" + PARAMETERS_FOR_RELATED_FIELD(currentProperty2.RelatedFields, currentEntity) +"))");%>;
                    Update_<%Response.Write(currentProperty2.GenerateAs);%>(this, e);
                    _<%Response.Write(currentProperty2.GenerateAs);%> = e.DescriptionString;
                  <% if (currentProperty.IsNullable && currentProperty.CLRType != "System.String") { %> } <%}%>
                }                
                <%}}}}%>
            }
            <%}%>
        }
                <% } else { %>
        /// <summary>
        /// 
        /// </summary>
        [DataMember()]
        <%Response.Write(accessModifier + " " + currentProperty.CLRType + " " + currentProperty.GenerateAs);%>
        {
            get
            {
                return _<%Response.Write(currentProperty.GenerateAs);%>;
            }
            <% if (!currentProperty.GenerateAsReadOnly) { %>
            set
            {
                base.PropertyModified();
                _<%Response.Write(currentProperty.GenerateAs);%> = value;
                <%foreach (PropertyNode currentProperty2 in currentEntity.Children) {
                if (currentProperty2.IsDescriptionField && currentProperty2.GenerateProperty) {
                foreach (RelatedField relField in currentProperty2.RelatedFields){
                if (relField.Name == currentProperty.Name) { %>                
                if (Update_<%Response.Write(currentProperty2.GenerateAs);%> != null)
                {
                  <% if (currentProperty.IsNullable  && currentProperty.CLRType != "System.String") { %> if (value != null ) { <%}%>
                    DescriptionFieldEventArg e = new DescriptionFieldEventArg(new <%Response.Write(DomainTreeHelper.GetFullGenerateAsForEntity(currentProperty2.Parent.Parent, currentProperty2.RelatedTableName) + "Object (" + PARAMETERS_FOR_RELATED_FIELD(currentProperty2.RelatedFields, currentEntity) +"))");%>;
                    Update_<%Response.Write(currentProperty2.GenerateAs);%>(this, e);
                    _<%Response.Write(currentProperty2.GenerateAs);%> = e.DescriptionString;
                  <% if (currentProperty.IsNullable  && currentProperty.CLRType != "System.String") { %> } <%}%>
                }                
                <%}}}}%>
            }
            <%}%>
        }
        <%}}}%>
        #endregion

        <%if (currentEntity.ToStringInfo.OverrideToString){%>
        #region "ToString()"
        <%Response.Write(OVERRIDE_TO_STRING(currentEntity));%>
        #endregion
        <%}%>        
        /// <summary>
        /// 
        /// </summary>
        protected override void SetOriginalValue()
        {
            base.InternalOriginalValue = (IObject) this.MemberwiseClone();
        }

        /// <summary>
        /// 
        /// </summary>
        object ICloneable.Clone()
        {
            <%Response.Write(currentEntity.GenerateAs);%>Object newObject;
            <%Response.Write(currentEntity.GenerateAs);%>Object newOriginalValue;

            newObject = (<%Response.Write(currentEntity.GenerateAs);%>Object) this.MemberwiseClone();
            if (base.InternalOriginalValue != null)
            {
                newOriginalValue = (<%Response.Write(currentEntity.GenerateAs);%>Object)this.OriginalValue().MemberwiseClone();
                newObject.InternalOriginalValue = newOriginalValue;
            }
            return newObject;
        }


        /// <summary>
        /// Returns de original value of object since was created or restored.
        /// </summary>
        public new <%Response.Write(currentEntity.GenerateAs);%>Object OriginalValue()
        {
            return (<%Response.Write(currentEntity.GenerateAs);%>Object)base.OriginalValue();
        }


        /// <summary>
        /// 
        /// </summary>
        void IMappeable<%Response.Write(currentEntity.GenerateAs);%>Object.HydrateFields(<%  string separator = "\r\n\t\t\t";
            foreach (PropertyNode currentProperty in currentEntity.Children) {
                if (currentProperty.NativeType != "None" && (currentProperty.GenerateProperty || currentProperty.IsPrimaryKey || currentProperty.IsForeignKey)) {
                    if (currentProperty.IsNullable && currentProperty.CLRType != "System.String" && currentProperty.CLRType != "System.Object" && currentProperty.CLRType != "System.Byte[]") {
                        Response.Write(separator + "System.Nullable<" + currentProperty.CLRType + "> " + currentProperty.GenerateAs);
                    } else {
                        Response.Write(separator + currentProperty.CLRType + " " + currentProperty.GenerateAs);
                    }
                    separator = ",\r\n\t\t\t";
            }
        }
        %>)
        {
        <%  separator = "\t";
            foreach (PropertyNode currentProperty in currentEntity.Children) {
                if (currentProperty.NativeType != "None" && (currentProperty.GenerateProperty || currentProperty.IsPrimaryKey || currentProperty.IsForeignKey)) {
                    Response.Write(separator + "_" + currentProperty.GenerateAs + " = " + currentProperty.GenerateAs + ";");
                    separator = "\r\n\t\t\t";
                }
            }
        %>
        }

        /// <summary>
        /// 
        /// </summary>
        object[] IMappeable<%Response.Write(currentEntity.GenerateAs);%>Object.GetFieldsForInsert()
        {
        <% separator = ""; int counter = 0; %>
        <% if (currentEntity.SelectedNativePropertiesCount == 0) { %>
            throw new NotSupportedException();
        <% } else { %>
            object[] _myArray = new object[<%Response.Write(currentEntity.SelectedNativePropertiesCount.ToString());%>];
            <%
            foreach (PropertyNode currentProperty in currentEntity.Children) {
            if ((currentProperty.NativeType != "None" && !currentProperty.GenerateAsReadOnly && currentProperty.GenerateProperty) || currentProperty.IsPrimaryKey || currentProperty.IsIdentity || currentProperty.IsForeignKey || currentProperty.IsRowVersion) {
                if (currentProperty.IsNullable && currentProperty.CLRType != "System.Object" && currentProperty.CLRType != "System.Byte[]") {
                    if (currentProperty.CLRType == "System.String")
                        Response.Write("if (!System.String.IsNullOrEmpty(_{1})) _myArray[{0}] = _{1};\r\n\t\t\t", counter, currentProperty.GenerateAs);
                    else
                        Response.Write("if (_{1}.HasValue) _myArray[{0}] = _{1}.Value;\r\n\t\t\t", counter, currentProperty.GenerateAs);
                } else {
                    Response.Write(String.Format("_myArray[{0}] = _{1};\r\n\t\t\t", counter, currentProperty.GenerateAs));
                }
                counter++;
            }}
            %>
            return _myArray;
        <% } // end else %>
        }

        /// <summary>
        /// 
        /// </summary>
        object[] IMappeable<%Response.Write(currentEntity.GenerateAs);%>Object.GetFieldsForUpdate()
        {
        <% int pkFieldCount = 0; %>
        <% if ((currentEntity.SelectedNativePropertiesCount+pkFieldCount) == 0) { %>
            throw new NotSupportedException();
        <% } else { %>
            <%
            foreach (PropertyNode currentProperty in currentEntity.Children)
                if (currentProperty.IsPrimaryKey && !currentProperty.IsAutoGenerated) pkFieldCount++;
            %>
            object[] _myArray = new object[<%Response.Write((currentEntity.SelectedNativePropertiesCount+pkFieldCount).ToString());%>];
            <%separator = ""; counter = 0;
            foreach (PropertyNode currentProperty in currentEntity.Children) {
            if ((currentProperty.NativeType != "None" && !currentProperty.GenerateAsReadOnly && currentProperty.GenerateProperty) || currentProperty.IsPrimaryKey || currentProperty.IsIdentity || currentProperty.IsForeignKey || currentProperty.IsRowVersion) {
                if (currentProperty.IsNullable && currentProperty.CLRType != "System.Object" && currentProperty.CLRType != "System.Byte[]") {
                    if (currentProperty.CLRType == "System.String")
                        Response.Write("if (!System.String.IsNullOrEmpty(_{1})) _myArray[{0}] = _{1};\r\n\t\t\t", counter, currentProperty.GenerateAs);
                    else
                        Response.Write("if (_{1}.HasValue) _myArray[{0}] = _{1}.Value;\r\n\t\t\t", counter, currentProperty.GenerateAs);
                } else {
                    Response.Write(String.Format("_myArray[{0}] = _{1};\r\n\t\t\t", counter, currentProperty.GenerateAs));
                }
                counter++;
            }}
            foreach (PropertyNode currentProperty in currentEntity.Children) {
            if (currentProperty.NativeType != "None" && currentProperty.IsPrimaryKey  && !currentProperty.IsAutoGenerated ) {
                if (currentProperty.IsNullable && currentProperty.CLRType != "System.Object" && currentProperty.CLRType != "System.Byte[]") {
                    if (currentProperty.CLRType == "System.String")
                        Response.Write("if (!System.String.IsNullOrEmpty(_{1})) _myArray[{0}] = this.OriginalValue()._{1};\r\n\t\t\t", counter, currentProperty.GenerateAs);
                    else
                        Response.Write("if (_{1}.HasValue) _myArray[{0}] = this.OriginalValue()._{1}.Value;\r\n\t\t\t", counter, currentProperty.GenerateAs);
                } else {
                    Response.Write(String.Format("_myArray[{0}] = this.OriginalValue()._{1};\r\n\t\t\t", counter, currentProperty.GenerateAs));
                }
                counter++;
            }}
            %>
            return _myArray;
        <% } // end else %>
        }

        /// <summary>
        /// 
        /// </summary>
        object[] IMappeable<%Response.Write(currentEntity.GenerateAs);%>Object.GetFieldsForDelete()
        {
        <% int length = currentEntity.PrimaryKeyFields.Count;
           if (currentEntity.GenerateAsVersionable) length++;
           if (length == 0) { %>
            throw new NotSupportedException();
        <% } else { %>
            object[] _myArray = new object[<%Response.Write(length.ToString());%>];
            <%separator = ""; counter = 0;
            foreach (PropertyNode currentProperty in currentEntity.Children) {
            if (currentProperty.IsPrimaryKey || currentProperty.IsRowVersion) {
                if (currentProperty.IsNullable && currentProperty.CLRType != "System.Object" && currentProperty.CLRType != "System.Byte[]") {
                    if (currentProperty.CLRType == "System.String")
                        Response.Write("if (!System.String.IsNullOrEmpty(_{1})) _myArray[{0}] = _{1};\r\n\t\t\t", counter, currentProperty.GenerateAs);
                    else
                        Response.Write("if (_{1}.HasValue) _myArray[{0}] = _{1}.Value;\r\n\t\t\t", counter, currentProperty.GenerateAs);
                } else {
                    Response.Write(String.Format("_myArray[{0}] = _{1};\r\n\t\t\t", counter, currentProperty.GenerateAs));
                }
                counter++;
            }}
            %>
            return _myArray;
        <% } // end else %>
        }


        /// <summary>
        /// 
        /// </summary>
        void IMappeable<%Response.Write(currentEntity.GenerateAs);%>Object.UpdateObjectFromOutputParams(object[] parameters){
            // Update properties from Output parameters
            <%Response.Write(UPDATE_OBJECT_FROM_OUT_PARAMS(currentEntity));%>
        }


        /// <summary>
        /// 
        /// </summary>
        object[] IUniqueIdentifiable.Identifier()
        {
            <%Response.Write(currentEntity.GenerateAs);%>Object o = null;
            if (ObjectStateHelper.IsModified(this))
                o = this.OriginalValue();
            else
                o = this;

            return new object[]
            {<%separator = "";
            if (currentEntity.PrimaryKeyFields.Count > 0) { // Si tiene PK
                foreach (string pkField in currentEntity.PrimaryKeyFields) {
                    string pkFieldGenerateAs ="";
                    foreach (PropertyNode currentProperty in currentEntity.Children)
                        if (currentProperty.Name == pkField) pkFieldGenerateAs = currentProperty.GenerateAs;
                    Response.Write(separator + "o." + pkFieldGenerateAs);
                    separator = ", ";
                }
            } else { // Si no tiene PK, se consideran  todos los campos como PK
                foreach (PropertyNode currentProperty in currentEntity.Children) {
                    Response.Write(separator + "o." + currentProperty.GenerateAs);
                    separator = ", ";
                }
            }
            %>};
        }


        /// <summary>
        /// 
        /// </summary>
        public bool Equals(<%Response.Write(currentEntity.GenerateAs);%>Object other)
        {
            return UniqueIdentifierHelper.IsSameObject((IUniqueIdentifiable)this, (IUniqueIdentifiable)other);
        }

    }

    /// <summary>
    /// 
    /// </summary>
    public interface IMappeable<%Response.Write(currentEntity.GenerateAs);%>Object
    {
        /// <summary>
        /// 
        /// </summary>
        void HydrateFields(<%  separator = "";
            foreach (PropertyNode currentProperty in currentEntity.Children) {
                if (currentProperty.NativeType != "None" && (currentProperty.GenerateProperty || currentProperty.IsPrimaryKey || currentProperty.IsForeignKey)) {
                    if (currentProperty.IsNullable && currentProperty.CLRType != "System.String" && currentProperty.CLRType != "System.Object" && currentProperty.CLRType != "System.Byte[]") {
                        Response.Write(separator + "System.Nullable<" + currentProperty.CLRType + "> " + currentProperty.GenerateAs);
                    } else {
                        Response.Write(separator + currentProperty.CLRType + " " + currentProperty.GenerateAs);
                    }
                    separator = ", \r\n\t\t\t";
            }
        }
        %>);

        /// <summary>
        /// 
        /// </summary>
        object[] GetFieldsForInsert();

        /// <summary>
        /// 
        /// </summary>
        object[] GetFieldsForUpdate();

        /// <summary>
        /// 
        /// </summary>
        object[] GetFieldsForDelete();

        /// <summary>
        /// 
        /// </summary>
        void UpdateObjectFromOutputParams(object[] parameters);
    }

    /// <summary>
    /// 
    /// </summary>
    [CollectionDataContract()]
    public partial class <%Response.Write(currentEntity.GenerateAs);%>ObjectList : ObjectList<<%Response.Write(currentEntity.GenerateAs);%>Object>
    {
    }
}

namespace <%Response.Write(parameters["RulesProjectName"]);%>.Views<%Response.Write(currentEntity.FormattedNamespace);%>
{
    /// <summary>
    /// 
    /// </summary>
    [CollectionDataContract()] 
    public partial class <%Response.Write(currentEntity.GenerateAs);%>ObjectListView
        : ObjectListView<Objects.<%Response.Write(currentEntity.FullGenerateAs);%>Object>
    {
        /// <summary>
        /// 
        /// </summary>
        public <%Response.Write(currentEntity.GenerateAs);%>ObjectListView(Objects.<%Response.Write(currentEntity.FullGenerateAs);%>ObjectList list): base(list)
        {
        }

        #region " Find Overloads "
        /// <summary>
        /// Finds the items that match the specified criteria.
        /// </summary>
        /// <param name="property">Property to search.</param>
        /// <param name="key">Value to find in the property.</param>
        /// <param name="stringLike">One of the <see cref="StringLike"/> enum values.</param>
        /// <returns>A list with the items that match the specified criteria.</returns>
        public <%Response.Write(currentEntity.GenerateAs);%>ObjectListView Find(string property, object key, StringLike stringLike )
        {
            return Find(property, key, true, stringLike);
        }

        /// <summary>
        /// Finds the items that match the specified criteria.
        /// </summary>
        /// <param name="property">Property to search.</param>
        /// <param name="key">Value to find in the property.</param>
        /// <param name="ignoreCase">Perform the search on case-sensitive/insensitive mode with strings.</param>
        /// <param name="stringLike">One of the <see cref="StringLike"/> enum values.</param>
        /// <returns>A list with the items that match the specified criteria.</returns>
        public <%Response.Write(currentEntity.GenerateAs);%>ObjectListView Find(string property, object key, bool ignoreCase, StringLike stringLike )
        {
            PropertyDescriptorCollection oProperties = TypeDescriptor.GetProperties(typeof(Objects.<%Response.Write(currentEntity.FullGenerateAs);%>Object));
            PropertyDescriptor oProp = oProperties.Find(property, true);
            <%Response.Write(currentEntity.GenerateAs);%>ObjectListView tempList = new <%Response.Write(currentEntity.GenerateAs);%>ObjectListView(new Objects.<%Response.Write(currentEntity.FullGenerateAs);%>ObjectList());
            Objects.<%Response.Write(currentEntity.FullGenerateAs);%>Object listItem;
            object tempObject;

            if (oProp != null)
            {
                for (int i = 0; i < Count; i++)
                {
                    listItem = this[i];
                    tempObject = oProp.GetValue(listItem);

                    if ((key == null) && (tempObject == null))      // If the value of the property and the key are null, this is the index.
                    {
                        tempList.Add(listItem);
                    }
                    else if (tempObject is string && key != null)   // If the value of the property is a string, perform a string comparison.
                    {
                        switch (stringLike)
                        {
                            case StringLike.Equal:
                                if (String.Compare(tempObject.ToString(), key.ToString(), ignoreCase) == 0)
                                    tempList.Add(listItem);
                                break;

                            case StringLike.StartsWith:
                                if (tempObject.ToString().StartsWith(key.ToString(), ignoreCase, System.Globalization.CultureInfo.CurrentCulture))
                                    tempList.Add(listItem);
                                break;

                            case StringLike.EndsWith:
                                if (tempObject.ToString().EndsWith(key.ToString(), ignoreCase, System.Globalization.CultureInfo.CurrentCulture))
                                    tempList.Add(listItem);
                                break;

                            case StringLike.Contains:
                                if (tempObject.ToString().Contains(key.ToString()))
                                    tempList.Add(listItem);
                                break;
                        }
                    }
                    else if (tempObject != null && tempObject.Equals(key))  // If the property is of another type, check with Equals.
                    {
                        tempList.Add(listItem);
                    }
                }
            }
            return tempList;
        }

        #endregion  // Find Overloads
    }
}

<%
Response.SaveBuffer("\\Objects\\Auto\\" + currentEntity.FullGenerateAs + "Object.Auto.cs");
}
}
%>
