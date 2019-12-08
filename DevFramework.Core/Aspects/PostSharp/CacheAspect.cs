﻿using DevFramework.Core.CrossCuttingConcerns.Caching;
using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.Aspects.PostSharp
{
    [Serializable]
    public class CacheAspect:MethodInterceptionAspect
    {
        private Type _CacheType;
        private int _cacheByMinute;
        private ICacheManager _CacheManager;

        public CacheAspect(Type cacheType,int cacheByMinute=60)
        {
            _CacheType = cacheType;
            _cacheByMinute = cacheByMinute;

        }

        public override void RuntimeInitialize(MethodBase method)
        {
            if (typeof(ICacheManager).IsAssignableFrom(_CacheType) == false)
                throw new Exception("Wrong Cache Manager Type");
            _CacheManager = (ICacheManager)Activator.CreateInstance(_CacheType);
            base.RuntimeInitialize(method);
        }

        public override void OnInvoke(MethodInterceptionArgs args)
        {
            var methodName = string.Format("{0}.{1}.{2}",args.Method.ReflectedType.Namespace, args.Method.ReflectedType.Name,
                args.Method.Name);
            var arguments = args.Arguments.ToList();

            var key = string.Format("{0}({1})",methodName,string.Join(",",arguments.Select(x=>x!=null?x.ToString():"<Null>")));
            if (_CacheManager.IsAdd(key))
                args.ReturnValue = _CacheManager.Get<object>(key);

            base.OnInvoke(args);
            _CacheManager.Add(key,args.ReturnValue,_cacheByMinute);
        }
    }
}
