This is a stripped down project to show the error I'm having using a ClaimsPrincipalFactory with an injected service that injects IHttpClientFactory.

The ClaimsPrincipalFactory needs to call UserService.UserInfo to get the authenticated users info along with roles. Ideally UserService should use an httpClient created by the factory so that the token is attached to the outgoing call automatically.
However doing it that way causes a run time error at runtime.


Here is the runtime error, pulled from the browser console:
Microsoft.AspNetCore.Components.WebAssembly.Rendering.WebAssemblyRenderer[100]
      Unhandled exception rendering component: ValueFactory attempted to access the Value property of this instance.
System.InvalidOperationException: ValueFactory attempted to access the Value property of this instance.
   at System.Lazy`1[[Microsoft.Extensions.Http.ActiveHandlerTrackingEntry, Microsoft.Extensions.Http, Version=6.0.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60]].ViaFactory(LazyThreadSafetyMode mode)
   at System.Lazy`1[[Microsoft.Extensions.Http.ActiveHandlerTrackingEntry, Microsoft.Extensions.Http, Version=6.0.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60]].ExecutionAndPublication(LazyHelper executionAndPublication, Boolean useDefaultConstructor)
   at System.Lazy`1[[Microsoft.Extensions.Http.ActiveHandlerTrackingEntry, Microsoft.Extensions.Http, Version=6.0.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60]].CreateValue()
   at System.Lazy`1[[Microsoft.Extensions.Http.ActiveHandlerTrackingEntry, Microsoft.Extensions.Http, Version=6.0.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60]].get_Value()
   at Microsoft.Extensions.Http.DefaultHttpClientFactory.CreateHandler(String name)
   at Microsoft.Extensions.Http.DefaultHttpClientFactory.CreateClient(String name)
   at AuthTester.Services.UserService..ctor(IHttpClientFactory clientFactory, IConfiguration config) in C:\Projects\AuthTester\AuthTester\Services\UserService.cs:line 20
   at System.Reflection.RuntimeConstructorInfo.InternalInvoke(Object obj, Object[] parameters, Boolean wrapExceptions)
   at System.Reflection.RuntimeConstructorInfo.DoInvoke(Object obj, BindingFlags invokeAttr, Binder binder, Object[] parameters, CultureInfo culture)
   at System.Reflection.RuntimeConstructorInfo.Invoke(BindingFlags invokeAttr, Binder binder, Object[] parameters, CultureInfo culture)
   at Microsoft.Extensions.DependencyInjection.ServiceLookup.CallSiteRuntimeResolver.VisitConstructor(ConstructorCallSite constructorCallSite, RuntimeResolverContext context)
   at Microsoft.Extensions.DependencyInjection.ServiceLookup.CallSiteVisitor`2[[Microsoft.Extensions.DependencyInjection.ServiceLookup.RuntimeResolverContext, Microsoft.Extensions.DependencyInjection, Version=6.0.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60],[System.Object, System.Private.CoreLib, Version=6.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]].VisitCallSiteMain(ServiceCallSite callSite, RuntimeResolverContext argument)
   at Microsoft.Extensions.DependencyInjection.ServiceLookup.CallSiteRuntimeResolver.VisitCache(ServiceCallSite callSite, RuntimeResolverContext context, ServiceProviderEngineScope serviceProviderEngine, RuntimeResolverLock lockType)
   at Microsoft.Extensions.DependencyInjection.ServiceLookup.CallSiteRuntimeResolver.VisitScopeCache(ServiceCallSite callSite, RuntimeResolverContext context)
   at Microsoft.Extensions.DependencyInjection.ServiceLookup.CallSiteVisitor`2[[Microsoft.Extensions.DependencyInjection.ServiceLookup.RuntimeResolverContext, Microsoft.Extensions.DependencyInjection, Version=6.0.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60],[System.Object, System.Private.CoreLib, Version=6.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]].VisitCallSite(ServiceCallSite callSite, RuntimeResolverContext argument)
   at Microsoft.Extensions.DependencyInjection.ServiceLookup.CallSiteRuntimeResolver.VisitConstructor(ConstructorCallSite constructorCallSite, RuntimeResolverContext context)
   at Microsoft.Extensions.DependencyInjection.ServiceLookup.CallSiteVisitor`2[[Microsoft.Extensions.DependencyInjection.ServiceLookup.RuntimeResolverContext, Microsoft.Extensions.DependencyInjection, Version=6.0.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60],[System.Object, System.Private.CoreLib, Version=6.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]].VisitCallSiteMain(ServiceCallSite callSite, RuntimeResolverContext argument)
   at Microsoft.Extensions.DependencyInjection.ServiceLookup.CallSiteRuntimeResolver.VisitCache(ServiceCallSite callSite, RuntimeResolverContext context, ServiceProviderEngineScope serviceProviderEngine, RuntimeResolverLock lockType)
   at Microsoft.Extensions.DependencyInjection.ServiceLookup.CallSiteRuntimeResolver.VisitScopeCache(ServiceCallSite callSite, RuntimeResolverContext context)
   at Microsoft.Extensions.DependencyInjection.ServiceLookup.CallSiteVisitor`2[[Microsoft.Extensions.DependencyInjection.ServiceLookup.RuntimeResolverContext, Microsoft.Extensions.DependencyInjection, Version=6.0.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60],[System.Object, System.Private.CoreLib, Version=6.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]].VisitCallSite(ServiceCallSite callSite, RuntimeResolverContext argument)
   at Microsoft.Extensions.DependencyInjection.ServiceLookup.CallSiteRuntimeResolver.VisitConstructor(ConstructorCallSite constructorCallSite, RuntimeResolverContext context)