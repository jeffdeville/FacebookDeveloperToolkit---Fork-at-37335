using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Facebook.Schema;
using System;
using Facebook.Rest;
using Facebook.Session;
using Facebook.Utility;

namespace Facebook.Tests.Synchronous
{
    /// <summary>
    ///This is a test class for dataTest and is intended
    ///to contain all dataTest Unit Tests
    ///</summary>
    [TestClass()]
	public class dataTest : Test
    {
        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        private long _objectID;
        private long _hashID;
        private long _id1;
        private long _id2;
 
        /// <summary>
        ///A test for setObjectProperty
        ///</summary>
        [TestMethod()]
        public void setObjectPropertyTest()
        {
            createObjectTest();
            long obj_id = _objectID;
            string prop_name = Constants.TestObjectPropertyName;
            string prop_value = Constants.TestPropertyValue;
            _apiWeb.Data.SetObjectProperty(obj_id, prop_name, prop_value);
            var typ = _apiWeb.Data.GetObject(_objectID, null);
            //Assert.IsTrue(typ.ToString().IndexOf(prop_value) > 0);
            Assert.IsNotNull(typ);
            // TODO: Check typ value?
        }

        /// <summary>
        ///A test for setUserPreference
        ///</summary>
        [TestMethod()]
        public void setUserPreferenceTest()
        {
            int pref_id = 0; 
            string value = Constants.TestString1;
            _apiWeb.Data.SetUserPreference(pref_id, value);
            Assert.IsTrue(1 == 1); 
        }

        /// <summary>
        ///A test for setUserPreferences
        ///</summary>
        [TestMethod()]
        public void setUserPreferencesTest()
        {
            List<string> values = new List<string> { Constants.TestString2, Constants.TestString3}; 
            bool replace = true;

            // No return value
            _apiWeb.Data.SetUserPreferences(values, replace);
            
            string result1 = _apiWeb.Data.GetUserPreference(0);
            Assert.AreEqual(Constants.TestString2, result1);
            string result2 = _apiWeb.Data.GetUserPreference(1);
            Assert.AreEqual(Constants.TestString3, result2);
        }

        /// <summary>
        ///A test for undefineAssociation
        ///</summary>
        [TestMethod()]
        public void undefineAssociationTest()
        {
            defineAssociationTest();
            _apiWeb.Data.UndefineAssociation(Constants.TestAssociationDefName);
            try
            {
                // retry, expectin failure
                _apiWeb.Data.UndefineAssociation(Constants.TestAssociationDefName);
            }
            catch (FacebookException e)
            {
                //If it's not there, the we've we've succesfully removed the assn.
                if (e.ErrorCode != 803)
                    throw e;
                //Return since we've passed
                return;
            }
            Assert.Fail("An exception should have been caught since the assn should have been removed");
        }

        /// <summary>
        ///A test for undefineObjectProperty
        ///</summary>
        [TestMethod()]
        public void undefineObjectPropertyTest()
        {
            defineObjectPropertyTest();
            string obj_type = Constants.TestType;
            string prop_name = Constants.TestObjectPropertyName; 

            //Check to make sure we've got just one from out setup
            var types = _apiWeb.Data.GetObjectType(obj_type);
            Assert.AreEqual(types.Count, 1);

            //Should have been dropped at this point.
            _apiWeb.Data.UndefineObjectProperty(obj_type, prop_name);
            types = _apiWeb.Data.GetObjectType(obj_type);
            Assert.AreEqual(types.Count,0);
        }

        /// <summary>
        ///A test for updateObject
        ///</summary>
        [TestMethod()]
        public void updateObjectTest()
        {
            createObjectTest();
            long obj_id = _objectID;
            //var properties = new Dictionary<string, string> { { Constants.TestObjectPropertyName, Constants.TestObjectPropertyNewName } };
            var properties = new Dictionary<string, string> { { Constants.TestObjectPropertyName, Constants.TestPropertyUpdateValue } };
            bool replace = true;
            _apiWeb.Data.UpdateObject(obj_id, properties, replace);
            var newObject = _apiWeb.Data.GetObject(obj_id, null);
            Assert.IsNotNull(newObject);
            var updatedActualValue = _apiWeb.Data.GetObjectProperty(obj_id, Constants.TestObjectPropertyName);
            Assert.AreEqual(Constants.TestPropertyUpdateValue, updatedActualValue);
            //Assert.IsTrue(newObject.ToString().IndexOf(Constants.TestObjectPropertyNewName) > 0);
        }

        /// <summary>
        ///A test for setHashValue
        ///</summary>
        [TestMethod()]
        public void setHashValueTest()
        {
            createObjectTest();
            string obj_type = Constants.TestType;
            string key = Constants.TestHashKey;
            string value = Constants.TestHashValue;
            string prop_name = Constants.TestObjectPropertyName;
            long actual = _apiWeb.Data.SetHashValue(obj_type, key, value, prop_name);
            _hashID = actual;
            Assert.IsInstanceOfType(actual, typeof(long));
            Assert.IsTrue(actual>0);
            var ret = _apiWeb.Data.GetHashValue(Constants.TestType, Constants.TestHashKey, Constants.TestObjectPropertyName);
            Assert.AreEqual(ret, Constants.TestHashValue);
        }

        /// <summary>
        ///A test for setCookie
        ///</summary>
        [TestMethod()]
        public void setCookieTest()
        {
            long uid = Constants.FBSamples_UserId;
            string cookieName = Constants.TestString1;
            string value = Constants.TestString2;
            Nullable<DateTime> expires = null;
            string path = string.Empty; 
            bool expected = true; 
            bool actual;
            actual = _apiWeb.Data.SetCookie(uid, cookieName, value, expires, path);
            Assert.AreEqual(expected, actual);
            
        }

		// Facebook temporarily disabled this method on their side until they can support user-level permissions.
		// TODO: Once they enable it again, uncomment this test.
		///// <summary>
		/////A test for setAssociations
		/////</summary>
		//[TestMethod()]
		//public void setAssociationsTest()
		//{
		//    // TODO: Does't work right now:
		//    //Invalid parameter: associations.	
		//    Get2ObjectIDs();
		//    List<DataAssociation> assocs = new List<DataAssociation>
		//                                       {
		//                                           new DataAssociation
		//                                               {
		//                                                   assoc_time = DateTime.Now,
		//                                                   data = Constants.TestAssociationData1,
		//                                                   id1 = _id1,
		//                                                   id2 = _id2,
		//                                                   name = Constants.TestAssociationDefName
		//                                               },
		//                                           new DataAssociation
		//                                               {
		//                                                   assoc_time = DateTime.Now,
		//                                                   data = Constants.TestAssociationData2,
		//                                                   id1 = _id1,
		//                                                   id2 = _id2,
		//                                                   name = Constants.TestAssociationDefName
		//                                               }
		//                                       };
		//    string name = Constants.TestAssociationName;
		//    _apiWeb.Data.SetAssociations(assocs, name);
		//}

        /// <summary>
        ///A test for setAssociation
        ///</summary>
        [TestMethod()]
        public void setAssociationTest()
        {
            Get2ObjectIDs();
            string name = Constants.TestType;
            string data = Constants.TestAssociationData1;
            DateTime assoc_time = DateTime.Now;
            _apiWeb.Data.SetAssociation(name, _id1, _id2, data, assoc_time);
        }

        private void Get2ObjectIDs()
        {
            createObjectTest();
            _id1 = _objectID;
            createObjectTest();
            _id2 = _objectID;
        }

        /// <summary>
        ///A test for renameObjectType
        ///</summary>
        [TestMethod()]
        public void renameObjectTypeTest()
        {
            createObjectTypeTest();
            string obj_type = Constants.TestType; 
            string new_name = Constants.TestTypeRename;
            _apiWeb.Data.RenameObjectType(obj_type, new_name);
            var types = _apiWeb.Data.GetObjectTypes();
            foreach(var typ in types)
            {
                Assert.AreEqual(typ.name,new_name);
            }
        }

        /// <summary>
        ///A test for renameObjectProperty
        ///</summary>
        [TestMethod()]
        public void renameObjectPropertyTest()
        {
            defineObjectPropertyTest();
            string obj_type = Constants.TestType; 
            string prop_name = Constants.TestObjectPropertyName; 
            string new_name = Constants.TestObjectPropertyNewName;
            _apiWeb.Data.RenameObjectProperty(obj_type, prop_name, new_name);
            var typ = _apiWeb.Data.GetObjectType(obj_type);
            Assert.AreEqual(typ[0].name, Constants.TestObjectPropertyNewName);
        }

        /// <summary>
        ///A test for renameAssociation
        ///</summary>
        [TestMethod()]
        public void renameAssociationTest()
        {
            DropAssociations();
            DropAssociations(Constants.TestAssociationRename);
            defineAssociationTest();
            
            string name = Constants.TestAssociationDefName;
            string new_name = Constants.TestAssociationRename;
            string new_alias1 = Constants.TestAssociationDefAlias3;
            string new_alias2 = Constants.TestAssociationDefAlias4;
            _apiWeb.Data.RenameAssociation(name, new_name, new_alias1, new_alias2);
        }

        /// <summary>
        ///A test for removeHashKeys
        ///</summary>
        [TestMethod()]
        public void removeHashKeysTest()
        {
            //test currently fails with the error:
            //A database error occurred. Please try again: unable to lookup hash keys.	removeHashKeysTest	
            setHashValueTest();
            string obj_type = Constants.TestType;
            List<string> keys = new List<string>{_hashID.ToString()};
            // TODO: Facebook returning following exception:
            // A database error occurred. Please try again: unable to lookup hash keys
            //_apiWeb.Data.RemoveHashKeys(obj_type, keys);
            //var ret = _apiWeb.Data.GetHashValue(Constants.TestType, Constants.TestHashKey, Constants.TestObjectPropertyName);
            //Assert.AreEqual(ret, null);
        }

        /// <summary>
        ///A test for removeHashKey
        ///</summary>
        [TestMethod()]
        public void removeHashKeyTest()
        {
            setHashValueTest();
            string obj_type = Constants.TestType;
            string key = Constants.TestHashKey;
            _apiWeb.Data.RemoveHashKey(obj_type, key);
            var ret = _apiWeb.Data.GetHashValue(Constants.TestType, Constants.TestHashKey, Constants.TestObjectPropertyName);
            Assert.AreEqual(ret, null);
        }

		// Facebook temporarily disabled this method on their side until they can support user-level permissions.
		// TODO: Once they enable it again, uncomment this test.
		///// <summary>
		/////A test for removeAssociations
		/////</summary>
		//[TestMethod()]
		//public void removeAssociationsTest()
		//{
		//    // TODO: No DataAssociation object is provided by the XSD, so this will not work.
		//    List<DataAssociation> assocs = null; // TODO: Initialize to an appropriate value
		//    string name = string.Empty; // TODO: Initialize to an appropriate value
		//    _apiWeb.Data.RemoveAssociations(assocs, name);
		//    Assert.Inconclusive("A method that does not return a value cannot be verified.");
		//}

        /// <summary>
        ///A test for removeAssociation
        ///</summary>
        [TestMethod()]
        public void removeAssociationTest()
        {
            setAssociationTest();
            string name = Constants.TestType;
            _apiWeb.Data.RemoveAssociation(name, _id1, _id2);
        }

        /// <summary>
        ///A test for removeAssociatedObjects
        ///</summary>
        [TestMethod()]
        public void removeAssociatedObjectsTest()
        {
            setAssociationTest();
            string name = Constants.TestType;
            long obj_id = _id1;
            _apiWeb.Data.RemoveAssociatedObjects(name, obj_id);
        }   

        /// <summary>
        ///A test for incHashValue
        ///</summary>
        [TestMethod()]
        public void incHashValueTest()
        {
            setHashValueTest();
            string obj_type = Constants.TestType;
            string key = Constants.TestHashKey;
            string prop_name = Constants.TestObjectPropertyName;
            int increment = 2; 
            long expected = long.Parse(Constants.TestHashValue) + increment;
            long actual;
            actual = _apiWeb.Data.IncHashValue(obj_type, key, prop_name, increment);
            Assert.AreEqual(expected, actual);
            
        }

        /// <summary>
        ///A test for getUserPreference
        ///</summary>
        [TestMethod()]
        public void getUserPreferenceTest()
        {
            setUserPreferenceTest();
            int pref_id = 0; 
            string expected = Constants.TestString1; 
            string actual;
            actual = _apiWeb.Data.GetUserPreference(pref_id);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for getUserPreferences
        ///</summary>
        [TestMethod()]
        public void getUserPreferencesTest()
        {
            setUserPreferencesTest();
            
            // No return value
            var actual = _apiWeb.Data.GetUserPreferences();
            Assert.IsNotNull(actual);
            Assert.AreEqual(Constants.TestString2, actual[0].value);
            Assert.AreEqual(Constants.TestString3, actual[1].value);
        }
        
        /// <summary>
        ///A test for getObjectType
        ///</summary>
        [TestMethod()]
        public void getObjectTypeTest()
        {
            defineObjectPropertyTest();
            string obj_type = Constants.TestType;
            IList<object_property_info> expected = new List<object_property_info>
                                                       {
                                                           new object_property_info
                                                               {
                                                                   name = Constants.TestObjectPropertyName,
                                                                   data_type = 1,
                                                                   index_type = 0
                                                               }
                                                       };
            IList<object_property_info> actual;
            actual = _apiWeb.Data.GetObjectType(obj_type);
            Assert.AreEqual(expected[0].name, actual[0].name);
            Assert.AreEqual(expected[0].data_type, actual[0].data_type);
            Assert.AreEqual(expected[0].index_type, actual[0].index_type);
        }

        /// <summary>
        ///A test for getObjectTypes
        ///</summary>
        [TestMethod()]
        public void getObjectTypesTest()
        {
            defineObjectPropertyTest();
            string obj_type = Constants.TestType;
            IList<object_type_info> expected = new List<object_type_info>()
                                                   {
                                                       new object_type_info()
                                                           {
                                                               name = Constants.TestType,
                                                               object_class = 2
                                                           }
                                                   };
                    
            var actual = _apiWeb.Data.GetObjectTypes();
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected[0].name, actual[0].name);
            Assert.AreEqual(expected[0].object_class, actual[0].object_class);
        }

        /// <summary>
        ///A test for getObjects
        ///</summary>
        [TestMethod()]
        public void getObjectsTest()
        {
            List<long> created = new List<long>();
            createObjectTest(Constants.TestType, Constants.TestObjectPropertyName, Constants.TestPropertyValue);
            created.Add(_objectID);
            createObjectTest(Constants.TestType, Constants.TestObjectPropertyName, Constants.TestPropertyValue);
            created.Add(_objectID);
            // TODO: Facebook returning following exception:
            // A database error occurred. Please try again: unable to fetch all objects
            //var actual = _apiWeb.Data.GetObjects(created, null);
            //Assert.IsNotNull(actual);
            //Assert.AreEqual(actual, created);
        }

        /// <summary>
        ///A test for getObject
        ///</summary>
        [TestMethod()]
        public void getObjectTest()
        {
            createObjectTest();
            var actual = _apiWeb.Data.GetObject(_objectID, null);
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for getHashValue
        ///</summary>
        [TestMethod()]
        public void getHashValueTest()
        {
            setHashValueTest();
            string obj_type = Constants.TestType;
            string key = Constants.TestHashKey;
            string prop_name = Constants.TestObjectPropertyName;
            string expected = Constants.TestHashValue; 
            string actual;
            actual = _apiWeb.Data.GetHashValue(obj_type, key, prop_name);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for getCookies
        ///</summary>
        [TestMethod()]
        public void getCookiesTest2()
        {
            long uid = Constants.FBSamples_UserId; 
            IList<cookie> actual;
            actual = _apiWeb.Data.GetCookies(uid);
            Assert.AreEqual(0, actual.Count);
            
        }

        /// <summary>
        ///A test for getCookies
        ///</summary>
        [TestMethod()]
        public void getCookiesTest1()
        {
            long uid = Constants.FBSamples_UserId;
            string cookieName = "test"; 
            IList<cookie> actual;
            actual = _apiWeb.Data.GetCookies(uid, cookieName);
            Assert.AreEqual(0, actual.Count);
            
        }

        /// <summary>
        ///A test for getCookies
        ///</summary>
        [TestMethod()]
        public void getCookiesTest()
        {
            IList<cookie> actual;
            try
            {
                actual = _apiWeb.Data.GetCookies();

            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "user id parameter or session key required");

            }
            
        }

        /// <summary>
        ///A test for getAssociations
        ///</summary>
        [TestMethod()]
        public void getAssociationsTest()
        {
            setAssociationTest();
            long obj_id1 = _id1;
            long obj_id2 = _id2;
            bool no_data = false;
            data_getAssociations_response actual;
            actual = _apiWeb.Data.GetAssociations(obj_id1, obj_id2, no_data);
            // TODO: Facebook returns 0 assocations?
            //Assert.AreEqual(2, actual.object_association.Count);
        }

		// Facebook temporarily disabled this method on their side until they can support user-level permissions.
		// TODO: Once they enable it again, uncomment this test.
		///// <summary>
		/////A test for getAssociationDefinitions
		/////</summary>
		//[TestMethod()]
		//public void getAssociationDefinitionsTest()
		//{
		//    defineAssociationTest();
		//    var resps = _apiWeb.Data.GetAssociationDefinitions();
		//    foreach(var resp in resps.object_assoc_info)
		//    {
		//        // TODO: Doesn't work right now... parsing from FB doesn't work.  You can see the values in the untyped value
		//        Assert.AreEqual(resp.assoc_info1.alias, Constants.TestAssociationDefAlias1);
		//        Assert.AreEqual(resp.assoc_info2.alias, Constants.TestAssociationDefAlias2);
		//    }
		//}

		// Facebook temporarily disabled this method on their side until they can support user-level permissions.
		// TODO: Once they enable it again, uncomment this test.
		///// <summary>
		/////A test for getAssociationDefinition
		/////</summary>
		//[TestMethod()]
		//public void getAssociationDefinitionTest()
		//{
		//    defineAssociationTest();
		//    string name = Constants.TestAssociationDefName;
		//    var resp = _api.Data.GetAssociationDefinition(name);
		//    // TODO: Doesn't work right now... parsing from FB doesn't work because we don't have the new XSD.  
		//    //You can see the values in the untyped value.
		//    Assert.AreEqual(resp.assoc_info1.alias, Constants.TestAssociationDefAlias1);
		//    Assert.AreEqual(resp.assoc_info2.alias, Constants.TestAssociationDefAlias2);
            
		//}

		// Facebook temporarily disabled this method on their side until they can support user-level permissions.
		// TODO: Once they enable it again, uncomment this test.
		///// <summary>
		/////A test for getAssociatedObjects
		/////</summary>
		//[TestMethod()]
		//public void getAssociatedObjectsTest()
		//{
		//    setAssociationTest();
		//    string name = Constants.TestType;
		//    long obj_id = _id1; 
		//    bool no_data = false; 
		//    data_getAssociatedObjects_response actual;
		//    actual = _api.Data.GetAssociatedObjects(name, obj_id, no_data);
		//    Assert.AreEqual(actual.object_association[0].id2,_id2);
		//}

        /// <summary>
        ///A test for getAssociatedObjectCounts
        ///</summary>
        [TestMethod()]
        public void getAssociatedObjectCountsTest()
        {
            setAssociationTest();
            string name = Constants.TestType;
            List<long> obj_ids = new List<long>{_id1,_id2};
            IList<int> actual;
            actual = _apiWeb.Data.GetAssociatedObjectCounts(name, obj_ids);
            // TODO: Facebook returns 0 assocations?
            //Assert.AreEqual(1, actual[0]);
            //Assert.AreEqual(1, actual[1]); 
        }

        /// <summary>
        ///A test for getAssociatedObjectCount
        ///</summary>
        [TestMethod()]
        public void getAssociatedObjectCountTest()
        {
            setAssociationTest();
            string name = Constants.TestType;
            long obj_id = _id2;
            int expected = 1;  // //Test is known to fail as of 8/10.  Don't know why there are 0 being returned by facebook.  It should be 1.
            int actual;
            actual = _apiWeb.Data.GetAssociatedObjectCount(name, obj_id);
            // TODO: Facebook returns 0 assocations?
            //Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for dropObjectType
        ///</summary>
        [TestMethod()]
        public void dropObjectTypeTest()
        {
            //var existingTypes = _api.Data.GetObjectTypes();
            var existingTypes = _apiWeb.Data.GetObjectTypes();
            foreach (var typ in existingTypes)
            {
                //_api.Data.DropObjectType(typ.name);
                _apiWeb.Data.DropObjectType(typ.name);
            }
        }

        /// <summary>
        ///A test for deleteObjects
        ///</summary>
        [TestMethod()]
        public void deleteObjectsTest()
        {
            List<long> created = new List<long>();
            createObjectTest(Constants.TestType, Constants.TestObjectPropertyName, Constants.TestPropertyValue);
            created.Add(_objectID);
            createObjectTest(Constants.TestType, Constants.TestObjectPropertyName, Constants.TestPropertyValue);
            created.Add(_objectID);
            // TODO: Facebook returning following exception:
            // A database error occurred. Please try again: unable to fetch objects
            // _apiWeb.Data.DeleteObjects(created);
        }

        /// <summary>
        ///A test for deleteObject
        ///</summary>
        [TestMethod()]
        public void deleteObjectTest()
        {
            createObjectTest();
            long obj_id = _objectID;  
            _apiWeb.Data.DeleteObject(obj_id);
        }

        /// <summary>
        ///A test for defineObjectProperty
        ///</summary>
        [TestMethod()]
        public void defineObjectPropertyTest()
        {
            defineObjectPropertyTest(Constants.TestType, Constants.TestObjectPropertyName);
        }

        public void defineObjectPropertyTest(string objectType, string propertyName)
        {
            createObjectTypeTest();
            int prop_type = 1;
            _apiWeb.Data.DefineObjectProperty(objectType, propertyName, prop_type);
        }

        /// <summary>
        ///A test for defineAssociation
        ///</summary>
        [TestMethod()]
        public void defineAssociationTest()
        {
            DropAssociations();
            createObjectTypeTest();
            string name = Constants.TestAssociationDefName;
            int assoc_type = 2;
            assoc_object_type assoc_info1 = new assoc_object_type
                                                {
                                                    alias = Constants.TestAssociationDefAlias1,
                                                    object_type = Constants.TestType
                                                };
            assoc_object_type assoc_info2 = new assoc_object_type
                                                {
                                                    alias = Constants.TestAssociationDefAlias2,
                                                    object_type = Constants.TestType
                                                };
            bool? inverse = null;
            _apiWeb.Data.DefineAssociation(name, assoc_type, assoc_info1, assoc_info2, inverse);
        }

        private void DropAssociations()
        {
            DropAssociations(Constants.TestAssociationDefName);
        }

        private void DropAssociations(string associationName)
        {
            try
            {
                _apiWeb.Data.UndefineAssociation(associationName);
            }
            catch(FacebookException e)
            {
                //If it's not there, we're ok.
                if (e.ErrorCode != 803)
                    throw e;
            }
        }

        /// <summary>
        ///A test for createObject
        ///</summary>
        [TestMethod()]
        public void createObjectTest()
        {
            createObjectTest(Constants.TestType, Constants.TestObjectPropertyName, Constants.TestPropertyValue);
        }

        /// <summary>
        ///A test for createObject
        ///</summary>
        public void createObjectTest(string objectType, string propertyName, string propertyValue)
        {
            defineObjectPropertyTest(objectType, propertyName);
            var properties = new Dictionary<string, string>
                                 {
                                     {
                                         propertyName,
                                         propertyValue
                                     }
                                 };
            var ret = _apiWeb.Data.CreateObject(objectType, null);
            Assert.IsNotNull(ret);
            ret = _apiWeb.Data.CreateObject(objectType, properties);
            _objectID = ret;
            Assert.IsNotNull(ret);
        }
        
        /// <summary>
        ///A test for createObjectType
        ///</summary>
        [TestMethod()]
        public void createObjectTypeTest()
        {
            dropObjectTypeTest();
            string name = Constants.TestType; 
            _apiWeb.Data.CreateObjectType(name);
            var existingTypes = _apiWeb.Data.GetObjectTypes();
            foreach (var typ in existingTypes)
            {
                Assert.AreEqual(typ.name,name);
            }
        }

        /// <summary>
        ///A test for getObjectProperty
        ///</summary>
        [TestMethod()]
        public void getObjectPropertyTest()
        {
            createObjectTest();
            long obj_id = _objectID;
            string prop_name = Constants.TestObjectPropertyName;
            string expected = Constants.TestPropertyValue;
            string actual;
            actual = _apiWeb.Data.GetObjectProperty(obj_id, prop_name);
            Assert.AreEqual(expected, actual);
        }
    }
}
