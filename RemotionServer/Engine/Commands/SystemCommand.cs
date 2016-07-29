using Engine;

namespace RemotionServer.Engine.Commands
{
    /// <summary>
    /// Инкапсулирует системные команды, такие как звук-меньше, звук-больше, кнопка Enter и т.д
    /// </summary>
    internal class SystemCommand : ICommand
    {
        private const string VOLUMEUP = "01";
        private const string VOLUMEDOWN = "02";
        private const string ENTER = "20";
        private const string CTRLPLUS = "21";
        private const string CTRLMINUS = "22";
        public void Execute(string parameter)
        {
            switch (parameter)
            {
                case (VOLUMEUP):
                    {
                        ControllerEngine.VolUp();
                    }
                    break;
                case (VOLUMEDOWN):
                    {
                        ControllerEngine.VolDown();
                    }
                    break;
                case (ENTER):
                    {
                        ControllerEngine.DoEnterPress();
                    }
                    break;
                case (CTRLPLUS):
                    {
                        ControllerEngine.ZoomIn();
                    }
                    break;
                case (CTRLMINUS):
                    {
                        ControllerEngine.ZoomOut();
                    }
                    break;
            }
        }
    }
}
