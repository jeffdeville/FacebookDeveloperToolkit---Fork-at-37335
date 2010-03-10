//Copyright (c) 2007, Moq Contributors
//http://code.google.com/p/moq-contrib/
//All rights reserved.

//Redistribution and use in source and binary forms, 
//with or without modification, are permitted provided 
//that the following conditions are met:

//    * Redistributions of source code must retain the 
//    above copyright notice, this list of conditions and 
//    the following disclaimer.

//    * Redistributions in binary form must reproduce 
//    the above copyright notice, this list of conditions 
//    and the following disclaimer in the documentation 
//    and/or other materials provided with the distribution.

//    * Neither the name of the Moq Contributors nor the 
//    names of its contributors may be used to endorse 
//    or promote products derived from this software 
//    without specific prior written permission.

//THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND 
//CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, 
//INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF 
//MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE 
//DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR 
//CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, 
//SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, 
//BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR 
//SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
//INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
//WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING 
//NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE 
//OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF 
//SUCH DAMAGE.

//[This is the BSD license, see
// http://www.opensource.org/licenses/bsd-license.php]

using System;
using Autofac;
using Autofac.Builder;
using Moq;
using System.Reflection;
using Autofac.Component.Activation;
using Autofac.Component;
using Autofac.Component.Scope;

namespace facebook.tests
{
	/// <summary>
	/// Auto-mocking factory that can create an instance of the 
	/// class under test and automatically inject mocks for all its dependencies.
	/// </summary>
	/// <remarks>
	/// Note that only interface dependencies will be mocked for now.
	/// </remarks>
	public class AutoMockContainer : IResolve
	{
		IContainer container;
		MockFactory factory;

		/// <summary>
		/// Initializes the container with the <see cref="MockFactory"/> that
		/// will be used to create dependent mocks.
		/// </summary>
		public AutoMockContainer(MockFactory factory)
		{
			this.factory = factory;
			var builder = new ContainerBuilder();
			builder.Register(this).OwnedByContainer();
			this.container = builder.Build();

			this.container.AddRegistrationSource(new MoqRegistrationSource(this.factory));
		}

		/// <summary>
		/// Gets or creates a mock for the given type, with 
		/// the default behavior specified by the factory.
		/// </summary>
		public Mock<T> GetMock<T>()
			where T : class
		{
			// TODO: support passing a different MockBehavior ?
			var obj = (IMocked<T>)this.Create<T>();
			return obj.Mock;
		}

		/// <summary>
		/// Creates an instance of a class under test, 
		/// injecting all necessary dependencies as mocks.
		/// </summary>
		/// <typeparam name="T">Requested object type.</typeparam>
		public T Create<T>() where T : class
		{
			if (!container.IsRegistered<T>())
			{
				var builder = new ContainerBuilder();

				builder.Register<T>();
				builder.Build(container);
			}

			return container.Resolve<T>();
		}

		/// <summary>
		/// Creates an instance of a class under test using 
		/// the given activator function.
		/// </summary>
		/// <typeparam name="T">Requested object type.</typeparam>
		public T Create<T>(Func<IResolve, T> activator) where T : class
		{
			if (!container.IsRegistered<T>())
			{
				var builder = new ContainerBuilder();

				builder.Register<T>(c => activator(this));
				builder.Build(container);
			}

			return container.Resolve<T>();
		}

		/// <summary>
		/// Registers and resolves the given service on the container.
		/// </summary>
		/// <typeparam name="TService">Service</typeparam>
		/// <typeparam name="TImplementation">The implementation of the service.</typeparam>
		public void Register<TService, TImplementation>()
		{
			var builder = new ContainerBuilder();

			builder.Register<TImplementation>().As<TService>().ContainerScoped();
			builder.Build(container);

			// TODO: why would you need back the instance you have just registered?
			// Isn't it supposed to be automatically injected from now on?
			//return Resolve<TService>();
		}

		/// <summary>
		/// Registers the given service instance on the container.
		/// </summary>
		/// <typeparam name="TService">Service type.</typeparam>
		/// <param name="instance">Service instance.</param>
		public void Register<TService>(TService instance)
		{
			var builder = new ContainerBuilder();

			builder.Register(instance).As<TService>();
			builder.Build(container);
		}

		/// <summary>
		/// Registers the given service creation delegate on the container.
		/// </summary>
		/// <typeparam name="TService">Service type.</typeparam>
		public void Register<TService>(Func<IResolve, TService> activator)
		{
			var builder = new ContainerBuilder();

			builder.Register(c => activator(this)).As<TService>();
			builder.Build(container);
		}

		/// <summary>
		/// Resolves a required service of the given type.
		/// </summary>
		public T Resolve<T>()
		{
			return container.Resolve<T>();
		}

		/// <summary> 
		/// Resolves unknown interfaces and Mocks using 
		/// the <see cref="MockFactory"/> from the scope. 
		/// </summary>
		class MoqRegistrationSource : IRegistrationSource
		{
			private readonly MockFactory factory;
			private readonly MethodInfo createMethod;

			public MoqRegistrationSource(MockFactory factory)
			{
				this.factory = factory;
				this.createMethod = factory.GetType().GetMethod("Create", new Type[] { });
			}

			/// <summary>
			/// Retrieve a registration for an unregistered service, to be used
			/// by the container.
			/// </summary>
			/// <param name="service">The service that was requested.</param>
			/// <param name="registration">A registration providing the service.</param>
			/// <returns>
			/// True if the registration could be created.
			/// </returns>
			public bool TryGetRegistration
				(Service service, out IComponentRegistration registration)
			{
				if (service == null)
					throw new ArgumentNullException("service");

				registration = null;

				var typedService = service as TypedService;
				if ((typedService == null) || (!typedService.ServiceType.IsInterface))
					return false;

				var descriptor = new Descriptor(
					new UniqueService(),
					new[] { service },
					typedService.ServiceType);

				registration = new Registration(
					descriptor,
					new DelegateActivator
						((c, p) =>
						 	{
						 		var specificCreateMethod = this.createMethod.MakeGenericMethod(new[] { typedService.ServiceType });
						 		var mock = (Mock)specificCreateMethod.Invoke(factory, null);
						 		return mock.Object;
						 	}),
					new ContainerScope(),
					InstanceOwnership.Container);

				return true;
			}
		}
	}

	/// <summary>
	/// Interface implemented by the <see cref="AutoMockContainer"/> so that 
	/// the <c>Register</c> overloads can receive a creation 
	/// function for the service, rather than just a type.
	/// </summary>
	public interface IResolve
	{
		/// <summary>
		/// Resolves a required service of the given type.
		/// </summary>
		T Resolve<T>();
	}
}