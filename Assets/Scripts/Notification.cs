using UnityEngine;
using Unity.Notifications.Android;

public class Notification : MonoBehaviour
{
    private void Start()
    {
        CreateNotification();
        SendNotification();
    }

    private void CreateNotification()
    {
        var c = new AndroidNotificationChannel()
        {
            Id = "notif1",
            Name = "reminder",
            Importance = Importance.High,
            Description = "Reminds the player to play the game.",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(c);
    }

    private void SendNotification()
    {
        var notification = new AndroidNotification();
        notification.Title = "Go to play!";
        notification.Text = "New levels waiting you!!!";
        notification.SmallIcon = "icon_0";
        notification.FireTime = System.DateTime.Now.AddSeconds(30);

        AndroidNotificationCenter.SendNotification(notification, "notif1");
    }
}
