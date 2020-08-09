using System;

namespace helper
{
    public class ExceptionHelper: Exception
    {
        public static void ThrowAutomationException(String message) => throw new Exception(message);

        public static void TerminateTestCase(String message){
            ThrowAutomationException(message);
        }
    }
}