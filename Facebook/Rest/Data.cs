using System;
using System.Collections.Generic;
using Facebook.Schema;
using Facebook.Session;
using Facebook.Utility;

namespace Facebook.Rest
{
	/// <summary>
	/// Facebook Data API methods.
	/// </summary>
	public class Data : RestBase, Facebook.Rest.IData
	{
		#region Methods

		#region Constructor

		/// <summary>
		/// Public constructor for facebook.Data
		/// </summary>
		/// <param name="session">Needs a connected Facebook Session object for making requests</param>
		public Data(IFacebookSession session)
			: base(session)
		{
		}

		#endregion Constructor

		#region Public Methods

#if !SILVERLIGHT

		#region Synchronous Methods

        /// <summary>
        /// Sets currently authenticated user's preference.
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="pref_id">(0-201) Numeric identifier of this preference.</param>
        /// <param name="value">(max. 128 characters) Value of the preference to set. Set it to "0" or "" to remove this preference.</param>
        /// <remarks>
        /// Each preference is a string of maximum 128 characters and each of them has a numeric identifier ranged from 0 to 200. Therefore, every application can store up to 201 string values for each of its user.  
        /// To "remove" a preference, set it to 0 or empty string. Both "0" and "" are considered as "not present", and getPreference() call will not return them. To tell them from each other, one can use some serialization format. For example, "n:0" for zeros and "s:" for empty strings. 
        /// </remarks>
		public void SetUserPreference(int pref_id, string value)
		{
			SetUserPreference(pref_id, value, false, null, null);
		}

        /// <summary>
        /// Sets currently authenticated user's preferences in batch. Each preference is a string of maximum 128 characters and each of them has a numeric identifier ranged from 0 to 200. Therefore, every application can store up to 201 string values for each of its user. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="values">Id-value pairs of preferences to set. Each id is an integer between 0 and 200 inclusively. Each value is a string with maximum length of 128 characters. Use "0" or "" to remove a preference.</param>
        /// <param name="replace">True to replace all existing preferences of this user; false to merge into existing preferences.</param>
        /// <remarks>To "remove" a preference, set it to 0 or empty string. Both "0" and "" are considered as "not present", and getPreference() call will not return them. To tell them from each other, one can use some serialization format. For example, "n:0" for zeros and "s:" for empty strings. </remarks>
        public void SetUserPreferences(List<string> values, bool replace)
		{
			SetUserPreferences(values, replace, false, null, null);
		}

        /// <summary>
        /// Gets currently authenticated user's preference. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="pref_id">(0-200) Numeric identifier of the preference to get. </param>
        /// <returns>This method returns the value of the specified preference. Empty string if the preference was not set, or it was set to "0" or empty string before.</returns>
        public string GetUserPreference(int pref_id)
		{
			return GetUserPreference(pref_id, false, null, null);
		}

        /// <summary>
        /// Gets currently authenticated user's preferences. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <returns>This method returns a List of id-value pairs of preferences. For preferences that are set to 0 or empty strings, they will NOT show up in the returned map.</returns>
        /// <remarks>For return value:
        /// This is a problem if you identify the preferences by numbers on your machine. 
        /// If a user fills out preference 0, 1, 2, and 4, but neglects to fill out preference 3 (because it may be optional) --> The array returned by this function 
        /// will map preference 4 to 3. This preference will still be identified by 4 because each initial key in the returned array stores another array that would map 4 to the user preference, 
        /// but you may have to iterate through each array until you find this missing preference depending on how many preferences you have and how many are missing.
        /// </remarks>
        public List<preference> GetUserPreferences()
        {
            return GetUserPreferences(false, null, null);
        }

        /// <summary>
        /// An object type is like a "table" in SQL terminology, or a "class" in object-oriented programming concepts. Each object type has a unique human-readable "name" that will be used to identify itself throughout the API. Each object type also has a list of properties that one has to define individually. Each property is like a "column" in an SQL table, or a "data member" in an object class. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="name">Name of this new object type. This name needs to be unique among all object types and associations defined for this application. This name also needs to be a valid identifier, which is no longer than 32 characters, starting with a letter (a-z) and consisting of only small letters (a-z), numbers (0-9) and/or underscores. </param>
        public void CreateObjectType(string name)
		{
			CreateObjectType(name, false, null, null);
		}

        /// <summary>
        /// Remove a previously defined object type. This will also delete ALL objects of this type. This deletion is NOT reversible. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="obj_type">Name of the object type to delete. This will also delete all objects that were created with the type. </param>
        public void DropObjectType(string obj_type)
		{
			DropObjectType(obj_type, false, null, null);
		}

        /// <summary>
        /// Rename a previously defined object type. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="obj_type">Previous name of the object type to rename. </param>
        /// <param name="new_name">New name to use. This name needs to be unique among all object types and associations defined for this application. This name also needs to be a valid identifier, which is no longer than 32 characters, starting with a letter (a-z) and consisting of only small letters (a-z), numbers (0-9) and/or underscores.</param>
        public void RenameObjectType(string obj_type, string new_name)
		{
			RenameObjectType(obj_type, new_name, false, null, null);
		}

        /// <summary>
        /// Add a new object property to an object type.
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="obj_type">Object type to add a new property to. </param>
        /// <param name="prop_name">Name of the new property to add. This name needs to be a valid identifier, which is no longer than 32 characters, starting with a letter (a-z) and consisting of only small letters (a-z), numbers (0-9) and/or underscores. </param>
        /// <param name="prop_type">Type of the new property: 1 for integer, 2 for string (max. 255 characters), 3 for text blob (max. 64kb)</param>
        public void DefineObjectProperty(string obj_type, string prop_name, int prop_type)
		{
			DefineObjectProperty(obj_type, prop_name, prop_type, false, null, null);
		}

        /// <summary>
        /// Remove a previously defined property of an object type. This will remove ALL values of this property of ALL objects of this type. This removal is NOT reversible. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="obj_type">Object type from which a property is removed. </param>
        /// <param name="prop_name">Name of the property to remove. </param>
        public void UndefineObjectProperty(string obj_type, string prop_name)
		{
			UndefineObjectProperty(obj_type, prop_name, false, null, null);
		}

        /// <summary>
        /// Rename a previously defined object property. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="obj_type">Object type of the property to rename. </param>
        /// <param name="prop_name">Name of the property to change. </param>
        /// <param name="new_name">	New name to use. This name needs to be a valid identifier, which is no longer than 32 characters, starting with a letter (a-z) and consisting of only small letters (a-z), numbers (0-9) and/or underscores. </param>
        public void RenameObjectProperty(string obj_type, string prop_name, string new_name)
		{
			RenameObjectProperty(obj_type, prop_name, new_name, false, null, null);
		}

        /// <summary>
        /// Get a list of all previously defined object types. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <returns>list of object type definitions, each of which has; name: name of object type; object_class: (reserved)</returns>
        public IList<object_type_info> GetObjectTypes()
		{
			return GetObjectTypes(false, null, null);
		}

        /// <summary>
        /// Get detailed definitions of an object type, including all its properties and their types. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="obj_type">Object type to get definition about. </param>
        /// <returns>list of object property definitions, each of which has; name: name of property; data_type: type of property. 1 for integer, 2 for string (max. 255 characters), 3 for text blob (max. 64kb); index_type: (reserved)</returns>
        public IList<object_property_info> GetObjectType(string obj_type)
		{
			return GetObjectType(obj_type, false, null, null);
		}

        /// <summary>
        /// Create a new object.
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="obj_type">Specifies which type of new object to create. </param>
        /// <param name="properties">Optional - Name-value pairs of properties this new object has. The parameters must be JSON encoded with double quoted property and value, i.e. {"name":"value"} </param>
        /// <returns>Numeric identifier (fbid) of newly created object. </returns>
        public long CreateObject(string obj_type, Dictionary<string, string> properties)
		{
			return CreateObject(obj_type, properties, false, null, null);
		}

        /// <summary>
        /// Update an object's properties. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="obj_id">Numeric identifier (fbid) of the object to modify. </param>
        /// <param name="properties">Name-value pairs of new properties. </param>
        /// <param name="replace">True if replace all existing properties; false to merge into existing ones.</param>
        public void UpdateObject(long obj_id, Dictionary<string, string> properties, bool replace)
		{
			UpdateObject(obj_id, properties, replace, false, null, null);
		}

        /// <summary>
        /// Delete an object permanently. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="obj_id">Numeric identifier (fbid) of the object to delete. </param>
        public void DeleteObject(long obj_id)
		{
			DeleteObject(obj_id, false, null, null);
		}

        /// <summary>
        /// Delete multiple objects permanently. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="obj_ids">A list of 64-bit integers that are numeric identifiers (fbids) of objects to delete.</param>
        public void DeleteObjects(List<long> obj_ids)
		{
			DeleteObjects(obj_ids, false, null, null);
		}

        /// <summary>
        /// Get an object's properties.
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="obj_id">Numeric identifier (fbid) of the object to query.</param>
        /// <param name="prop_names">Optional - A list of property names (strings) to selectively query a subset of object properties. If not specified, all properties will be returned.</param>
        /// <returns>An array of the values only (not the names) of specified properties of the object. </returns>
        /// <remarks>The second (index 1) is the object id (fbid); after that they will be properties you added yourself.</remarks>
        public data_getObject_response GetObject(long obj_id, List<string> prop_names)
		{
			return GetObject(obj_id, prop_names, false, null, null);
		}

        /// <summary>
        /// Get properties of multiple objects. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="obj_ids">A list of 64-bit numeric identifiers (fbids) of objects to query. For example: [fbid1, fbid2] </param>
        /// <param name="prop_names">Optional - A list of property names (strings) to selectively query a subset of object properties. If not specified, all properties will be returned. </param>
        /// <returns>list of name-value pairs. </returns>
        public IList<container> GetObjects(List<long> obj_ids, List<string> prop_names)
		{
			return GetObjects(obj_ids, prop_names, false, null, null);
		}

        /// <summary>
        /// Get properties of multiple objects. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="obj_id">A list of 64-bit numeric identifiers (fbids) of objects to query. For example: [fbid1, fbid2] </param>
        /// <param name="prop_name">Optional - A list of property names (strings) to selectively query a subset of object properties. If not specified, all properties will be returned. </param>
        /// <returns>list of name-value pairs. </returns>
        public string GetObjectProperty(long obj_id, string prop_name)
		{
			return GetObjectProperty(obj_id, prop_name, false, null, null);
		}

        /// <summary>
        /// Set a single property of an object. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="obj_id">Object's numeric identifier (fbid).</param>
        /// <param name="prop_name">Property's name.</param>
        /// <param name="prop_value">Property's value.</param>
        public void SetObjectProperty(long obj_id, string prop_name, string prop_value)
		{
			SetObjectProperty(obj_id, prop_name, prop_value, false, null, null);
		}

        /// <summary>
        /// Get a property value by a hash key. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="obj_type">Object's type. This is required, so that different object types may use the same hash keys for different objects. </param>
        /// <param name="key">Hash key (string object identifier) for locating the object. This is created by a call to Data.setHashValue. </param>
        /// <param name="prop_name">Name of the property to query. </param>
        /// <returns>string: property's value. Empty string will be returned (without any error), if object with the specified hash key was not found or created. </returns>
        public string GetHashValue(string obj_type, string key, string prop_name)
		{
			return GetHashValue(obj_type, key, prop_name, false, null, null);
		}

        /// <summary>
        /// Set a property value by a hash key. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="obj_type">Object's type. This is required so that different object types can use the same hash keys for different objects. </param>
        /// <param name="key">Hash key. This is a unique string chosen by the user that can be used to refer to the object in subsequent function calls. </param>
        /// <param name="value">Property's value to set. If the hash key exists, this will overwrite any previous value. </param>
        /// <param name="prop_name">Name of the property to set. </param>
        /// <returns>Numeric identifier (fbid) of the object. </returns>
        public long SetHashValue(string obj_type, string key, string value, string prop_name)
		{
			return SetHashValue(obj_type, key, value, prop_name, false, null, null);
		}

        /// <summary>
        /// Atomically increases a numeric property by a hash key. This is different than "setHashValue(getHashValue() + increment)", which has two API functions calls that are not atomically done (subject to race conditions with values overwritten by interleaved API calls). 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="obj_type">Object's type. This is required, so that different object types may use the same hash keys for different objects. </param>
        /// <param name="key">Hash key (string object identifier) for locating the object. </param>
        /// <param name="prop_name">Name of the property to set. </param>
        /// <param name="increment">Optional - Default is 1. Increments to add to current value, which is 0 if object was not found or created. Use negative number for decrements. </param>
        /// <returns>Property's value after incremented. </returns>
        public long IncHashValue(string obj_type, string key, string prop_name, int increment)
		{
			return IncHashValue(obj_type, key, prop_name, increment, false, null, null);
		}

        /// <summary>
        /// Delete an object by a hash key. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="obj_type">Object's type. This is required, so that different object types may use the same hash keys for different objects. </param>
        /// <param name="key">Hash key (string object identifier) to remove. </param>
        public void RemoveHashKey(string obj_type, string key)
		{
			RemoveHashKey(obj_type, key, false, null, null);
		}

        /// <summary>
        /// Delete multiple objects by a list of hash keys. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="obj_type">Object's type. This is required, so that different object types may use the same hash keys for different objects. </param>
        /// <param name="keys">A list of hash keys (string object identifier) to remove.</param>
        public void RemoveHashKeys(string obj_type, List<string> keys)
		{
			RemoveHashKeys(obj_type, keys, false, null, null);
		}

        /// <summary>
        /// An object association is a directional relationship between two object identifiers. For example, Application Installation: user id => installed application ids; Marriage: husband => wife; Friendship: user id => friend user id; Gift: giver => receiver 
        /// Each association has at least 3 names that are required to describe it: name of the association itself: "installation", "marriage", "friendship", "gift". alias1, name of the first object identifier: "user id", "husband", "giver". alias2, name of the second object identifier: "application id", "wife", "friend user id", "receiver". 
        /// For some associations, we also need reverse direction for a lookup by the second object identifier. For examples, in "marriage" case, not only may we need to look up wife ids by husband ids, but we may also need to look up husband ids by wife ids. We call this a two-way association. Since "husband to wife" is not the same as "wife to husband", we call this a two-way asymmetric association.
        /// In some other two-way associations, backward association has the same meaning of forward association. For example, in "friendship", if "A is B's friend" then "B is A's friend" as well. We call these types of two-way associations symmetric. For a symmetric association, when "A to B" is added, we also add "B to A", so that a reverse lookup can find out exactly the same information. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="name">Name of forward association to create. This name needs to be unique among all object types and associations defined for this application. This name also needs to be a valid identifier, which is no longer than 32 characters, starting with a letter (a-z) and consisting of only small letters (a-z), numbers (0-9) and/or underscores. </param>
        /// <param name="assoc_type">Type of this association:
        /// 1: one-way association, where reverse lookup is not needed;
        /// 2: two-way symmetric association, where a backward association (B to A) is always created when a forward association (A to B) is created.
        /// 3: two-way asymmetric association, where a backward association (B to A) has different meaning than a forward association (A to B). </param>
        /// <param name="assoc_info1">Describes object identifier 1 in an association. This is a data structure that has:
        /// alias: name of object identifier 1. This alias needs to be a valid identifier, which is no longer than 32 characters, starting with a letter (a-z) and consisting of only small letters (a-z), numbers (0-9) and/or underscores.
        /// object_type: Optional - object type of object identifier 1.
        /// unique: Optional - Default to false. Whether each unique object identifier 1 can only appear once in all associations of this type. </param>
        /// <param name="assoc_info2">Describes object identifier 2 in an association. This is a data structure that has:
        /// alias: name of object identifier 2. This alias needs to be a valid identifier, which is no longer than 32 characters, starting with a letter (a-z) and consisting of only small letters (a-z), numbers (0-9) and/or underscores.
        /// object_type: Optional - object type of object identifier 2.
        /// unique: Optional - Default to false. Whether each unique object identifier 2 can only appear once in all associations of this type. </param>
        /// <param name="inverse">Optional - name of backward association, if it is two-way asymmetric. This name needs to be unique among all object types and associations defined for this application. This name also needs to be a valid identifier, which is no longer than 32 characters, starting with a letter (a-z) and consisting of only small letters (a-z), numbers (0-9) and/or underscores. </param>
        public void DefineAssociation(string name, int assoc_type, assoc_object_type assoc_info1, assoc_object_type assoc_info2, bool? inverse)
		{
			DefineAssociation(name, assoc_type, assoc_info1, assoc_info2, inverse, false, null, null);
		}

        /// <summary>
        /// Remove a previously defined association. This will also delete this type of associations established between objects. This deletion is not reversible. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="name">Name of the association to remove.</param>
        public void UndefineAssociation(string name)
		{
			UndefineAssociation(name, false, null, null);
		}

        /// <summary>
        /// Rename a previously defined association. Any renaming here only affects one direction. To change names and aliases for another direction, rename with the name of that direction of association. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="name">Name of the association to change. </param>
        /// <param name="new_name">Optional - New name to use. This name needs to be unique among all object types and associations defined for this application. This name also needs to be a valid identifier, which is no longer than 32 characters, starting with a letter (a-z) and consisting of only small letters (a-z), numbers (0-9) and/or underscores. </param>
        /// <param name="new_alias1">Optional - New alias for object identifier 1 to use. This alias needs to be a valid identifier, which is no longer than 32 characters, starting with a letter (a-z) and consisting of only small letters (a-z), numbers (0-9) and/or underscores. </param>
        /// <param name="new_alias2">Optional - New alias for object identifier 2 to use. This alias needs to be a valid identifier, which is no longer than 32 characters, starting with a letter (a-z) and consisting of only small letters (a-z), numbers (0-9) and/or underscores. </param>
        public void RenameAssociation(string name, string new_name, string new_alias1, string new_alias2)
		{
			RenameAssociation(name, new_name, new_alias1, new_alias2, false, null, null);
		}

        /// <summary>
        /// Get detailed definition of an association. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="name">	Name of the association. </param>
        /// <returns>An object association information data structure that has:
        /// name: name of the association;
        /// * assoc_type: an integer indicating association's type:
        /// o 1: one-way association, where reverse lookup is not needed;
        /// o 2: two-way symmetric association, where a backward association (B to A) is always created when a forward association (A to B) is created.
        /// o 3: two-way asymmetric association, where a backward association (B to A) has different meaning than a forward association (A to B). 
        /// * assoc_info1: object identifier 1's information:
        /// o alias: name of object identifier 1.
        /// o object_type: Optional - object type of object identifier 1.
        /// o unique: Whether each unique object identifier 1 can only appear once in all associations of this type. 
        /// * assoc_info2: object identifier 2's information:
        /// o alias: name of object identifier 2.
        /// o object_type: Optional - object type of object identifier 2.
        /// o unique: Whether each unique object identifier 1 can only appear once in all associations of this type.
        /// </returns>
		public data_getAssociationDefinition_response GetAssociationDefinition(string name)
		{
			return GetAssociationDefinition(name, false, null, null);
		}

        /// <summary>
        /// Get detailed definitions of all previously defined associations. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <returns>A list of object association information data structures, each of which has: 
        /// name: name of the association;
        /// * assoc_type: an integer indicating association's type:
        /// o 1: one-way association, where reverse lookup is not needed;
        /// o 2: two-way symmetric association, where a backward association (B to A) is always created when a forward association (A to B) is created.
        /// o 3: two-way asymmetric association, where a backward association (B to A) has different meaning than a forward association (A to B). 
        /// * assoc_info1: object identifier 1's information:
        /// o alias: name of object identifier 1.
        /// o object_type: Optional - object type of object identifier 1.
        /// o unique: Whether each unique object identifier 1 can only appear once in all associations of this type. 
        /// * assoc_info2: object identifier 2's information:
        /// o alias: name of object identifier 2.
        /// o object_type: Optional - object type of object identifier 2.
        /// o unique: Whether each unique object identifier 1 can only appear once in all associations of this type.
        /// </returns>
		public data_getAssociationDefinitions_response GetAssociationDefinitions()
		{
			return GetAssociationDefinitions(false, null, null);
		}

        /// <summary>
        /// Creates an association between two object identifiers. The order of these two identifiers matters, unless this is a symmetric two-way association. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="name">Name of the association to set. </param>
        /// <param name="obj_id1">Object identifier 1. </param>
        /// <param name="obj_id2">Object identifier 2. </param>
        /// <param name="data">Optional - An arbitrary data (max. 255 characters) to store with this association. </param>
        /// <param name="assoc_time">Optional - Default to association creation time. A timestamp to store with this association. This timestamp is represented as number of seconds since the Unix Epoch (January 1 1970 00:00:00 GMT). </param>
        public void SetAssociation(string name, long obj_id1, long obj_id2, string data, DateTime assoc_time)
		{
			SetAssociation(name, obj_id1, obj_id2, data, assoc_time, false, null, null);
		}

        /// <summary>
        /// Creates a list of associations. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="assocs">A list of associations to set. Each of them has:
        /// * name: string, optional - name of the association to set;
        /// * id1: int64, object identifier 1;
        /// * id2: int64, object identifier 2;
        /// * data: string, optional - an arbitrary data (max. 255 characters) to store with this association.
        /// * time: integer, optional - default to association creation time. A timestamp to store with this association. This timestamp is represented as number of seconds since the Unix Epoch (January 1 1970 00:00:00 GMT). </param>
        /// <param name="name">Optional - default association name if association name is not specified in the list. </param>
        public void SetAssociations(IList<DataAssociation> assocs, string name)
		{
			SetAssociations(assocs, name, false, null, null);
		}

        /// <summary>
        /// Removes an association between two object identifiers. The order of these two identifiers matters, unless this is a symmetric two-way association. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="name">Name of the association. </param>
        /// <param name="obj_id1">Object identifier 1. </param>
        /// <param name="obj_id2">Object identifier 2. </param>
        public void RemoveAssociation(string name, long obj_id1, long obj_id2)
		{
			RemoveAssociation(name, obj_id1, obj_id2, false, null, null);
		}

        /// <summary>
        /// Removes a list of associations. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="assocs">A list of associations to remove. Each of them has:
        /// * name: string, optional - name of the association;
        /// * id1: int64, object identifier 1;
        /// * id2: int64, object identifier 2; </param>
        /// <param name="name">Optional - default association name if association name is not specified in the list. </param>
        public void RemoveAssociations(List<DataAssociation> assocs, string name)
		{
			RemoveAssociations(assocs, name, false, null, null);
		}

        /// <summary>
        /// The name of this function may be misleading, but it actually removes associations between any other objects and a specified object. Those other associated objects will NOT be removed or deleted. Only the associations will be broken and deleted. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="name">Name of the association. </param>
        /// <param name="obj_id">Object identifier. </param>
        public void RemoveAssociatedObjects(string name, long obj_id)
		{
			RemoveAssociatedObjects(name, obj_id, false, null, null);
		}

        /// <summary>
        /// Returns a list of object ids that are associated with specified object. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="name">Name of the association. </param>
        /// <param name="obj_id">Object identifier. </param>
        /// <param name="no_data">True if only return object identifiers; false to return data and time as well. </param>
        /// <returns>A list of objects associated with the given id, each of which has:
        /// * id2: object identifier 2;
        /// * data: arbitrary data stored with this association; and
        /// * time: association creation time or a timestamp stored with this association. </returns>
		public data_getAssociatedObjects_response GetAssociatedObjects(string name, long obj_id, bool no_data)
		{
			return GetAssociatedObjects(name, obj_id, no_data, false, null, null);
		}

        /// <summary>
        /// Returns count of object ids that are associated with specified object. This function takes constant time to return the count, regardless how many objects are associated. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="name">Name of the association. </param>
        /// <param name="obj_id">Object identifier. </param>
        /// <returns>integer: count of associated objects of the specified object. Seems to be returning empty string for zero. </returns>
        public int GetAssociatedObjectCount(string name, long obj_id)
		{
			return GetAssociatedObjectCount(name, obj_id, false, null, null);
		}

        /// <summary>
        /// Returns individual counts of object ids that are associated with a list of specified objects. This function takes constant time to return the counts, regardless how many objects are associated with each queried object. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="name">	Name of the association. </param>
        /// <param name="obj_ids">A list of 64-bit numeric object identifiers. </param>
        /// <returns>A list of integers, each of which is count of associated objects of one object in the specified list. The order of counts match exactly with input list. </returns>
        public IList<int> GetAssociatedObjectCounts(string name, List<long> obj_ids)
		{
			return GetAssociatedObjectCounts(name, obj_ids, false, null, null);
		}

        /// <summary>
        /// Get all associations between two object identifiers. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="obj_id1">Object identifier 1. </param>
        /// <param name="obj_id2">Object identifier 2. </param>
        /// <param name="no_data">True if only return object identifiers; false to return data and time as well. </param>
        /// <returns>A list of associations, each of which has:
        /// * name: name of the association to set;
        /// * id1: object identifier 1;
        /// * id2: object identifier 2;
        /// * data: arbitrary information stored with this association; and
        /// * time: association creation time or a timestamp that was stored with this association. </returns>
		public data_getAssociations_response GetAssociations(long obj_id1, long obj_id2, bool no_data)
		{
			return GetAssociations(obj_id1, obj_id2, no_data, false, null, null);
		}

        /// <summary>
        /// Get Cookies for the specified user. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <returns>A list of cookies</returns>
        public IList<cookie> GetCookies()
		{
			return GetCookies(Session.UserId);
		}

        /// <summary>
        /// Get Cookies for the specified user. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="uid">User id </param>
        /// <returns>A list of cookies</returns>
        public IList<cookie> GetCookies(long uid)
		{
			return GetCookies(uid, null);
		}

        /// <summary>
        /// Get Cookies for the specified user. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="uid">User id </param>
        /// <param name="cookieName">Optional- Cookie name </param>
        /// <returns>A list of cookies</returns>
        public IList<cookie> GetCookies(long uid, string cookieName)
		{
			return GetCookies(uid, cookieName, false, null, null);
		}

        /// <summary>
        /// Set Cookies for the specified user. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="uid">User id </param>
        /// <param name="cookieName">Cookie name </param>
        /// <param name="value">cookie value </param>
        /// <param name="expires">Time stamp when the cookie should expire. If not specified, the cookie expires after 24 hours. (The time stamp can be longer than 24 hours and currently has no limit)</param>
        /// <param name="path">Path relative to the application's callback URL, with which the cookie should be associated. (default value is /) </param>
        /// <returns>This method returns true indicating success or false indicating failure.</returns>
        public bool SetCookie(long uid, string cookieName, string value, DateTime? expires, string path)
		{
			return SetCookie(uid, cookieName, value, expires, path, false, null, null);
		}
		
		#endregion

#endif

		#region Asynchronous Methods
        
        /// <summary>
        /// Sets currently authenticated user's preference.
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="pref_id">(0-201) Numeric identifier of this preference.</param>
        /// <param name="value">(max. 128 characters) Value of the preference to set. Set it to "0" or "" to remove this preference.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <remarks>
        /// Each preference is a string of maximum 128 characters and each of them has a numeric identifier ranged from 0 to 200. Therefore, every application can store up to 201 string values for each of its user.  
        /// To "remove" a preference, set it to 0 or empty string. Both "0" and "" are considered as "not present", and getPreference() call will not return them. To tell them from each other, one can use some serialization format. For example, "n:0" for zeros and "s:" for empty strings. 
        /// </remarks>
		public void SetUserPreferenceAsync(int pref_id, string value, SetUserPreferenceCallback callback, Object state)
		{
			SetUserPreference(pref_id, value, true, callback, state);
		}

        /// <summary>
        /// Sets currently authenticated user's preferences in batch. Each preference is a string of maximum 128 characters and each of them has a numeric identifier ranged from 0 to 200. Therefore, every application can store up to 201 string values for each of its user. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="values">Id-value pairs of preferences to set. Each id is an integer between 0 and 200 inclusively. Each value is a string with maximum length of 128 characters. Use "0" or "" to remove a preference.</param>
        /// <param name="replace">True to replace all existing preferences of this user; false to merge into existing preferences.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <remarks>To "remove" a preference, set it to 0 or empty string. Both "0" and "" are considered as "not present", and getPreference() call will not return them. To tell them from each other, one can use some serialization format. For example, "n:0" for zeros and "s:" for empty strings. </remarks>
        public void SetUserPreferencesAsync(List<string> values, bool replace, SetUserPreferencesCallback callback, Object state)
		{
			SetUserPreferences(values, replace, true, callback, state);
		}

        /// <summary>
        /// Gets currently authenticated user's preference. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="pref_id">(0-200) Numeric identifier of the preference to get. </param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns the value of the specified preference. Empty string if the preference was not set, or it was set to "0" or empty string before.</returns>
        public void GetUserPreferenceAsync(int pref_id, GetUserPreferenceCallback callback, Object state)
		{
			GetUserPreference(pref_id, true, callback, state);
		}

        /// <summary>
        /// Gets currently authenticated user's preferences. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns a List of id-value pairs of preferences. For preferences that are set to 0 or empty strings, they will NOT show up in the returned map.</returns>
        /// <remarks>For return value:
        /// This is a problem if you identify the preferences by numbers on your machine. 
        /// If a user fills out preference 0, 1, 2, and 4, but neglects to fill out preference 3 (because it may be optional) --> The array returned by this function 
        /// will map preference 4 to 3. This preference will still be identified by 4 because each initial key in the returned array stores another array that would map 4 to the user preference, 
        /// but you may have to iterate through each array until you find this missing preference depending on how many preferences you have and how many are missing.
        /// </remarks>
        public void GetUserPreferencesAsync(GetUserPreferenceCallback callback, Object state)
        {
            GetUserPreferences(true, callback, state);
        }

        /// <summary>
        /// An object type is like a "table" in SQL terminology, or a "class" in object-oriented programming concepts. Each object type has a unique human-readable "name" that will be used to identify itself throughout the API. Each object type also has a list of properties that one has to define individually. Each property is like a "column" in an SQL table, or a "data member" in an object class. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="name">Name of this new object type. This name needs to be unique among all object types and associations defined for this application. This name also needs to be a valid identifier, which is no longer than 32 characters, starting with a letter (a-z) and consisting of only small letters (a-z), numbers (0-9) and/or underscores. </param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        public void CreateObjectTypeAsync(string name, CreateObjectTypeCallback callback, Object state)
		{
			CreateObjectType(name, true, callback, state);
		}

        /// <summary>
        /// Remove a previously defined object type. This will also delete ALL objects of this type. This deletion is NOT reversible. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="obj_type">Name of the object type to delete. This will also delete all objects that were created with the type. </param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        public void DropObjectTypeAsync(string obj_type, DropObjectTypeCallback callback, Object state)
		{
			DropObjectType(obj_type, true, callback, state);
		}

        /// <summary>
        /// Rename a previously defined object type. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="obj_type">Previous name of the object type to rename. </param>
        /// <param name="new_name">New name to use. This name needs to be unique among all object types and associations defined for this application. This name also needs to be a valid identifier, which is no longer than 32 characters, starting with a letter (a-z) and consisting of only small letters (a-z), numbers (0-9) and/or underscores.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        public void RenameObjectTypeAsync(string obj_type, string new_name, RenameObjectTypeCallback callback, Object state)
		{
			RenameObjectType(obj_type, new_name, true, callback, state);
		}

        /// <summary>
        /// Add a new object property to an object type.
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="obj_type">Object type to add a new property to. </param>
        /// <param name="prop_name">Name of the new property to add. This name needs to be a valid identifier, which is no longer than 32 characters, starting with a letter (a-z) and consisting of only small letters (a-z), numbers (0-9) and/or underscores. </param>
        /// <param name="prop_type">Type of the new property: 1 for integer, 2 for string (max. 255 characters), 3 for text blob (max. 64kb)</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        public void DefineObjectPropertyAsync(string obj_type, string prop_name, int prop_type, DefineObjectPropertyCallback callback, Object state)
		{
			DefineObjectProperty(obj_type, prop_name, prop_type, true, callback, state);
		}

        /// <summary>
        /// Remove a previously defined property of an object type. This will remove ALL values of this property of ALL objects of this type. This removal is NOT reversible. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="obj_type">Object type from which a property is removed. </param>
        /// <param name="prop_name">Name of the property to remove. </param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        public void UndefineObjectPropertyAsync(string obj_type, string prop_name, UndefineObjectPropertyCallback callback, Object state)
		{
			UndefineObjectProperty(obj_type, prop_name, true, callback, state);
		}

        /// <summary>
        /// Rename a previously defined object property. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="obj_type">Object type of the property to rename. </param>
        /// <param name="prop_name">Name of the property to change. </param>
        /// <param name="new_name">	New name to use. This name needs to be a valid identifier, which is no longer than 32 characters, starting with a letter (a-z) and consisting of only small letters (a-z), numbers (0-9) and/or underscores. </param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        public void RenameObjectPropertyAsync(string obj_type, string prop_name, string new_name, RenameObjectPropertyCallback callback, Object state)
		{
			RenameObjectProperty(obj_type, prop_name, new_name, true, callback, state);
		}

        /// <summary>
        /// Get a list of all previously defined object types. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>list of object type definitions, each of which has; name: name of object type; object_class: (reserved)</returns>
        public IList<object_type_info> GetObjectTypesAsync(GetObjectTypesCallback callback, Object state)
		{
			return GetObjectTypes(true, callback, state);
		}

        /// <summary>
        /// Get detailed definitions of an object type, including all its properties and their types. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="obj_type">Object type to get definition about. </param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>list of object property definitions, each of which has; name: name of property; data_type: type of property. 1 for integer, 2 for string (max. 255 characters), 3 for text blob (max. 64kb); index_type: (reserved)</returns>
        public IList<object_property_info> GetObjectTypeAsync(string obj_type, GetObjectTypeCallback callback, Object state)
		{
			return GetObjectType(obj_type, true, callback, state);
		}

        /// <summary>
        /// Create a new object.
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="obj_type">Specifies which type of new object to create. </param>
        /// <param name="properties">Optional - Name-value pairs of properties this new object has. The parameters must be JSON encoded with double quoted property and value, i.e. {"name":"value"} </param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>Numeric identifier (fbid) of newly created object. </returns>
        public long CreateObjectAsync(string obj_type, Dictionary<string, string> properties, CreateObjectCallback callback, Object state)
		{
			return CreateObject(obj_type, properties, true, callback, state);
		}

        /// <summary>
        /// Update an object's properties. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="obj_id">Numeric identifier (fbid) of the object to modify. </param>
        /// <param name="properties">Name-value pairs of new properties. </param>
        /// <param name="replace">True if replace all existing properties; false to merge into existing ones.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        public void UpdateObjectAsync(long obj_id, Dictionary<string, string> properties, bool replace, UpdateObjectCallback callback, Object state)
		{
			UpdateObject(obj_id, properties, replace, true, callback, state);
		}

        /// <summary>
        /// Delete an object permanently. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="obj_id">Numeric identifier (fbid) of the object to delete. </param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        public void DeleteObjectAsync(long obj_id, DeleteObjectCallback callback, Object state)
		{
			DeleteObject(obj_id, true, callback, state);
		}

        /// <summary>
        /// Delete multiple objects permanently. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="obj_ids">A list of 64-bit integers that are numeric identifiers (fbids) of objects to delete.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        public void DeleteObjectsAsync(List<long> obj_ids, DeleteObjectsCallback callback, Object state)
		{
			DeleteObjects(obj_ids, true, callback, state);
		}

        /// <summary>
        /// Get an object's properties.
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="obj_id">Numeric identifier (fbid) of the object to query.</param>
        /// <param name="prop_names">Optional - A list of property names (strings) to selectively query a subset of object properties. If not specified, all properties will be returned.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>An array of the values only (not the names) of specified properties of the object. </returns>
        /// <remarks>The second (index 1) is the object id (fbid); after that they will be properties you added yourself.</remarks>
        public data_getObject_response GetObjectAsync(long obj_id, List<string> prop_names, GetObjectCallback callback, Object state)
		{
			return GetObject(obj_id, prop_names, true, callback, state);
		}

        /// <summary>
        /// Get properties of multiple objects. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="obj_ids">A list of 64-bit numeric identifiers (fbids) of objects to query. For example: [fbid1, fbid2] </param>
        /// <param name="prop_names">Optional - A list of property names (strings) to selectively query a subset of object properties. If not specified, all properties will be returned. </param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>list of name-value pairs. </returns>
        public IList<container> GetObjectsAsync(List<long> obj_ids, List<string> prop_names, GetObjectsCallback callback, Object state)
		{
			return GetObjects(obj_ids, prop_names, true, callback, state);
		}

        /// <summary>
        /// Get properties of multiple objects. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="obj_id">A list of 64-bit numeric identifiers (fbids) of objects to query. For example: [fbid1, fbid2] </param>
        /// <param name="prop_name">Optional - A list of property names (strings) to selectively query a subset of object properties. If not specified, all properties will be returned. </param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>list of name-value pairs. </returns>
        public string GetObjectPropertyAsync(long obj_id, string prop_name, GetObjectPropertyCallback callback, Object state)
		{
			return GetObjectProperty(obj_id, prop_name, true, callback, state);
		}

        /// <summary>
        /// Set a single property of an object. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="obj_id">Object's numeric identifier (fbid).</param>
        /// <param name="prop_name">Property's name.</param>
        /// <param name="prop_value">Property's value.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        public void SetObjectPropertyAsync(long obj_id, string prop_name, string prop_value, SetObjectPropertyCallback callback, Object state)
		{
			SetObjectProperty(obj_id, prop_name, prop_value, true, callback, state);
		}

        /// <summary>
        /// Get a property value by a hash key. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="obj_type">Object's type. This is required, so that different object types may use the same hash keys for different objects. </param>
        /// <param name="key">Hash key (string object identifier) for locating the object. This is created by a call to Data.setHashValue. </param>
        /// <param name="prop_name">Name of the property to query. </param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>string: property's value. Empty string will be returned (without any error), if object with the specified hash key was not found or created. </returns>
        public string GetHashValueAsync(string obj_type, string key, string prop_name, GetHashValueCallback callback, Object state)
		{
			return GetHashValue(obj_type, key, prop_name, true, callback, state);
		}

        /// <summary>
        /// Set a property value by a hash key. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="obj_type">Object's type. This is required so that different object types can use the same hash keys for different objects. </param>
        /// <param name="key">Hash key. This is a unique string chosen by the user that can be used to refer to the object in subsequent function calls. </param>
        /// <param name="value">Property's value to set. If the hash key exists, this will overwrite any previous value. </param>
        /// <param name="prop_name">Name of the property to set. </param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>Numeric identifier (fbid) of the object. </returns>
        public long SetHashValueAsync(string obj_type, string key, string value, string prop_name, SetHashValueCallback callback, Object state)
		{
			return SetHashValue(obj_type, key, value, prop_name, true, callback, state);
		}

        /// <summary>
        /// Atomically increases a numeric property by a hash key. This is different than "setHashValue(getHashValue() + increment)", which has two API functions calls that are not atomically done (subject to race conditions with values overwritten by interleaved API calls). 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="obj_type">Object's type. This is required, so that different object types may use the same hash keys for different objects. </param>
        /// <param name="key">Hash key (string object identifier) for locating the object. </param>
        /// <param name="prop_name">Name of the property to set. </param>
        /// <param name="increment">Optional - Default is 1. Increments to add to current value, which is 0 if object was not found or created. Use negative number for decrements. </param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>Property's value after incremented. </returns>
        public long IncHashValueAsync(string obj_type, string key, string prop_name, int increment, IncHashValueCallback callback, Object state)
		{
			return IncHashValue(obj_type, key, prop_name, increment, true, callback, state);
		}

        /// <summary>
        /// Delete an object by a hash key. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="obj_type">Object's type. This is required, so that different object types may use the same hash keys for different objects. </param>
        /// <param name="key">Hash key (string object identifier) to remove. </param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        public void RemoveHashKeyAsync(string obj_type, string key, RemoveHashKeyCallback callback, Object state)
		{
			RemoveHashKey(obj_type, key, true, callback, state);
		}

        /// <summary>
        /// Delete multiple objects by a list of hash keys. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="obj_type">Object's type. This is required, so that different object types may use the same hash keys for different objects. </param>
        /// <param name="keys">A list of hash keys (string object identifier) to remove.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        public void RemoveHashKeysAsync(string obj_type, List<string> keys, RemoveHashKeysCallback callback, Object state)
		{
			RemoveHashKeys(obj_type, keys, true, callback, state);
		}

        /// <summary>
        /// An object association is a directional relationship between two object identifiers. For example, Application Installation: user id => installed application ids; Marriage: husband => wife; Friendship: user id => friend user id; Gift: giver => receiver 
        /// Each association has at least 3 names that are required to describe it: name of the association itself: "installation", "marriage", "friendship", "gift". alias1, name of the first object identifier: "user id", "husband", "giver". alias2, name of the second object identifier: "application id", "wife", "friend user id", "receiver". 
        /// For some associations, we also need reverse direction for a lookup by the second object identifier. For examples, in "marriage" case, not only may we need to look up wife ids by husband ids, but we may also need to look up husband ids by wife ids. We call this a two-way association. Since "husband to wife" is not the same as "wife to husband", we call this a two-way asymmetric association.
        /// In some other two-way associations, backward association has the same meaning of forward association. For example, in "friendship", if "A is B's friend" then "B is A's friend" as well. We call these types of two-way associations symmetric. For a symmetric association, when "A to B" is added, we also add "B to A", so that a reverse lookup can find out exactly the same information. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="name">Name of forward association to create. This name needs to be unique among all object types and associations defined for this application. This name also needs to be a valid identifier, which is no longer than 32 characters, starting with a letter (a-z) and consisting of only small letters (a-z), numbers (0-9) and/or underscores. </param>
        /// <param name="assoc_type">Type of this association:
        /// 1: one-way association, where reverse lookup is not needed;
        /// 2: two-way symmetric association, where a backward association (B to A) is always created when a forward association (A to B) is created.
        /// 3: two-way asymmetric association, where a backward association (B to A) has different meaning than a forward association (A to B). </param>
        /// <param name="assoc_info1">Describes object identifier 1 in an association. This is a data structure that has:
        /// alias: name of object identifier 1. This alias needs to be a valid identifier, which is no longer than 32 characters, starting with a letter (a-z) and consisting of only small letters (a-z), numbers (0-9) and/or underscores.
        /// object_type: Optional - object type of object identifier 1.
        /// unique: Optional - Default to false. Whether each unique object identifier 1 can only appear once in all associations of this type. </param>
        /// <param name="assoc_info2">Describes object identifier 2 in an association. This is a data structure that has:
        /// alias: name of object identifier 2. This alias needs to be a valid identifier, which is no longer than 32 characters, starting with a letter (a-z) and consisting of only small letters (a-z), numbers (0-9) and/or underscores.
        /// object_type: Optional - object type of object identifier 2.
        /// unique: Optional - Default to false. Whether each unique object identifier 2 can only appear once in all associations of this type. </param>
        /// <param name="inverse">Optional - name of backward association, if it is two-way asymmetric. This name needs to be unique among all object types and associations defined for this application. This name also needs to be a valid identifier, which is no longer than 32 characters, starting with a letter (a-z) and consisting of only small letters (a-z), numbers (0-9) and/or underscores. </param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        public void DefineAssociationAsync(string name, int assoc_type, assoc_object_type assoc_info1, assoc_object_type assoc_info2, bool? inverse, DefineAssociationCallback callback, Object state)
		{
			DefineAssociation(name, assoc_type, assoc_info1, assoc_info2, inverse, true, callback, state);
		}

        /// <summary>
        /// Remove a previously defined association. This will also delete this type of associations established between objects. This deletion is not reversible. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="name">Name of the association to remove.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        public void UndefineAssociationAsync(string name, UndefineAssociationCallback callback, Object state)
		{
			UndefineAssociation(name, true, callback, state);
		}

        /// <summary>
        /// Rename a previously defined association. Any renaming here only affects one direction. To change names and aliases for another direction, rename with the name of that direction of association. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="name">Name of the association to change. </param>
        /// <param name="new_name">Optional - New name to use. This name needs to be unique among all object types and associations defined for this application. This name also needs to be a valid identifier, which is no longer than 32 characters, starting with a letter (a-z) and consisting of only small letters (a-z), numbers (0-9) and/or underscores. </param>
        /// <param name="new_alias1">Optional - New alias for object identifier 1 to use. This alias needs to be a valid identifier, which is no longer than 32 characters, starting with a letter (a-z) and consisting of only small letters (a-z), numbers (0-9) and/or underscores. </param>
        /// <param name="new_alias2">Optional - New alias for object identifier 2 to use. This alias needs to be a valid identifier, which is no longer than 32 characters, starting with a letter (a-z) and consisting of only small letters (a-z), numbers (0-9) and/or underscores. </param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        public void RenameAssociationAsync(string name, string new_name, string new_alias1, string new_alias2, RenameAssociationCallback callback, Object state)
		{
			RenameAssociation(name, new_name, new_alias1, new_alias2, true, callback, state);
		}

        /// <summary>
        /// Get detailed definition of an association. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="name">	Name of the association. </param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>An object association information data structure that has:
        /// name: name of the association;
        /// * assoc_type: an integer indicating association's type:
        /// o 1: one-way association, where reverse lookup is not needed;
        /// o 2: two-way symmetric association, where a backward association (B to A) is always created when a forward association (A to B) is created.
        /// o 3: two-way asymmetric association, where a backward association (B to A) has different meaning than a forward association (A to B). 
        /// * assoc_info1: object identifier 1's information:
        /// o alias: name of object identifier 1.
        /// o object_type: Optional - object type of object identifier 1.
        /// o unique: Whether each unique object identifier 1 can only appear once in all associations of this type. 
        /// * assoc_info2: object identifier 2's information:
        /// o alias: name of object identifier 2.
        /// o object_type: Optional - object type of object identifier 2.
        /// o unique: Whether each unique object identifier 1 can only appear once in all associations of this type.
        /// </returns>
		public data_getAssociationDefinition_response GetAssociationDefinitionAsync(string name, GetAssociationDefinitionCallback callback, Object state)
		{
			return GetAssociationDefinition(name, true, callback, state);
		}

        /// <summary>
        /// Get detailed definitions of all previously defined associations. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>A list of object association information data structures, each of which has: 
        /// name: name of the association;
        /// * assoc_type: an integer indicating association's type:
        /// o 1: one-way association, where reverse lookup is not needed;
        /// o 2: two-way symmetric association, where a backward association (B to A) is always created when a forward association (A to B) is created.
        /// o 3: two-way asymmetric association, where a backward association (B to A) has different meaning than a forward association (A to B). 
        /// * assoc_info1: object identifier 1's information:
        /// o alias: name of object identifier 1.
        /// o object_type: Optional - object type of object identifier 1.
        /// o unique: Whether each unique object identifier 1 can only appear once in all associations of this type. 
        /// * assoc_info2: object identifier 2's information:
        /// o alias: name of object identifier 2.
        /// o object_type: Optional - object type of object identifier 2.
        /// o unique: Whether each unique object identifier 1 can only appear once in all associations of this type.
        /// </returns>
		public data_getAssociationDefinitions_response GetAssociationDefinitionsAsync(GetAssociationDefinitionsCallback callback, Object state)
		{
			return GetAssociationDefinitions(true, callback, state);
		}

        /// <summary>
        /// Creates an association between two object identifiers. The order of these two identifiers matters, unless this is a symmetric two-way association. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="name">Name of the association to set. </param>
        /// <param name="obj_id1">Object identifier 1. </param>
        /// <param name="obj_id2">Object identifier 2. </param>
        /// <param name="data">Optional - An arbitrary data (max. 255 characters) to store with this association. </param>
        /// <param name="assoc_time">Optional - Default to association creation time. A timestamp to store with this association. This timestamp is represented as number of seconds since the Unix Epoch (January 1 1970 00:00:00 GMT). </param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        public void SetAssociationAsync(string name, long obj_id1, long obj_id2, string data, DateTime assoc_time, SetAssociationCallback callback, Object state)
		{
			SetAssociation(name, obj_id1, obj_id2, data, assoc_time, true, callback, state);
		}

        /// <summary>
        /// Creates a list of associations. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="assocs">A list of associations to set. Each of them has:
        /// * name: string, optional - name of the association to set;
        /// * id1: int64, object identifier 1;
        /// * id2: int64, object identifier 2;
        /// * data: string, optional - an arbitrary data (max. 255 characters) to store with this association.
        /// * time: integer, optional - default to association creation time. A timestamp to store with this association. This timestamp is represented as number of seconds since the Unix Epoch (January 1 1970 00:00:00 GMT). </param>
        /// <param name="name">Optional - default association name if association name is not specified in the list. </param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        public void SetAssociationsAsync(IList<DataAssociation> assocs, string name, SetAssociationsCallback callback, Object state)
		{
			SetAssociations(assocs, name, true, callback, state);
		}

        /// <summary>
        /// Removes an association between two object identifiers. The order of these two identifiers matters, unless this is a symmetric two-way association. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="name">Name of the association. </param>
        /// <param name="obj_id1">Object identifier 1. </param>
        /// <param name="obj_id2">Object identifier 2. </param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        public void RemoveAssociationAsync(string name, long obj_id1, long obj_id2, RemoveAssociationCallback callback, Object state)
		{
			RemoveAssociation(name, obj_id1, obj_id2, true, callback, state);
		}

        /// <summary>
        /// Removes a list of associations. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="assocs">A list of associations to remove. Each of them has:
        /// * name: string, optional - name of the association;
        /// * id1: int64, object identifier 1;
        /// * id2: int64, object identifier 2; </param>
        /// <param name="name">Optional - default association name if association name is not specified in the list. </param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        public void RemoveAssociationsAsync(List<DataAssociation> assocs, string name, RemoveAssociationsCallback callback, Object state)
		{
			RemoveAssociations(assocs, name, true, callback, state);
		}

        /// <summary>
        /// The name of this function may be misleading, but it actually removes associations between any other objects and a specified object. Those other associated objects will NOT be removed or deleted. Only the associations will be broken and deleted. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="name">Name of the association. </param>
        /// <param name="obj_id">Object identifier. </param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        public void RemoveAssociatedObjectsAsync(string name, long obj_id, RemoveAssociatedObjectsCallback callback, Object state)
		{
			RemoveAssociatedObjects(name, obj_id, true, callback, state);
		}

        /// <summary>
        /// Returns a list of object ids that are associated with specified object. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="name">Name of the association. </param>
        /// <param name="obj_id">Object identifier. </param>
        /// <param name="no_data">True if only return object identifiers; false to return data and time as well. </param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>A list of objects associated with the given id, each of which has:
        /// * id2: object identifier 2;
        /// * data: arbitrary data stored with this association; and
        /// * time: association creation time or a timestamp stored with this association. </returns>
		public data_getAssociatedObjects_response GetAssociatedObjectsAsync(string name, long obj_id, bool no_data, GetAssociatedObjectsCallback callback, Object state)
		{
			return GetAssociatedObjects(name, obj_id, no_data, true, callback, state);
		}

        /// <summary>
        /// Returns count of object ids that are associated with specified object. This function takes constant time to return the count, regardless how many objects are associated. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="name">Name of the association. </param>
        /// <param name="obj_id">Object identifier. </param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>integer: count of associated objects of the specified object. Seems to be returning empty string for zero. </returns>
        public int GetAssociatedObjectCountAsync(string name, long obj_id, GetAssociatedObjectCountCallback callback, Object state)
		{
			return GetAssociatedObjectCount(name, obj_id, true, callback, state);
		}

        /// <summary>
        /// Returns individual counts of object ids that are associated with a list of specified objects. This function takes constant time to return the counts, regardless how many objects are associated with each queried object. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="name">	Name of the association. </param>
        /// <param name="obj_ids">A list of 64-bit numeric object identifiers. </param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>A list of integers, each of which is count of associated objects of one object in the specified list. The order of counts match exactly with input list. </returns>
        public IList<int> GetAssociatedObjectCountsAsync(string name, List<long> obj_ids, GetAssociatedObjectCountsCallback callback, Object state)
		{
			return GetAssociatedObjectCounts(name, obj_ids, true, callback, state);
		}

        /// <summary>
        /// Get all associations between two object identifiers. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="obj_id1">Object identifier 1. </param>
        /// <param name="obj_id2">Object identifier 2. </param>
        /// <param name="no_data">True if only return object identifiers; false to return data and time as well. </param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>A list of associations, each of which has:
        /// * name: name of the association to set;
        /// * id1: object identifier 1;
        /// * id2: object identifier 2;
        /// * data: arbitrary information stored with this association; and
        /// * time: association creation time or a timestamp that was stored with this association. </returns>
		public data_getAssociations_response GetAssociationsAsync(long obj_id1, long obj_id2, bool no_data, GetAssociationsCallback callback, Object state)
		{
			return GetAssociations(obj_id1, obj_id2, no_data, true, callback, state);
		}

        /// <summary>
        /// Get Cookies for the specified user. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>A list of cookies</returns>
        public IList<cookie> GetCookiesAsync(GetCookiesCallback callback, Object state)
		{
			return GetCookiesAsync(Session.UserId, callback, state);
		}

        /// <summary>
        /// Get Cookies for the specified user. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="uid">User id </param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>A list of cookies</returns>
        public IList<cookie> GetCookiesAsync(long uid, GetCookiesCallback callback, Object state)
		{
			return GetCookiesAsync(uid, null, callback, state);
		}

        /// <summary>
        /// Get Cookies for the specified user. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="uid">User id </param>
        /// <param name="cookieName">Optional- Cookie name </param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>A list of cookies</returns>
        public IList<cookie> GetCookiesAsync(long uid, string cookieName, GetCookiesCallback callback, Object state)
		{
			return GetCookies(uid, cookieName, true, callback, state);
		}

        /// <summary>
        /// Set Cookies for the specified user. 
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="uid">User id </param>
        /// <param name="cookieName">Cookie name </param>
        /// <param name="value">cookie value </param>
        /// <param name="expires">Time stamp when the cookie should expire. If not specified, the cookie expires after 24 hours. (The time stamp can be longer than 24 hours and currently has no limit)</param>
        /// <param name="path">Path relative to the application's callback URL, with which the cookie should be associated. (default value is /) </param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns true indicating success or false indicating failure.</returns>
        public bool SetCookieAsync(long uid, string cookieName, string value, DateTime? expires, string path, SetCookieCallback callback, Object state)
		{
			return SetCookie(uid, cookieName, value, expires, path, true, callback, state);
		}

		#endregion

    	#endregion Public Methods
        
		#region Private Methods

		private void SetUserPreference(int pref_id, string value, bool isAsync, SetUserPreferenceCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.data.setUserPreference" } };
			Utilities.AddRequiredParameter(parameterList, "pref_id", pref_id);
			Utilities.AddRequiredParameter(parameterList, "value", value);

			if (isAsync)
			{
				SendRequestAsync<data_setUserPreference_response, bool>(parameterList, new FacebookCallCompleted<bool>(callback), state);
				return;
			}

			SendRequest(parameterList);
		}

		private void SetUserPreferences(List<string> values, bool replace, bool isAsync, SetUserPreferencesCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.data.setUserPreferences" } };
			Utilities.AddJSONArray(parameterList, "values", values);
			Utilities.AddParameter(parameterList, "replace", replace);

			if (isAsync)
			{
				SendRequestAsync<data_setUserPreferences_response, bool>(parameterList, new FacebookCallCompleted<bool>(callback), state);
				return;
			}

			SendRequest(parameterList);
		}

		private string GetUserPreference(int pref_id, bool isAsync, GetUserPreferenceCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.data.getUserPreference" } };
			Utilities.AddRequiredParameter(parameterList, "pref_id", pref_id);

			if (isAsync)
			{
				SendRequestAsync<data_getUserPreference_response, string>(parameterList, new FacebookCallCompleted<string>(callback), state);
				return null;
			}

			var response = SendRequest<data_getUserPreference_response>(parameterList);
			return response == null ? null : response.TypedValue;
		}

        private List<preference> GetUserPreferences(bool isAsync, GetUserPreferenceCallback callback, Object state)
        {
            var parameterList = new Dictionary<string, string> { { "method", "facebook.data.getUserPreferences" } };
            
            if (isAsync)
            {
                SendRequestAsync<data_getUserPreferences_response, string>(parameterList, new FacebookCallCompleted<string>(callback), state);
                return null;
            }

            var response = SendRequest<data_getUserPreferences_response>(parameterList);
            return response == null ? null : response.preference;
        }

		private void CreateObjectType(string name, bool isAsync, CreateObjectTypeCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.data.createObjectType" } };
			Utilities.AddRequiredParameter(parameterList, "name", name);

			if (isAsync)
			{
				SendRequestAsync<data_createObjectType_response, bool>(parameterList, new FacebookCallCompleted<bool>(callback), state);
				return;
			}

			SendRequest(parameterList);
		}

		private void DropObjectType(string obj_type, bool isAsync, DropObjectTypeCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.data.dropObjectType" } };
			Utilities.AddRequiredParameter(parameterList, "obj_type", obj_type);

			if (isAsync)
			{
				SendRequestAsync<data_dropObjectType_response, bool>(parameterList, new FacebookCallCompleted<bool>(callback), state);
				return;
			}

			SendRequest(parameterList);
		}

		private void RenameObjectType(string obj_type, string new_name, bool isAsync, RenameObjectTypeCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.data.renameObjectType" } };
			Utilities.AddRequiredParameter(parameterList, "obj_type", obj_type);
			Utilities.AddRequiredParameter(parameterList, "new_name", new_name);

			if (isAsync)
			{
				SendRequestAsync<data_renameObjectType_response, bool>(parameterList, new FacebookCallCompleted<bool>(callback), state);
				return;
			}

			SendRequest(parameterList);
		}

		private void DefineObjectProperty(string obj_type, string prop_name, int prop_type, bool isAsync, DefineObjectPropertyCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.data.defineObjectProperty" } };
			Utilities.AddRequiredParameter(parameterList, "obj_type", obj_type);
			Utilities.AddRequiredParameter(parameterList, "prop_name", prop_name);
			Utilities.AddRequiredParameter(parameterList, "prop_type", prop_type);

			if (isAsync)
			{
				SendRequestAsync<data_defineObjectProperty_response, bool>(parameterList, new FacebookCallCompleted<bool>(callback), state);
				return;
			}

			SendRequest(parameterList);
		}

		private void UndefineObjectProperty(string obj_type, string prop_name, bool isAsync, UndefineObjectPropertyCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.data.undefineObjectProperty" } };
			Utilities.AddRequiredParameter(parameterList, "obj_type", obj_type);
			Utilities.AddRequiredParameter(parameterList, "prop_name", prop_name);

			if (isAsync)
			{
				SendRequestAsync<data_undefineObjectProperty_response, bool>(parameterList, new FacebookCallCompleted<bool>(callback), state);
				return;
			}

			SendRequest(parameterList);
		}

		private void RenameObjectProperty(string obj_type, string prop_name, string new_name, bool isAsync, RenameObjectPropertyCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.data.renameObjectProperty" } };
			Utilities.AddRequiredParameter(parameterList, "obj_type", obj_type);
			Utilities.AddRequiredParameter(parameterList, "prop_name", prop_name);
			Utilities.AddRequiredParameter(parameterList, "new_name", new_name);

			if (isAsync)
			{
				SendRequestAsync<data_renameObjectProperty_response, bool>(parameterList, new FacebookCallCompleted<bool>(callback), state);
				return;
			}

			SendRequest(parameterList);
		}

		private IList<object_type_info> GetObjectTypes(bool isAsync, GetObjectTypesCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.data.getObjectTypes" } };
            
			if (isAsync)
			{
				SendRequestAsync<data_getObjectTypes_response, IList<object_type_info>>(parameterList, new FacebookCallCompleted<IList<object_type_info>>(callback), state, "object_type_info");
				return null;
            }

            var response = SendRequest<data_getObjectTypes_response>(parameterList);
			return response == null ? null : response.object_type_info;
		}

		private IList<object_property_info> GetObjectType(string obj_type, bool isAsync, GetObjectTypeCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.data.getObjectType" } };
			Utilities.AddRequiredParameter(parameterList, "obj_type", obj_type);

			if (isAsync)
			{
				SendRequestAsync<data_getObjectType_response, IList<object_property_info>>(parameterList, new FacebookCallCompleted<IList<object_property_info>>(callback), state, "object_property_info");
				return null;
			}

			var response = SendRequest<data_getObjectType_response>(parameterList);
			return response == null ? null : response.object_property_info;
		}

		private long CreateObject(string obj_type, Dictionary<string, string> properties, bool isAsync, CreateObjectCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.data.createObject" } };
			Utilities.AddRequiredParameter(parameterList, "obj_type", obj_type);
			Utilities.AddJSONAssociativeArray(parameterList, "properties", properties);

			if (isAsync)
			{
				SendRequestAsync<data_createObject_response, long>(parameterList, new FacebookCallCompleted<long>(callback), state);
				return 0;
			}
			var response = SendRequest<data_createObject_response>(parameterList);
			return response == null ? 0 : response.TypedValue;
		}


		private void UpdateObject(long obj_id, Dictionary<string, string> properties, bool replace, bool isAsync, UpdateObjectCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.data.updateObject" } };
			Utilities.AddRequiredParameter(parameterList, "obj_id", obj_id);
			Utilities.AddJSONAssociativeArray(parameterList, "properties", properties);
			parameterList.Add("replace", replace.ToString());

			if (isAsync)
			{
				SendRequestAsync<data_updateObject_response, bool>(parameterList, new FacebookCallCompleted<bool>(callback), state);
				return;
			}

			SendRequest(parameterList);
		}

		private void DeleteObject(long obj_id, bool isAsync, DeleteObjectCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.data.deleteObject" } };
			Utilities.AddRequiredParameter(parameterList, "obj_id", obj_id);

			if (isAsync)
			{
				SendRequestAsync<data_deleteObject_response, bool>(parameterList, new FacebookCallCompleted<bool>(callback), state);
				return;
			}

			SendRequest(parameterList);
		}

		private void DeleteObjects(List<long> obj_ids, bool isAsync, DeleteObjectsCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.data.deleteObjects" } };
			Utilities.AddList(parameterList, "obj_ids", obj_ids);

			if (isAsync)
			{
				SendRequestAsync<data_deleteObjects_response, bool>(parameterList, new FacebookCallCompleted<bool>(callback), state);
				return;
			}

			SendRequest(parameterList);
		}

		private data_getObject_response GetObject(long obj_id, List<string> prop_names, bool isAsync, GetObjectCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.data.getObject" } };
			Utilities.AddRequiredParameter(parameterList, "obj_id", obj_id);
			Utilities.AddList(parameterList, "prop_names", prop_names);

			if (isAsync)
			{
				SendRequestAsync(parameterList, new FacebookCallCompleted<data_getObject_response>(callback), state);
				return null;
			}

			return SendRequest<data_getObject_response>(parameterList);
		}

		private IList<container> GetObjects(List<long> obj_ids, List<string> prop_names, bool isAsync, GetObjectsCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.data.getObjects" } };
			Utilities.AddList(parameterList, "obj_ids", obj_ids);
			Utilities.AddList(parameterList, "prop_names", prop_names);

			if (isAsync)
			{
				SendRequestAsync<data_getObjects_response, IList<container>>(parameterList, new FacebookCallCompleted<IList<container>>(callback), state, "data_getObjects_response_elt");
				return null;
			}

			var response = SendRequest<data_getObjects_response>(parameterList);
			return response == null ? null : response.data_getObjects_response_elt;
		}

		private string GetObjectProperty(long obj_id, string prop_name, bool isAsync, GetObjectPropertyCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.data.getObjectProperty" } };
			Utilities.AddRequiredParameter(parameterList, "obj_id", obj_id);
			Utilities.AddRequiredParameter(parameterList, "prop_name", prop_name);

			if (isAsync)
			{
				SendRequestAsync<data_getObjectProperty_response, string>(parameterList, new FacebookCallCompleted<string>(callback), state);
				return null;
			}

			var response = SendRequest<data_getObjectProperty_response>(parameterList);
			return response == null ? null : response.TypedValue;
		}

		private void SetObjectProperty(long obj_id, string prop_name, string prop_value, bool isAsync, SetObjectPropertyCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.data.setObjectProperty" } };
			Utilities.AddRequiredParameter(parameterList, "obj_id", obj_id);
			Utilities.AddRequiredParameter(parameterList, "prop_name", prop_name);
			Utilities.AddRequiredParameter(parameterList, "prop_value", prop_value);

			if (isAsync)
			{
				SendRequestAsync<data_setObjectProperty_response, bool>(parameterList, new FacebookCallCompleted<bool>(callback), state);
				return;
			}

			SendRequest(parameterList);
		}

		private string GetHashValue(string obj_type, string key, string prop_name, bool isAsync, GetHashValueCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.data.getHashValue" } };
			Utilities.AddRequiredParameter(parameterList, "obj_type", obj_type);
			Utilities.AddRequiredParameter(parameterList, "key", key);
			Utilities.AddRequiredParameter(parameterList, "prop_name", prop_name);

			if (isAsync)
			{
				SendRequestAsync<data_getHashValue_response, string>(parameterList, new FacebookCallCompleted<string>(callback), state);
				return null;
			}

			var response = SendRequest<data_getHashValue_response>(parameterList);
			return response == null ? null : response.TypedValue;
		}

		private long SetHashValue(string obj_type, string key, string value, string prop_name, bool isAsync, SetHashValueCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.data.setHashValue" } };
			Utilities.AddRequiredParameter(parameterList, "obj_type", obj_type);
			Utilities.AddRequiredParameter(parameterList, "key", key);
			Utilities.AddRequiredParameter(parameterList, "value", value);
			Utilities.AddRequiredParameter(parameterList, "prop_name", prop_name);

			if (isAsync)
			{
				SendRequestAsync<data_setHashValue_response, long>(parameterList, new FacebookCallCompleted<long>(callback), state);
				return 0;
			}

			var response = SendRequest<data_setHashValue_response>(parameterList);
			return response == null ? 0 : response.TypedValue;
		}

		private long IncHashValue(string obj_type, string key, string prop_name, int increment, bool isAsync, IncHashValueCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.data.incHashValue" } };
			Utilities.AddRequiredParameter(parameterList, "obj_type", obj_type);
			Utilities.AddRequiredParameter(parameterList, "key", key);
			Utilities.AddRequiredParameter(parameterList, "prop_name", prop_name);
			Utilities.AddRequiredParameter(parameterList, "increment", increment);

			if (isAsync)
			{
				SendRequestAsync<data_incHashValue_response, long>(parameterList, new FacebookCallCompleted<long>(callback), state);
				return 0;
			}

			var response = SendRequest<data_incHashValue_response>(parameterList);
			return response == null ? 0 : response.TypedValue;
		}

		private void RemoveHashKey(string obj_type, string key, bool isAsync, RemoveHashKeyCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.data.removeHashKey" } };
			Utilities.AddRequiredParameter(parameterList, "obj_type", obj_type);
			Utilities.AddRequiredParameter(parameterList, "key", key);

			if (isAsync)
			{
				SendRequestAsync<data_removeHashKey_response, bool>(parameterList, new FacebookCallCompleted<bool>(callback), state);
				return;
			}

			SendRequest(parameterList);
		}

		private void RemoveHashKeys(string obj_type, List<string> keys, bool isAsync, RemoveHashKeysCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.data.removeHashKeys" } };
			Utilities.AddRequiredParameter(parameterList, "obj_type", obj_type);
			Utilities.AddList(parameterList, "keys", keys);

			if (isAsync)
			{
				SendRequestAsync<data_removeHashKeys_response, bool>(parameterList, new FacebookCallCompleted<bool>(callback), state);
				return;
			}

			SendRequest(parameterList);
		}

		private void DefineAssociation(string name, int assoc_type, assoc_object_type assoc_info1, assoc_object_type assoc_info2, bool? inverse, bool isAsync, DefineAssociationCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.data.defineAssociation" } };
			Utilities.AddRequiredParameter(parameterList, "name", name);
			Utilities.AddRequiredParameter(parameterList, "assoc_type", assoc_type);
			Utilities.AddRequiredParameter(parameterList, "assoc_info1", assoc_info1);
			Utilities.AddRequiredParameter(parameterList, "assoc_info2", assoc_info2);
			if (inverse != null)
				Utilities.AddParameter(parameterList, "inverse", (bool)inverse);

			if (isAsync)
			{
				SendRequestAsync<data_defineAssociation_response, bool>(parameterList, new FacebookCallCompleted<bool>(callback), state);
				return;
			}

			SendRequest(parameterList);
		}

		private void UndefineAssociation(string name, bool isAsync, UndefineAssociationCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.data.undefineAssociation" } };
			Utilities.AddRequiredParameter(parameterList, "name", name);

			if (isAsync)
			{
				SendRequestAsync<data_undefineAssociation_response, bool>(parameterList, new FacebookCallCompleted<bool>(callback), state);
				return;
			}

			SendRequest(parameterList);
		}

		private void RenameAssociation(string name, string new_name, string new_alias1, string new_alias2, bool isAsync, RenameAssociationCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.data.renameAssociation" } };
			Utilities.AddRequiredParameter(parameterList, "name", name);
			Utilities.AddRequiredParameter(parameterList, "new_name", new_name);
			Utilities.AddRequiredParameter(parameterList, "new_alias1", new_alias1);
			Utilities.AddRequiredParameter(parameterList, "new_alias2", new_alias2);

			if (isAsync)
			{
				SendRequestAsync<data_renameAssociation_response, bool>(parameterList, new FacebookCallCompleted<bool>(callback), state);
				return;
			}

			SendRequest(parameterList);
		}

		private data_getAssociationDefinition_response GetAssociationDefinition(string name, bool isAsync, GetAssociationDefinitionCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.data.getAssociationDefinition" } };
			Utilities.AddRequiredParameter(parameterList, "name", name);

			if (isAsync)
			{
				SendRequestAsync(parameterList, new FacebookCallCompleted<data_getAssociationDefinition_response>(callback), state);
				return null;
			}

			return SendRequest<data_getAssociationDefinition_response>(parameterList);
		}

		private data_getAssociationDefinitions_response GetAssociationDefinitions(bool isAsync, GetAssociationDefinitionsCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.data.getAssociationDefinitions" } };

			if (isAsync)
			{
				SendRequestAsync(parameterList, new FacebookCallCompleted<data_getAssociationDefinitions_response>(callback), state);
				return null;
			}

			return SendRequest<data_getAssociationDefinitions_response>(parameterList);
		}

		private void SetAssociation(string name, long obj_id1, long obj_id2, string data, DateTime assoc_time, bool isAsync, SetAssociationCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.data.setAssociation" } };
			Utilities.AddRequiredParameter(parameterList, "name", name);
			Utilities.AddRequiredParameter(parameterList, "obj_id1", obj_id1);
			Utilities.AddRequiredParameter(parameterList, "obj_id2", obj_id2);
			Utilities.AddRequiredParameter(parameterList, "data", data);
			Utilities.AddRequiredParameter(parameterList, "assoc_time", (double)DateHelper.ConvertDateToDouble(DateTime.Now));

			if (isAsync)
			{
				SendRequestAsync<data_setAssociation_response, bool>(parameterList, new FacebookCallCompleted<bool>(callback), state);
				return;
			}

			SendRequest(parameterList);
		}

		private void SetAssociations(IList<DataAssociation> assocs, string name, bool isAsync, SetAssociationsCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.data.setAssociations" } };
			Utilities.AddRequiredParameter(parameterList, "assocs", assocs);
            //Utilities.AddRequiredParameter(parameterList, "associations", assocs);
			Utilities.AddRequiredParameter(parameterList, "name", name);

			if (isAsync)
			{
				SendRequestAsync<data_setAssociations_response, bool>(parameterList, new FacebookCallCompleted<bool>(callback), state);
				return;
			}

			SendRequest(parameterList);
		}

		private void RemoveAssociation(string name, long obj_id1, long obj_id2, bool isAsync, RemoveAssociationCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.data.removeAssociation" } };
			Utilities.AddRequiredParameter(parameterList, "name", name);
			Utilities.AddRequiredParameter(parameterList, "obj_id1", obj_id1);
			Utilities.AddRequiredParameter(parameterList, "obj_id2", obj_id2);

			if (isAsync)
			{
				SendRequestAsync<data_removeAssociation_response, bool>(parameterList, new FacebookCallCompleted<bool>(callback), state);
				return;
			}

			SendRequest(parameterList);
		}

		private void RemoveAssociations(List<DataAssociation> assocs, string name, bool isAsync, RemoveAssociationsCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.data.removeAssociations" } };
			//Utilities.AddList(parameterList, "assocs", assocs);
			Utilities.AddRequiredParameter(parameterList, "name", name);

			if (isAsync)
			{
				SendRequestAsync<data_removeAssociations_response, bool>(parameterList, new FacebookCallCompleted<bool>(callback), state);
				return;
			}

			SendRequest(parameterList);
		}

		private void RemoveAssociatedObjects(string name, long obj_id, bool isAsync, RemoveAssociatedObjectsCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.data.removeAssociatedObjects" } };
			Utilities.AddRequiredParameter(parameterList, "name", name);
			Utilities.AddRequiredParameter(parameterList, "obj_id", obj_id);

			if (isAsync)
			{
				SendRequestAsync<data_removeAssociatedObjects_response, bool>(parameterList, new FacebookCallCompleted<bool>(callback), state);
				return;
			}

			SendRequest(parameterList);
		}

		private data_getAssociatedObjects_response GetAssociatedObjects(string name, long obj_id, bool no_data, bool isAsync, GetAssociatedObjectsCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.data.getAssociatedObjects" } };
			Utilities.AddRequiredParameter(parameterList, "name", name);
			Utilities.AddRequiredParameter(parameterList, "obj_id", obj_id);
			parameterList.Add("no_data", no_data.ToString());

			if (isAsync)
			{
				SendRequestAsync(parameterList, new FacebookCallCompleted<data_getAssociatedObjects_response>(callback), state);
				return null;
			}

			return SendRequest<data_getAssociatedObjects_response>(parameterList);
		}

		private int GetAssociatedObjectCount(string name, long obj_id, bool isAsync, GetAssociatedObjectCountCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.data.getAssociatedObjectCount" } };
			Utilities.AddRequiredParameter(parameterList, "name", name);
			Utilities.AddRequiredParameter(parameterList, "obj_id", obj_id);

			if (isAsync)
			{
				SendRequestAsync<data_getAssociatedObjectCount_response, int>(parameterList, new FacebookCallCompleted<int>(callback), state);
				return 0;
			}

			var response = SendRequest<data_getAssociatedObjectCount_response>(parameterList);
			return response == null ? 0 : response.TypedValue;
		}

		private IList<int> GetAssociatedObjectCounts(string name, List<long> obj_ids, bool isAsync, GetAssociatedObjectCountsCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.data.getAssociatedObjectCounts" } };
			Utilities.AddRequiredParameter(parameterList, "name", name);
			Utilities.AddList(parameterList, "obj_ids", obj_ids);

			if (isAsync)
			{
				SendRequestAsync<data_getAssociatedObjectCounts_response, IList<int>>(parameterList, new FacebookCallCompleted<IList<int>>(callback), state, "data_getAssociatedObjectCounts_response_elt");
				return null;
			}

			var response = SendRequest<data_getAssociatedObjectCounts_response>(parameterList);
			return response == null ? null : response.data_getAssociatedObjectCounts_response_elt;
		}

		private data_getAssociations_response GetAssociations(long obj_id1, long obj_id2, bool no_data, bool isAsync, GetAssociationsCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.data.getAssociations" } };
			Utilities.AddRequiredParameter(parameterList, "obj_id1", obj_id1);
			Utilities.AddRequiredParameter(parameterList, "obj_id2", obj_id2);
			parameterList.Add("no_data", no_data.ToString());

			if (isAsync)
			{
				SendRequestAsync(parameterList, new FacebookCallCompleted<data_getAssociations_response>(callback), state);
				return null;
			}

			return SendRequest<data_getAssociations_response>(parameterList);
		}
		
		private IList<cookie> GetCookies(long uid, string cookieName, bool isAsync, GetCookiesCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.data.getCookies" } };
			Utilities.AddRequiredParameter(parameterList, "uid", uid);
			Utilities.AddOptionalParameter(parameterList, "name", cookieName);

			if (isAsync)
			{
                SendRequestAsync<data_getCookies_response, IList<cookie>>(parameterList, !string.IsNullOrEmpty(Session.SessionKey), new FacebookCallCompleted<IList<cookie>>(callback), state, "cookie");
				return null;
			}

            return SendRequest<data_getCookies_response>(parameterList, !string.IsNullOrEmpty(Session.SessionKey)).cookie;
		}

		private bool SetCookie(long uid, string cookieName, string value, DateTime? expires, string path, bool isAsync, SetCookieCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.data.setCookie" } };
			Utilities.AddRequiredParameter(parameterList, "uid", uid);
			Utilities.AddRequiredParameter(parameterList, "name", cookieName);
			Utilities.AddRequiredParameter(parameterList, "value", value);
			Utilities.AddOptionalParameter(parameterList, "expires", DateHelper.ConvertDateToDouble(expires));
			Utilities.AddOptionalParameter(parameterList, "path", path);

			if (isAsync)
			{
                SendRequestAsync<data_setCookie_response, bool>(parameterList, !string.IsNullOrEmpty(Session.SessionKey), new FacebookCallCompleted<bool>(callback), state);
				return true;
			}

            var response = SendRequest<data_setCookie_response>(parameterList, !string.IsNullOrEmpty(Session.SessionKey));
			return response == null ? true : response.TypedValue;
		}

		#endregion Private Methods

		#endregion Methods

		#region Delegates

        /// <summary>
        /// Delegate called when SetUserPreference call is completed.
        /// </summary>
        /// <param name="result">Boolean result.</param>
        /// <param name="state">An object containing state information for this asynchronous request.</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void SetUserPreferenceCallback(bool result, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when SetUserPreferences call is completed.
        /// </summary>
        /// <param name="result">Boolean result.</param>
        /// <param name="state">An object containing state information for this asynchronous request.</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void SetUserPreferencesCallback(bool result, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when GetUserPreference call is completed.
        /// </summary>
        /// <param name="preference">Preference string.</param>
        /// <param name="state">An object containing state information for this asynchronous request.</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void GetUserPreferenceCallback(string preference, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when CreateObjectType call is completed.
        /// </summary>
        /// <param name="result">Boolean result.</param>
        /// <param name="state">An object containing state information for this asynchronous request.</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void CreateObjectTypeCallback(bool result, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when DropObjectType call is completed.
        /// </summary>
        /// <param name="result">Boolean result.</param>
        /// <param name="state">An object containing state information for this asynchronous request.</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void DropObjectTypeCallback(bool result, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when RenameObjectType call is completed.
        /// </summary>
        /// <param name="result">Boolean result.</param>
        /// <param name="state">An object containing state information for this asynchronous request.</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void RenameObjectTypeCallback(bool result, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when DefineObjectProperty call is completed.
        /// </summary>
        /// <param name="result">Boolean result.</param>
        /// <param name="state">An object containing state information for this asynchronous request.</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void DefineObjectPropertyCallback(bool result, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when UndefineObjectProperty call is completed.
        /// </summary>
        /// <param name="result">Boolean result.</param>
        /// <param name="state">An object containing state information for this asynchronous request.</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void UndefineObjectPropertyCallback(bool result, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when RenameObjectProperty call is completed.
        /// </summary>
        /// <param name="result">Boolean result.</param>
        /// <param name="state">An object containing state information for this asynchronous request.</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void RenameObjectPropertyCallback(bool result, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when GetObjectTypes call is completed.
        /// </summary>
        /// <param name="types">A list of object_type_info types.</param>
        /// <param name="state">An object containing state information for this asynchronous request.</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void GetObjectTypesCallback(IList<object_type_info> types, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when GetObjectType call is completed.
        /// </summary>
        /// <param name="properties">A list of object_property_info types.</param>
        /// <param name="state">An object containing state information for this asynchronous request.</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void GetObjectTypeCallback(IList<object_property_info> properties, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when CreateObject call is completed.
        /// </summary>
        /// <param name="object_id">Object identifier.</param>
        /// <param name="state">An object containing state information for this asynchronous request.</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void CreateObjectCallback(long object_id, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when UpdateObject call is completed.
        /// </summary>
        /// <param name="result">Boolean result.</param>
        /// <param name="state">An object containing state information for this asynchronous request.</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void UpdateObjectCallback(bool result, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when DeleteObject call is completed.
        /// </summary>
        /// <param name="result">Boolean result.</param>
        /// <param name="state">An object containing state information for this asynchronous request.</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void DeleteObjectCallback(bool result, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when DeleteObjects call is completed.
        /// </summary>
        /// <param name="result">Boolean result.</param>
        /// <param name="state">An object containing state information for this asynchronous request.</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void DeleteObjectsCallback(bool result, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when GetObject call is completed.
        /// </summary>
        /// <param name="response">A data_getObject_response result.</param>
        /// <param name="state">An object containing state information for this asynchronous request.</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void GetObjectCallback(data_getObject_response response, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when GetObjects call is completed.
        /// </summary>
        /// <param name="objects">A List of container objects.</param>
        /// <param name="state">An object containing state information for this asynchronous request.</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void GetObjectsCallback(IList<container> objects, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when GetObjectProperty call is completed.
        /// </summary>
        /// <param name="property">Property string.</param>
        /// <param name="state">An object containing state information for this asynchronous request.</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void GetObjectPropertyCallback(string property, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when SetObjectProperty call is completed.
        /// </summary>
        /// <param name="result">Boolean result.</param>
        /// <param name="state">An object containing state information for this asynchronous request.</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void SetObjectPropertyCallback(bool result, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when GetHashValue call is completed.
        /// </summary>
        /// <param name="hashValue">Hash string result.</param>
        /// <param name="state">An object containing state information for this asynchronous request.</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void GetHashValueCallback(string hashValue, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when SetHashValue call is completed.
        /// </summary>
        /// <param name="object_id">Object identifer.</param>
        /// <param name="state">An object containing state information for this asynchronous request.</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void SetHashValueCallback(long object_id, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when IncHashValue call is completed.
        /// </summary>
        /// <param name="object_id">Object identifer.</param>
        /// <param name="state">An object containing state information for this asynchronous request.</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void IncHashValueCallback(long object_id, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when RemoveHashKey call is completed.
        /// </summary>
        /// <param name="result">Boolean result.</param>
        /// <param name="state">An object containing state information for this asynchronous request.</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void RemoveHashKeyCallback(bool result, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when RemoveHashKeys call is completed.
        /// </summary>
        /// <param name="result">Boolean result.</param>
        /// <param name="state">An object containing state information for this asynchronous request.</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void RemoveHashKeysCallback(bool result, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when DefineAssociation call is completed.
        /// </summary>
        /// <param name="result">Boolean result.</param>
        /// <param name="state">An object containing state information for this asynchronous request.</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void DefineAssociationCallback(bool result, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when UndefineAssociation call is completed.
        /// </summary>
        /// <param name="result">Boolean result.</param>
        /// <param name="state">An object containing state information for this asynchronous request.</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void UndefineAssociationCallback(bool result, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when RenameAssociation call is completed.
        /// </summary>
        /// <param name="result">Boolean result.</param>
        /// <param name="state">An object containing state information for this asynchronous request.</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void RenameAssociationCallback(bool result, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when GetAssociationDefinition call is completed.
        /// </summary>
        /// <param name="response">A data_getAssociationDefinition_response object.</param>
        /// <param name="state">An object containing state information for this asynchronous request.</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void GetAssociationDefinitionCallback(data_getAssociationDefinition_response response, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when GetAssociationDefinitions call is completed.
        /// </summary>
        /// <param name="response">A data_getAssociationDefinitions_response object.</param>
        /// <param name="state">An object containing state information for this asynchronous request.</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void GetAssociationDefinitionsCallback(data_getAssociationDefinitions_response response, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when SetAssociation call is completed.
        /// </summary>
        /// <param name="result">Boolean result.</param>
        /// <param name="state">An object containing state information for this asynchronous request.</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void SetAssociationCallback(bool result, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when SetAssociations call is completed.
        /// </summary>
        /// <param name="result">Boolean result.</param>
        /// <param name="state">An object containing state information for this asynchronous request.</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void SetAssociationsCallback(bool result, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when RemoveAssociation call is completed.
        /// </summary>
        /// <param name="result">Boolean result.</param>
        /// <param name="state">An object containing state information for this asynchronous request.</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void RemoveAssociationCallback(bool result, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when RemoveAssociations call is completed.
        /// </summary>
        /// <param name="result">Boolean result.</param>
        /// <param name="state">An object containing state information for this asynchronous request.</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void RemoveAssociationsCallback(bool result, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when RemoveAssociatedObjects call is completed.
        /// </summary>
        /// <param name="result">Boolean result.</param>
        /// <param name="state">An object containing state information for this asynchronous request.</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void RemoveAssociatedObjectsCallback(bool result, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when GetAssociatedObjects call is completed.
        /// </summary>
        /// <param name="response">A data_getAssociatedObjects_response object.</param>
        /// <param name="state">An object containing state information for this asynchronous request.</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void GetAssociatedObjectsCallback(data_getAssociatedObjects_response response, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when GetAssociatedObjectCount call is completed.
        /// </summary>
        /// <param name="objectCount">Object count.</param>
        /// <param name="state">An object containing state information for this asynchronous request.</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void GetAssociatedObjectCountCallback(int objectCount, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when GetAssociatedObjectCounts call is completed.
        /// </summary>
        /// <param name="objectCounts">A List of object counts.</param>
        /// <param name="state">An object containing state information for this asynchronous request.</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void GetAssociatedObjectCountsCallback(IList<int> objectCounts, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when GetAssociations call is completed.
        /// </summary>
        /// <param name="response">A data_getAssociations_response object.</param>
        /// <param name="state">An object containing state information for this asynchronous request.</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void GetAssociationsCallback(data_getAssociations_response response, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when GetCookies call is completed.
        /// </summary>
        /// <param name="cookies">A List of cookies.</param>
        /// <param name="state">An object containing state information for this asynchronous request.</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void GetCookiesCallback(IList<cookie> cookies, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when SetCookie call is completed.
        /// </summary>
        /// <param name="result">Boolean result.</param>
        /// <param name="state">An object containing state information for this asynchronous request.</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void SetCookieCallback(bool result, Object state, FacebookException e);
			
		#endregion Delegates
	}

	/// <summary>
	/// Id-value pairs
	/// </summary>
	public class DataAssociation
	{
		/// <summary>
		/// Name of the association to set. 
		/// </summary>
		public string name { get; set; }
		/// <summary>
		/// Object identifier 1. 
		/// </summary>
		public double id1 { get; set; }
		/// <summary>
		/// Object identifier 2.
		/// </summary>
		public double id2 { get; set; }
		/// <summary>
		/// Optional - An arbitrary data (max. 255 characters) to store with this association. 
		/// </summary>
		public string data { get; set; }
		/// <summary>
		/// Optional - Default to association creation time. A timestamp to store with this association. 
		/// </summary>
		public DateTime assoc_time { get; set; }
	}
}