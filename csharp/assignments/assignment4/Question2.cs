using System;

namespace assignment4
{
    /*
     * Scenario: Mobile Phone – Ring Notification System
        You are simulating a mobile phone that can ring when someone calls. Different parts of the phone (like ringtone player, screen display, and vibration motor) need to react to the ring event.

        To model this, use delegates and events.

        Task:
        Implement the following in C#:

        1. MobilePhone class
        Has a delegate RingEventHandler and an event OnRing.

        Has a method ReceiveCall() which triggers the OnRing event.

        2. Subscriber classes (handlers):
        RingtonePlayer – prints: "Playing ringtone..."

        ScreenDisplay – prints: "Displaying caller information..."

        VibrationMotor – prints: "Phone is vibrating..."

        3. In the Main() method:
        Create an instance of MobilePhone.

        Subscribe all three components to the OnRing event.

        Call ReceiveCall() to simulate an incoming call.
     */
    class Question2
    {
        class MobilePhone
        {
            public delegate void RingEventHandler();
            public event RingEventHandler OnRing;
            public void ReceiveCall()
            {
                OnRing.Invoke();
            }
        }
        class RingtonePlayer
        {
            public static void PlayRingtone()
            {
                Console.WriteLine("Playing ringtone...");
            }
        }

        class ScreenDisplay
        {
            public static void ShowCallerInfo()
            {
                Console.WriteLine("Displaying caller information...");
            }
        }

        class VibrationMotor
        {
            public static void Vibrate()
            {
                Console.WriteLine("Phone is vibrating...");
            }
        }
        static void Main()
        {
            MobilePhone mobilePhone = new MobilePhone();

            mobilePhone.OnRing += RingtonePlayer.PlayRingtone;
            mobilePhone.OnRing += ScreenDisplay.ShowCallerInfo;
            mobilePhone.OnRing += VibrationMotor.Vibrate;
            mobilePhone.ReceiveCall();
            Console.Read();
        }
    }

}
