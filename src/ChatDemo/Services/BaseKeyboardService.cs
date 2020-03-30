using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ChatDemo.Services
{
    public class BaseKeyboardService : IKeyboardService
    {
        IList<Type> _keyboardFocusLockInputSources;

        public bool ShouldLockKeyboardFocus { get; private set; } = false;

        public IList<Type> KeyboardFocusLockInputSources
            => _keyboardFocusLockInputSources ?? (_keyboardFocusLockInputSources = new List<Type>());

        public bool UpdateKeyboardFocusState(Type focusSource)
            => OnUpdateKeyboardFocusState(focusSource);

        public void ShowKeyboard()
            => OnHideKeyboard();

        public void HideKeyboard()
            => OnHideKeyboard();

        public void RegisterKeyboardFocusLockInputSource(Xamarin.Forms.View view)
        {
            var nativeType = OnConvertToNativeType(view);
            var resolvedType = ResolveNativeType(nativeType)?.GetGenericArguments().LastOrDefault();

            if (resolvedType != null && !KeyboardFocusLockInputSources.Contains(resolvedType))
                KeyboardFocusLockInputSources.Add(resolvedType);
        }

        protected virtual bool OnUpdateKeyboardFocusState(Type focusSource)
            => ShouldLockKeyboardFocus = KeyboardFocusLockInputSources.Contains(focusSource);

        protected virtual void OnShowKeyboard() { }

        protected virtual void OnHideKeyboard()
            => ShouldLockKeyboardFocus = true;

        protected virtual Type OnConvertToNativeType(Xamarin.Forms.View view)
            => throw new NotImplementedException();

        Type ResolveNativeType(Type type)
        {
            if (type != null && (!type.IsGenericType || type.GetGenericArguments().Length == 0))
                type = ResolveNativeType(type.GetTypeInfo()?.BaseType);

            return type;
        }
    }
}