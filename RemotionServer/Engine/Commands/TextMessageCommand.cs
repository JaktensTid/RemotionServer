using Engine;

namespace RemotionServer.Engine.Commands
{
    /// <summary>
    /// Инкапсулирует юзер-сообщения
    /// </summary>
    internal class TextMessageCommand : ICommand
    {
        public void Execute(string parameter)
        {
            ControllerEngine.TypeCustomMessage(parameter);
        }
    }
}
