using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Observer
{

    public static class Event
    {
        
        private static int c;

        public static class Language
        {
            public static readonly int Change = ++c;
            public static readonly int ChangeInSettings = ++c;
        }

    }

    public static class Manager
    {
        private static readonly Dictionary<int, UnityEvent> VoidEvents = new Dictionary<int, UnityEvent>();
        private static readonly Dictionary<int, object> ArgsEvents = new Dictionary<int, object>();

        #region Void Events

        public static void Register(int type, UnityAction action)
        {
            if (VoidEvents != null && !VoidEvents.ContainsKey(type)) VoidEvents.Add(type, new UnityEvent());
            VoidEvents[type].AddListener(action);
        }

        public static void Notify(int type)
        {
            if (VoidEvents != null && VoidEvents.ContainsKey(type)) VoidEvents[type].Invoke();
        }

        #endregion

        #region Args Events

        private class ArgEvent<A>: UnityEvent<A> {}
        private class ArgEvent<A, B>: UnityEvent<A, B> {}
        private class ArgEvent<A, B, C>: UnityEvent<A, B, C> {}

        public static void Register<A>(int type, UnityAction<A> action)
        {
            if (ArgsEvents != null && !ArgsEvents.ContainsKey(type)) ArgsEvents.Add(type, new ArgEvent<A>());
            (ArgsEvents[type] as ArgEvent<A>).AddListener(action);
        }

        public static void Register<A, B>(int type, UnityAction<A, B> action)
        {
            if (ArgsEvents != null && !ArgsEvents.ContainsKey(type)) ArgsEvents.Add(type, new ArgEvent<A, B>());
            (ArgsEvents[type] as ArgEvent<A, B>).AddListener(action);
        }

        public static void Register<A, B, C>(int type, UnityAction<A, B, C> action)
        {
            if (ArgsEvents != null && !ArgsEvents.ContainsKey(type)) ArgsEvents.Add(type, new ArgEvent<A, B, C>());
            (ArgsEvents[type] as ArgEvent<A, B, C>).AddListener(action);
        }

        public static void Notify<A>(int type, A value)
        {
            if (ArgsEvents != null && ArgsEvents.ContainsKey(type)) (ArgsEvents[type] as ArgEvent<A>).Invoke(value);
        }

        public static void Notify<A, B>(int type, A value, B value2)
        {
            if (ArgsEvents != null && ArgsEvents.ContainsKey(type)) (ArgsEvents[type] as ArgEvent<A, B>).Invoke(value, value2);
        }

        public static void Notify<A, B, C>(int type, A value, B value2, C value3)
        {
            if (ArgsEvents != null && ArgsEvents.ContainsKey(type)) (ArgsEvents[type] as ArgEvent<A, B, C>).Invoke(value, value2, value3);
        }

        #endregion

    }

}
