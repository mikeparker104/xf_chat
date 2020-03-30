using System;
using System.Collections.Generic;

namespace ChatDemo.Services
{
    public interface IKeyboardService
    {
        bool ShouldLockKeyboardFocus { get; }
        bool UpdateKeyboardFocusState(Type focusSource);
        IList<Type> KeyboardFocusLockInputSources { get; }
        void ShowKeyboard();
        void HideKeyboard();
        void RegisterKeyboardFocusLockInputSource(Xamarin.Forms.View view);
    }
}