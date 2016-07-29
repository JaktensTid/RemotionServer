using RemotionServer.Engine.Commands;

namespace RemotionServer.Engine
{
    internal class Invoker
    {
        #region Constants
        const char SYSTEMCOMMAND = 's';
        const char MOUSECOMMAND = 'm';
        const char TEXTMESSAGECOMMAND = 't';
        const char SETTINGSCOMMAND = 'e';
        #endregion

        #region Variables
        readonly ICommand systemCommand;
        readonly ICommand mouseCommand;
        readonly ICommand settingsCommand;
        readonly ICommand textmessageCommand;
        #endregion

        public Invoker()
        {
            systemCommand = new SystemCommand();
            mouseCommand = new MouseCommand();
            settingsCommand = new SettingsCommand();
            textmessageCommand = new TextMessageCommand();
        }

        /// <summary>
        /// Запускает соответсвующую команду в зависимости от флага command
        /// </summary>
        /// <param name="data">Данные с сообщением с андроид-устройства</param>
        /// <param name="command">Флаг</param>
        public void Invoke(string data, char command)
        {
            switch (command)
            {
                case SYSTEMCOMMAND:
                    {
                        systemCommand.Execute(data);
                    }
                    break;
                case MOUSECOMMAND:
                    {
                        mouseCommand.Execute(data);
                    }
                    break;
                case TEXTMESSAGECOMMAND:
                    {
                        textmessageCommand.Execute(data);
                    }
                    break;
                case SETTINGSCOMMAND:
                    {
                        settingsCommand.Execute(data);
                    }
                    break;
            }
        }
    }
}
