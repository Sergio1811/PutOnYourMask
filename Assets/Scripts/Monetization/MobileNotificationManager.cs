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
            c.Description= "Reminds the player has full money in chest";
        };
        AndroidNotificationCenter.RegisterNotificationChannel(c);
    }

    void SendNotification()
    {
        var notification = new AndroidNotification();
        notification.Title = "Your chests are full!";
        notification.Text = "Pick up all your gold to keep winning more.";
        notification.FireTime = System.DateTime.Now.AddSeconds(10);
        notification.LargeIcon = "icon_1";

        AndroidNotificationCenter.SendNotification(notification, "full_money");
    }
}
#endif
