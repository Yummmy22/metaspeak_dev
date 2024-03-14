using System;

namespace Gigadrillgames.AUP.Common
{
    public interface IDispatcher
    {
        void InvokeAction(Action fn);
        void InvokePendingAction();
    }
}