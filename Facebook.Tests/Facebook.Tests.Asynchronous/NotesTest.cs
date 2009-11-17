using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Facebook.Rest;
using Facebook.Utility;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Facebook.Schema;
using System.Collections.Generic;

namespace Facebook.Tests.Asynchronous
{
	[TestClass]
	public class NotesTest : AsyncFacebookTest
	{
		/// <summary>
		///A test for get
		///</summary>
		[TestMethod()]
		[Asynchronous]
		public void getTest()
		{
			_api.Notes.GetAsync(GetCompleted, null);
		}

		private void GetCompleted(IList<note> notes, object state, FacebookException e)
		{
			Assert.IsNotNull(notes);
			EnqueueTestComplete();
		}

		/// <summary>
		///A test for get (using desktop app)
		///</summary>
		[TestMethod()]
		[Asynchronous]
		public void createTest()
		{
			_api.Notes.CreateAsync(Constants.FBSamples_UserId, "Test Note Title", "Test note content", CreateCompleted, null);
		}

		private void CreateCompleted(long noteId, object state, FacebookException e)
		{
			Assert.IsTrue(noteId > 0);
			EnqueueTestComplete();
		}

		// TODO: implement this test
		///// <summary>
		/////A test for get (using web app)
		/////</summary>
		//[TestMethod()]
		//public void createTest2()
		//{
		//    var actual = _apiWeb.Notes.Create(Constants.FBSamples_UserId, "Test Note Title", "Test note content");
		//    Assert.IsNotNull(actual);
		//}

		/// <summary>
		///A test for edit (using desktop app)
		///</summary>
		[TestMethod()]
		[Asynchronous]
		public void editTest()
		{
			_api.Notes.CreateAsync(Constants.FBSamples_UserId, "Test Note Title", "Test note content", CreateForEditCompleted, null);
		}

		private void CreateForEditCompleted(long noteId, object state, FacebookException e)
		{
			_api.Notes.EditAsync(noteId, "Test Note Title Edited", "Test note content edited", EditCompleted, null);
		}

		private void EditCompleted(bool result, object state, FacebookException e)
		{
			Assert.IsTrue(result);
			EnqueueTestComplete();
		}

		/// <summary>
		///A test for delete
		///</summary>
		[TestMethod()]
		[Asynchronous]
		public void deleteTest()
		{
			_api.Notes.CreateAsync(Constants.FBSamples_UserId, "Test Note Title", "Test note content", CreateForDeleteCompleted, null);
		}

		private void CreateForDeleteCompleted(long noteId, object state, FacebookException e)
		{
			_api.Notes.DeleteAsync(noteId, DeleteCompleted, null);
		}

		private void DeleteCompleted(bool result, object state, FacebookException e)
		{
			Assert.IsTrue(result);
			EnqueueTestComplete();
		}

		// TODO: implement this test
		///// <summary>
		/////A test for edit (using web app)
		/////</summary>
		//[TestMethod()]
		//public void editTest2()
		//{
		//    var actual = _apiWeb.Notes.Create(Constants.FBSamples_UserId, "Test Note Title", "Test note content");
		//    var actual2 = _apiWeb.Notes.Edit(actual, "Test Note Title Edited", "Test note content edited");

		//    Assert.IsNotNull(actual2);
		//}
	}
}
