using System;
using System.Collections.Generic;
using System.Reflection;
using MInject.Runtime.Installer;
using MInject.Runtime.Service;
using UnityEngine;

namespace MInject.Runtime.Context
{
    public class ContextBase : MonoBehaviour
    {
        [SerializeField] private List<MonoInstallerBase> installers;
        private readonly Dictionary<Type, IService> _serviceDic = new();

        protected virtual void Awake()
        {
            
        }

        protected void Initialize()
        {
            InstallInstallers();
            InjectMethods();
        }

        private void InstallInstallers()
        {
            foreach (var installer in installers)
            {
                InjectInstallerContext(installer);
                installer.InstallBindings();
            }
            InjectServices();
        }
        
        public void RegisterService(IService service)
        {
            var type = service.GetType();
            _serviceDic[type] = service;
        }

        public T GetService<T>() where T : class, IService
        {
            _serviceDic.TryGetValue(typeof(T), out var service);
            return service as T;
        }

        private void InjectInstallerContext(MonoInstallerBase installer)
        {
            var types = GetTypeHierarchy(installer.GetType());

            foreach (var t in types)
            {
                InvokeInjectFields(t, installer);
                InvokeInjectProperties(t, installer);
                //InvokeInjectMethods(new [] { installer });
            }
        }

        private void InjectServices()
        {
            InvokeInjectMethods(_serviceDic.Values);
        }

        private void InjectMethods()
        {
            var targets = GetInjectTargets();
            InvokeInjectMethods(targets);
        }

        private void InvokeInjectFields(Type type, object target)
        {
            var fields = GetFields(type);

            foreach (var field in fields)
            {
                if (field.FieldType == typeof(ContextBase) && Attribute.IsDefined(field, typeof(InjectAttribute)))
                {
                    field.SetValue(target, this);
                }
            }
        }

        private void InvokeInjectProperties(Type type, object target)
        {
            var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);

            foreach (var property in properties)
            {
                if (!property.CanWrite) 
                    continue;

                if (property.PropertyType == typeof(ContextBase) && Attribute.IsDefined(property, typeof(InjectAttribute)))
                {
                    property.SetValue(target, this);
                }
            }
        }

        private void InvokeInjectMethods(IEnumerable<object> targets)
        {
            foreach (var target in targets)
            {
                var type = target.GetType();
                var methods = GetMethods(type);

                foreach (var method in methods)
                {
                    if (method.GetCustomAttribute(typeof(InjectAttribute)) == null)
                        continue;

                    var parameters = method.GetParameters();
                    object[] paramValues = new object[parameters.Length];

                    for (int i = 0; i < parameters.Length; i++)
                    {
                        var paramType = parameters[i].ParameterType;
                        var serviceInstance = GetServiceByType(paramType);

                        if (serviceInstance == null)
                        {
                            Debug.LogError($"[Context] Inject edilemedi: {paramType.Name} (hedef: {type.Name})");
                        }

                        paramValues[i] = serviceInstance;
                    }

                    method.Invoke(target, paramValues);
                }
            }
        }

        private IEnumerable<object> GetInjectTargets()
        {
            return FindObjectsOfType<MonoBehaviour>();
        }
        
        private object GetServiceByType(Type type)
        {
            foreach (var service in _serviceDic.Values)
            {
                if (service == null) 
                    continue;

                if (type.IsAssignableFrom(service.GetType()))
                {
                    return service;
                }
            }
            return null;
        }

        private List<Type> GetTypeHierarchy(Type type)
        {
            var types = new List<Type>();
            while (type != null && type != typeof(object))
            {
                types.Add(type);
                type = type.BaseType;
            }
            types.Reverse();
            return types;
        }

        private FieldInfo[] GetFields(Type type)
        {
            return type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly);
        }

        private MethodInfo[] GetMethods(Type type)
        {
            return type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        }
    }
}
