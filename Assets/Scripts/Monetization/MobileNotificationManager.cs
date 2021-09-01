#if UNITY_ANDROID
using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;

public class MobileNotificationManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CreateNotificationChannel();
        SendNotification();
    }

    void CreateNotificationChannel()
    {
        var c = new AndroidNotificationChannel();
        {
            c.Id = "full_money";
            c.Name = "playerReminder";
            c.Importance = Importance.High;       
            c.Description= "Reminds the player to come back to the game.";
        };
        AndroidNotificationCenter.RegisterNotificationChannel(c);
    }

    void SendNotification()
    {
        var notification = new AndroidNotification();
        notification.Title = "The virus is growing!";
        notification.Text = "Enter the game and fight against it.";
        notification.FireTime = System.DateTime.Now.AddSeconds(3600);
        notification.LargeIcon = "icon_1";

        AndroidNotificationCenter.SendNotification(notification, "full_money");
    }
}
#endif
