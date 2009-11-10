using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Facebook.Tests.Asynchronous
{
	class Constants
	{
		public static long FBSamples_flid = 26568837591;
		public static long FBSamples_eid = 21503777827;
		public static long FBSamples_gid = 2373063483;
		public static string FBSamples_aid = "2828311517185749905";
		public static string FBSamples_pid = "2828311517186799515";
		public static string FBSamples_ApplicationKey = "e77f1c7e6b1fa2d4d8495622bac2881a";
		public static string FBSamples_Secret = "6871d1a0ce2c9b9f4778e2ae1c192dbd";
		public static string FBSamples_SessionKey = "62f384d21562bfeb7f6f57db-658517591";
		public static string FBSamples_SessionSecret = "5c4cb267d93a4e8be61980b41b14df58";
		public static string FBSamples_WebApplicationKey = "e739cf9a3580497cbbd7e6e4e98f7627";
		public static string FBSamples_WebSecret = "f6184d3fdab6d99baa018e3f61db7f77";
		public static string FBSamples_WebSessionKey = "f6184d3fdab6d99baa018e3f61db7f77";
		public static string FBSamples_WebApplicationKey2 = "c559128010f3edee33796fd4205361c2";
		public static string FBSamples_WebSecret2 = "85887d1c9a8334e5059742468a5400ee";
		public static long FBSamples_UserId = 658517591;
		public static string FBSamples_Name = "FB Samples";
		public static long FBSamples_friend1 = 828485692;
		public static long FBSamples_friend2 = 824555570;
		public static long FBSamples_listing1 = 33191562936;
		public static long FBSamples_page = 27401808209;
		public static List<long> FBSamples_uids = new List<long>() { 658517591, 824555570 };
		public static string FBSamples_setFBML = "<fb:name uid=\"658517591\"> is testing setFBML.</fb:name>";

        public static string FBSamples_Email_Facebook = "facebook@claritycon.com";
        
		//DataStore Test Data
		public static string TestString1 = "test_string_one";
		public static string TestString2 = "test_string_two";
		public static string TestString3 = "test_string_three";
		public static string TestType = "test_type";
		public static string TestTypeRename = "test_type_two";
		public static string TestObjectPropertyName = "test_object_property_name";
		public static string TestObjectPropertyNewName = "test_new_object_property_name";
		public static string TestPropertyName = "test_property_name";
		public static string TestPropertyValue = "1000";
		public static string TestPropertyUpdateValue = "1001";
		public static string TestHashKey = "test_hash_key";
		public static string TestHashValue = "2000";
		public static string TestAssociationDefName = "test_association_def_name";
		public static string TestAssociationDefAlias1 = "test_association_def_alias_one";
		public static string TestAssociationDefAlias2 = "test_association_def_alias_two";
		public static string TestAssociationDefAlias3 = "test_association_def_alias_three";
		public static string TestAssociationDefAlias4 = "test_association_def_alias_four";
		public static string TestAssociationName = "test_association_name";
		public static string TestAssociationRename = "test_association_rename";
		public static string TestAssociationData1 = "test_association_data_one";
		public static string TestAssociationData2 = "test_association_data_two";

		public static string AdminTestAgeRestriction = "2+";

		//Stream Test Data
		public static DateTime MinFacebookDate = new DateTime(1970, 1, 2); // a data guaranteed to be in the Unix epoch, with some room for daylight savings adjustments
	}
}
