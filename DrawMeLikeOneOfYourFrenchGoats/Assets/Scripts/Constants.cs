using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants 
{
    /*
     * Constants for bullets
     */
    public const float bulletSpeed = 5;
    public const float bulletPushForce = 650;
    public const float picpicPushForce = 850;
    public const float walkindDeadPushForce = 1050;
    public const int bulletLifetime = 10;

    /*
     * Constants for PicPic
     */
    public const float picpicSpeed = 1f;

    /*
    * Constants for Walking Dead
    */
    public const float walkingDeadSpeed = 1f;
    public const float goatNormalJumpForce = 425f;
    public const float goatRageJumpForce = 800f;

    public const float rageLostPerSecond = .25f;
}
