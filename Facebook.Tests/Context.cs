using System;
using Moq;
using NUnit.Framework;

namespace facebook.tests
{
	[TestFixture]
	public abstract class Context
	{
		private Exception exception;
		protected MockFactory factory;
		protected AutoMockContainer container;

		[TestFixtureSetUp]
		public void initialize()
		{
			exception = null;
			factory = new MockFactory(MockBehavior.Loose);
			container = new AutoMockContainer(factory);
			setupContext();

			try
			{
				act();
			}
			catch (Exception e)
			{
				exception = e;
				if (ShouldRethrowException(e.GetType()))
				{
					throw;
				}
			}
		}

		
		public Exception CaughtException
		{
			get { return exception; }
		}

		public abstract void setupContext();
		public abstract void act();

		private bool ShouldRethrowException(Type exceptionType)
		{
			ContextExpectedExceptionAttribute attribute = GetExpectedExceptionAttribute();

			if (attribute == null)
			{
				return true;
			}

			if (attribute.ExceptionType != null && attribute.ExceptionType != exceptionType)
			{
				return true;
			}

			return false;
		}

		private ContextExpectedExceptionAttribute GetExpectedExceptionAttribute()
		{
			System.Reflection.MemberInfo info = this.GetType();
			object[] attributes = info.GetCustomAttributes(true);

			for (int i = 0; i < attributes.Length; i++)
			{
				if (attributes[i] is ContextExpectedExceptionAttribute)
				{
					return (ContextExpectedExceptionAttribute)attributes[i];
				}
			}
			return null;
		}
	}

	[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
	public sealed class ContextExpectedExceptionAttribute : Attribute {
		public ContextExpectedExceptionAttribute(Type exceptionType)
		{
			ExceptionType = exceptionType;
		}
		public Type ExceptionType { get; set; }

    
	}
}