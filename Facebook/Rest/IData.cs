﻿using System;
namespace Facebook.Rest
{
	public interface IData : IRestBase
	{
		long CreateObject(string obj_type, System.Collections.Generic.Dictionary<string, string> properties);
		long CreateObjectAsync(string obj_type, System.Collections.Generic.Dictionary<string, string> properties, Data.CreateObjectCallback callback, object state);
		void CreateObjectType(string name);
		void CreateObjectTypeAsync(string name, Data.CreateObjectTypeCallback callback, object state);
		void DefineAssociation(string name, int assoc_type, Facebook.Schema.assoc_object_type assoc_info1, Facebook.Schema.assoc_object_type assoc_info2, bool? inverse);
		void DefineAssociationAsync(string name, int assoc_type, Facebook.Schema.assoc_object_type assoc_info1, Facebook.Schema.assoc_object_type assoc_info2, bool? inverse, Data.DefineAssociationCallback callback, object state);
		void DefineObjectProperty(string obj_type, string prop_name, int prop_type);
		void DefineObjectPropertyAsync(string obj_type, string prop_name, int prop_type, Data.DefineObjectPropertyCallback callback, object state);
		void DeleteObject(long obj_id);
		void DeleteObjectAsync(long obj_id, Data.DeleteObjectCallback callback, object state);
		void DeleteObjects(System.Collections.Generic.List<long> obj_ids);
		void DeleteObjectsAsync(System.Collections.Generic.List<long> obj_ids, Data.DeleteObjectsCallback callback, object state);
		void DropObjectType(string obj_type);
		void DropObjectTypeAsync(string obj_type, Data.DropObjectTypeCallback callback, object state);
		int GetAssociatedObjectCount(string name, long obj_id);
		int GetAssociatedObjectCountAsync(string name, long obj_id, Data.GetAssociatedObjectCountCallback callback, object state);
		System.Collections.Generic.IList<int> GetAssociatedObjectCounts(string name, System.Collections.Generic.List<long> obj_ids);
		System.Collections.Generic.IList<int> GetAssociatedObjectCountsAsync(string name, System.Collections.Generic.List<long> obj_ids, Data.GetAssociatedObjectCountsCallback callback, object state);
		Facebook.Schema.data_getAssociatedObjects_response GetAssociatedObjects(string name, long obj_id, bool no_data);
		Facebook.Schema.data_getAssociatedObjects_response GetAssociatedObjectsAsync(string name, long obj_id, bool no_data, Data.GetAssociatedObjectsCallback callback, object state);
		Facebook.Schema.data_getAssociationDefinition_response GetAssociationDefinition(string name);
		Facebook.Schema.data_getAssociationDefinition_response GetAssociationDefinitionAsync(string name, Data.GetAssociationDefinitionCallback callback, object state);
		Facebook.Schema.data_getAssociationDefinitions_response GetAssociationDefinitions();
		Facebook.Schema.data_getAssociationDefinitions_response GetAssociationDefinitionsAsync(Data.GetAssociationDefinitionsCallback callback, object state);
		Facebook.Schema.data_getAssociations_response GetAssociations(long obj_id1, long obj_id2, bool no_data);
		Facebook.Schema.data_getAssociations_response GetAssociationsAsync(long obj_id1, long obj_id2, bool no_data, Data.GetAssociationsCallback callback, object state);
		System.Collections.Generic.IList<Facebook.Schema.cookie> GetCookies();
		System.Collections.Generic.IList<Facebook.Schema.cookie> GetCookies(long uid);
		System.Collections.Generic.IList<Facebook.Schema.cookie> GetCookies(long uid, string cookieName);
		System.Collections.Generic.IList<Facebook.Schema.cookie> GetCookiesAsync(Data.GetCookiesCallback callback, object state);
		System.Collections.Generic.IList<Facebook.Schema.cookie> GetCookiesAsync(long uid, Data.GetCookiesCallback callback, object state);
		System.Collections.Generic.IList<Facebook.Schema.cookie> GetCookiesAsync(long uid, string cookieName, Data.GetCookiesCallback callback, object state);
		string GetHashValue(string obj_type, string key, string prop_name);
		string GetHashValueAsync(string obj_type, string key, string prop_name, Data.GetHashValueCallback callback, object state);
		Facebook.Schema.data_getObject_response GetObject(long obj_id, System.Collections.Generic.List<string> prop_names);
		Facebook.Schema.data_getObject_response GetObjectAsync(long obj_id, System.Collections.Generic.List<string> prop_names, Data.GetObjectCallback callback, object state);
		string GetObjectProperty(long obj_id, string prop_name);
		string GetObjectPropertyAsync(long obj_id, string prop_name, Data.GetObjectPropertyCallback callback, object state);
		System.Collections.Generic.IList<Facebook.Schema.container> GetObjects(System.Collections.Generic.List<long> obj_ids, System.Collections.Generic.List<string> prop_names);
		System.Collections.Generic.IList<Facebook.Schema.container> GetObjectsAsync(System.Collections.Generic.List<long> obj_ids, System.Collections.Generic.List<string> prop_names, Data.GetObjectsCallback callback, object state);
		System.Collections.Generic.IList<Facebook.Schema.object_property_info> GetObjectType(string obj_type);
		System.Collections.Generic.IList<Facebook.Schema.object_property_info> GetObjectTypeAsync(string obj_type, Data.GetObjectTypeCallback callback, object state);
		System.Collections.Generic.IList<Facebook.Schema.object_type_info> GetObjectTypes();
		System.Collections.Generic.IList<Facebook.Schema.object_type_info> GetObjectTypesAsync(Data.GetObjectTypesCallback callback, object state);
		string GetUserPreference(int pref_id);
		void GetUserPreferenceAsync(int pref_id, Data.GetUserPreferenceCallback callback, object state);
		System.Collections.Generic.List<Facebook.Schema.preference> GetUserPreferences();
		void GetUserPreferencesAsync(Data.GetUserPreferenceCallback callback, object state);
		long IncHashValue(string obj_type, string key, string prop_name, int increment);
		long IncHashValueAsync(string obj_type, string key, string prop_name, int increment, Data.IncHashValueCallback callback, object state);
		void RemoveAssociatedObjects(string name, long obj_id);
		void RemoveAssociatedObjectsAsync(string name, long obj_id, Data.RemoveAssociatedObjectsCallback callback, object state);
		void RemoveAssociation(string name, long obj_id1, long obj_id2);
		void RemoveAssociationAsync(string name, long obj_id1, long obj_id2, Data.RemoveAssociationCallback callback, object state);
		void RemoveAssociations(System.Collections.Generic.List<DataAssociation> assocs, string name);
		void RemoveAssociationsAsync(System.Collections.Generic.List<DataAssociation> assocs, string name, Data.RemoveAssociationsCallback callback, object state);
		void RemoveHashKey(string obj_type, string key);
		void RemoveHashKeyAsync(string obj_type, string key, Data.RemoveHashKeyCallback callback, object state);
		void RemoveHashKeys(string obj_type, System.Collections.Generic.List<string> keys);
		void RemoveHashKeysAsync(string obj_type, System.Collections.Generic.List<string> keys, Data.RemoveHashKeysCallback callback, object state);
		void RenameAssociation(string name, string new_name, string new_alias1, string new_alias2);
		void RenameAssociationAsync(string name, string new_name, string new_alias1, string new_alias2, Data.RenameAssociationCallback callback, object state);
		void RenameObjectProperty(string obj_type, string prop_name, string new_name);
		void RenameObjectPropertyAsync(string obj_type, string prop_name, string new_name, Data.RenameObjectPropertyCallback callback, object state);
		void RenameObjectType(string obj_type, string new_name);
		void RenameObjectTypeAsync(string obj_type, string new_name, Data.RenameObjectTypeCallback callback, object state);
		void SetAssociation(string name, long obj_id1, long obj_id2, string data, DateTime assoc_time);
		void SetAssociationAsync(string name, long obj_id1, long obj_id2, string data, DateTime assoc_time, Data.SetAssociationCallback callback, object state);
		void SetAssociations(System.Collections.Generic.IList<DataAssociation> assocs, string name);
		void SetAssociationsAsync(System.Collections.Generic.IList<DataAssociation> assocs, string name, Data.SetAssociationsCallback callback, object state);
		bool SetCookie(long uid, string cookieName, string value, DateTime? expires, string path);
		bool SetCookieAsync(long uid, string cookieName, string value, DateTime? expires, string path, Data.SetCookieCallback callback, object state);
		long SetHashValue(string obj_type, string key, string value, string prop_name);
		long SetHashValueAsync(string obj_type, string key, string value, string prop_name, Data.SetHashValueCallback callback, object state);
		void SetObjectProperty(long obj_id, string prop_name, string prop_value);
		void SetObjectPropertyAsync(long obj_id, string prop_name, string prop_value, Data.SetObjectPropertyCallback callback, object state);
		void SetUserPreference(int pref_id, string value);
		void SetUserPreferenceAsync(int pref_id, string value, Data.SetUserPreferenceCallback callback, object state);
		void SetUserPreferences(System.Collections.Generic.List<string> values, bool replace);
		void SetUserPreferencesAsync(System.Collections.Generic.List<string> values, bool replace, Data.SetUserPreferencesCallback callback, object state);
		void UndefineAssociation(string name);
		void UndefineAssociationAsync(string name, Data.UndefineAssociationCallback callback, object state);
		void UndefineObjectProperty(string obj_type, string prop_name);
		void UndefineObjectPropertyAsync(string obj_type, string prop_name, Data.UndefineObjectPropertyCallback callback, object state);
		void UpdateObject(long obj_id, System.Collections.Generic.Dictionary<string, string> properties, bool replace);
		void UpdateObjectAsync(long obj_id, System.Collections.Generic.Dictionary<string, string> properties, bool replace, Data.UpdateObjectCallback callback, object state);
	}
}
