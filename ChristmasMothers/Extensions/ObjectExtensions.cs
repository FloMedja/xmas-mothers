using System;

namespace ChristmasMothers.Extensions
{
    public static class ObjectExtensions
    {
        public static void Maybe<TInstance>(this TInstance instance, Action<TInstance> action)
        {
            if ((object)instance == null)
                return;
            action(instance);
        }

        public static void Maybe<TInstance>(this object instance, Action<TInstance> action) where TInstance : class
        {
            ObjectExtensions.Maybe<TInstance>(instance as TInstance, action);
        }

        //public static TResult SelectOrDefault<TInstance, TResult>(this TInstance instance, Func<TInstance, TResult> selector, TResult defaultValue = null)
        //{
        //    if ((object)instance != null)
        //        return selector(instance);
        //    return defaultValue;
        //}

        public static T With<T>(this T target, Action<T> assignations)
        {
            assignations(target);
            return target;
        }

        public static bool IsNotNull(this object target)
        {
            return target != null;
        }

        public static bool IsNull(this object target)
        {
            return target == null;
        }
    }
}

